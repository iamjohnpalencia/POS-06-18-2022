Imports MySql.Data.MySqlClient
Imports System.Drawing.Printing
Imports System.Threading
'Imports Excel = Microsoft.Office.Interop.Excel
Imports PdfSharp
Imports PdfSharp.Drawing
Imports PdfSharp.Pdf
Imports System.IO
Imports System.Text
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Xml
Imports EJOURNAL_PDF

Public Class Reports
    Private WithEvents printdoc As PrintDocument = New PrintDocument
    Private WithEvents printdocXread As PrintDocument = New PrintDocument

    Private WithEvents printdocZRead As PrintDocument = New PrintDocument



    Private WithEvents printdocInventory As PrintDocument = New PrintDocument
    Private WithEvents printdocReturns As PrintDocument = New PrintDocument
    Private WithEvents printsales As PrintDocument = New PrintDocument
    Private WithEvents printtransactiontype As PrintDocument = New PrintDocument

    Private PrintPreviewDialog1 As New PrintPreviewDialog
    Private PrintPreviewDialogXread As New PrintPreviewDialog
    Private PrintPreviewDialogZread As New PrintPreviewDialog
    Private PrintPreviewDialogInventory As New PrintPreviewDialog
    Private PrintPreviewDialogReturns As New PrintPreviewDialog
    Private previewsales As New PrintPreviewDialog
    Private previewtransactiontype As New PrintPreviewDialog

    Dim buttons As DataGridViewButtonColumn = New DataGridViewButtonColumn()
    Dim user_id As String
    Dim pagingAdapter As MySqlDataAdapter
    Dim pagingDS As DataSet
    Dim scrollVal As Integer
    Dim fullname As String
    Dim tbl As String
    Dim flds As String
    Public Shared transaction_number As String

    Dim data As String
    Dim data2 As String
    Dim total

    Private Sub Reports_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            'DateTimePickerZXreading.MaxDate = DateTime.Today
            'DateTimePickerZXreadingTo.MinDate = DateTime.Today
            TabControl1.TabPages(0).Text = "Daily Transactions"
            TabControl1.TabPages(1).Text = "System Logs"
            TabControl1.TabPages(2).Text = "Sales Report"
            TabControl1.TabPages(3).Text = "Custom Report"
            TabControl1.TabPages(4).Text = "Expense Report"
            TabControl1.TabPages(5).Text = "Transaction Logs"
            TabControl1.TabPages(6).Text = "Crew Sales"
            TabControl1.TabPages(7).Text = "Item Return"
            TabControl1.TabPages(8).Text = "Deposit Slip"
            ComboBoxTransactionType.SelectedIndex = 0
            ToolStripComboBoxStatus.SelectedIndex = 0

            SelectDisctinctDaily()
            reportsdailytransaction(False)
            reportssystemlogs(False)
            reportssales(False)
            reportstransactionlogs(False)
            expensereports(False)
            LoadUsers()
            LoadCouponTypes()

            reportsreturnsandrefunds(False)
            viewdeposit(False)

            LoadCrewSales(False)
            'If ClientRole = "Admin" Then
            '    ButtonZreadAdmin.Visible = True

            'Else
            '    ButtonZreadAdmin.Visible = False

            'End If
            If S_Zreading = Format(Now().AddDays(1), "yyyy-MM-dd") Then
                'ButtonZread.Enabled = False
                ButtonZReading.Enabled = False
                'ButtonZreadAdmin.Enabled = False
            End If

            If DataGridViewDaily.Rows.Count > 0 Then
                Dim arg = New DataGridViewCellEventArgs(0, 0)
                DataGridViewDaily_CellClick(sender, arg)
            End If

            If DataGridViewEXPENSES.Rows.Count > 0 Then
                'Dim arg = New DataGridViewCellEventArgs(0, 0)
                'DataGridViewEXPENSES_CellClick(sender, arg)
            End If
            With DataGridViewTransactionDetails
                .Columns.Item(2).DefaultCellStyle.Format = "n2"
                .Columns.Item(3).DefaultCellStyle.Format = "n2"
                .RowHeadersVisible = False
                .AllowUserToAddRows = False
                .AllowUserToDeleteRows = False
                .AllowUserToOrderColumns = False
                .AllowUserToResizeColumns = False
                .AllowUserToResizeRows = False
                .Font = New Font("tahoma", 10)
                .CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
                .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            End With
            With DataGridViewCustomReport
                '.RowHeadersVisible = False
                '.AllowUserToAddRows = False
                '.AllowUserToDeleteRows = False
                '.AllowUserToOrderColumns = False
                '.AllowUserToResizeColumns = False
                '.AllowUserToResizeRows = False
                .Font = New Font("tahoma", 10)
                .CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
                .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
                '.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            End With

            ToolStripComboBoxTaxType.SelectedIndex = 0
            ToolStripComboBoxTransactionType.SelectedIndex = 0

            LoadProducts()

            If ClientRole = "Crew" Then
                ButtonZReading.Enabled = False
                ButtonZREADREPRINT.Enabled = False
                ButtonEJournalPDF.Enabled = False
                ButtonUserLogs.Enabled = False
                ButtonEJournal.Enabled = False
                ButtonAuditTrail.Enabled = False
            End If

            If ClientRole = "Head Crew" Then
                ButtonZReading.Enabled = False
                ButtonZREADREPRINT.Enabled = False
                ButtonEJournal.Enabled = False
                ButtonAuditTrail.Enabled = False
            End If

            If ClientRole = "Manager" Then
                ButtonZReading.Enabled = True
            End If

            LabelDate.Text = "Z-READ DATE: " & Format(StringToDate(S_Zreading), "MMMM dd, yyyy").ToString.ToUpper
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Reports: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub

    Private Sub LoadCrewSales(bool As Boolean)
        Try
            DataGridViewCrewSales.Rows.Clear()
            Dim query As String = ""
            Dim Table As String = ""
            Dim Fields As String = "dt.transaction_number, dt.grosssales, SUM(dtd.quantity) , dt.crew_id, dt.created_at"
            If bool = False Then
                Table = "loc_daily_transaction dt LEFT JOIN loc_daily_transaction_details dtd ON dt.transaction_number = dtd.transaction_number WHERE DATE_FORMAT(dt.created_at , '%y-%m-%d') = DATE_FORMAT(CURDATE(), '%y-%m-%d') AND dt.crew_id = '" & ClientCrewID & "' GROUP BY dt.created_at"
            Else
                Table = "loc_daily_transaction dt LEFT JOIN loc_daily_transaction_details dtd ON dt.transaction_number = dtd.transaction_number WHERE dt.zreading >= '" & Format(DateTimePicker5.Value, "yyyy-MM-dd") & "' AND dt.zreading <= '" & Format(DateTimePicker6.Value, "yyyy-MM-dd") & "' AND dt.crew_id = '" & ComboBoxUserIDS.Text & "' GROUP BY dt.created_at"
            End If
            Dim CrewSalesDt = AsDatatable(Table, Fields, DataGridViewCrewSales)
            For Each row As DataRow In CrewSalesDt.Rows
                DataGridViewCrewSales.Rows.Add(row("transaction_number"), row("grosssales"), row("SUM(dtd.quantity)"), row("crew_id"), row("created_at"))
            Next

            LabelCrewSalesQty.Text = SumOfColumnsToDecimal(DataGridViewCrewSales, 2)
            LabelCrewSalesTotal.Text = SumOfColumnsToDecimal(DataGridViewCrewSales, 1)
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Reports: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub LoadUsers()
        Try
            Dim sql = "Select uniq_id FROM loc_users"
            Dim cmd As MySqlCommand = New MySqlCommand(sql, LocalhostConn)
            Using reader As MySqlDataReader = cmd.ExecuteReader
                While reader.Read
                    ComboBoxUserIDS.Items.Add(reader("uniq_id"))
                End While
            End Using
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Reports: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub

    Private Sub LoadCouponTypes()
        Try
            Dim ConnectionLocal As MySqlConnection = LocalhostConn()
            Dim sql = "Select Couponname_ FROM tbcoupon ORDER BY type ASC"
            Dim cmd As MySqlCommand = New MySqlCommand(sql, ConnectionLocal)
            Dim da As MySqlDataAdapter = New MySqlDataAdapter(cmd)
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            ToolStripComboBoxDiscType.Items.Clear()
            ToolStripComboBoxDiscType.Items.Add("All")
            For i As Integer = 0 To dt.Rows.Count - 1 Step +1
                ToolStripComboBoxDiscType.Items.Add(dt(i)(0))
            Next
            ToolStripComboBoxDiscType.SelectedIndex = 0
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Reports: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub SelectDisctinctDaily()
        Try
            Dim ConnectionLocal As MySqlConnection = LocalhostConn()
            Dim sql = "Select DISTINCT transaction_type FROM loc_daily_transaction ORDER BY transaction_type ASC"
            Dim cmd As MySqlCommand = New MySqlCommand(sql, ConnectionLocal)
            Dim da As MySqlDataAdapter = New MySqlDataAdapter(cmd)
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            ComboBoxTransactionType.Items.Clear()
            ComboBoxTransactionType.Items.Add("All")
            ComboBoxTransactionType.Items.Add("All(Cash)")
            ComboBoxTransactionType.Items.Add("All(Others)")

            ToolStripComboBoxTransactionType.Items.Clear()
            ToolStripComboBoxTransactionType.Items.Add("All")
            ToolStripComboBoxTransactionType.Items.Add("All(Cash)")
            ToolStripComboBoxTransactionType.Items.Add("All(Others)")

            For i As Integer = 0 To dt.Rows.Count - 1 Step +1
                ComboBoxTransactionType.Items.Add(dt(i)(0))
                ToolStripComboBoxTransactionType.Items.Add(dt(i)(0))
            Next

            ComboBoxTransactionType.SelectedIndex = 0
            ToolStripComboBoxTransactionType.SelectedIndex = 0
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Reports: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub

    Public Sub reportssystemlogs(ByVal searchdate As Boolean)
        Try
            table = "`loc_system_logs`"
            fields = "`log_type`, `log_description`, `log_date_time`"
            If searchdate = False Then
                where = " WHERE Date(log_date_time) = CURRENT_DATE() And log_type <> 'TRANSACTION' AND log_store = '" & ClientStoreID & "' AND guid = '" & ClientGuid & "' ORDER BY log_date_time DESC"
            Else
                where = " WHERE log_type <> 'TRANSACTION' AND log_store = '" & ClientStoreID & "' AND guid = '" & ClientGuid & "' AND date(log_date_time) >= '" & Format(DateTimePicker9.Value, "yyyy-MM-dd") & "' AND date(log_date_time) <= '" & Format(DateTimePicker10.Value, "yyyy-MM-dd") & "' ORDER BY  log_date_time DESC"
            End If
            With DataGridViewSysLog
                .Columns(0).HeaderText = "Type"
                .Columns(1).HeaderText = "Description"
                .Columns(2).HeaderText = "Date and Time"
            End With
            Dim AsDt = AsDatatable(table & where, "`log_type`, `log_description`, `log_date_time`", DataGridViewSysLog)
            Dim Desc As String = ""
            Dim Type As String = ""
            For Each row As DataRow In AsDt.Rows
                If row("log_type") = "BG-1" Then
                    row("log_type") = "Balance"
                    row("log_description") = "Begginning Balance : Shift 1 : " & row("log_description")
                ElseIf row("log_type") = "BG-2" Then
                    row("log_type") = "Balance"
                    row("log_description") = "Begginning Balance : Shift 2 : " & row("log_description")
                ElseIf row("log_type") = "BG-3" Then
                    row("log_type") = "Balance"
                    row("log_description") = "Begginning Balance : Shift 3 : " & row("log_description")
                ElseIf row("log_type") = "BG-4" Then
                    row("log_type") = "Balance"
                    row("log_description") = "Begginning Balance : Shift 4 : " & row("log_description")
                End If
                DataGridViewSysLog.Rows.Add(row("log_type"), row("log_description"), row("log_date_time"))
            Next
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Reports: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Public Sub reportsreturnsandrefunds(ByVal searchdate As Boolean)
        Try
            Dim Table As String = "loc_refund_return_details"
            Dim Fields As String = "transaction_number, crew_id, reason, created_at, total"
            Dim WhereVal As String = ""
            Dim ReturnsRefunds
            If searchdate = False Then
                WhereVal = " WHERE date(zreading) = CURRENT_DATE() AND store_id = '" & ClientStoreID & "' AND guid = '" & ClientGuid & "'"
                ReturnsRefunds = AsDatatable(Table & WhereVal, Fields, DataGridViewReturns)
            Else
                WhereVal = " WHERE date(zreading) >= '" & Format(DateTimePicker14.Value, "yyyy-MM-dd") & "' AND date(zreading) <= '" & Format(DateTimePicker13.Value, "yyyy-MM-dd") & "' AND store_id = '" & ClientStoreID & "' AND guid = '" & ClientGuid & "'"
                ReturnsRefunds = AsDatatable(Table & WhereVal, Fields, DataGridViewReturns)
            End If
            With DataGridViewReturns
                For Each row As DataRow In ReturnsRefunds.Rows
                    DataGridViewReturns.Rows.Add(row("transaction_number"), row("crew_id"), row("reason"), row("created_at"), row("total"))
                Next
                .Columns(0).HeaderText = "Sales Invoice #"
                .Columns(1).HeaderText = "Service Crew"
                .Columns(2).HeaderText = "Reason"
                .Columns(3).HeaderText = "Date and Time"
                .Columns(4).HeaderText = "Total"
                .Columns(4).Visible = False
            End With
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Reports: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Public Sub reportstransactionlogs(ByVal searchdate As Boolean)
        Try
            table = "`loc_system_logs`"
            fields = "`log_type`, `log_description`, `log_date_time`"
            If searchdate = False Then
                where = " log_type = 'TRANSACTION' AND date(log_date_time) = CURRENT_DATE() AND log_store = '" & ClientStoreID & "' AND guid = '" & ClientGuid & "' "
                GLOBAL_SELECT_ALL_FUNCTION_WHERE(table:=table, datagrid:=DataGridViewTRANSACTIONLOGS, fields:=fields, where:=where)
            Else
                where = " log_type = 'TRANSACTION' AND date(log_date_time) >= '" & Format(DateTimePicker11.Value, "yyyy-MM-dd") & "' AND date(log_date_time) <= '" & Format(DateTimePicker12.Value, "yyyy-MM-dd") & "' AND log_store = '" & ClientStoreID & "' AND guid = '" & ClientGuid & "' "
                GLOBAL_SELECT_ALL_FUNCTION_WHERE(table:=table, datagrid:=DataGridViewTRANSACTIONLOGS, fields:=fields, where:=where)
            End If
            With DataGridViewTRANSACTIONLOGS
                .Columns(0).HeaderText = "Type"
                .Columns(1).HeaderText = "Description"
                .Columns(2).HeaderText = "Date and Time"
            End With
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Reports: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Public Sub reportsdailytransaction(ByVal searchdate As Boolean)
        Try
            Dim ActiveColumn As String = ""
            If ToolStripComboBoxStatus.Text = "Complete" Then
                ActiveColumn = " active IN(1,3) "
            Else
                ActiveColumn = " active = 2 "
            End If

            Dim table = "`loc_daily_transaction`"

            Dim fields = "`transaction_number`, `grosssales`, `totaldiscount`, `amounttendered`, `change`, `amountdue`, `vatablesales`, `vatexemptsales`, `zeroratedsales`, `vatpercentage`, `lessvat`, `transaction_type`, `discount_type`, `totaldiscountedamount`, `si_number`, `crew_id`, `created_at`, `active`"
            Dim DailyTable
            If searchdate = False Then
                If ComboBoxTransactionType.Text = "All" Then
                    where = " WHERE zreading = CURRENT_DATE() AND " & ActiveColumn & " AND store_id = '" & ClientStoreID & "' AND guid = '" & ClientGuid & "' ORDER BY `created_at` DESC"
                ElseIf ComboBoxTransactionType.Text = "All(Cash)" Then
                    where = " WHERE zreading = CURRENT_DATE() AND " & ActiveColumn & " AND store_id = '" & ClientStoreID & "' AND guid = '" & ClientGuid & "' AND transaction_type IN ('Walk-In','Registered') ORDER BY `created_at` DESC"
                ElseIf ComboBoxTransactionType.Text = "All(Others)" Then
                    where = " WHERE zreading = CURRENT_DATE() AND " & ActiveColumn & " AND store_id = '" & ClientStoreID & "' AND guid = '" & ClientGuid & "' AND transaction_type NOT IN ('Walk-In','Registered') ORDER BY `created_at` DESC"
                Else
                    where = " WHERE zreading = CURRENT_DATE() AND " & ActiveColumn & " AND store_id = '" & ClientStoreID & "' AND guid = '" & ClientGuid & "' AND `transaction_type` = '" & ComboBoxTransactionType.Text & "' ORDER BY `created_at` DESC"
                End If
                DailyTable = AsDatatable(table & where, fields, DataGridViewDaily)
                For Each row As DataRow In DailyTable.rows
                    DataGridViewDaily.Rows.Add(row("transaction_number"), row("grosssales"), row("totaldiscount"), row("amounttendered"), row("change"), row("amountdue"), row("vatablesales"), row("vatexemptsales"), row("zeroratedsales"), row("vatpercentage"), row("lessvat"), row("transaction_type"), row("discount_type"), row("totaldiscountedamount"), row("si_number"), row("crew_id"), row("created_at"), row("active"))
                Next
            Else
                If ComboBoxTransactionType.Text = "All" Then
                    where = " WHERE zreading >= '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' and zreading <= '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' AND " & ActiveColumn & " AND store_id = '" & ClientStoreID & "' AND guid = '" & ClientGuid & "' ORDER BY `created_at` DESC"
                ElseIf ComboBoxTransactionType.Text = "All(Cash)" Then
                    where = " WHERE zreading >= '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' and zreading <= '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' AND " & ActiveColumn & " AND store_id = '" & ClientStoreID & "' AND guid = '" & ClientGuid & "' AND transaction_type IN ('Walk-In','Registered') ORDER BY `created_at` DESC"
                ElseIf ComboBoxTransactionType.Text = "All(Others)" Then
                    where = " WHERE zreading >= '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' and zreading <= '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' AND " & ActiveColumn & " AND store_id = '" & ClientStoreID & "' AND guid = '" & ClientGuid & "' AND transaction_type NOT IN ('Walk-In','Registered') ORDER BY `created_at` DESC"
                Else
                    where = " WHERE zreading >= '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' and zreading <= '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' AND " & ActiveColumn & " AND store_id = '" & ClientStoreID & "' AND guid = '" & ClientGuid & "' AND `transaction_type` = '" & ComboBoxTransactionType.Text & "' ORDER BY `created_at` DESC"
                End If
                DailyTable = AsDatatable(table & where, fields, DataGridViewDaily)
                For Each row As DataRow In DailyTable.rows
                    DataGridViewDaily.Rows.Add(row("transaction_number"), row("grosssales"), row("totaldiscount"), row("amounttendered"), row("change"), row("amountdue"), row("vatablesales"), row("vatexemptsales"), row("zeroratedsales"), row("vatpercentage"), row("lessvat"), row("transaction_type"), row("discount_type"), row("totaldiscountedamount"), row("si_number"), row("crew_id"), row("created_at"), row("active"))
                Next
            End If
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Public Sub reportssales(ByVal searchdate As Boolean)
        Try
            table = "`loc_daily_transaction_details`"
            fields = "`product_sku`, `product_name`, sum(`quantity`), `price`, sum(`total`), `created_at` ,`product_category`"
            If searchdate = False Then
                where = " zreading = CURRENT_DATE()  AND active = 1 AND store_id = '" & ClientStoreID & "' AND guid = '" & ClientGuid & "' GROUP BY `product_name`"
                GLOBAL_SELECT_ALL_FUNCTION_WHERE(table:=table, datagrid:=DataGridViewSales, fields:=fields, where:=where)
            Else
                where = " zreading >= '" & Format(DateTimePicker3.Value, "yyyy-MM-dd") & "' AND zreading <= '" & Format(DateTimePicker4.Value, "yyyy-MM-dd") & "' AND active = 1  AND store_id = '" & ClientStoreID & "' AND guid = '" & ClientGuid & "'  GROUP BY `product_name`"
                GLOBAL_SELECT_ALL_FUNCTION_WHERE(table:=table, datagrid:=DataGridViewSales, fields:=fields, where:=where)
            End If
            With DataGridViewSales
                .Columns(0).HeaderText = "Product Code"
                .Columns(1).HeaderText = "Product Name"
                .Columns(2).HeaderText = "Quantity"
                .Columns(3).HeaderText = "Price"
                .Columns(4).HeaderText = "Total Price"
                .Columns(5).HeaderText = "Date"
                .Columns(6).Visible = False
                Label10.Text = "P " & SumOfColumnsToDecimal(DataGridViewSales, 4)
                Label9.Text = SumOfColumnsToInt(DataGridViewSales, 2)
            End With
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Reports: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub


    Public Sub expensereports(ByVal searchdate As Boolean)
        Try
            table = "`loc_expense_list`"
            fields = "`expense_id`, `crew_id`, `expense_number`, `total_amount`, `paid_amount`, `unpaid_amount`, `created_at`"
            If searchdate = False Then
                where = " zreading = date(CURRENT_DATE()) AND store_id = '" & ClientStoreID & "' AND guid = '" & ClientGuid & "'"
                GLOBAL_SELECT_ALL_FUNCTION_WHERE(table:=table, datagrid:=DataGridViewEXPENSES, fields:=fields, where:=where)
            Else
                where = " zreading >= '" & Format(DateTimePicker7.Value, "yyyy-MM-dd") & "' and zreading <= '" & Format(DateTimePicker8.Value, "yyyy-MM-dd") & "' AND store_id = '" & ClientStoreID & "' AND guid = '" & ClientGuid & "'"
                GLOBAL_SELECT_ALL_FUNCTION_WHERE(table:=table, datagrid:=DataGridViewEXPENSES, fields:=fields, where:=where)
            End If
            With DataGridViewEXPENSES
                .Columns(0).Visible = False
                .Columns(1).Visible = False
                .Columns(2).HeaderCell.Value = "Expense Number"
                .Columns(3).HeaderCell.Value = "Amount"
                .Columns(4).HeaderCell.Value = "Paid Amount"
                .Columns(5).HeaderCell.Value = "Unpaid Amount"
                .Columns(6).HeaderCell.Value = "Date"
            End With
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Reports: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Public Sub viewexpensesdetails(ByVal expense_number As String)
        Try
            table = "`loc_expense_details`"
            fields = "`expense_type`, `item_info`, `quantity`, `price`, `amount`, `created_at`"
            GLOBAL_SELECT_ALL_FUNCTION_WHERE(table:=table, datagrid:=DataGridViewEXPENSEDET, fields:=fields, where:=" expense_number = '" & expense_number & "'")
            With DataGridViewEXPENSEDET
                .Columns(0).HeaderCell.Value = "Type"
                .Columns(1).HeaderCell.Value = "Description"
                .Columns(2).HeaderCell.Value = "Quantity"
                .Columns(3).HeaderCell.Value = "Price"
                .Columns(4).HeaderCell.Value = "Amount"
                .Columns(5).HeaderCell.Value = "Date"
            End With
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Public Sub viewtransactiondetails(ByVal transaction_number As String)
        Try
            Dim DailyTable
            table = "`loc_daily_transaction_details` WHERE transaction_number = '" & transaction_number & "'"
            fields = "`product_name`, `quantity`, `price`, `total`, `product_category`, `upgraded`, `addontype`"
            DailyTable = AsDatatable(table, fields, DataGridViewTransactionDetails)
            For Each row As DataRow In DailyTable.rows
                Dim Upgrade = ""
                If row("upgraded") = 0 Then
                    Upgrade = "NO"
                Else
                    Upgrade = "YES"
                End If
                DataGridViewTransactionDetails.Rows.Add(row("product_name"), row("quantity"), row("price"), row("total"), row("product_category"), Upgrade, row("addontype"))
            Next

        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Reports: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        Finally
            da.Dispose()
        End Try
    End Sub
    Public Sub viewdeposit(ByVal searchdate As Boolean)
        Try
            table = "`loc_deposit`"
            fields = "`dep_id`, `name`, `crew_id`, `transaction_number`, `amount`, `bank`, `transaction_date`, `store_id`, `guid`, `created_at`"
            If searchdate = False Then
                where = " date(transaction_date) = date(CURRENT_DATE()) AND store_id = '" & ClientStoreID & "' AND guid = '" & ClientGuid & "'"
                GLOBAL_SELECT_ALL_FUNCTION_WHERE(table:=table, datagrid:=DataGridViewDeposits, fields:=fields, where:=where)
            Else
                where = " date(transaction_date) >= '" & Format(DateTimePicker16.Value, "yyyy-MM-dd") & "' and date(transaction_date) <= '" & Format(DateTimePicker15.Value, "yyyy-MM-dd") & "' AND store_id = '" & ClientStoreID & "' AND guid = '" & ClientGuid & "'"
                GLOBAL_SELECT_ALL_FUNCTION_WHERE(table:=table, datagrid:=DataGridViewDeposits, fields:=fields, where:=where)
            End If
            With DataGridViewDeposits
                .Columns(0).Visible = False
                .Columns(1).HeaderCell.Value = "Full Name"
                .Columns(2).HeaderCell.Value = "Service Crew"
                .Columns(3).HeaderCell.Value = "Transaction Number"
                .Columns(4).HeaderCell.Value = "Amount"
                .Columns(5).HeaderCell.Value = "Bank"
                .Columns(6).HeaderCell.Value = "Transaction Date"
                .Columns(7).Visible = False
                .Columns(8).Visible = False
                .Columns(9).Visible = False
            End With
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Reports: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub

    Private Sub LoadProducts()
        Try
            ToolStripComboBoxProducts.Items.Clear()
            ToolStripComboBoxProducts.Items.Add("All")
            Dim ConnectionLocal As MySqlConnection = LocalhostConn()
            Dim Sql = "SELECT product_name FROM loc_admin_products"
            Dim cmd As MySqlCommand = New MySqlCommand(Sql, ConnectionLocal)
            Using reader As MySqlDataReader = cmd.ExecuteReader
                If reader.HasRows Then
                    While reader.Read
                        ToolStripComboBoxProducts.Items.Add(reader("product_name"))
                    End While
                End If
            End Using
            ToolStripComboBoxProducts.SelectedIndex = 0
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Reports: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Dim TotalDiscountCustomReports As Double = 0
    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        Try
            TotalDiscountCustomReports = 0
            DataGridViewCustomReport.Rows.Clear()
            CustomReport(ToolStripComboBoxProducts.Text, ToolStripComboBoxTaxType.Text, ToolStripComboBoxTransactionType.Text, ToolStripComboBoxDiscType.Text)
            ToolStripStatusLabel2.Text = DataGridViewCustomReport.Rows.Count
            TotalDiscountCustomReports = sum("coupon_total", "loc_coupon_data WHERE zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND  zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND status = 1")
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Reports: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Dim CustomReportLessVat As Double = 0
    Dim CustomReportVat As Double = 0
    Dim CustomReportdt As DataTable

    Private Sub CustomReport(ProductName, TaxType, TransactionType, DiscountType)
        Try

            Dim ConnectionLocal As MySqlConnection = LocalhostConn()
            'Dim QueryDiscType = "SELECT TYPE from tbcoupon WHERE Couponname_ = '" & DiscountType & "'"
            'Dim CmdDiscType As MySqlCommand = New MySqlCommand(QueryDiscType, ConnectionLocal)
            'Using reader As MySqlDataReader = CmdDiscType.ExecuteReader
            '    If reader.HasRows Then
            '        While reader.Read
            '            DiscountType = reader("TYPE")
            '        End While
            '    End If
            'End Using

            Dim cmd As MySqlCommand
            Dim da As MySqlDataAdapter
            CustomReportdt = New DataTable
            Dim sql As String = ""

            If ProductName = "All" Then
                If TaxType = "All" Then
                    If TransactionType = "All" Then
                        If DiscountType = "All" Then
                            sql = "SELECT product_name, transaction_number, quantity, price, total, created_at, product_sku FROM loc_daily_transaction_details WHERE zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND active = 1 "
                        Else
                            sql = "SELECT LDT.product_name, LDT.transaction_number, LDT.quantity, LDT.price, LDT.total, LDT.created_at, LDT.product_sku FROM loc_daily_transaction_details LDT LEFT JOIN loc_daily_transaction LD ON LDT.transaction_number = LD.transaction_number WHERE date(LDT.created_at) >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND date(LDT.created_at) <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND LDT.active = 1 AND LD.discount_type = '" & DiscountType & "'"
                        End If
                    ElseIf TransactionType = "All(Cash)" Then
                        If DiscountType = "All" Then
                            sql = "SELECT product_name, transaction_number, quantity, price, total, created_at, product_sku FROM loc_daily_transaction_details WHERE zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND active = 1 AND transaction_type IN('Walk-In','Registered')"
                        Else
                            sql = "SELECT LDT.product_name, LDT.transaction_number, LDT.quantity, LDT.price, LDT.total, LDT.created_at, LDT.product_sku FROM loc_daily_transaction_details LDT LEFT JOIN loc_daily_transaction LD ON LDT.transaction_number = LD.transaction_number WHERE date(LDT.created_at) >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND date(LDT.created_at) <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND LDT.active = 1 AND LDT.transaction_type IN('Walk-In','Registered') AND LD.discount_type = '" & DiscountType & "'"
                        End If
                    ElseIf TransactionType = "All(Others)" Then
                        If DiscountType = "All" Then
                            sql = "SELECT product_name, transaction_number, quantity, price, total, created_at, product_sku FROM loc_daily_transaction_details WHERE zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND active = 1 AND transaction_type NOT IN('Walk-In','Registered')"
                        Else
                            sql = "SELECT LDT.product_name, LDT.transaction_number, LDT.quantity, LDT.price, LDT.total, LDT.created_at, LDT.product_sku FROM loc_daily_transaction_details LDT LEFT JOIN loc_daily_transaction LD ON LDT.transaction_number = LD.transaction_number WHERE date(LDT.created_at) >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND date(LDT.created_at) <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND LDT.active = 1 AND LDT.transaction_type NOT IN('Walk-In','Registered') AND LD.discount_type = '" & DiscountType & "'"
                        End If
                    Else
                        If DiscountType = "All" Then
                            sql = "SELECT product_name, transaction_number, quantity, price, total, created_at, product_sku FROM loc_daily_transaction_details WHERE zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND active = 1 AND transaction_type = '" & TransactionType & "' "
                        Else
                            sql = "SELECT LDT.product_name, LDT.transaction_number, LDT.quantity, LDT.price, LDT.total, LDT.created_at, LDT.product_sku FROM loc_daily_transaction_details LDT LEFT JOIN loc_daily_transaction LD ON LDT.transaction_number = LD.transaction_number WHERE date(LDT.created_at) >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND date(LDT.created_at) <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND LDT.active = 1 AND LDT.transaction_type = '" & TransactionType & "' AND LD.discount_type = '" & DiscountType & "'"
                        End If
                    End If
                Else
                    If TaxType = "VAT" Then
                        If TransactionType = "All" Then
                            If DiscountType = "All" Or DiscountType = "N/A" Then
                                sql = "SELECT LDT.product_name as PName, LDT.transaction_number as TN, LDT.quantity as QTY, LDT.price as P, LDT.total as T, LDT.created_at as CA, LDT.product_sku as SKU FROM loc_daily_transaction_details LDT INNER JOIN loc_daily_transaction LD ON LDT.transaction_number = LD.transaction_number WHERE LD.zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND LD.zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND LD.discount_type = 'N/A' AND LD.zeroratedsales = 0 AND LD.active = 1"
                            End If
                        ElseIf TransactionType = "All(Cash)" Then
                            If DiscountType = "All" Or DiscountType = "N/A" Then
                                sql = "SELECT LDT.product_name as PName, LDT.transaction_number as TN, LDT.quantity as QTY, LDT.price as P, LDT.total as T, LDT.created_at as CA, LDT.product_sku as SKU FROM loc_daily_transaction_details LDT INNER JOIN loc_daily_transaction LD ON LDT.transaction_number = LD.transaction_number WHERE LD.zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND LD.zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND LD.discount_type = 'N/A' AND LD.zeroratedsales = 0 AND LD.active = 1 AND LD.transaction_type IN('Walk-In','Registered')"
                            End If
                        ElseIf TransactionType = "All(Others)" Then
                            If DiscountType = "All" Or DiscountType = "N/A" Then
                                sql = "SELECT LDT.product_name as PName, LDT.transaction_number as TN, LDT.quantity as QTY, LDT.price as P, LDT.total as T, LDT.created_at as CA, LDT.product_sku as SKU FROM loc_daily_transaction_details LDT INNER JOIN loc_daily_transaction LD ON LDT.transaction_number = LD.transaction_number WHERE LD.zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND LD.zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND LD.discount_type = 'N/A' AND LD.zeroratedsales = 0 AND LD.active = 1 AND LD.transaction_type NOT IN('Walk-In','Registered')"
                            End If
                        Else
                            If DiscountType = "All" Or DiscountType = "N/A" Then
                                sql = "SELECT LDT.product_name as PName, LDT.transaction_number as TN, LDT.quantity as QTY, LDT.price as P, LDT.total as T, LDT.created_at as CA, LDT.product_sku as SKU FROM loc_daily_transaction_details LDT INNER JOIN loc_daily_transaction LD ON LDT.transaction_number = LD.transaction_number WHERE LD.zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND LD.zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND LD.discount_type = 'N/A' AND LD.zeroratedsales = 0 AND LD.active = 1 AND LD.transaction_type = '" & TransactionType & "' "
                            End If
                        End If
                    ElseIf TaxType = "NONVAT" Then
                        Dim Types As String = "'Senior Discount 20%','PWD Discount 20%','Sports Discount 20%'"
                        If TransactionType = "All" Then
                            If DiscountType = "All" Or DiscountType = "Percentage(w/o vat)" Then
                                sql = "SELECT LDT.product_name as PName, LDT.transaction_number as TN, LDT.quantity as QTY, LDT.price as P, LDT.total as T, LDT.created_at as CA, LDT.product_sku as SKU FROM loc_daily_transaction_details LDT INNER JOIN loc_daily_transaction LD ON LDT.transaction_number = LD.transaction_number WHERE LD.zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND LD.zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND LD.discount_type IN (" & Types & ") AND LD.active = 1"
                            End If
                        ElseIf TransactionType = "All(Cash)" Then
                            If DiscountType = "All" Or DiscountType = "Percentage(w/o vat)" Then
                                sql = "SELECT LDT.product_name as PName, LDT.transaction_number as TN, LDT.quantity as QTY, LDT.price as P, LDT.total as T, LDT.created_at as CA, LDT.product_sku as SKU FROM loc_daily_transaction_details LDT INNER JOIN loc_daily_transaction LD ON LDT.transaction_number = LD.transaction_number WHERE LD.zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND LD.zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND LD.discount_type IN (" & Types & ") AND LD.active = 1 AND LD.transaction_type IN('Walk-In','Registered')"
                            End If
                        ElseIf TransactionType = "All(Others)" Then
                            If DiscountType = "All" Or DiscountType = "Percentage(w/o vat)" Then
                                sql = "SELECT LDT.product_name as PName, LDT.transaction_number as TN, LDT.quantity as QTY, LDT.price as P, LDT.total as T, LDT.created_at as CA, LDT.product_sku as SKU FROM loc_daily_transaction_details LDT INNER JOIN loc_daily_transaction LD ON LDT.transaction_number = LD.transaction_number WHERE LD.zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND LD.zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND LD.discount_type IN (" & Types & ") AND LD.active = 1 AND LD.transaction_type NOT IN('Walk-In','Registered')"
                            End If
                        Else
                            If DiscountType = "All" Or DiscountType = "Percentage(w/o vat)" Then
                                sql = "SELECT LDT.product_name as PName, LDT.transaction_number as TN, LDT.quantity as QTY, LDT.price as P, LDT.total as T, LDT.created_at as CA, LDT.product_sku as SKU FROM loc_daily_transaction_details LDT INNER JOIN loc_daily_transaction LD ON LDT.transaction_number = LD.transaction_number WHERE LD.zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND LD.zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND LD.discount_type IN (" & Types & ") AND LD.transaction_type = '" & TransactionType & "' AND LD.active = 1"
                            End If
                        End If
                    ElseIf TaxType = "ZERO RATED" Then
                        If TransactionType = "All" Then
                            If DiscountType = "All" Then
                                sql = "SELECT LDT.product_name as PName, LDT.transaction_number as TN, LDT.quantity as QTY, LDT.price as P, LDT.total as T, LDT.created_at as CA, LDT.product_sku as SKU FROM loc_daily_transaction_details LDT INNER JOIN loc_daily_transaction LD ON LDT.transaction_number = LD.transaction_number WHERE LD.zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND LD.zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND LD.zeroratedsales > 0 "
                            Else
                                sql = "SELECT LDT.product_name as PName, LDT.transaction_number as TN, LDT.quantity as QTY, LDT.price as P, LDT.total as T, LDT.created_at as CA, LDT.product_sku as SKU FROM loc_daily_transaction_details LDT INNER JOIN loc_daily_transaction LD ON LDT.transaction_number = LD.transaction_number WHERE LD.zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND LD.zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND LD.zeroratedsales > 0 AND LD.discount_type = '" & DiscountType & "'"
                            End If
                        ElseIf TransactionType = "All(Cash)" Then
                            If DiscountType = "All" Then
                                sql = "SELECT LDT.product_name as PName, LDT.transaction_number as TN, LDT.quantity as QTY, LDT.price as P, LDT.total as T, LDT.created_at as CA, LDT.product_sku as SKU FROM loc_daily_transaction_details LDT INNER JOIN loc_daily_transaction LD ON LDT.transaction_number = LD.transaction_number WHERE LD.zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND LD.zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND LD.zeroratedsales > 0 AND LD.transaction_type IN('Walk-In','Registered')"
                            Else
                                sql = "SELECT LDT.product_name as PName, LDT.transaction_number as TN, LDT.quantity as QTY, LDT.price as P, LDT.total as T, LDT.created_at as CA, LDT.product_sku as SKU FROM loc_daily_transaction_details LDT INNER JOIN loc_daily_transaction LD ON LDT.transaction_number = LD.transaction_number WHERE LD.zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND LD.zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND LD.zeroratedsales > 0 AND LD.transaction_type IN('Walk-In','Registered') AND LD.discount_type = '" & DiscountType & "'"
                            End If
                        ElseIf TransactionType = "All(Others)" Then
                            If DiscountType = "All" Then
                                sql = "SELECT LDT.product_name as PName, LDT.transaction_number as TN, LDT.quantity as QTY, LDT.price as P, LDT.total as T, LDT.created_at as CA, LDT.product_sku as SKU FROM loc_daily_transaction_details LDT INNER JOIN loc_daily_transaction LD ON LDT.transaction_number = LD.transaction_number WHERE LD.zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND LD.zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND LD.zeroratedsales > 0 AND LD.transaction_type NOT IN('Walk-In','Registered')"
                            Else
                                sql = "SELECT LDT.product_name as PName, LDT.transaction_number as TN, LDT.quantity as QTY, LDT.price as P, LDT.total as T, LDT.created_at as CA, LDT.product_sku as SKU FROM loc_daily_transaction_details LDT INNER JOIN loc_daily_transaction LD ON LDT.transaction_number = LD.transaction_number WHERE LD.zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND LD.zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND LD.zeroratedsales > 0 AND LD.transaction_type NOT IN('Walk-In','Registered') AND LD.discount_type = '" & DiscountType & "'"
                            End If
                        Else
                            If DiscountType = "All" Then
                                sql = "SELECT LDT.product_name as PName, LDT.transaction_number as TN, LDT.quantity as QTY, LDT.price as P, LDT.total as T, LDT.created_at as CA, LDT.product_sku as SKU FROM loc_daily_transaction_details LDT INNER JOIN loc_daily_transaction LD ON LDT.transaction_number = LD.transaction_number WHERE LD.zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND LD.zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND LD.zeroratedsales > 0 AND LD.transaction_type = '" & TransactionType & "' AND LD.active = 1"
                            Else
                                sql = "SELECT LDT.product_name as PName, LDT.transaction_number as TN, LDT.quantity as QTY, LDT.price as P, LDT.total as T, LDT.created_at as CA, LDT.product_sku as SKU FROM loc_daily_transaction_details LDT INNER JOIN loc_daily_transaction LD ON LDT.transaction_number = LD.transaction_number WHERE LD.zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND LD.zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND LD.zeroratedsales > 0 AND LD.transaction_type = '" & TransactionType & "' AND LD.active = 1 AND LD.discount_type = '" & DiscountType & "'"
                            End If
                        End If
                    End If
                End If
            Else
                If TaxType = "All" Then
                    If TransactionType = "All" Then
                        If DiscountType = "All" Then
                            sql = "SELECT product_name, transaction_number, quantity, price, total, created_at, product_sku FROM loc_daily_transaction_details WHERE zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND product_name = '" & ProductName & "' AND active = 1"
                        Else
                            sql = "SELECT LDT.product_name, LDT.transaction_number, LDT.quantity, LDT.price, LDT.total, LDT.created_at, LDT.product_sku FROM loc_daily_transaction_details LDT LEFT JOIN loc_daily_transaction LD ON LDT.transaction_number = LD.transaction_number WHERE date(LDT.created_at) >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND date(LDT.created_at) <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND LDT.product_name = '" & ProductName & "' AND LDT.active = 1 AND LD.discount_type = '" & DiscountType & "'"
                        End If
                    ElseIf TransactionType = "All(Cash)" Then
                        If DiscountType = "All" Then
                            sql = "SELECT product_name, transaction_number, quantity, price, total, created_at, product_sku FROM loc_daily_transaction_details WHERE zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND product_name = '" & ProductName & "' AND active = 1 AND transaction_type IN('Walk-In','Registered')"
                        Else
                            sql = "SELECT LDT.product_name, LDT.transaction_number, LDT.quantity, LDT.price, LDT.total, LDT.created_at, LDT.product_sku FROM loc_daily_transaction_details LDT LEFT JOIN loc_daily_transaction LD ON LDT.transaction_number = LD.transaction_number WHERE date(LDT.created_at) >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND date(LDT.created_at) <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND LDT.product_name = '" & ProductName & "' AND LDT.active = 1 AND LDT.transaction_type IN('Walk-In','Registered') AND LD.discount_type = '" & DiscountType & "'"
                        End If
                    ElseIf TransactionType = "All(Others)" Then
                        If DiscountType = "All" Then
                            sql = "SELECT product_name, transaction_number, quantity, price, total, created_at, product_sku FROM loc_daily_transaction_details WHERE zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND product_name = '" & ProductName & "' AND active = 1 AND transaction_type NOT IN('Walk-In','Registered')"
                        Else
                            sql = "SELECT LDT.product_name, LDT.transaction_number, LDT.quantity, LDT.price, LDT.total, LDT.created_at, LDT.product_sku FROM loc_daily_transaction_details LDT LEFT JOIN loc_daily_transaction LD ON LDT.transaction_number = LD.transaction_number WHERE date(LDT.created_at) >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND date(LDT.created_at) <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND LDT.product_name = '" & ProductName & "' AND LDT.active = 1 AND LDT.transaction_type NOT IN('Walk-In','Registered') AND LD.discount_type = '" & DiscountType & "'"
                        End If
                    Else
                        If DiscountType = "All" Then
                            sql = "SELECT product_name, transaction_number, quantity, price, total, created_at, product_sku FROM loc_daily_transaction_details WHERE zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND product_name = '" & ProductName & "' AND transaction_type = '" & TransactionType & "' AND active = 1"
                        Else
                            sql = "SELECT LDT.product_name, LDT.transaction_number, LDT.quantity, LDT.price, LDT.total, LDT.created_at, LDT.product_sku FROM loc_daily_transaction_details LDT LEFT JOIN loc_daily_transaction LD ON LDT.transaction_number = LD.transaction_number WHERE date(LDT.created_at) >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND date(LDT.created_at) <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND LDT.product_name = '" & ProductName & "' AND LDT.transaction_type = '" & TransactionType & "' AND active = 1 AND LD.discount_type = '" & DiscountType & "'"
                        End If
                    End If
                Else
                    If TaxType = "VAT" Then
                        If TransactionType = "All" Then
                            If DiscountType = "All" Or DiscountType = "N/A" Then
                                sql = "SELECT LDT.product_name as PName, LDT.transaction_number as TN, LDT.quantity as QTY, LDT.price as P, LDT.total as T, LDT.created_at as CA, LDT.product_sku as SKU FROM loc_daily_transaction_details LDT INNER JOIN loc_daily_transaction LD ON LDT.transaction_number = LD.transaction_number WHERE LD.zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND LD.zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND LDT.product_name = '" & ProductName & "' AND LD.discount_type = 'N/A' AND LD.zeroratedsales = 0 AND LD.active = 1"
                            End If
                        ElseIf TransactionType = "All(Cash)" Then
                            If DiscountType = "All" Or DiscountType = "N/A" Then
                                sql = "SELECT LDT.product_name as PName, LDT.transaction_number as TN, LDT.quantity as QTY, LDT.price as P, LDT.total as T, LDT.created_at as CA, LDT.product_sku as SKU FROM loc_daily_transaction_details LDT INNER JOIN loc_daily_transaction LD ON LDT.transaction_number = LD.transaction_number WHERE LD.zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND LD.zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND LDT.product_name = '" & ProductName & "' AND LD.discount_type = 'N/A' AND LD.zeroratedsales = 0 AND LD.active = 1 AND LD.transaction_type IN('Walk-In','Registered')"
                            End If
                        ElseIf TransactionType = "All(Others)" Then
                            If DiscountType = "All" Or DiscountType = "N/A" Then
                                sql = "SELECT LDT.product_name as PName, LDT.transaction_number as TN, LDT.quantity as QTY, LDT.price as P, LDT.total as T, LDT.created_at as CA, LDT.product_sku as SKU FROM loc_daily_transaction_details LDT INNER JOIN loc_daily_transaction LD ON LDT.transaction_number = LD.transaction_number WHERE LD.zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND LD.zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND LDT.product_name = '" & ProductName & "' AND LD.discount_type = 'N/A' AND LD.zeroratedsales = 0 AND LD.active = 1 AND LD.transaction_type NOT IN('Walk-In','Registered')"
                            End If
                        Else
                            If DiscountType = "All" Or DiscountType = "N/A" Then
                                sql = "SELECT LDT.product_name as PName, LDT.transaction_number as TN, LDT.quantity as QTY, LDT.price as P, LDT.total as T, LDT.created_at as CA, LDT.product_sku as SKU FROM loc_daily_transaction_details LDT INNER JOIN loc_daily_transaction LD ON LDT.transaction_number = LD.transaction_number WHERE LD.zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND LD.zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND LDT.product_name = '" & ProductName & "' AND LD.discount_type = 'N/A' AND LD.zeroratedsales = 0 AND LD.transaction_type = '" & TransactionType & "' AND LD.active = 1"
                            End If
                        End If
                    ElseIf TaxType = "NONVAT" Then
                        Dim Types As String = "'Senior Discount 20%','PWD Discount 20%','Sports Discount 20%'"
                        If TransactionType = "All" Then
                            If DiscountType = "All" Or DiscountType = "Percentage(w/o vat)" Then
                                sql = "SELECT LDT.product_name as PName, LDT.transaction_number as TN, LDT.quantity as QTY, LDT.price as P, LDT.total as T, LDT.created_at as CA, LDT.product_sku as SKU FROM loc_daily_transaction_details LDT INNER JOIN loc_daily_transaction LD ON LDT.transaction_number = LD.transaction_number WHERE LD.zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND LD.zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND LDT.product_name = '" & ProductName & "' AND LD.discount_type IN (" & Types & ") AND LD.active = 1"
                            End If
                        ElseIf TransactionType = "All(Cash)" Then
                            If DiscountType = "All" Or DiscountType = "Percentage(w/o vat)" Then
                                sql = "SELECT LDT.product_name as PName, LDT.transaction_number as TN, LDT.quantity as QTY, LDT.price as P, LDT.total as T, LDT.created_at as CA, LDT.product_sku as SKU FROM loc_daily_transaction_details LDT INNER JOIN loc_daily_transaction LD ON LDT.transaction_number = LD.transaction_number WHERE LD.zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND LD.zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND LDT.product_name = '" & ProductName & "' AND LD.discount_type IN (" & Types & ") AND LD.active = 1 AND LD.transaction_type IN('Walk-In','Registered')"
                            End If
                        ElseIf TransactionType = "All(Others)" Then
                            If DiscountType = "All" Or DiscountType = "Percentage(w/o vat)" Then
                                sql = "SELECT LDT.product_name as PName, LDT.transaction_number as TN, LDT.quantity as QTY, LDT.price as P, LDT.total as T, LDT.created_at as CA, LDT.product_sku as SKU FROM loc_daily_transaction_details LDT INNER JOIN loc_daily_transaction LD ON LDT.transaction_number = LD.transaction_number WHERE LD.zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND LD.zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND LDT.product_name = '" & ProductName & "' AND LD.discount_type IN (" & Types & ") AND LD.active = 1 AND LD.transaction_type NOT IN('Walk-In','Registered')"
                            End If
                        Else
                            If DiscountType = "All" Or DiscountType = "Percentage(w/o vat)" Then
                                sql = "SELECT LDT.product_name as PName, LDT.transaction_number as TN, LDT.quantity as QTY, LDT.price as P, LDT.total as T, LDT.created_at as CA, LDT.product_sku as SKU FROM loc_daily_transaction_details LDT INNER JOIN loc_daily_transaction LD ON LDT.transaction_number = LD.transaction_number WHERE LD.zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND LD.zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND LDT.product_name = '" & ProductName & "' AND LD.discount_type IN (" & Types & ") AND LD.transaction_type = '" & TransactionType & "' AND LD.active = 1"
                            End If
                        End If
                    ElseIf TaxType = "ZERO RATED" Then
                        If TransactionType = "All" Then
                            If DiscountType = "All" Then
                                sql = "SELECT LDT.product_name as PName, LDT.transaction_number as TN, LDT.quantity as QTY, LDT.price as P, LDT.total as T, LDT.created_at as CA, LDT.product_sku as SKU FROM loc_daily_transaction_details LDT INNER JOIN loc_daily_transaction LD ON LDT.transaction_number = LD.transaction_number WHERE LD.zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND LD.zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND LDT.product_name = '" & ProductName & "' AND LD.zeroratedsales > 0 AND LD.active = 1"
                            Else
                                sql = "SELECT LDT.product_name as PName, LDT.transaction_number as TN, LDT.quantity as QTY, LDT.price as P, LDT.total as T, LDT.created_at as CA, LDT.product_sku as SKU FROM loc_daily_transaction_details LDT INNER JOIN loc_daily_transaction LD ON LDT.transaction_number = LD.transaction_number WHERE LD.zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND LD.zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND LDT.product_name = '" & ProductName & "' AND LD.zeroratedsales > 0 AND LD.active = 1 AND LD.discount_type = '" & DiscountType & "'"
                            End If
                        ElseIf TransactionType = "All(Cash)" Then
                            If DiscountType = "All" Then
                                sql = "SELECT LDT.product_name as PName, LDT.transaction_number as TN, LDT.quantity as QTY, LDT.price as P, LDT.total as T, LDT.created_at as CA, LDT.product_sku as SKU FROM loc_daily_transaction_details LDT INNER JOIN loc_daily_transaction LD ON LDT.transaction_number = LD.transaction_number WHERE LD.zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND LD.zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND LDT.product_name = '" & ProductName & "' AND LD.zeroratedsales > 0 AND LD.active = 1 AND LD.transaction_type IN('Walk-In','Registered')"
                            Else
                                sql = "SELECT LDT.product_name as PName, LDT.transaction_number as TN, LDT.quantity as QTY, LDT.price as P, LDT.total as T, LDT.created_at as CA, LDT.product_sku as SKU FROM loc_daily_transaction_details LDT INNER JOIN loc_daily_transaction LD ON LDT.transaction_number = LD.transaction_number WHERE LD.zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND LD.zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND LDT.product_name = '" & ProductName & "' AND LD.zeroratedsales > 0 AND LD.active = 1 AND LD.transaction_type IN('Walk-In','Registered') AND LD.discount_type = '" & DiscountType & "'"
                            End If
                        ElseIf TransactionType = "All(Others)" Then
                            If DiscountType = "All" Then
                                sql = "SELECT LDT.product_name as PName, LDT.transaction_number as TN, LDT.quantity as QTY, LDT.price as P, LDT.total as T, LDT.created_at as CA, LDT.product_sku as SKU FROM loc_daily_transaction_details LDT INNER JOIN loc_daily_transaction LD ON LDT.transaction_number = LD.transaction_number WHERE LD.zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND LD.zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND LDT.product_name = '" & ProductName & "' AND LD.zeroratedsales > 0 AND LD.active = 1 AND LD.transaction_type NOT IN('Walk-In','Registered')"
                            Else
                                sql = "SELECT LDT.product_name as PName, LDT.transaction_number as TN, LDT.quantity as QTY, LDT.price as P, LDT.total as T, LDT.created_at as CA, LDT.product_sku as SKU FROM loc_daily_transaction_details LDT INNER JOIN loc_daily_transaction LD ON LDT.transaction_number = LD.transaction_number WHERE LD.zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND LD.zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND LDT.product_name = '" & ProductName & "' AND LD.zeroratedsales > 0 AND LD.active = 1 AND LD.transaction_type NOT IN('Walk-In','Registered') AND LD.discount_type = '" & DiscountType & "'"
                            End If
                        Else
                            If DiscountType = "All" Then
                                sql = "SELECT LDT.product_name as PName, LDT.transaction_number as TN, LDT.quantity as QTY, LDT.price as P, LDT.total as T, LDT.created_at as CA, LDT.product_sku as SKU FROM loc_daily_transaction_details LDT INNER JOIN loc_daily_transaction LD ON LDT.transaction_number = LD.transaction_number WHERE LD.zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND LD.zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND LDT.product_name = '" & ProductName & "' AND LD.zeroratedsales > 0 AND LD.transaction_type = '" & TransactionType & "' AND LD.active = 1"
                            Else
                                sql = "SELECT LDT.product_name as PName, LDT.transaction_number as TN, LDT.quantity as QTY, LDT.price as P, LDT.total as T, LDT.created_at as CA, LDT.product_sku as SKU FROM loc_daily_transaction_details LDT INNER JOIN loc_daily_transaction LD ON LDT.transaction_number = LD.transaction_number WHERE LD.zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND LD.zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND LDT.product_name = '" & ProductName & "' AND LD.zeroratedsales > 0 AND LD.transaction_type = '" & TransactionType & "' AND LD.active = 1 AND LD.discount_type = '" & DiscountType & "'"
                            End If
                        End If
                    End If
                End If
            End If

            If sql <> "" Then
                cmd = New MySqlCommand(sql, ConnectionLocal)
                da = New MySqlDataAdapter(cmd)
                da.Fill(CustomReportdt)

                For Each row As DataRow In CustomReportdt.Rows
                    If TaxType = "All" Then
                        DataGridViewCustomReport.Rows.Add(row("product_name"), row("transaction_number"), row("quantity"), row("price"), row("total"), row("created_at"), row("product_sku"))
                    Else
                        DataGridViewCustomReport.Rows.Add(row("PName"), row("TN"), row("QTY"), row("P"), row("T"), row("CA"), row("SKU"))
                    End If
                Next
            End If

            Dim sql1 As String = ""
            Dim cmd1 As MySqlCommand

            Dim list As List(Of String) = New List(Of String)

            For i As Integer = 0 To DataGridViewCustomReport.Rows.Count - 1 Step +1
                list.Add(DataGridViewCustomReport.Rows(i).Cells(1).Value)
            Next

            Dim result As List(Of String) = list.Distinct().ToList

            CustomReportVat = 0
            CustomReportLessVat = 0
            ' Display result.
            For Each element As String In result
                If ProductName = "All" Then
                    If TaxType = "All" Then
                        If TransactionType = "All" Then
                            If DiscountType = "All" Then
                                sql1 = "SELECT vatablesales, lessvat FROM loc_daily_transaction WHERE zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND transaction_number = '" & element & "'"
                            Else
                                sql1 = "SELECT vatablesales, lessvat FROM loc_daily_transaction WHERE zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND transaction_number = '" & element & "' AND discount_type = '" & DiscountType & "'"
                            End If
                        ElseIf TransactionType = "All(Cash)" Then
                            If DiscountType = "All" Then
                                sql1 = "SELECT vatablesales, lessvat FROM loc_daily_transaction WHERE zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND transaction_number = '" & element & "' AND transaction_type IN('Walk-In','Registered')"
                            Else
                                sql1 = "SELECT vatablesales, lessvat FROM loc_daily_transaction WHERE zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND transaction_number = '" & element & "' AND transaction_type IN('Walk-In','Registered') AND discount_type = '" & DiscountType & "'"
                            End If
                        ElseIf TransactionType = "All(Others)" Then
                            If DiscountType = "All" Then
                                sql1 = "SELECT vatablesales, lessvat FROM loc_daily_transaction WHERE zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND transaction_number = '" & element & "' AND transaction_type NOT IN('Walk-In','Registered')"
                            Else
                                sql1 = "SELECT vatablesales, lessvat FROM loc_daily_transaction WHERE zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND transaction_number = '" & element & "' AND transaction_type NOT IN('Walk-In','Registered') AND discount_type = '" & DiscountType & "'"
                            End If
                        Else
                            If DiscountType = "All" Then
                                sql1 = "SELECT vatablesales, lessvat FROM loc_daily_transaction WHERE zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND transaction_type = '" & TransactionType & "' AND transaction_number = '" & element & "'"
                            Else
                                sql1 = "SELECT vatablesales, lessvat FROM loc_daily_transaction WHERE zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND transaction_type = '" & TransactionType & "' AND transaction_number = '" & element & "' AND discount_type = '" & DiscountType & "'"
                            End If
                        End If
                    Else
                        If TaxType = "VAT" Then
                            If TransactionType = "All" Then
                                If DiscountType = "All" Or DiscountType = "N/A" Then
                                    sql1 = "SELECT vatablesales, lessvat FROM loc_daily_transaction WHERE zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND discount_type = 'N/A' AND zeroratedsales = 0 AND transaction_number = '" & element & "'"
                                End If
                            ElseIf TransactionType = "All(Cash)" Then
                                If DiscountType = "All" Or DiscountType = "N/A" Then
                                    sql1 = "SELECT vatablesales, lessvat FROM loc_daily_transaction WHERE zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND discount_type = 'N/A' AND zeroratedsales = 0 AND transaction_number = '" & element & "' AND transaction_type IN('Walk-In','Registered')"
                                End If
                            ElseIf TransactionType = "All(Others)" Then
                                If DiscountType = "All" Or DiscountType = "N/A" Then
                                    sql1 = "SELECT vatablesales, lessvat FROM loc_daily_transaction WHERE zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND discount_type = 'N/A' AND zeroratedsales = 0 AND transaction_number = '" & element & "' AND transaction_type NOT IN('Walk-In','Registered')"
                                End If
                            Else
                                If DiscountType = "All" Or DiscountType = "N/A" Then
                                    sql1 = "SELECT vatablesales, lessvat FROM loc_daily_transaction WHERE zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND discount_type = 'N/A' AND zeroratedsales = 0 AND transaction_type = '" & TransactionType & "' AND transaction_number = '" & element & "'"
                                End If
                            End If
                        ElseIf TaxType = "NONVAT" Then
                            Dim Types As String = "'Senior Discount 20%','PWD Discount 20%','Sports Discount 20%'"
                            If TransactionType = "All" Then
                                If DiscountType = "All" Or DiscountType = "Percentage(w/o vat)" Then
                                    sql1 = "SELECT vatablesales, lessvat FROM loc_daily_transaction WHERE zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND discount_type IN (" & Types & ") AND transaction_number = '" & element & "'"
                                End If
                            ElseIf TransactionType = "All(Cash)" Then
                                If DiscountType = "All" Or DiscountType = "Percentage(w/o vat)" Then
                                    sql1 = "SELECT vatablesales, lessvat FROM loc_daily_transaction WHERE zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND discount_type IN (" & Types & ") AND transaction_number = '" & element & "' AND transaction_type IN('Walk-In','Registered')"
                                End If
                            ElseIf TransactionType = "All(Others)" Then
                                If DiscountType = "All" Or DiscountType = "Percentage(w/o vat)" Then
                                    sql1 = "SELECT vatablesales, lessvat FROM loc_daily_transaction WHERE zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND discount_type IN (" & Types & ") AND transaction_number = '" & element & "' AND transaction_type NOT IN('Walk-In','Registered')"
                                End If
                            Else
                                If DiscountType = "All" Or DiscountType = "Percentage(w/o vat)" Then
                                    sql1 = "SELECT vatablesales, lessvat FROM loc_daily_transaction WHERE zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND discount_type IN (" & Types & ") AND transaction_type = '" & TransactionType & "' AND transaction_number = '" & element & "'"
                                End If
                            End If
                        ElseIf TaxType = "ZERO RATED" Then
                            If TransactionType = "All" Then
                                If DiscountType = "All" Then
                                    sql1 = "SELECT vatablesales, lessvat FROM loc_daily_transaction WHERE zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND zeroratedsales > 0 AND transaction_number = '" & element & "'"
                                Else
                                    sql1 = "SELECT vatablesales, lessvat FROM loc_daily_transaction WHERE zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND zeroratedsales > 0 AND transaction_number = '" & element & "' AND discount_type = '" & DiscountType & "'"
                                End If
                            ElseIf TransactionType = "All(Cash)" Then
                                If DiscountType = "All" Then
                                    sql1 = "SELECT vatablesales, lessvat FROM loc_daily_transaction WHERE zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND zeroratedsales > 0 AND transaction_number = '" & element & "' AND transaction_type IN('Walk-In','Registered')"
                                Else
                                    sql1 = "SELECT vatablesales, lessvat FROM loc_daily_transaction WHERE zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND zeroratedsales > 0 AND transaction_number = '" & element & "' AND transaction_type IN('Walk-In','Registered') AND discount_type = '" & DiscountType & "'"
                                End If
                            ElseIf TransactionType = "All(Others)" Then
                                If DiscountType = "All" Then
                                    sql1 = "SELECT vatablesales, lessvat FROM loc_daily_transaction WHERE zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND zeroratedsales > 0 AND transaction_number = '" & element & "' AND transaction_type NOT IN('Walk-In','Registered')"
                                Else
                                    sql1 = "SELECT vatablesales, lessvat FROM loc_daily_transaction WHERE zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND zeroratedsales > 0 AND transaction_number = '" & element & "' AND transaction_type NOT IN('Walk-In','Registered') AND discount_type = '" & DiscountType & "'"
                                End If
                            Else
                                If DiscountType = "All" Then
                                    sql1 = "SELECT vatablesales, lessvat FROM loc_daily_transaction WHERE zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND zeroratedsales > 0 AND transaction_type = '" & TransactionType & "' AND transaction_number = '" & element & "'"
                                Else
                                    sql1 = "SELECT vatablesales, lessvat FROM loc_daily_transaction WHERE zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND zeroratedsales > 0 AND transaction_type = '" & TransactionType & "' AND transaction_number = '" & element & "' AND discount_type = '" & DiscountType & "'"
                                End If
                            End If
                        End If
                    End If
                Else
                    If TaxType = "All" Then
                        If TransactionType = "All" Then
                            If DiscountType = "All" Then
                                sql1 = "SELECT LD.vatablesales, LD.lessvat FROM loc_daily_transaction LD INNER JOIN loc_daily_transaction_details LDT ON LD.transaction_number = LDT.transaction_number WHERE LD.zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND LD.zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND LD.transaction_number = '" & element & "' AND LDT.product_name = '" & ProductName & "'"
                            Else
                                sql1 = "SELECT LD.vatablesales, LD.lessvat FROM loc_daily_transaction LD INNER JOIN loc_daily_transaction_details LDT ON LD.transaction_number = LDT.transaction_number WHERE LD.zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND LD.zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND LD.transaction_number = '" & element & "' AND LDT.product_name = '" & ProductName & "' AND discount_type = '" & DiscountType & "'"
                            End If
                        ElseIf TransactionType = "All(Cash)" Then
                            If DiscountType = "All" Then
                                sql1 = "SELECT LD.vatablesales, LD.lessvat FROM loc_daily_transaction LD INNER JOIN loc_daily_transaction_details LDT ON LD.transaction_number = LDT.transaction_number WHERE LD.zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND LD.zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND LD.transaction_number = '" & element & "' AND LDT.product_name = '" & ProductName & "' AND LD.transaction_type IN('Walk-In','Registered')"
                            Else
                                sql1 = "SELECT LD.vatablesales, LD.lessvat FROM loc_daily_transaction LD INNER JOIN loc_daily_transaction_details LDT ON LD.transaction_number = LDT.transaction_number WHERE LD.zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND LD.zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND LD.transaction_number = '" & element & "' AND LDT.product_name = '" & ProductName & "' AND LD.transaction_type IN('Walk-In','Registered') AND discount_type = '" & DiscountType & "'"
                            End If
                        ElseIf TransactionType = "All(Others)" Then
                            If DiscountType = "All" Then
                                sql1 = "SELECT LD.vatablesales, LD.lessvat FROM loc_daily_transaction LD INNER JOIN loc_daily_transaction_details LDT ON LD.transaction_number = LDT.transaction_number WHERE LD.zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND LD.zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND LD.transaction_number = '" & element & "' AND LDT.product_name = '" & ProductName & "' AND LD.transaction_type NOT IN('Walk-In','Registered')"
                            Else
                                sql1 = "SELECT LD.vatablesales, LD.lessvat FROM loc_daily_transaction LD INNER JOIN loc_daily_transaction_details LDT ON LD.transaction_number = LDT.transaction_number WHERE LD.zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND LD.zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND LD.transaction_number = '" & element & "' AND LDT.product_name = '" & ProductName & "' AND LD.transaction_type NOT IN('Walk-In','Registered') AND discount_type = '" & DiscountType & "'"
                            End If
                        Else
                            If DiscountType = "All" Then
                                sql1 = "SELECT LD.vatablesales, LD.lessvat FROM loc_daily_transaction LD INNER JOIN loc_daily_transaction_details LDT ON LD.transaction_number = LDT.transaction_number WHERE LD.zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND LD.zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND LD.transaction_type = '" & TransactionType & "' AND LD.transaction_number = '" & element & "' AND LDT.product_name = '" & ProductName & "'"
                            Else
                                sql1 = "SELECT LD.vatablesales, LD.lessvat FROM loc_daily_transaction LD INNER JOIN loc_daily_transaction_details LDT ON LD.transaction_number = LDT.transaction_number WHERE LD.zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND LD.zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND LD.transaction_type = '" & TransactionType & "' AND LD.transaction_number = '" & element & "' AND LDT.product_name = '" & ProductName & "' AND discount_type = '" & DiscountType & "'"
                            End If
                        End If
                    Else
                        If TaxType = "VAT" Then
                            If TransactionType = "All" Then
                                If DiscountType = "All" Or DiscountType = "N/A" Then
                                    sql1 = "SELECT LD.vatablesales, LD.lessvat FROM loc_daily_transaction LD INNER JOIN loc_daily_transaction_details LDT ON LD.transaction_number = LDT.transaction_number WHERE LD.zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND LD.zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND LD.discount_type = 'N/A' AND zeroratedsales = 0 AND LD.transaction_number = '" & element & "' AND LDT.product_name = '" & ProductName & "'"
                                End If
                            ElseIf TransactionType = "All(Cash)" Then
                                If DiscountType = "All" Or DiscountType = "N/A" Then
                                    sql1 = "SELECT LD.vatablesales, LD.lessvat FROM loc_daily_transaction LD INNER JOIN loc_daily_transaction_details LDT ON LD.transaction_number = LDT.transaction_number WHERE LD.zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND LD.zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND LD.discount_type = 'N/A' AND zeroratedsales = 0 AND LD.transaction_number = '" & element & "' AND LDT.product_name = '" & ProductName & "' AND LD.transaction_type IN('Walk-In','Registered')"
                                End If
                            ElseIf TransactionType = "All(Others)" Then
                                If DiscountType = "All" Or DiscountType = "N/A" Then
                                    sql1 = "SELECT LD.vatablesales, LD.lessvat FROM loc_daily_transaction LD INNER JOIN loc_daily_transaction_details LDT ON LD.transaction_number = LDT.transaction_number WHERE LD.zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND LD.zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND LD.discount_type = 'N/A' AND zeroratedsales = 0 AND LD.transaction_number = '" & element & "' AND LDT.product_name = '" & ProductName & "' AND LD.transaction_type NOT IN('Walk-In','Registered')"
                                End If
                            Else
                                If DiscountType = "All" Or DiscountType = "N/A" Then
                                    sql1 = "SELECT LD.vatablesales, LD.lessvat FROM loc_daily_transaction LD INNER JOIN loc_daily_transaction_details LDT ON LD.transaction_number = LDT.transaction_number WHERE LD.zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND LD.zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND LD.discount_type = 'N/A' AND zeroratedsales = 0 AND LD.transaction_type = '" & TransactionType & "' AND LD.transaction_number = '" & element & "' AND LDT.product_name = '" & ProductName & "'"
                                End If
                            End If
                        ElseIf TaxType = "NONVAT" Then
                            Dim Types As String = "'Senior Discount 20%','PWD Discount 20%','Sports Discount 20%'"
                            If TransactionType = "All" Then
                                If DiscountType = "All" Or DiscountType = "Percentage(w/o vat)" Then
                                    sql1 = "SELECT LD.vatablesales, LD.lessvat FROM loc_daily_transaction LD INNER JOIN loc_daily_transaction_details LDT ON LD.transaction_number = LDT.transaction_number WHERE LD.zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND LD.zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND LD.discount_type IN (" & Types & ") AND LD.transaction_number = '" & element & "' AND LDT.product_name = '" & ProductName & "'"
                                End If
                            ElseIf TransactionType = "All(Cash)" Then
                                If DiscountType = "All" Or DiscountType = "Percentage(w/o vat)" Then
                                    sql1 = "SELECT LD.vatablesales, LD.lessvat FROM loc_daily_transaction LD INNER JOIN loc_daily_transaction_details LDT ON LD.transaction_number = LDT.transaction_number WHERE LD.zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND LD.zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND LD.discount_type IN (" & Types & ") AND LD.transaction_number = '" & element & "' AND LDT.product_name = '" & ProductName & "' AND LD.transaction_type IN('Walk-In','Registered')"
                                End If
                            ElseIf TransactionType = "All(Others)" Then
                                If DiscountType = "All" Or DiscountType = "Percentage(w/o vat)" Then
                                    sql1 = "SELECT LD.vatablesales, LD.lessvat FROM loc_daily_transaction LD INNER JOIN loc_daily_transaction_details LDT ON LD.transaction_number = LDT.transaction_number WHERE LD.zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND LD.zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND LD.discount_type IN (" & Types & ") AND LD.transaction_number = '" & element & "' AND LDT.product_name = '" & ProductName & "' AND LD.transaction_type NOT IN('Walk-In','Registered')"
                                End If
                            Else
                                If DiscountType = "All" Or DiscountType = "Percentage(w/o vat)" Then
                                    sql1 = "SELECT LD.vatablesales, LD.lessvat FROM loc_daily_transaction LD INNER JOIN loc_daily_transaction_details LDT ON LD.transaction_number = LDT.transaction_number WHERE LD.zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND LD.zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND LD.discount_type IN (" & Types & ") AND LD.transaction_type = '" & TransactionType & "' AND LD.transaction_number = '" & element & "' AND LDT.product_name = '" & ProductName & "'"
                                End If
                            End If
                        ElseIf TaxType = "ZERO RATED" Then
                            If TransactionType = "All" Then
                                If DiscountType = "All" Then
                                    sql1 = "SELECT LD.vatablesales, LD.lessvat FROM loc_daily_transaction LD INNER JOIN loc_daily_transaction_details LDT ON LD.transaction_number = LDT.transaction_number WHERE LD.zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND LD.zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND LD.zeroratedsales > 0 AND LD.transaction_number = '" & element & "' AND LDT.product_name = '" & ProductName & "'"
                                Else
                                    sql1 = "SELECT LD.vatablesales, LD.lessvat FROM loc_daily_transaction LD INNER JOIN loc_daily_transaction_details LDT ON LD.transaction_number = LDT.transaction_number WHERE LD.zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND LD.zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND LD.zeroratedsales > 0 AND LD.transaction_number = '" & element & "' AND LDT.product_name = '" & ProductName & "' AND LD.discount_type = '" & DiscountType & "'"
                                End If
                            ElseIf TransactionType = "All(Cash)" Then
                                If DiscountType = "All" Then
                                    sql1 = "SELECT LD.vatablesales, LD.lessvat FROM loc_daily_transaction LD INNER JOIN loc_daily_transaction_details LDT ON LD.transaction_number = LDT.transaction_number WHERE LD.zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND LD.zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND LD.zeroratedsales > 0 AND LD.transaction_number = '" & element & "' AND LDT.product_name = '" & ProductName & "' AND LD.transaction_type IN('Walk-In','Registered')"
                                Else
                                    sql1 = "SELECT LD.vatablesales, LD.lessvat FROM loc_daily_transaction LD INNER JOIN loc_daily_transaction_details LDT ON LD.transaction_number = LDT.transaction_number WHERE LD.zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND LD.zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND LD.zeroratedsales > 0 AND LD.transaction_number = '" & element & "' AND LDT.product_name = '" & ProductName & "' AND LD.transaction_type IN('Walk-In','Registered') AND LD.discount_type = '" & DiscountType & "'"
                                End If
                            ElseIf TransactionType = "All(Others)" Then
                                If DiscountType = "All" Then
                                    sql1 = "SELECT LD.vatablesales, LD.lessvat FROM loc_daily_transaction LD INNER JOIN loc_daily_transaction_details LDT ON LD.transaction_number = LDT.transaction_number WHERE LD.zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND LD.zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND LD.zeroratedsales > 0 AND LD.transaction_number = '" & element & "' AND LDT.product_name = '" & ProductName & "' AND LD.transaction_type NOT IN('Walk-In','Registered')"
                                Else
                                    sql1 = "SELECT LD.vatablesales, LD.lessvat FROM loc_daily_transaction LD INNER JOIN loc_daily_transaction_details LDT ON LD.transaction_number = LDT.transaction_number WHERE LD.zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND LD.zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND LD.zeroratedsales > 0 AND LD.transaction_number = '" & element & "' AND LDT.product_name = '" & ProductName & "' AND LD.transaction_type NOT IN('Walk-In','Registered') AND LD.discount_type = '" & DiscountType & "'"
                                End If
                            Else
                                If DiscountType = "All" Then
                                    sql1 = "SELECT LD.vatablesales, LD.lessvat FROM loc_daily_transaction LD INNER JOIN loc_daily_transaction_details LDT ON LD.transaction_number = LDT.transaction_number WHERE LD.zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND LD.zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND LD.zeroratedsales > 0 AND LD.transaction_type = '" & TransactionType & "' AND LD.transaction_number = '" & element & "' AND LDT.product_name = '" & ProductName & "'"
                                Else
                                    sql1 = "SELECT LD.vatablesales, LD.lessvat FROM loc_daily_transaction LD INNER JOIN loc_daily_transaction_details LDT ON LD.transaction_number = LDT.transaction_number WHERE LD.zreading >= '" & Format(DateTimePicker17.Value, "yyyy-MM-dd") & "' AND LD.zreading <= '" & Format(DateTimePicker18.Value, "yyyy-MM-dd") & "' AND LD.zeroratedsales > 0 AND LD.transaction_type = '" & TransactionType & "' AND LD.transaction_number = '" & element & "' AND LDT.product_name = '" & ProductName & "' AND LD.discount_type = '" & DiscountType & "'"
                                End If
                            End If
                        End If
                    End If
                End If
                cmd1 = New MySqlCommand(sql1, ConnectionLocal)
                Using reader As MySqlDataReader = cmd1.ExecuteReader
                    If reader.HasRows Then
                        While reader.Read
                            CustomReportVat += reader("vatablesales")
                            CustomReportLessVat += reader("lessvat")
                        End While
                    End If
                End Using
            Next
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Reports: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub ToolStripButton6_Click(sender As Object, e As EventArgs) Handles ToolStripButton6.Click
        reportsdailytransaction(True)
        DataGridViewTransactionDetails.Rows.Clear()
    End Sub
    Private Sub ButtonSearchSystemLogs_Click(sender As Object, e As EventArgs) Handles ButtonSearchSystemLogs.Click
        reportssystemlogs(True)
    End Sub
    Private Sub ButtonSearchTotalDailySales_Click(sender As Object, e As EventArgs) Handles ButtonSearchTotalDailySales.Click
        reportssales(True)
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        reportstransactionlogs(True)
    End Sub
    Private Sub ButtonSearchExpenses_Click(sender As Object, e As EventArgs) Handles ButtonSearchExpenses.Click
        expensereports(True)
        DataGridViewEXPENSEDET.DataSource = Nothing
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        reportsreturnsandrefunds(True)
    End Sub
    Private Sub Button4_Click_1(sender As Object, e As EventArgs) Handles Button4.Click
        viewdeposit(True)
    End Sub
    Private Sub DataGridViewEXPENSES_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridViewEXPENSES.CellClick
        Try
            If DataGridViewEXPENSES.Rows.Count > 0 Then
                Dim datagridid = DataGridViewEXPENSES.SelectedRows(0).Cells(2).Value.ToString()
                viewexpensesdetails(datagridid)
            End If
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub DataGridViewDaily_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridViewDaily.CellClick
        Try
            'transaction_number = (Val(TextBoxCustomerID.Text))
            If DataGridViewDaily.Rows.Count > 0 Then
                viewtransactiondetails(transaction_number:=DataGridViewDaily.SelectedRows(0).Cells(0).Value)
            End If
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub ToolStripButton4_Click(sender As Object, e As EventArgs) Handles ToolStripButton4.Click
        'If ComboBoxTransactionType.Text = "All" Then
        If DataGridViewTransactionDetails.Rows.Count > 0 Then
            total = SumOfColumnsToDecimal(DataGridViewTransactionDetails, 3)
            Try
                If DataGridViewDaily.SelectedRows.Count = 1 Then
                    Dim TotalLines As Integer = 0
                    Dim BodyLine As Integer = 540
                    If DataGridViewDaily.SelectedRows(0).Cells(2).Value > 0 Then
                        BodyLine = 570
                    Else
                        BodyLine = 500
                    End If
                    Dim CountHeaderLine As Integer = count("id", "loc_receipt WHERE type = 'Header' AND status = 1")
                    Dim ProductLine As Integer = 0
                    Dim CountFooterLine As Integer = count("id", "loc_receipt WHERE type = 'Footer' AND status = 1")

                    CountHeaderLine *= 10
                    CountFooterLine *= 10

                    For i As Integer = 0 To DataGridViewTransactionDetails.Rows.Count - 1 Step +1
                        ProductLine += 10
                        If DataGridViewTransactionDetails.Rows(i).Cells(5).Value = "YES" Then
                            ProductLine += 10
                        End If
                    Next

                    TotalLines = CountHeaderLine + ProductLine + CountFooterLine + BodyLine
                    printdoc.DefaultPageSettings.PaperSize = New PaperSize("Custom", ReturnPrintSize(), TotalLines)
                    Dim TrnID = DataGridViewDaily.SelectedRows(0).Cells(0).Value.ToString

                    Dim XMLName As String = "R" & TrnID & FullDateFormatForSaving().ToString & ".xml"


                    XML_Writer = New XmlTextWriter(pdfSharpMod.XML_Path & XMLName, Encoding.UTF8)
                    XML_Writer.WriteStartDocument(True)
                    XML_Writer.Formatting = Formatting.Indented
                    XML_Writer.Indentation = 2
                    XML_Writer.WriteStartElement("Table")

                    If S_Reprint = "YES" Then
                        printdoc.Print()
                    Else
                        PrintPreviewDialog1.Document = printdoc
                        PrintPreviewDialog1.ShowDialog()
                    End If

                    XML_Writer.WriteEndElement()
                    XML_Writer.WriteEndDocument()
                    XML_Writer.Close()
                    SaveXMLInfo(XMLName)

                    AuditTrail.LogToAuditTral("Report", "Reports/Daily Sales: Reprint Sales, " & ClientCrewID, "Normal")

                    InsertIntoEJournal()
                End If

            Catch ex As Exception
                MessageBox.Show("An error occurred while trying to load the " &
                        "document for Print Preview. Make sure you currently have " &
                        "access to a printer. A printer must be localconnected and " &
                        "accessible for Print Preview to work.", Me.Text,
                         MessageBoxButtons.OK, MessageBoxIcon.Error)
                SendErrorReport(ex.ToString)
            End Try
        Else
            MsgBox("Select Transaction First!")
        End If
        'Else
        '    printtransactiontype.DefaultPageSettings.PaperSize = New PaperSize("Custom", 200, 200)
        '    If S_Reprint = "YES" Then
        '        printtransactiontype.Print()
        '    Else
        '        previewtransactiontype.Document = printtransactiontype
        '        previewtransactiontype.ShowDialog()
        '    End If
        'End If
    End Sub

    Private Sub pdoctransactiontype_PrintPage(sender As Object, e As System.Drawing.Printing.PrintPageEventArgs) Handles printtransactiontype.PrintPage
        Try
            ReceiptHeader(sender, e, False)
            Dim WalkinTotal As Decimal = 0
            Dim Registered As Decimal = 0
            Dim GCash As Decimal = 0
            Dim Grab As Decimal = 0
            Dim Paymaya As Decimal = 0
            Dim Lalafood As Decimal = 0
            Dim RepExpense As Decimal = 0
            Dim FoodPanda As Decimal = 0
            Dim Others As Decimal = 0

            Dim WalkinTotalqty As Integer = 0
            Dim Registeredqty As Integer = 0
            Dim GCashqty As Integer = 0
            Dim Grabqty As Integer = 0
            Dim Paymayaqty As Integer = 0
            Dim Lalafoodqty As Integer = 0
            Dim RepExpenseqty As Integer = 0
            Dim FoodPandaqty As Integer = 0
            Dim Othersqty As Integer = 0

            With DataGridViewDaily
                For i As Integer = 0 To .Rows.Count - 1 Step +1
                    If .Rows(i).Cells(11).Value = "Walk-In" Then
                        WalkinTotal += .Rows(i).Cells(1).Value
                        WalkinTotalqty += 1
                    ElseIf .Rows(i).Cells(11).Value = "Registered" Then
                        Registered += .Rows(i).Cells(1).Value
                        Registeredqty += 1
                    ElseIf .Rows(i).Cells(11).Value = "GCash" Then
                        GCash += .Rows(i).Cells(1).Value
                        GCashqty += 1
                    ElseIf .Rows(i).Cells(11).Value = "Grab" Then
                        Grab += .Rows(i).Cells(1).Value
                        Grabqty += 1
                    ElseIf .Rows(i).Cells(11).Value = "Paymaya" Then
                        Paymaya += .Rows(i).Cells(1).Value
                        Paymayaqty += 1
                    ElseIf .Rows(i).Cells(11).Value = "Lalafood" Then
                        Lalafood += .Rows(i).Cells(1).Value
                        Lalafoodqty += 1
                    ElseIf .Rows(i).Cells(11).Value = "Complimentary Expenses" Then
                        RepExpense += .Rows(i).Cells(1).Value
                        RepExpenseqty += 1
                    ElseIf .Rows(i).Cells(11).Value = "Food Panda" Then
                        FoodPanda += .Rows(i).Cells(1).Value
                        FoodPandaqty += 1
                    ElseIf .Rows(i).Cells(11).Value = "Others" Then
                        Others += .Rows(i).Cells(1).Value
                        Othersqty += 1
                    End If
                Next
            End With

            Dim font As New Font("Tahoma", 6)
            Dim font1 As New Font("Tahoma", 6, FontStyle.Bold)
            RightToLeftDisplay(sender, e, 120, "LIST OF TRANSACTION TYPE:", "", font1, 0, 0)
            RightToLeftDisplay(sender, e, 140, "Type/Count:", ":" & "Total", font, 0, 0)

            With ComboBoxTransactionType
                If .Text = "Walk-In" Then
                    RightToLeftDisplay(sender, e, 160, "Walk-In(" & WalkinTotalqty & ")", NUMBERFORMAT(WalkinTotal), font, 0, 0)
                ElseIf .Text = "Registered" Then
                    RightToLeftDisplay(sender, e, 160, "Registered(" & Registeredqty & ")", NUMBERFORMAT(Registered), font, 0, 0)
                ElseIf .Text = "GCash" Then
                    RightToLeftDisplay(sender, e, 160, "GCash(" & GCashqty & ")", NUMBERFORMAT(GCash), font, 0, 0)
                ElseIf .Text = "Grab" Then
                    RightToLeftDisplay(sender, e, 160, "Grab(" & Grabqty & ")", NUMBERFORMAT(Grab), font, 0, 0)
                ElseIf .Text = "Paymaya" Then
                    RightToLeftDisplay(sender, e, 160, "Paymaya(" & Paymayaqty & ")", NUMBERFORMAT(Paymaya), font, 0, 0)
                ElseIf .Text = "Lalafood" Then
                    RightToLeftDisplay(sender, e, 160, "Lalafood(" & Lalafoodqty & ")", NUMBERFORMAT(Lalafood), font, 0, 0)
                ElseIf .Text = "Complimentary Expenses" Then
                    RightToLeftDisplay(sender, e, 160, "Complimentary Expenses(" & RepExpenseqty & ")", NUMBERFORMAT(RepExpense), font, 0, 0)
                ElseIf .Text = "Food Panda" Then
                    RightToLeftDisplay(sender, e, 160, "Food Panda(" & FoodPandaqty & ")", NUMBERFORMAT(FoodPanda), font, 0, 0)
                ElseIf .Text = "Others" Then
                    RightToLeftDisplay(sender, e, 160, "Others(" & Othersqty & ")", NUMBERFORMAT(Others), font, 0, 0)
                End If
            End With

            CenterTextDisplay(sender, e, "From: " & Format(DateTimePicker1.Value, "yyyy-MM-dd") & " - To: " & Format(DateTimePicker2.Value, "yyyy-MM-dd"), font, 180)
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Reports: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub pdoc_PrintPage(sender As Object, e As System.Drawing.Printing.PrintPageEventArgs) Handles printdoc.PrintPage
        Try
            Dim FontDefault As Font
            Dim AddLine As Integer = 20
            Dim CategorySpacing As Integer = 20
            If My.Settings.PrintSize = "57mm" Then
                FontDefault = New Font("Tahoma", 6)
            Else
                FontDefault = New Font("Tahoma", 7)
            End If

            If My.Settings.PrintSize = "80mm" Then
                CategorySpacing = 50
            End If

            If ToolStripComboBoxStatus.Text = "Complete" Then
                ReceiptHeaderOne(sender, e, False, DataGridViewDaily.SelectedRows(0).Cells(0).Value, True, True)
                ReceiptBody(sender, e, False, DataGridViewDaily.SelectedRows(0).Cells(0).Value, True)
                If DataGridViewDaily.SelectedRows(0).Cells(2).Value > 0 Then
                    ReceiptBodyFooter(sender, e, False, DataGridViewDaily.SelectedRows(0).Cells(0).Value, True, True)
                Else
                    ReceiptBodyFooter(sender, e, False, DataGridViewDaily.SelectedRows(0).Cells(0).Value, True, False)
                End If

                ReceiptFooterOne(sender, e, False, False)
            Else
                ReceiptHeaderOne(sender, e, True, DataGridViewDaily.SelectedRows(0).Cells(0).Value, True, True)
                ReceiptBody(sender, e, True, DataGridViewDaily.SelectedRows(0).Cells(0).Value, True)

                If DataGridViewDaily.SelectedRows(0).Cells(2).Value > 0 Then
                    ReceiptBodyFooter(sender, e, False, DataGridViewDaily.SelectedRows(0).Cells(0).Value, True, True)
                Else
                    ReceiptBodyFooter(sender, e, False, DataGridViewDaily.SelectedRows(0).Cells(0).Value, True, False)
                End If
                ReceiptFooterOne(sender, e, True, False)
            End If



            'Dim totalDisplay = NUMBERFORMAT(DataGridViewDaily.SelectedRows(0).Cells(8).Value)
            'a = 40
            'Dim font1 As New Font("Tahoma", 6, FontStyle.Bold)
            'Dim font2 As New Font("Tahoma", 7, FontStyle.Bold)
            'Dim font As New Font("Tahoma", 6)
            'Dim fontaddon As New Font("Tahoma", 5)

            'If DataGridViewDaily.SelectedRows(0).Cells(17).Value = 2 Then
            '    ReceiptHeader(sender, e, True)
            'Else
            '    ReceiptHeader(sender, e, False)
            'End If

            'Dim format1st As StringFormat = New StringFormat(StringFormatFlags.DirectionRightToLeft)
            'Dim abc As Integer = 0
            'If DataGridViewDaily.SelectedRows(0).Cells(17).Value = 2 Then
            '    abc = 50
            'Else
            '    abc = 40
            'End If
            'Try
            '    Dim ConnectionLocal As MySqlConnection = LocalhostConn()
            '    Dim Query1 As String = "SELECT senior_name, senior_id FROM loc_senior_details WHERE transaction_number = '" & DataGridViewDaily.SelectedRows(0).Cells(0).Value & "'"
            '    Dim CmdQ As MySqlCommand = New MySqlCommand(Query1, ConnectionLocal)
            '    Using reader As MySqlDataReader = CmdQ.ExecuteReader
            '        If reader.HasRows Then
            '            While reader.Read
            '                SimpleTextDisplay(sender, e, reader("senior_name") & " - " & reader("senior_id"), font, 30, 82)
            '            End While
            '        End If
            '    End Using
            '    CmdQ.Dispose()
            '    ConnectionLocal.Close()

            'Catch ex As Exception
            '    MsgBox(ex.ToString)
            'End Try
            'For i As Integer = 0 To DataGridViewTransactionDetails.Rows.Count - 1 Step +1
            '    Dim rect1st As RectangleF = New RectangleF(10.0F, 115 + abc, 173.0F, 100.0F)
            '    Dim price = NUMBERFORMAT(DataGridViewTransactionDetails.Rows(i).Cells(3).Value)

            '    If DataGridViewTransactionDetails.Rows(i).Cells(4).Value.ToString = "Add-Ons" Then
            '        If DataGridViewTransactionDetails.Rows(i).Cells(6).Value.ToString = "Classic" Then
            '            RightToLeftDisplay(sender, e, abc + 115, "     @" & DataGridViewTransactionDetails.Rows(i).Cells(0).Value, price, fontaddon, 0, 0)
            '        Else
            '            RightToLeftDisplay(sender, e, abc + 115, DataGridViewTransactionDetails.Rows(i).Cells(1).Value & " " & DataGridViewTransactionDetails.Rows(i).Cells(0).Value, price, font, 0, 0)
            '        End If
            '    Else
            '        RightToLeftDisplay(sender, e, abc + 115, DataGridViewTransactionDetails.Rows(i).Cells(1).Value & " " & DataGridViewTransactionDetails.Rows(i).Cells(0).Value, price, font, 0, 0)
            '        If DataGridViewTransactionDetails.Rows(i).Cells(5).Value = "YES" Then
            '            abc += 10
            '            a += 10

            '            RightToLeftDisplay(sender, e, abc + 115, "     + UPGRADE BRWN " & DataGridViewTransactionDetails.Rows(i).Cells(5).Value, "", fontaddon, 0, 0)
            '        End If
            '    End If
            '    a += 10
            '    abc += 10
            'Next
            'With DataGridViewDaily
            '    Dim b As Integer = .SelectedRows(0).Cells(14).Value
            '    Dim SINUMBERSTRING As String = b.ToString(S_SIFormat)
            '    If .SelectedRows(0).Cells(2).Value < 1 Then
            '        If DataGridViewDaily.SelectedRows(0).Cells(17).Value = 2 Then
            '            a += 130
            '        Else
            '            a += 120
            '        End If

            '        RightToLeftDisplay(sender, e, a, "AMOUNT DUE:", "P" & .SelectedRows(0).Cells(5).Value.ToString, font2, 0, 0)
            '        RightToLeftDisplay(sender, e, a + 15, "CASH:", "P" & .SelectedRows(0).Cells(5).Value.ToString, font1, 0, 0)
            '        RightToLeftDisplay(sender, e, a + 25, "CHANGE:", "P" & .SelectedRows(0).Cells(4).Value.ToString, font1, 0, 0)
            '        PrintStars(sender, e, font, a + 23)
            '        RightToLeftDisplay(sender, e, a + 52, "     VATable Sales", "    " & .SelectedRows(0).Cells(6).Value.ToString, font, 0, 0)
            '        RightToLeftDisplay(sender, e, a + 62, "     Vat Exempt Sales", "    " & .SelectedRows(0).Cells(7).Value.ToString, font, 0, 0)
            '        RightToLeftDisplay(sender, e, a + 72, "     Zero-Rated Sales", "    " & .SelectedRows(0).Cells(8).Value.ToString, font, 0, 0)
            '        RightToLeftDisplay(sender, e, a + 82, "     VAT Amount" & "(" & Val(S_Tax) * 100 & "%)", "    " & .SelectedRows(0).Cells(9).Value.ToString, font, 0, 0)
            '        RightToLeftDisplay(sender, e, a + 92, "     Less Vat", "    " & .SelectedRows(0).Cells(10).Value.ToString, font, 0, 0)
            '        RightToLeftDisplay(sender, e, a + 102, "     Total", "    " & .SelectedRows(0).Cells(5).Value.ToString, font, 0, 0)
            '        a += 4
            '        PrintStars(sender, e, font, a + 92)
            '        a += 1
            '        SimpleTextDisplay(sender, e, "Transaction Type: " & .SelectedRows(0).Cells(11).Value.ToString, font, 0, a + 100)
            '        SimpleTextDisplay(sender, e, "Total Item(s): " & SumOfColumnsToInt(DataGridViewTransactionDetails, 1), font, 0, a + 110)
            '        SimpleTextDisplay(sender, e, "Cashier: " & .SelectedRows(0).Cells(15).Value.ToString & " " & returnfullname(where:= .SelectedRows(0).Cells(15).Value.ToString), font, 0, a + 120)
            '        SimpleTextDisplay(sender, e, "Str No: " & ClientStoreID, font, 110, a + 110)
            '        SimpleTextDisplay(sender, e, "Date & Time: " & .SelectedRows(0).Cells(16).Value, font, 0, a + 130)
            '        SimpleTextDisplay(sender, e, "Terminal No: " & S_Terminal_No, font, 110, a + 140)
            '        SimpleTextDisplay(sender, e, "Ref. #: " & .SelectedRows(0).Cells(0).Value.ToString, font, 0, a + 140)
            '        SimpleTextDisplay(sender, e, "SI No: " & SINUMBERSTRING, font, 0, a + 150)
            '        SimpleTextDisplay(sender, e, "Reprint Copy", font, 0, a + 160)
            '        SimpleTextDisplay(sender, e, "THIS SERVES AS AN OFFICIAL RECEIPT", font, 0, a + 170)
            '        PrintStars(sender, e, font, a + 185)

            '        If DataGridViewDaily.SelectedRows(0).Cells(17).Value = 1 Then
            '            ReceiptFooter(sender, e, a + 12, False)
            '        Else
            '            ReceiptFooter(sender, e, a + 12, True)
            '        End If
            '    Else
            '        a += 100
            '        Dim sql = "SELECT * FROM loc_coupon_data WHERE transaction_number = '" & .SelectedRows(0).Cells(0).Value.ToString & "'"
            '        Dim cmd As MySqlCommand = New MySqlCommand(sql, LocalhostConn)
            '        Dim da As MySqlDataAdapter = New MySqlDataAdapter(cmd)
            '        Dim dt As DataTable = New DataTable
            '        da.Fill(dt)
            '        Dim CouponNameReports = dt(0)(2)
            '        Dim CouponDescReports = dt(0)(3)
            '        Dim CouponTypeReports = dt(0)(4)
            '        Dim CouponLineReports = dt(0)(5)
            '        Dim CouponTotalReports = dt(0)(6)
            '        SimpleTextDisplay(sender, e, CouponNameReports & "(" & CouponTypeReports & ")", font, 0, a)
            '        SimpleTextDisplay(sender, e, CouponDescReports, font, 0, a + 10)
            '        a += 40 + CouponLineReports
            '        RightToLeftDisplay(sender, e, a - 18, "Total Discount:", "P" & CouponTotalReports, font, 0, 0)
            '        Dim SubTotal = SumOfColumnsToDecimal(DataGridViewTransactionDetails, 3)

            '        RightToLeftDisplay(sender, e, a, "SUB TOTAL:", "P" & SubTotal, font1, 0, 0)
            '        RightToLeftDisplay(sender, e, a + 10, "DISCOUNT:", .SelectedRows(0).Cells(2).Value.ToString & "-", font1, 0, 0)
            '        RightToLeftDisplay(sender, e, a + 20, "AMOUNT DUE:", "P" & .SelectedRows(0).Cells(5).Value.ToString, font2, 0, 0)
            '        RightToLeftDisplay(sender, e, a + 30, "CASH:", "P" & .SelectedRows(0).Cells(3).Value.ToString, font1, 0, 0)
            '        RightToLeftDisplay(sender, e, a + 40, "CHANGE:", "P" & .SelectedRows(0).Cells(4).Value.ToString, font1, 0, 0)
            '        PrintStars(sender, e, font, a + 37)
            '        a += 4
            '        RightToLeftDisplay(sender, e, a + 65, "     VATable Sales", "    " & .SelectedRows(0).Cells(6).Value.ToString, font, 0, 0)
            '        RightToLeftDisplay(sender, e, a + 75, "     Vat Exempt Sales", "    " & .SelectedRows(0).Cells(7).Value.ToString, font, 0, 0)
            '        RightToLeftDisplay(sender, e, a + 85, "     Zero-Rated Sales", "    " & .SelectedRows(0).Cells(8).Value.ToString, font, 0, 0)
            '        RightToLeftDisplay(sender, e, a + 95, "     VAT Amount" & "(" & Val(S_Tax) * 100 & "%)", "    " & .SelectedRows(0).Cells(9).Value.ToString, font, 0, 0)
            '        RightToLeftDisplay(sender, e, a + 105, "     Less Vat", "    " & .SelectedRows(0).Cells(10).Value.ToString, font, 0, 0)
            '        RightToLeftDisplay(sender, e, a + 115, "     Total", "    " & .SelectedRows(0).Cells(5).Value.ToString, font, 0, 0)
            '        a += 5
            '        PrintStars(sender, e, font, a + 101)
            '        a += 4
            '        SimpleTextDisplay(sender, e, "Transaction Type: " & .SelectedRows(0).Cells(11).Value.ToString, font, 0, a + 110)
            '        SimpleTextDisplay(sender, e, "Total Item(s): " & SumOfColumnsToInt(DataGridViewTransactionDetails, 1), font, 0, a + 120)
            '        SimpleTextDisplay(sender, e, "Cashier: " & .SelectedRows(0).Cells(15).Value.ToString & " " & returnfullname(where:= .SelectedRows(0).Cells(15).Value.ToString), font, 0, a + 130)
            '        SimpleTextDisplay(sender, e, "Str No: " & ClientStoreID, font, 120, a + 120)
            '        SimpleTextDisplay(sender, e, "Date & Time: " & .SelectedRows(0).Cells(16).Value, font, 0, a + 140)
            '        SimpleTextDisplay(sender, e, "Terminal No: " & S_Terminal_No, font, 120, a + 150)
            '        SimpleTextDisplay(sender, e, "Ref. #: " & .SelectedRows(0).Cells(0).Value.ToString, font, 0, a + 150)
            '        SimpleTextDisplay(sender, e, "SI No: " & SINUMBERSTRING, font, 0, a + 160)
            '        SimpleTextDisplay(sender, e, "Reprint Copy", font, 0, a + 170)
            '        SimpleTextDisplay(sender, e, "THIS SERVES AS AN OFFICIAL RECEIPT", font, 0, a + 180)
            '        a += 6
            '        PrintStars(sender, e, font, a + 190)
            '        a += 16
            '        If DataGridViewDaily.SelectedRows(0).Cells(17).Value = 1 Then
            '            ReceiptFooter(sender, e, a, False)
            '        Else
            '            ReceiptFooter(sender, e, a, True)
            '        End If
            '    End If
            'End With
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub


    Dim ThreadZXRead As Thread
    Dim ThreadlistZXRead As List(Of Thread) = New List(Of Thread)


    Private Sub XZreadingInventory(zreaddate)
        Try
            Dim Con As MySqlConnection = New MySqlConnection
            Con = LocalhostConn()
            Dim Fields As String = "`inventory_id`, `store_id`, `formula_id`, `product_ingredients`, `sku`, `stock_primary`, `stock_secondary`, `stock_no_of_servings`, `stock_status`, `critical_limit`, `guid`, `created_at`, `crew_id`, `synced`, `server_date_modified`, `server_inventory_id`, `zreading`"
            Dim cmd As MySqlCommand
            With DataGridViewZreadInventory
                For i As Integer = 0 To .Rows.Count - 1 Step +1
                    cmd = New MySqlCommand("INSERT INTO loc_zread_inventory (" & Fields & ") VALUES (@1,@2,@3,@4,@5,@6,@7,@8,@9,@10,@11,@12,@13,@14,@15,@16,@17)", Con)
                    cmd.Parameters.Add("@1", MySqlDbType.Int64).Value = .Rows(i).Cells(0).Value.ToString
                    cmd.Parameters.Add("@2", MySqlDbType.VarChar).Value = .Rows(i).Cells(1).Value.ToString
                    cmd.Parameters.Add("@3", MySqlDbType.Int64).Value = .Rows(i).Cells(2).Value.ToString
                    cmd.Parameters.Add("@4", MySqlDbType.VarChar).Value = .Rows(i).Cells(3).Value.ToString
                    cmd.Parameters.Add("@5", MySqlDbType.VarChar).Value = .Rows(i).Cells(4).Value.ToString
                    cmd.Parameters.Add("@6", MySqlDbType.Double).Value = .Rows(i).Cells(5).Value.ToString
                    cmd.Parameters.Add("@7", MySqlDbType.Double).Value = .Rows(i).Cells(6).Value.ToString
                    cmd.Parameters.Add("@8", MySqlDbType.Double).Value = .Rows(i).Cells(7).Value.ToString
                    cmd.Parameters.Add("@9", MySqlDbType.Int64).Value = .Rows(i).Cells(8).Value.ToString
                    cmd.Parameters.Add("@10", MySqlDbType.Int64).Value = .Rows(i).Cells(9).Value.ToString
                    cmd.Parameters.Add("@11", MySqlDbType.VarChar).Value = .Rows(i).Cells(10).Value.ToString
                    cmd.Parameters.Add("@12", MySqlDbType.Text).Value = FullDate24HR()
                    cmd.Parameters.Add("@13", MySqlDbType.VarChar).Value = .Rows(i).Cells(12).Value.ToString
                    cmd.Parameters.Add("@14", MySqlDbType.VarChar).Value = "Unsynced"
                    cmd.Parameters.Add("@15", MySqlDbType.Text).Value = "N/A"
                    cmd.Parameters.Add("@16", MySqlDbType.Int64).Value = .Rows(i).Cells(15).Value.ToString
                    cmd.Parameters.Add("@17", MySqlDbType.Text).Value = S_Zreading
                    cmd.ExecuteNonQuery()
                Next
                Con.Close()
            End With
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Reports: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub

    Dim threadlist As List(Of Thread) = New List(Of Thread)
    Dim thread1 As Thread


    Private Sub MainInventorySub()
        Try
            With DataGridViewZreadInventory
                Dim MainInvId As Integer = 0
                Dim SubInvId As Integer = 0

                Dim MICommand As MySqlCommand
                Dim MIDa As MySqlDataAdapter
                Dim MiDt As DataTable

                Dim MPrimary As Double = 0
                Dim MSecondary As Double = 0
                Dim MNoOfServings As Double = 0

                Dim ZPrimary As Double = 0
                Dim ZSecondary As Double = 0
                Dim ZNoOfServings As Double = 0

                Dim TPrimary As Double = 0
                Dim TSecondary As Double = 0
                Dim TNoOfServings As Double = 0

                For i As Integer = 0 To .Rows.Count - 1 Step +1
                    MainInvId = .Rows(i).Cells(16).Value
                    SubInvId = .Rows(i).Cells(0).Value
                    If MainInvId <> 0 Then
                        Dim MIQuery As String = ""
                        'Get main product stock
                        MIQuery = "SELECT stock_primary, stock_secondary, stock_no_of_servings FROM loc_pos_inventory WHERE inventory_id = " & MainInvId
                        MICommand = New MySqlCommand(MIQuery, LocalhostConn)
                        MIDa = New MySqlDataAdapter(MICommand)
                        MiDt = New DataTable
                        MIDa.Fill(MiDt)
                        For Each row As DataRow In MiDt.Rows
                            MPrimary = row("stock_primary")
                            MSecondary = row("stock_secondary")
                            MNoOfServings = row("stock_no_of_servings")
                        Next
                        'Get sub product value : 5 stock_primary, 6 stock_secondary , 7 stock_no_of_servings
                        ZPrimary = .Rows(i).Cells(5).Value
                        ZSecondary = .Rows(i).Cells(6).Value
                        ZNoOfServings = .Rows(i).Cells(7).Value
                        'Total inventory : Main - Sub = Total zread inv

                        TPrimary = MPrimary - Math.Abs(ZPrimary)
                        TSecondary = MSecondary - Math.Abs(ZSecondary)
                        TNoOfServings = MNoOfServings - Math.Abs(ZNoOfServings)
                        'Update Main inventory 

                        Dim MIQuery1 = "Update loc_pos_inventory SET stock_primary = @1, stock_secondary = @2, stock_no_of_servings = @3 WHERE inventory_id = " & MainInvId
                        Dim MICommand1 = New MySqlCommand(MIQuery1, LocalhostConn)
                        MICommand1.Parameters.Add("@1", MySqlDbType.Double).Value = TPrimary
                        MICommand1.Parameters.Add("@2", MySqlDbType.Double).Value = TSecondary
                        MICommand1.Parameters.Add("@3", MySqlDbType.Double).Value = TNoOfServings
                        MICommand1.ExecuteNonQuery()
                        'Update Sub inventory 
                        Dim MIQuery2 = "Update loc_pos_inventory SET stock_primary = 0, stock_secondary = 0, stock_no_of_servings = 0 WHERE inventory_id = " & SubInvId
                        Dim MICommand2 = New MySqlCommand(MIQuery2, LocalhostConn)
                        MICommand2.ExecuteNonQuery()
                        MICommand2.Dispose()
                        LocalhostConn.Close()
                    End If
                Next
            End With
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Reports: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub


    Dim PrintSalesDatatable As DataTable

    Private Sub ToolStripButton8_Click(sender As Object, e As EventArgs) Handles ToolStripButtonPrintSales.Click
        Try
            Dim TotalLines As Integer = 0
            Dim BodyLine As Integer = 650
            Dim CountHeaderLine As Integer = count("id", "loc_receipt WHERE type = 'Header' AND status = 1")
            Dim ProductLine As Integer = 0

            Dim sql = "SELECT product_sku , SUM(quantity), SUM(total), product_category FROM loc_daily_transaction_details WHERE zreading >= '" & Format(DateTimePicker3.Value, "yyyy-MM-dd") & "' AND zreading <= '" & Format(DateTimePicker4.Value, "yyyy-MM-dd") & "' AND active = 1  AND store_id = '" & ClientStoreID & "' AND guid = '" & ClientGuid & "' GROUP BY product_name"
            Dim cmd As MySqlCommand = New MySqlCommand(sql, LocalhostConn)
            Dim da As MySqlDataAdapter = New MySqlDataAdapter(cmd)
            PrintSalesDatatable = New DataTable
            da.Fill(PrintSalesDatatable)

            For i As Integer = 0 To PrintSalesDatatable.Rows.Count - 1 Step +1
                ProductLine += 10
            Next
            CountHeaderLine *= 10

            TotalLines = CountHeaderLine + ProductLine + BodyLine
            printsales.DefaultPageSettings.PaperSize = New PaperSize("Custom", ReturnPrintSize(), TotalLines)

            If S_Print_Sales_Report = "YES" Then
                printsales.Print()
            Else

                previewsales.Document = printsales
                previewsales.ShowDialog()
            End If
            AuditTrail.LogToAuditTral("Report", "Reports/Sales Report: Generated Report, " & ClientCrewID, "Normal")

        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Reports: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub printsales_printdoc(sender As Object, e As System.Drawing.Printing.PrintPageEventArgs) Handles printsales.PrintPage
        Try
            Dim BrandFont As Font
            Dim FontDefault As Font
            Dim FontDefaultBold As Font
            Dim FontDefaultLine As Font
            If My.Settings.PrintSize = "57mm" Then
                BrandFont = New Font("Tahoma", 7, FontStyle.Bold)
                FontDefault = New Font("Tahoma", 5)
                FontDefaultBold = New Font("Tahoma", 6, FontStyle.Bold)
                FontDefaultLine = New Font("Tahoma", 6)
            Else
                BrandFont = New Font("Tahoma", 8, FontStyle.Bold)
                FontDefault = New Font("Tahoma", 6)
                FontDefaultBold = New Font("Tahoma", 7, FontStyle.Bold)
                FontDefaultLine = New Font("Tahoma", 7)
            End If

            ReceiptHeaderOne(sender, e, False, "", False, False)

            Dim IfPrintSmall As Integer = 0
            Dim IfPrintQty As Integer = 0

            If My.Settings.PrintSize = "57mm" Then
                IfPrintSmall = -20
                IfPrintQty = -10
            End If

            SimpleTextDisplay(sender, e, "ITEM SOLD REPORT", FontDefault, 0, RECEIPTLINECOUNT)
            RECEIPTLINECOUNT += 10
            SimpleTextDisplay(sender, e, "BY DEPARTMENT", FontDefault, 0, RECEIPTLINECOUNT)
            RECEIPTLINECOUNT += 10
            SimpleTextDisplay(sender, e, "TERMINAL NO.", FontDefault, 0, RECEIPTLINECOUNT)
            RECEIPTLINECOUNT += 10
            PrintSmallLine(sender, e, FontDefaultLine, RECEIPTLINECOUNT)
            RECEIPTLINECOUNT += 10
            SimpleTextDisplay(sender, e, "ITEMS", FontDefault, 0, RECEIPTLINECOUNT)
            SimpleTextDisplay(sender, e, "QTY", FontDefault, 70 + IfPrintSmall, RECEIPTLINECOUNT)
            SimpleTextDisplay(sender, e, "%", FontDefault, 120 + IfPrintSmall, RECEIPTLINECOUNT)
            SimpleTextDisplay(sender, e, "AMOUNT", FontDefault, 170 + IfPrintSmall, RECEIPTLINECOUNT)
            RECEIPTLINECOUNT += 10
            PrintSmallLine(sender, e, FontDefaultLine, RECEIPTLINECOUNT)
            RECEIPTLINECOUNT += 30
            PrintSmallLine(sender, e, FontDefaultLine, RECEIPTLINECOUNT - 10)
            CenterTextDisplay(sender, e, "SIMPLY PERFECT", FontDefault, RECEIPTLINECOUNT)

            RECEIPTLINECOUNT += 20

            Dim maxLength As Integer = 0
            Dim title As String = ""
            Dim TotalSales As Integer = SumOfColumnsToDecimal(DataGridViewSales, 4)
            Dim TotalPercentage As Double = 0
            Dim Percentage As Double = 0
            Dim SimplyPerfectQty As Integer = 0
            Dim SimplyPerfectPercentage As Double = 0
            Dim SimplyPerfectTotalSales As Double = 0

            For i As Integer = 0 To DataGridViewSales.Rows.Count - 1 Step +1

                If DataGridViewSales.Rows(i).Cells(6).Value.ToString = "Simply Perfect" Then
                    maxLength = Math.Min(DataGridViewSales.Rows(i).Cells(0).Value.Length, 15)
                    title = DataGridViewSales.Rows(i).Cells(0).Value.Substring(0, maxLength)

                    If maxLength = 15 Then
                        title &= ".."
                    End If

                    SimpleTextDisplay(sender, e, title, FontDefault, 0, RECEIPTLINECOUNT - 20)
                    Percentage = DataGridViewSales.Rows(i).Cells(3).Value / TotalSales * 100
                    Percentage = Percentage * DataGridViewSales.Rows(i).Cells(2).Value
                    SimplyPerfectPercentage += Percentage
                    RightDisplay1(sender, e, RECEIPTLINECOUNT, DataGridViewSales.Rows(i).Cells(2).Value, Math.Round(Percentage, 2) & "%", FontDefault, 60, 77 + IfPrintQty)
                    SimplyPerfectQty += DataGridViewSales.Rows(i).Cells(2).Value
                    SimplyPerfectTotalSales += DataGridViewSales.Rows(i).Cells(4).Value
                    RightDisplay1(sender, e, RECEIPTLINECOUNT, "", Math.Round(DataGridViewSales.Rows(i).Cells(4).Value, 2), FontDefault, 60, 145 + IfPrintSmall)
                    RECEIPTLINECOUNT += 10
                End If
            Next

            PrintSmallLine(sender, e, FontDefaultLine, RECEIPTLINECOUNT)
            RECEIPTLINECOUNT += 10
            SimpleTextDisplay(sender, e, "SUBTOTAL", FontDefault, 0, RECEIPTLINECOUNT - 20)
            SimplyPerfectPercentage = SimplyPerfectTotalSales / TotalSales * 100
            RightDisplay1(sender, e, RECEIPTLINECOUNT, SimplyPerfectQty, Math.Round(SimplyPerfectPercentage, 2) & "%", FontDefault, 60, 77 + IfPrintQty)
            RightDisplay1(sender, e, RECEIPTLINECOUNT, "", NUMBERFORMAT(Math.Round(SimplyPerfectTotalSales, 2)), FontDefault, 60, 145 + IfPrintSmall)
            RECEIPTLINECOUNT += 10
            PrintSmallLine(sender, e, FontDefaultLine, RECEIPTLINECOUNT)
            RECEIPTLINECOUNT += 10
            CenterTextDisplay(sender, e, "PERFECT COMBINATION", FontDefault, RECEIPTLINECOUNT)
            RECEIPTLINECOUNT += 20
            TotalPercentage += SimplyPerfectPercentage
            Dim PerfectCombinationQty As Integer = 0
            Dim PerfectCombinationPercentage As Double = 0
            Dim PerfectCombinationTotalSales As Double = 0


            For i As Integer = 0 To DataGridViewSales.Rows.Count - 1 Step +1

                If DataGridViewSales.Rows(i).Cells(6).Value.ToString = "Perfect Combination" Then
                    maxLength = Math.Min(DataGridViewSales.Rows(i).Cells(0).Value.Length, 15)
                    title = DataGridViewSales.Rows(i).Cells(0).Value.Substring(0, maxLength)

                    If maxLength = 15 Then
                        title &= ".."
                    End If

                    SimpleTextDisplay(sender, e, title, FontDefault, 0, RECEIPTLINECOUNT - 20)
                    Percentage = DataGridViewSales.Rows(i).Cells(3).Value / TotalSales * 100
                    Percentage = Percentage * DataGridViewSales.Rows(i).Cells(2).Value
                    PerfectCombinationPercentage += Percentage
                    RightDisplay1(sender, e, RECEIPTLINECOUNT, DataGridViewSales.Rows(i).Cells(2).Value, Math.Round(Percentage, 2) & "%", FontDefault, 60, 77 + IfPrintQty)
                    PerfectCombinationQty += DataGridViewSales.Rows(i).Cells(2).Value
                    PerfectCombinationTotalSales += DataGridViewSales.Rows(i).Cells(4).Value
                    RightDisplay1(sender, e, RECEIPTLINECOUNT, "", Math.Round(DataGridViewSales.Rows(i).Cells(4).Value, 2), FontDefault, 60, 145 + IfPrintSmall)
                    RECEIPTLINECOUNT += 10
                End If

            Next

            PrintSmallLine(sender, e, FontDefaultLine, RECEIPTLINECOUNT)
            RECEIPTLINECOUNT += 10
            SimpleTextDisplay(sender, e, "SUBTOTAL", FontDefault, 0, RECEIPTLINECOUNT - 20)
            PerfectCombinationPercentage = PerfectCombinationTotalSales / TotalSales * 100
            RightDisplay1(sender, e, RECEIPTLINECOUNT, PerfectCombinationQty, Math.Round(PerfectCombinationPercentage, 2) & "%", FontDefault, 60, 77 + IfPrintQty)
            RightDisplay1(sender, e, RECEIPTLINECOUNT, "", Math.Round(PerfectCombinationTotalSales, 2), FontDefault, 60, 145 + IfPrintSmall)
            RECEIPTLINECOUNT += 10
            PrintSmallLine(sender, e, FontDefaultLine, RECEIPTLINECOUNT)
            RECEIPTLINECOUNT += 10
            CenterTextDisplay(sender, e, "PREMIUM", FontDefault, RECEIPTLINECOUNT)
            RECEIPTLINECOUNT += 20
            TotalPercentage += PerfectCombinationPercentage

            Dim PremiumQty As Integer = 0
            Dim PremiumPercentage As Double = 0
            Dim PremiumTotalSales As Double = 0

            For i As Integer = 0 To DataGridViewSales.Rows.Count - 1 Step +1

                If DataGridViewSales.Rows(i).Cells(6).Value.ToString = "Premium" Then
                    maxLength = Math.Min(DataGridViewSales.Rows(i).Cells(0).Value.Length, 15)
                    title = DataGridViewSales.Rows(i).Cells(0).Value.Substring(0, maxLength)

                    If maxLength = 15 Then
                        title &= ".."
                    End If

                    SimpleTextDisplay(sender, e, title, FontDefault, 0, RECEIPTLINECOUNT - 20)
                    Percentage = DataGridViewSales.Rows(i).Cells(3).Value / TotalSales * 100
                    Percentage = Percentage * DataGridViewSales.Rows(i).Cells(2).Value
                    PremiumPercentage += Percentage
                    RightDisplay1(sender, e, RECEIPTLINECOUNT, DataGridViewSales.Rows(i).Cells(2).Value, Math.Round(Percentage, 2) & "%", FontDefault, 60, 77 + IfPrintQty)
                    PremiumQty += DataGridViewSales.Rows(i).Cells(2).Value
                    PremiumTotalSales += DataGridViewSales.Rows(i).Cells(4).Value
                    RightDisplay1(sender, e, RECEIPTLINECOUNT, "", Math.Round(DataGridViewSales.Rows(i).Cells(4).Value, 2), FontDefault, 60, 145 + IfPrintSmall)
                    RECEIPTLINECOUNT += 10
                End If

            Next

            PrintSmallLine(sender, e, FontDefaultLine, RECEIPTLINECOUNT)
            RECEIPTLINECOUNT += 10
            SimpleTextDisplay(sender, e, "SUBTOTAL", FontDefault, 0, RECEIPTLINECOUNT - 20)
            PremiumPercentage = PremiumTotalSales / TotalSales * 100
            RightDisplay1(sender, e, RECEIPTLINECOUNT, PremiumQty, Math.Round(PremiumPercentage, 2) & "%", FontDefault, 60, 77 + IfPrintQty)
            RightDisplay1(sender, e, RECEIPTLINECOUNT, "", Math.Round(PremiumTotalSales, 2), FontDefault, 60, 145 + IfPrintSmall)
            RECEIPTLINECOUNT += 10
            PrintSmallLine(sender, e, FontDefaultLine, RECEIPTLINECOUNT)
            RECEIPTLINECOUNT += 10
            CenterTextDisplay(sender, e, "COMBO", FontDefault, RECEIPTLINECOUNT)
            RECEIPTLINECOUNT += 20

            TotalPercentage += PremiumPercentage

            Dim ComboQty As Integer = 0
            Dim ComboPercentage As Double = 0
            Dim ComboTotalSales As Double = 0


            For i As Integer = 0 To DataGridViewSales.Rows.Count - 1 Step +1

                If DataGridViewSales.Rows(i).Cells(6).Value.ToString = "Combo" Then
                    maxLength = Math.Min(DataGridViewSales.Rows(i).Cells(0).Value.Length, 15)
                    title = DataGridViewSales.Rows(i).Cells(0).Value.Substring(0, maxLength)

                    If maxLength = 15 Then
                        title &= ".."
                    End If

                    SimpleTextDisplay(sender, e, title, FontDefault, 0, RECEIPTLINECOUNT - 20)

                    Percentage = DataGridViewSales.Rows(i).Cells(3).Value / TotalSales * 100
                    Percentage = Percentage * DataGridViewSales.Rows(i).Cells(2).Value
                    ComboPercentage += Percentage
                    RightDisplay1(sender, e, RECEIPTLINECOUNT, DataGridViewSales.Rows(i).Cells(2).Value, Math.Round(Percentage, 2) & "%", FontDefault, 60, 77 + IfPrintQty)
                    ComboQty += DataGridViewSales.Rows(i).Cells(2).Value
                    ComboTotalSales += DataGridViewSales.Rows(i).Cells(4).Value
                    RightDisplay1(sender, e, RECEIPTLINECOUNT, "", Math.Round(DataGridViewSales.Rows(i).Cells(4).Value, 2), FontDefault, 60, 145 + IfPrintSmall)
                    RECEIPTLINECOUNT += 10
                End If

            Next

            PrintSmallLine(sender, e, FontDefaultLine, RECEIPTLINECOUNT)
            RECEIPTLINECOUNT += 10
            SimpleTextDisplay(sender, e, "SUBTOTAL", FontDefault, 0, RECEIPTLINECOUNT - 20)
            ComboPercentage = ComboTotalSales / TotalSales * 100
            RightDisplay1(sender, e, RECEIPTLINECOUNT, ComboQty, Math.Round(ComboPercentage, 2) & "%", FontDefault, 60, 77 + IfPrintQty)
            RightDisplay1(sender, e, RECEIPTLINECOUNT, "", Math.Round(ComboTotalSales, 2), FontDefault, 60, 145 + IfPrintSmall)
            RECEIPTLINECOUNT += 10
            PrintSmallLine(sender, e, FontDefaultLine, RECEIPTLINECOUNT)
            RECEIPTLINECOUNT += 10
            CenterTextDisplay(sender, e, "SAVORY", FontDefault, RECEIPTLINECOUNT)
            RECEIPTLINECOUNT += 20
            TotalPercentage += ComboPercentage


            Dim SavoryQty As Integer = 0
            Dim SavoryPercentage As Double = 0
            Dim SavoryTotalSales As Double = 0

            For i As Integer = 0 To DataGridViewSales.Rows.Count - 1 Step +1

                If DataGridViewSales.Rows(i).Cells(6).Value.ToString = "Savory" Then
                    maxLength = Math.Min(DataGridViewSales.Rows(i).Cells(0).Value.Length, 15)
                    title = DataGridViewSales.Rows(i).Cells(0).Value.Substring(0, maxLength)

                    If maxLength = 15 Then
                        title &= ".."
                    End If

                    SimpleTextDisplay(sender, e, title, FontDefault, 0, RECEIPTLINECOUNT - 20)

                    Percentage = DataGridViewSales.Rows(i).Cells(3).Value / TotalSales * 100
                    Percentage = Percentage * DataGridViewSales.Rows(i).Cells(2).Value
                    SavoryPercentage += Percentage
                    RightDisplay1(sender, e, RECEIPTLINECOUNT, DataGridViewSales.Rows(i).Cells(2).Value, Math.Round(Percentage, 2) & "%", FontDefault, 60, 77 + IfPrintQty)
                    SavoryQty += DataGridViewSales.Rows(i).Cells(2).Value
                    SavoryTotalSales += DataGridViewSales.Rows(i).Cells(4).Value
                    RightDisplay1(sender, e, RECEIPTLINECOUNT, "", Math.Round(DataGridViewSales.Rows(i).Cells(4).Value, 2), FontDefault, 60, 145 + IfPrintSmall)
                    RECEIPTLINECOUNT += 10
                End If

            Next

            PrintSmallLine(sender, e, FontDefaultLine, RECEIPTLINECOUNT)
            RECEIPTLINECOUNT += 10
            SimpleTextDisplay(sender, e, "SUBTOTAL", FontDefault, 0, RECEIPTLINECOUNT - 20)
            SavoryPercentage = SavoryTotalSales / TotalSales * 100
            RightDisplay1(sender, e, RECEIPTLINECOUNT, SavoryQty, Math.Round(SavoryPercentage, 2) & "%", FontDefault, 60, 77 + IfPrintQty)
            RightDisplay1(sender, e, RECEIPTLINECOUNT, "", Math.Round(SavoryTotalSales, 2), FontDefault, 60, 145 + IfPrintSmall)
            RECEIPTLINECOUNT += 10
            PrintSmallLine(sender, e, FontDefaultLine, RECEIPTLINECOUNT)
            RECEIPTLINECOUNT += 10
            CenterTextDisplay(sender, e, "FAMOUS BLENDS", FontDefault, RECEIPTLINECOUNT)
            RECEIPTLINECOUNT += 20
            TotalPercentage += SavoryPercentage


            Dim FamousBlendsQty As Integer = 0
            Dim FamousBlendsPercentage As Double = 0
            Dim FamousBlendsTotalSales As Double = 0

            For i As Integer = 0 To DataGridViewSales.Rows.Count - 1 Step +1

                If DataGridViewSales.Rows(i).Cells(6).Value.ToString = "Famous Blends" Then
                    maxLength = Math.Min(DataGridViewSales.Rows(i).Cells(0).Value.Length, 15)
                    title = DataGridViewSales.Rows(i).Cells(0).Value.Substring(0, maxLength)

                    If maxLength = 15 Then
                        title &= ".."
                    End If

                    SimpleTextDisplay(sender, e, title, FontDefault, 0, RECEIPTLINECOUNT - 20)

                    Percentage = DataGridViewSales.Rows(i).Cells(3).Value / TotalSales * 100
                    Percentage = Percentage * DataGridViewSales.Rows(i).Cells(2).Value
                    FamousBlendsPercentage += Percentage
                    RightDisplay1(sender, e, RECEIPTLINECOUNT, DataGridViewSales.Rows(i).Cells(2).Value, Math.Round(Percentage, 2) & "%", FontDefault, 60, 77 + IfPrintQty)
                    FamousBlendsQty += DataGridViewSales.Rows(i).Cells(2).Value
                    FamousBlendsTotalSales += DataGridViewSales.Rows(i).Cells(4).Value
                    RightDisplay1(sender, e, RECEIPTLINECOUNT, "", Math.Round(DataGridViewSales.Rows(i).Cells(4).Value, 2), FontDefault, 60, 145 + IfPrintSmall)
                    RECEIPTLINECOUNT += 10
                End If

            Next

            PrintSmallLine(sender, e, FontDefaultLine, RECEIPTLINECOUNT)
            RECEIPTLINECOUNT += 10
            SimpleTextDisplay(sender, e, "SUBTOTAL", FontDefault, 0, RECEIPTLINECOUNT - 20)
            FamousBlendsPercentage = FamousBlendsTotalSales / TotalSales * 100
            RightDisplay1(sender, e, RECEIPTLINECOUNT, FamousBlendsQty, Math.Round(FamousBlendsPercentage, 2) & "%", FontDefault, 60, 77 + IfPrintQty)
            RightDisplay1(sender, e, RECEIPTLINECOUNT, "", Math.Round(FamousBlendsTotalSales, 2), FontDefault, 60, 145 + IfPrintSmall)
            RECEIPTLINECOUNT += 10
            PrintSmallLine(sender, e, FontDefaultLine, RECEIPTLINECOUNT)
            RECEIPTLINECOUNT += 10
            CenterTextDisplay(sender, e, "ADD-ONS", FontDefault, RECEIPTLINECOUNT)
            RECEIPTLINECOUNT += 20
            TotalPercentage += FamousBlendsPercentage

            Dim AddOnsQty As Integer = 0
            Dim AddOnsPercentage As Double = 0
            Dim AddOnsTotalSales As Double = 0

            For i As Integer = 0 To DataGridViewSales.Rows.Count - 1 Step +1

                If DataGridViewSales.Rows(i).Cells(6).Value.ToString = "Add-Ons" Then
                    maxLength = Math.Min(DataGridViewSales.Rows(i).Cells(0).Value.Length, 15)
                    title = DataGridViewSales.Rows(i).Cells(0).Value.Substring(0, maxLength)

                    If maxLength = 15 Then
                        title &= ".."
                    End If

                    SimpleTextDisplay(sender, e, title, FontDefault, 0, RECEIPTLINECOUNT - 20)

                    Percentage = DataGridViewSales.Rows(i).Cells(3).Value / TotalSales * 100
                    Percentage = Percentage * DataGridViewSales.Rows(i).Cells(2).Value
                    AddOnsPercentage += Percentage
                    RightDisplay1(sender, e, RECEIPTLINECOUNT, DataGridViewSales.Rows(i).Cells(2).Value, Math.Round(Percentage, 2) & "%", FontDefault, 60, 77 + IfPrintQty)

                    AddOnsQty += DataGridViewSales.Rows(i).Cells(2).Value
                    AddOnsTotalSales += DataGridViewSales.Rows(i).Cells(4).Value
                    RightDisplay1(sender, e, RECEIPTLINECOUNT, "", Math.Round(DataGridViewSales.Rows(i).Cells(4).Value, 2), FontDefault, 60, 145 + IfPrintSmall)
                    RECEIPTLINECOUNT += 10
                End If

            Next

            PrintSmallLine(sender, e, FontDefaultLine, RECEIPTLINECOUNT - 20)
            RECEIPTLINECOUNT += 10
            SimpleTextDisplay(sender, e, "SUBTOTAL", FontDefault, 0, RECEIPTLINECOUNT - 20)
            AddOnsPercentage = AddOnsTotalSales / TotalSales * 100
            RightDisplay1(sender, e, RECEIPTLINECOUNT, AddOnsQty, Math.Round(AddOnsPercentage, 2) & "%", FontDefault, 60, 77 + IfPrintQty)
            RightDisplay1(sender, e, RECEIPTLINECOUNT, "", Math.Round(AddOnsTotalSales, 2), FontDefault, 60, 145 + IfPrintSmall)
            RECEIPTLINECOUNT += 10
            PrintSmallLine(sender, e, FontDefaultLine, RECEIPTLINECOUNT)
            RECEIPTLINECOUNT += 10
            CenterTextDisplay(sender, e, "OTHERS", FontDefault, RECEIPTLINECOUNT)
            RECEIPTLINECOUNT += 20

            TotalPercentage += AddOnsPercentage

            Dim OthersQty As Integer = 0
            Dim OthersPercentage As Double = 0
            Dim OthersTotalSales As Double = 0


            For i As Integer = 0 To DataGridViewSales.Rows.Count - 1 Step +1

                If DataGridViewSales.Rows(i).Cells(6).Value.ToString = "Others" Then
                    maxLength = Math.Min(DataGridViewSales.Rows(i).Cells(0).Value.Length, 15)
                    title = DataGridViewSales.Rows(i).Cells(0).Value.Substring(0, maxLength)

                    If maxLength = 15 Then
                        title &= ".."
                    End If

                    SimpleTextDisplay(sender, e, title, FontDefault, 0, RECEIPTLINECOUNT - 20)

                    Percentage = DataGridViewSales.Rows(i).Cells(3).Value / TotalSales * 100
                    Percentage = Percentage * DataGridViewSales.Rows(i).Cells(2).Value
                    OthersPercentage += Percentage
                    RightDisplay1(sender, e, RECEIPTLINECOUNT, DataGridViewSales.Rows(i).Cells(2).Value, Math.Round(Percentage, 2) & "%", FontDefault, 60, 77 + IfPrintQty)
                    OthersQty += DataGridViewSales.Rows(i).Cells(2).Value
                    OthersTotalSales += DataGridViewSales.Rows(i).Cells(4).Value
                    RightDisplay1(sender, e, RECEIPTLINECOUNT, "", Math.Round(DataGridViewSales.Rows(i).Cells(4).Value, 2), FontDefault, 60, 145 + IfPrintSmall)
                    RECEIPTLINECOUNT += 10
                End If

            Next

            PrintSmallLine(sender, e, FontDefaultLine, RECEIPTLINECOUNT - 20)
            RECEIPTLINECOUNT += 10
            SimpleTextDisplay(sender, e, "SUBTOTAL", FontDefault, 0, RECEIPTLINECOUNT - 20)
            OthersPercentage = OthersTotalSales / TotalSales * 100
            RightDisplay1(sender, e, RECEIPTLINECOUNT, OthersQty, Math.Round(OthersPercentage, 2) & "%", FontDefault, 60, 77 + IfPrintQty)
            RightDisplay1(sender, e, RECEIPTLINECOUNT, "", Math.Round(OthersTotalSales, 2), FontDefault, 60, 145 + IfPrintSmall)
            RECEIPTLINECOUNT += 10
            TotalPercentage += OthersPercentage

            PrintSmallLine(sender, e, FontDefaultLine, RECEIPTLINECOUNT)
            RECEIPTLINECOUNT += 10
            SimpleTextDisplay(sender, e, "TOTAL", FontDefault, 0, RECEIPTLINECOUNT - 20)
            RightDisplay1(sender, e, RECEIPTLINECOUNT, SumOfColumnsToInt(DataGridViewSales, 2), Math.Round(TotalPercentage, 2) & "%", FontDefault, 60, 77 + IfPrintQty)
            RightDisplay1(sender, e, RECEIPTLINECOUNT, "", NUMBERFORMAT(TotalSales), FontDefault, 60, 145 + IfPrintSmall)
            RECEIPTLINECOUNT += 20


            Dim TotalDiscount As Double = 0
            ThreadZXRead = New Thread(Sub() TotalDiscount = sum("totaldiscount", "loc_daily_transaction WHERE zreading >= '" & Format(DateTimePicker3.Value, "yyyy-MM-dd") & "' AND zreading <= '" & Format(DateTimePicker4.Value, "yyyy-MM-dd") & "'  AND active = 1"))
            ThreadZXRead.Start()
            ThreadlistZXRead.Add(ThreadZXRead)
            For Each t In ThreadlistZXRead
                t.Join()
            Next

            Dim LessVat As Double = 0
            ThreadZXRead = New Thread(Sub() LessVat = sum("lessvat", "loc_daily_transaction WHERE zreading >= '" & Format(DateTimePicker3.Value, "yyyy-MM-dd") & "' AND zreading <= '" & Format(DateTimePicker4.Value, "yyyy-MM-dd") & "' AND  active = 1"))
            ThreadZXRead.Start()
            ThreadlistZXRead.Add(ThreadZXRead)
            For Each t In ThreadlistZXRead
                t.Join()
            Next

            Dim DailySales As Double = 0
            ThreadZXRead = New Thread(Sub() DailySales = TotalSales - LessVat - TotalDiscount)
            ThreadZXRead.Start()
            ThreadlistZXRead.Add(ThreadZXRead)
            For Each t In ThreadlistZXRead
                t.Join()
            Next

            RightDisplay1(sender, e, RECEIPTLINECOUNT, "DISCOUNT", NUMBERFORMAT(TotalDiscount), FontDefault, 205 + IfPrintSmall, 0)
            RECEIPTLINECOUNT += 10
            RightDisplay1(sender, e, RECEIPTLINECOUNT, "LESS VAT", NUMBERFORMAT(LessVat), FontDefault, 205 + IfPrintSmall, 0)
            RECEIPTLINECOUNT += 10
            RightDisplay1(sender, e, RECEIPTLINECOUNT, "SUPERCHARGE", NUMBERFORMAT(0), FontDefault, 205 + IfPrintSmall, 0)
            RECEIPTLINECOUNT += 10
            RightDisplay1(sender, e, RECEIPTLINECOUNT, "CORKAGE", NUMBERFORMAT(0), FontDefault, 205 + IfPrintSmall, 0)
            RECEIPTLINECOUNT += 10
            RightDisplay1(sender, e, RECEIPTLINECOUNT, "SERVICE CHARGE", NUMBERFORMAT(0), FontDefault, 205 + IfPrintSmall, 0)
            RECEIPTLINECOUNT += 10
            RightDisplay1(sender, e, RECEIPTLINECOUNT, "DELIVERY CHARGE", NUMBERFORMAT(0), FontDefault, 205 + IfPrintSmall, 0)
            RECEIPTLINECOUNT += 10
            RightDisplay1(sender, e, RECEIPTLINECOUNT, "TAKEOUT CHARGE", NUMBERFORMAT(0), FontDefault, 205 + IfPrintSmall, 0)
            RECEIPTLINECOUNT += 10
            RightDisplay1(sender, e, RECEIPTLINECOUNT, "ADD VAT", NUMBERFORMAT(0), FontDefault, 205 + IfPrintSmall, 0)
            RECEIPTLINECOUNT += 10
            RightDisplay1(sender, e, RECEIPTLINECOUNT, "EXCESS GC", NUMBERFORMAT(0), FontDefault, 205 + IfPrintSmall, 0)
            RECEIPTLINECOUNT += 10
            RightDisplay1(sender, e, RECEIPTLINECOUNT, "EXCESS CHK", NUMBERFORMAT(0), FontDefault, 205 + IfPrintSmall, 0)
            RECEIPTLINECOUNT += 10
            PrintSmallLine(sender, e, FontDefaultLine, RECEIPTLINECOUNT)
            RECEIPTLINECOUNT += 10
            SimpleTextDisplay(sender, e, "G. TOTAL", FontDefault, 0, RECEIPTLINECOUNT - 20)
            RightDisplay1(sender, e, RECEIPTLINECOUNT, SumOfColumnsToInt(DataGridViewSales, 2), Math.Round(TotalPercentage, 2) & "%", FontDefault, 60, 77 + IfPrintQty)
            RightDisplay1(sender, e, RECEIPTLINECOUNT, "", NUMBERFORMAT(DailySales), FontDefault, 60, 145 + IfPrintSmall)
            RECEIPTLINECOUNT += 10
            PrintSmallLine(sender, e, FontDefaultLine, RECEIPTLINECOUNT)
            RECEIPTLINECOUNT += 10
            CenterTextDisplay(sender, e, S_Zreading & Format(Now(), " hh:mm:ss tt"), FontDefault, RECEIPTLINECOUNT)


        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Reports: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Dim ColumnSpacing As Integer = 0
    Private Sub printreturns_printdoc(sender As Object, e As System.Drawing.Printing.PrintPageEventArgs) Handles printdocReturns.PrintPage
        Try
            Dim FontDefault As Font
            Dim FontDefaultBold As Font
            Dim FontDefaultLine As Font
            If My.Settings.PrintSize = "57mm" Then
                FontDefault = New Font("Tahoma", 5)
                FontDefaultBold = New Font("Tahoma", 5, FontStyle.Bold)
                FontDefaultLine = New Font("Tahoma", 6)
            Else
                FontDefault = New Font("Tahoma", 6)
                FontDefaultBold = New Font("Tahoma", 6, FontStyle.Bold)
                FontDefaultLine = New Font("Tahoma", 7)
            End If

            ReceiptHeaderOne(sender, e, False, "", False, False)
            RECEIPTLINECOUNT = 30
            PrintSmallLine(sender, e, FontDefaultLine, RECEIPTLINECOUNT)
            RECEIPTLINECOUNT += 30
            With DataGridViewReturns
                Dim FooterSpacing As Integer = 0
                If CheckBoxPRINTALL.Checked = False Then
                    RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "Return Item Logs : " & Format(DateTimePicker14.Value, "yyyy-MM-dd") & " - " & Format(DateTimePicker13.Value, "yyyy-MM-dd"), "", FontDefault, 20, 0)
                    RECEIPTLINECOUNT += 20
                    RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "Transaction Number: ", "", FontDefault, 20, 0)

                    RECEIPTLINECOUNT += 10
                    RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "Service Crew: ", "", FontDefault, 20, 0)
                    SimpleTextDisplay(sender, e, Space(40) & .SelectedRows(0).Cells(0).Value.ToString, FontDefault, 0, RECEIPTLINECOUNT - 30)
                    RECEIPTLINECOUNT += 10
                    RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "Date: ", "", FontDefault, 20, 0)
                    SimpleTextDisplay(sender, e, Space(40) & .SelectedRows(0).Cells(1).Value.ToString, FontDefault, 0, RECEIPTLINECOUNT - 30)
                    RECEIPTLINECOUNT += 10
                    RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "Reason:", "", FontDefault, 20, 0)
                    SimpleTextDisplay(sender, e, Space(40) & .SelectedRows(0).Cells(3).Value.ToString, FontDefault, 0, RECEIPTLINECOUNT - 30)
                    RECEIPTLINECOUNT += 10
                    RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, Space(5) & .SelectedRows(0).Cells(2).Value.ToString, "", FontDefault, 20, 0)
                    SimpleTextDisplay(sender, e, Space(40) & "TOTAL: " & .SelectedRows(0).Cells(4).Value.ToString, FontDefault, 0, RECEIPTLINECOUNT - 30)

                    PrintSmallLine(sender, e, FontDefaultLine, RECEIPTLINECOUNT)
                    RECEIPTLINECOUNT += 30
                    CenterTextDisplay(sender, e, S_Zreading & " " & Format(Now(), "HH:mm:ss"), FontDefault, RECEIPTLINECOUNT)
                    RECEIPTLINECOUNT += 10
                Else
                    For i As Integer = 0 To .Rows.Count - 1 Step +1
                        RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "Return Item Logs : " & Format(DateTimePicker14.Value, "yyyy-MM-dd") & " - " & Format(DateTimePicker13.Value, "yyyy-MM-dd"), "", FontDefault, 20, 0)
                        RECEIPTLINECOUNT += 20
                        RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "Transaction Number: ", "", FontDefault, 20, 0)
                        SimpleTextDisplay(sender, e, Space(40) & .Rows(i).Cells(0).Value.ToString, FontDefault, 0, RECEIPTLINECOUNT - 20)
                        RECEIPTLINECOUNT += 10
                        RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "Service Crew: ", "", FontDefault, 20, 0)
                        SimpleTextDisplay(sender, e, Space(40) & .Rows(i).Cells(1).Value.ToString, FontDefault, 0, RECEIPTLINECOUNT - 20)
                        RECEIPTLINECOUNT += 10
                        RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "Date: ", "", FontDefault, 20, 0)
                        SimpleTextDisplay(sender, e, Space(40) & .Rows(i).Cells(3).Value.ToString, FontDefault, 0, RECEIPTLINECOUNT - 20)
                        RECEIPTLINECOUNT += 10
                        RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "Reason:", "", FontDefault, 20, 0)
                        SimpleTextDisplay(sender, e, Space(40) & "TOTAL: " & .Rows(i).Cells(4).Value.ToString, FontDefault, 0, RECEIPTLINECOUNT - 20)
                        RECEIPTLINECOUNT += 10
                        RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, Space(5) & .Rows(i).Cells(2).Value.ToString, "", FontDefault, 20, 0)
                        RECEIPTLINECOUNT += 20

                    Next
                    PrintSmallLine(sender, e, FontDefaultLine, RECEIPTLINECOUNT - 20)
                    CenterTextDisplay(sender, e, S_Zreading & " " & Format(Now(), "HH:mm:ss"), FontDefault, RECEIPTLINECOUNT + 10)
                End If
            End With
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Reports: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub ToolStripButton9_Click(sender As Object, e As EventArgs) Handles ToolStripButton9.Click
        Try

            Dim TotalLines As Integer = 0
            Dim BodyLine As Integer = 150
            Dim CountHeaderLine As Integer = count("id", "loc_receipt WHERE type = 'Header' AND status = 1")
            Dim ProductLine As Integer = 0

            CountHeaderLine *= 10


            If DataGridViewReturns.Rows.Count > 0 Then

                If CheckBoxPRINTALL.Checked = False Then
                    TotalLines = CountHeaderLine + ProductLine + BodyLine
                    printdocReturns.DefaultPageSettings.PaperSize = New PaperSize("Custom", ReturnPrintSize(), TotalLines)
                    If S_Print_Returns = "YES" Then
                        printdocReturns.Print()
                    Else
                        PrintPreviewDialogReturns.Document = printdocReturns
                        PrintPreviewDialogReturns.ShowDialog()
                    End If
                Else
                    BodyLine = 70
                    For i As Integer = 0 To DataGridViewReturns.Rows.Count - 1 Step +1
                        ProductLine += 80
                    Next
                    TotalLines = CountHeaderLine + ProductLine + BodyLine
                    printdocReturns.DefaultPageSettings.PaperSize = New PaperSize("Custom", ReturnPrintSize(), TotalLines)
                    If S_Print_Returns = "YES" Then
                        printdocReturns.Print()
                    Else
                        PrintPreviewDialogReturns.Document = printdocReturns
                        PrintPreviewDialogReturns.ShowDialog()
                    End If
                End If
                AuditTrail.LogToAuditTral("Report", "Item Return: Generated Report, " & ClientCrewID, "Normal")

                ProductLine = 0
                ColumnSpacing = 0
            Else
                MsgBox("Select returned product first.")
            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Reports: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub ButtonSearchCrewSales_Click(sender As Object, e As EventArgs) Handles ButtonSearchCrewSales.Click
        Try
            If ComboBoxUserIDS.SelectedIndex = -1 Then
                MsgBox("Select crew id first")
            Else

                LoadCrewSales(True)
            End If
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub

    Private Sub ToolStripButton3_Click(sender As Object, e As EventArgs) Handles ToolStripButton3.Click
        Try
            Dim document As Pdf.PdfDocument = New Pdf.PdfDocument
            document.Info.Title = "Created with PDFsharp"
            Dim page As Pdf.PdfPage = document.Pages.Add
            Dim gfx As XGraphics = XGraphics.FromPdfPage(page)
            Dim font As XFont = New XFont("Verdana", 9, XFontStyle.Regular)
            Dim font1 As XFont = New XFont("Verdana", 9, XFontStyle.Bold)


            Dim list As List(Of String) = New List(Of String)

            For i As Integer = 0 To DataGridViewCustomReport.Rows.Count - 1 Step +1
                If Not list.Contains(DataGridViewCustomReport.Rows(i).Cells(1).Value) Then
                    list.Add(DataGridViewCustomReport.Rows(i).Cells(1).Value)
                End If
            Next

            Dim result As List(Of String) = list.Distinct().ToList
            Dim TotalNetSales As Double = 0
            Dim ConnectionLocal As MySqlConnection = LocalhostConn()

            For Each element As String In result
                'Console.WriteLine(element.ToString)
                Dim Query = "SELECT amountdue FROM loc_daily_transaction WHERE transaction_number = '" & element & "'"
                Dim Cmd As MySqlCommand = New MySqlCommand(Query, ConnectionLocal)
                Using reader As MySqlDataReader = Cmd.ExecuteReader
                    If reader.HasRows Then
                        While reader.Read
                            TotalNetSales += reader("amountdue")
                        End While
                    End If
                End Using
            Next

            If DataGridViewCustomReport.Rows.Count > 0 Then
                ' Create a new PDF document
                Dim NextPage As Integer = DataGridViewCustomReport.Rows.Count
                Dim PageRows As Integer = 50
                Dim TotalRowsPerPage As Integer = NextPage / PageRows

                If NextPage <= 50 Then
                    TotalRowsPerPage = 1
                Else
                    TotalRowsPerPage += 1
                End If
                Dim Kahitano As Integer = 1
                Dim GetDgvRowCount As Integer = 0

                For a = 1 To TotalRowsPerPage

                    If a <> Kahitano Then
                        page = document.AddPage
                        gfx = XGraphics.FromPdfPage(page)
                        gfx.DrawString("Date From - To: " & DateTimePicker17.Value.ToString & " | " & DateTimePicker18.Value.ToString, font, XBrushes.Black, 50, 50)
                        gfx.DrawString("Product Name: " & ToolStripComboBoxProducts.Text, font, XBrushes.Black, 50, 61)
                        gfx.DrawString("Tax Type: " & ToolStripComboBoxTaxType.Text, font, XBrushes.Black, 50, 72)
                        gfx.DrawString("Transaction Type: " & ToolStripComboBoxTransactionType.Text, font, XBrushes.Black, 50, 83)
                        gfx.DrawString("Discount Type: " & ToolStripComboBoxDiscType.Text, font, XBrushes.Black, 50, 94)
                        gfx.DrawString("Product Name", font1, XBrushes.Black, 50, 103 + 10)
                        gfx.DrawString("Transaction Number", font1, XBrushes.Black, 130, 103 + 10)
                        gfx.DrawString("Quantity", font1, XBrushes.Black, 240, 103 + 10)
                        gfx.DrawString("Price", font1, XBrushes.Black, 290, 103 + 10)
                        gfx.DrawString("Total", font1, XBrushes.Black, 330, 103 + 10)
                        gfx.DrawString("Date Created", font1, XBrushes.Black, 370, 103 + 10)

                        Dim RowCount As Integer = 10
                        Dim CountPage As Integer = 0
                        With DataGridViewCustomReport

                            For i As Integer = GetDgvRowCount To .Rows.Count - 1 Step +1
                                If CountPage < PageRows Then
                                    gfx.DrawString(.Rows(i).Cells(6).Value, font, XBrushes.Black, 50, 123 + RowCount)
                                    gfx.DrawString(.Rows(i).Cells(1).Value, font, XBrushes.Black, 130, 123 + RowCount)
                                    gfx.DrawString(.Rows(i).Cells(2).Value, font, XBrushes.Black, 240, 123 + RowCount)
                                    gfx.DrawString(.Rows(i).Cells(3).Value, font, XBrushes.Black, 290, 123 + RowCount)
                                    gfx.DrawString(.Rows(i).Cells(4).Value, font, XBrushes.Black, 330, 123 + RowCount)
                                    gfx.DrawString(.Rows(i).Cells(5).Value, font, XBrushes.Black, 370, 123 + RowCount)
                                    RowCount += 10
                                    CountPage += 1
                                    GetDgvRowCount += 1
                                Else
                                    Exit For
                                End If

                            Next
                        End With

                        gfx.DrawString("Total Items: " & SumOfColumnsToInt(DataGridViewCustomReport, 2), font, XBrushes.Black, 50, 133 + RowCount)
                        gfx.DrawString("Total Discount: " & NUMBERFORMAT(TotalDiscountCustomReports), font, XBrushes.Black, 50, 143 + RowCount)
                        gfx.DrawString("Net Sales: " & NUMBERFORMAT(TotalNetSales), font, XBrushes.Black, 50, 153 + RowCount)
                        gfx.DrawString("Vatable Sales: " & CustomReportVat, font, XBrushes.Black, 50, 163 + RowCount)
                        gfx.DrawString("Less Vat: " & CustomReportLessVat, font, XBrushes.Black, 50, 173 + RowCount)
                        gfx.DrawString("Date Generated: " & FullDate24HR(), font, XBrushes.Black, 50, 183 + RowCount)

                        Kahitano += 1
                    Else
                        gfx.DrawString("Date From - To: " & DateTimePicker17.Value.ToString & " | " & DateTimePicker18.Value.ToString, font, XBrushes.Black, 50, 50)
                        gfx.DrawString("Product Name: " & ToolStripComboBoxProducts.Text, font, XBrushes.Black, 50, 61)
                        gfx.DrawString("Tax Type: " & ToolStripComboBoxTaxType.Text, font, XBrushes.Black, 50, 72)
                        gfx.DrawString("Transaction Type: " & ToolStripComboBoxTransactionType.Text, font, XBrushes.Black, 50, 83)
                        gfx.DrawString("Discount Type: " & ToolStripComboBoxDiscType.Text, font, XBrushes.Black, 50, 94)
                        gfx.DrawString("Product Name", font1, XBrushes.Black, 50, 103 + 10)
                        gfx.DrawString("Transaction Number", font1, XBrushes.Black, 130, 103 + 10)
                        gfx.DrawString("Quantity", font1, XBrushes.Black, 240, 103 + 10)
                        gfx.DrawString("Price", font1, XBrushes.Black, 290, 103 + 10)
                        gfx.DrawString("Total", font1, XBrushes.Black, 330, 103 + 10)
                        gfx.DrawString("Date Created", font1, XBrushes.Black, 370, 103 + 10)

                        Dim RowCount As Integer = 10
                        Dim CountPage As Integer = 0
                        With DataGridViewCustomReport

                            For i As Integer = 0 To .Rows.Count - 1 Step +1

                                If i < PageRows Then
                                    gfx.DrawString(.Rows(i).Cells(6).Value, font, XBrushes.Black, 50, 123 + RowCount)
                                    gfx.DrawString(.Rows(i).Cells(1).Value, font, XBrushes.Black, 130, 123 + RowCount)
                                    gfx.DrawString(.Rows(i).Cells(2).Value, font, XBrushes.Black, 240, 123 + RowCount)
                                    gfx.DrawString(.Rows(i).Cells(3).Value, font, XBrushes.Black, 290, 123 + RowCount)
                                    gfx.DrawString(.Rows(i).Cells(4).Value, font, XBrushes.Black, 330, 123 + RowCount)
                                    gfx.DrawString(.Rows(i).Cells(5).Value, font, XBrushes.Black, 370, 123 + RowCount)
                                    RowCount += 10
                                    CountPage += 1
                                    GetDgvRowCount += 1
                                Else
                                    Exit For
                                End If
                            Next
                        End With

                        gfx.DrawString("Total Items: " & SumOfColumnsToInt(DataGridViewCustomReport, 2), font, XBrushes.Black, 50, 133 + RowCount)
                        gfx.DrawString("Total Discount: " & NUMBERFORMAT(TotalDiscountCustomReports), font, XBrushes.Black, 50, 143 + RowCount)
                        gfx.DrawString("Net Sales: " & NUMBERFORMAT(TotalNetSales), font, XBrushes.Black, 50, 153 + RowCount)
                        gfx.DrawString("Vatable Sales: " & CustomReportVat, font, XBrushes.Black, 50, 163 + RowCount)
                        gfx.DrawString("Less Vat: " & CustomReportLessVat, font, XBrushes.Black, 50, 173 + RowCount)
                        gfx.DrawString("Date Generated: " & FullDate24HR(), font, XBrushes.Black, 50, 183 + RowCount)
                    End If
                Next
                AuditTrail.LogToAuditTral("Report", "Reports/Custom Report: Generated Report, " & ClientCrewID, "Normal")

                Dim filename = My.Computer.FileSystem.SpecialDirectories.Desktop & "\Custom Report-" & FullDateFormatForSaving() & ".pdf"
                document.Save(filename)

                ' ...and start a viewer.
                Process.Start(filename)





                '    page = document.AddPage
                '    gfx = XGraphics.FromPdfPage(page)

                '    gfx.DrawString("Date From - To: " & DateTimePicker17.Value.ToString & " | " & DateTimePicker18.Value.ToString, font, XBrushes.Black, 50, 50)

                '    ' Save the document...

            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Reports: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs)
        Try


            'Dim xlApp As Excel.Application = New Microsoft.Office.Interop.Excel.Application()
            'Dim raXL As Excel.Range
            'Dim CountCell As Integer = 0

            'CountCell = DataGridViewCustomReport.Rows.Count + 1
            'If xlApp Is Nothing Then
            '    MessageBox.Show("Excel is not properly installed!!")
            '    Exit Sub
            'End If


            'Dim xlWorkBook As Excel.Workbook
            'Dim xlWorkSheet As Excel.Worksheet
            'Dim misValue As Object = System.Reflection.Missing.Value

            'xlWorkBook = xlApp.Workbooks.Add(misValue)
            'xlWorkSheet = xlWorkBook.Sheets("sheet1")


            'xlWorkSheet.Cells(1, 1).Value = "Product Name"
            'xlWorkSheet.Cells(1, 2).Value = "Transaction Number"
            'xlWorkSheet.Cells(1, 3).Value = "Quantity"
            'xlWorkSheet.Cells(1, 4).Value = "Price"
            'xlWorkSheet.Cells(1, 5).Value = "Total"
            'xlWorkSheet.Cells(1, 6).Value = "Date"

            'raXL = xlWorkSheet.Range("B1:B" & CountCell)
            'raXL.NumberFormat = "@"
            'raXL = xlWorkSheet.Range("C1:C" & CountCell)
            'raXL.NumberFormat = "@"
            'raXL = xlWorkSheet.Range("D1:D" & CountCell)
            'raXL.NumberFormat = "@"
            'raXL = xlWorkSheet.Range("E1:E" & CountCell)
            'raXL.NumberFormat = "@"

            'raXL = xlWorkSheet.Range("A1:F1")
            'raXL.Font.Bold = True

            'Dim RCount As Integer = 3

            'With DataGridViewCustomReport
            '    For i = 1 To .Rows.Count
            '        xlWorkSheet.Cells(i + 1, 1).Value = .Rows(i - 1).Cells(0).Value
            '        xlWorkSheet.Cells(i + 1, 2).Value = .Rows(i - 1).Cells(1).Value
            '        xlWorkSheet.Cells(i + 1, 3).Value = .Rows(i - 1).Cells(2).Value
            '        xlWorkSheet.Cells(i + 1, 4).Value = .Rows(i - 1).Cells(3).Value
            '        xlWorkSheet.Cells(i + 1, 5).Value = .Rows(i - 1).Cells(4).Value
            '        xlWorkSheet.Cells(i + 1, 6).Value = .Rows(i - 1).Cells(5).Value
            '        RCount += 1
            '    Next
            'End With

            'xlWorkSheet.Cells(RCount, 1).Value = "Total Items"
            'xlWorkSheet.Cells(RCount, 2).Value = DataGridViewCustomReport.Rows.Count
            'raXL = xlWorkSheet.Range("A" & RCount)
            'raXL.Font.Bold = True
            'RCount += 1
            'xlWorkSheet.Cells(RCount, 1).Value = "Total Sales"
            'xlWorkSheet.Cells(RCount, 2).Value = SumOfColumnsToDecimal(DataGridViewCustomReport, 3)
            'raXL = xlWorkSheet.Range("A" & RCount)
            'raXL.Font.Bold = True
            'RCount += 1
            'xlWorkSheet.Cells(RCount, 1).Value = "VAT"
            'xlWorkSheet.Cells(RCount, 2).Value = CustomReportVat
            'raXL = xlWorkSheet.Range("A" & RCount)
            'raXL.Font.Bold = True
            'RCount += 1
            'xlWorkSheet.Cells(RCount, 1).Value = "LESS VAT"
            'xlWorkSheet.Cells(RCount, 2).Value = CustomReportLessVat
            'raXL = xlWorkSheet.Range("A" & RCount)
            'raXL.Font.Bold = True
            'RCount += 1
            'xlWorkSheet.Cells(RCount, 1).Value = "Date Generated"
            'xlWorkSheet.Cells(RCount, 2).Value = FullDate24HR()
            'raXL = xlWorkSheet.Range("A" & RCount)
            'raXL.Font.Bold = True
            'RCount += 1


            'raXL = xlWorkSheet.Range("A1", "F1")
            'raXL.EntireColumn.AutoFit()

            'Dim Path = My.Computer.FileSystem.SpecialDirectories.Desktop & "\Custom Report-" & FullDateFormatForSaving() & ".xls"

            'xlWorkBook.SaveAs(Path, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue,
            ' Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue)
            'xlWorkBook.Close(True, misValue, misValue)
            'xlApp.Quit()

            'releaseObject(xlWorkSheet)
            'releaseObject(xlWorkBook)
            'releaseObject(xlApp)

            'MessageBox.Show("Excel file created , you can find the file " & Path)
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub releaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try
    End Sub


    'Dim ThreadEJournal As Thread
    'Dim ThreadListEJournal As List(Of Thread) = New List(Of Thread)
    'Private Sub BackgroundWorkerEJournal_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorkerEJournal.DoWork
    '    Try
    '        For i = 0 To 100
    '            BackgroundWorkerEJournal.ReportProgress(i)
    '            Thread.Sleep(20)
    '            If i = 0 Then
    '                ToolStripStatusLabel1.Text = "Loading please wait"
    '                ThreadEJournal = New Thread(Sub() GenerateTxtFile())
    '                ThreadEJournal.Start()
    '                ThreadListEJournal.Add(ThreadEJournal)
    '            End If
    '        Next
    '        For Each t In ThreadListEJournal
    '            t.Join()
    '            If (BackgroundWorkerEJournal.CancellationPending) Then
    '                e.Cancel = True
    '                Exit For
    '            End If
    '        Next
    '    Catch ex As Exception
    '        SendErrorReport(ex.ToString)
    '    End Try
    'End Sub

    'Private Sub BackgroundWorkerEJournal_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorkerEJournal.ProgressChanged
    '    Try
    '        ToolStripProgressBar1.Value = e.ProgressPercentage
    '    Catch ex As Exception
    '        SendErrorReport(ex.ToString)
    '    End Try
    'End Sub

    'Private Sub BackgroundWorkerEJournal_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorkerEJournal.RunWorkerCompleted
    '    Try
    '        DisableFormClose = False
    '        ToolStripButton7.Enabled = True
    '        ToolStripButton4.Enabled = True
    '    Catch ex As Exception
    '        SendErrorReport(ex.ToString)
    '    End Try
    'End Sub






    'Private Sub ToolStripButton10_Click(sender As Object, e As EventArgs) Handles ButtonZread.Click
    '    Try
    '        Dim msg = MessageBox.Show("Are you sure you want to generate Z-READ ? Press Yes to continue or No to cancel", "Z-reading", MessageBoxButtons.YesNo, MessageBoxIcon.Information)

    '        If msg = DialogResult.Yes Then
    '            My.Settings.zcounter += 1
    '            Dim ConnectionLocal As MySqlConnection = LocalhostConn()
    '            'Fill dgv inv
    '            GLOBAL_SELECT_ALL_FUNCTION("loc_pos_inventory", "*", DataGridViewZreadInventory)
    '            'Update inventory
    '            MainInventorySub()
    '            'Fill again
    '            GLOBAL_SELECT_ALL_FUNCTION("loc_pos_inventory", "*", DataGridViewZreadInventory)
    '            'Print zread
    '            XreadOrZread = "Z-READ"

    '            printdocXread.DefaultPageSettings.PaperSize = New PaperSize("Custom", ReturnPrintSize(), 1000)

    '            If S_Print_XZRead = "YES" Then
    '                printdocXread.Print()
    '            Else
    '                PrintPreviewDialogXread.Document = printdocXread
    '                PrintPreviewDialogXread.ShowDialog()
    '            End If

    '            GetOldGrandtotal()
    '            'Update Zread

    '            S_Zreading = Format(DateAdd("d", 1, S_Zreading), "yyyy-MM-dd")
    '            sql = "UPDATE loc_settings SET S_Zreading = '" & S_Zreading & "'"
    '            cmd = New MySqlCommand(sql, ConnectionLocal)
    '            cmd.ExecuteNonQuery()
    '            cmd.Dispose()

    '            sql = "UPDATE loc_pos_inventory SET zreading = '" & S_Zreading & "'"
    '            LocalhostConn.Close()
    '            cmd = New MySqlCommand(sql, ConnectionLocal)
    '            cmd.ExecuteNonQuery()

    '            cmd.Dispose()
    '            ConnectionLocal.Close()
    '            'Insert to local zread inv
    '            XZreadingInventory(S_Zreading)


    '            If S_Zreading = Format(Now().AddDays(1), "yyyy-MM-dd") Then
    '                ButtonZread.Enabled = False
    '                ButtonZreadAdmin.Enabled = False
    '            End If
    '            Button7.PerformClick()

    '            SystemLogDesc = "Z-Reading : " & FullDate24HR() & " Crew : " & returnfullname(ClientCrewID)
    '            SystemLogType = "Z-READ"
    '            GLOBAL_SYSTEM_LOGS(SystemLogType, SystemLogDesc)

    '            'S_OLDGRANDTOTAL += GROSSSALE


    '        End If
    '    Catch ex As Exception
    '        SendErrorReport(ex.ToString)
    '    End Try
    'End Sub

    Private Sub ToolStripButton11_Click(sender As Object, e As EventArgs)

    End Sub
    Private Sub PrintDocument2_PrintPage(sender As Object, e As PrintPageEventArgs) Handles printdocZRead.PrintPage
        Try

            ReceiptHeaderOne(sender, e, False, "", False, False)
            ZXBody(sender, e)
            ZFooter(sender, e, ReprintZRead, Format(DateTimePickerZXreading.Value, "yyyy-MM-dd"), Format(DateTimePickerZXreadingTo.Value, "yyyy-MM-dd"))
            AuditTrail.LogToAuditTral("System", "Reports: Generated " & XREADORZREAD & ", " & ClientCrewID, "Normal")

        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Reports: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Dim ReprintZRead As Boolean = False
    Private Sub FillZreadData(ZReadDateFilter, Fromdate, ToDate)
        Try
            ResetZReadingVariables()

            ThreadZXRead = New Thread(Sub() ZXBegSINo = ReturnRowDouble("si_number", "loc_daily_transaction WHERE " & ZReadDateFilter & " LIMIT 1"))
            ThreadZXRead.Start()
            ThreadlistZXRead.Add(ThreadZXRead)
            For Each t In ThreadlistZXRead
                t.Join()
            Next

            Dim BegSi As Integer = Val(ZXBegSINo)
            ZXBegSINo = BegSi.ToString(S_SIFormat)

            ThreadZXRead = New Thread(Sub() ZXEndSINo = ReturnRowDouble("si_number", "loc_daily_transaction WHERE " & ZReadDateFilter & " ORDER by transaction_id DESC LIMIT 1"))
            ThreadZXRead.Start()
            ThreadlistZXRead.Add(ThreadZXRead)
            For Each t In ThreadlistZXRead
                t.Join()
            Next

            Dim EndSi As Integer = Val(ZXEndSINo)
            ZXEndSINo = EndSi.ToString(S_SIFormat)

            ThreadZXRead = New Thread(Sub() ZXBegTransNo = returnselect("transaction_number", "loc_daily_transaction WHERE " & ZReadDateFilter & " LIMIT 1"))
            ThreadZXRead.Start()
            ThreadlistZXRead.Add(ThreadZXRead)
            For Each t In ThreadlistZXRead
                t.Join()
            Next

            ThreadZXRead = New Thread(Sub() ZXEndTransNo = returnselect("transaction_number", "loc_daily_transaction WHERE " & ZReadDateFilter & " ORDER by transaction_id DESC LIMIT 1"))
            ThreadZXRead.Start()
            ThreadlistZXRead.Add(ThreadZXRead)
            For Each t In ThreadlistZXRead
                t.Join()
            Next

            ThreadZXRead = New Thread(Sub() ZXBegBalance = returnselect("log_description", "loc_system_logs WHERE log_type IN ('BG-1','BG-2','BG-3','BG-4') AND " & ZReadDateFilter & " ORDER by log_date_time DESC LIMIT 1"))
            ThreadZXRead.Start()
            ThreadlistZXRead.Add(ThreadZXRead)
            For Each t In ThreadlistZXRead
                t.Join()
            Next

            ZXBegBalance = If(ZXBegBalance = "", 0, ZXBegBalance)


            If S_ZeroRated = "0" Then
                ThreadZXRead = New Thread(Sub() ZXLessVat = sum("lessvat", "loc_daily_transaction WHERE " & ZReadDateFilter & " AND active = 1"))
                ThreadZXRead.Start()
                ThreadlistZXRead.Add(ThreadZXRead)
                For Each t In ThreadlistZXRead
                    t.Join()
                Next
            Else
                'ZXLessVat = 0
                ThreadZXRead = New Thread(Sub() ZXLessVatOthers = sum("lessvat", "loc_daily_transaction WHERE " & ZReadDateFilter & " AND active = 1"))
                ThreadZXRead.Start()
                ThreadlistZXRead.Add(ThreadZXRead)
                For Each t In ThreadlistZXRead
                    t.Join()
                Next

            End If

            If S_ZeroRated = "0" Then
                ThreadZXRead = New Thread(Sub() ZXGross = sum("grosssales", "loc_daily_transaction WHERE " & ZReadDateFilter & " AND active = 1"))
                ThreadZXRead.Start()
                ThreadlistZXRead.Add(ThreadZXRead)
                For Each t In ThreadlistZXRead
                    t.Join()
                Next
            Else
                ThreadZXRead = New Thread(Sub() ZXGross = sum("grosssales", "loc_daily_transaction WHERE " & ZReadDateFilter & " AND active = 1"))
                ThreadZXRead.Start()
                ThreadlistZXRead.Add(ThreadZXRead)
                For Each t In ThreadlistZXRead
                    t.Join()
                Next
                ZXGross += ZXLessVatOthers
            End If


            'ZXLessVatDiplomat N/A
            'ZXLessVatOthers N/A
            'ZXAdditionalVat N/A

            ThreadZXRead = New Thread(Sub() ZXVatAmount = sum("vatpercentage", "loc_daily_transaction WHERE " & ZReadDateFilter & " AND active = 1"))
            ThreadZXRead.Start()
            ThreadlistZXRead.Add(ThreadZXRead)
            For Each t In ThreadlistZXRead
                t.Join()
            Next

            'ZXLocalGovTax

            ThreadZXRead = New Thread(Sub() ZXVatableSales = sum("vatablesales", "loc_daily_transaction WHERE " & ZReadDateFilter & " AND active = 1"))
            ThreadZXRead.Start()
            ThreadlistZXRead.Add(ThreadZXRead)
            For Each t In ThreadlistZXRead
                t.Join()
            Next

            ThreadZXRead = New Thread(Sub() ZXZeroRatedSales = sum("zeroratedsales", "loc_daily_transaction WHERE " & ZReadDateFilter & " AND active = 1"))
            ThreadZXRead.Start()
            ThreadlistZXRead.Add(ThreadZXRead)
            For Each t In ThreadlistZXRead
                t.Join()
            Next

            ThreadZXRead = New Thread(Sub() ZXTotalDiscounts = sum("coupon_total", "loc_coupon_data WHERE " & ZReadDateFilter & " AND status = 1"))
            ThreadZXRead.Start()
            ThreadlistZXRead.Add(ThreadZXRead)
            For Each t In ThreadlistZXRead
                t.Join()
            Next

            ThreadZXRead = New Thread(Sub() ZXLessDiscVE = sum("coupon_total", "loc_coupon_data WHERE " & ZReadDateFilter & " AND coupon_type = 'Percentage(w/o vat)' AND status = 1"))
            ThreadZXRead.Start()
            ThreadlistZXRead.Add(ThreadZXRead)
            For Each t In ThreadlistZXRead
                t.Join()
            Next


            If S_ZeroRated = "0" Then
                ThreadZXRead = New Thread(Sub() ZXDailySales = ZXGross - ZXLessVat - ZXLessDiscVE)
                'ThreadZXRead = New Thread(Sub() ZXDailySales = ZXGross - ZXLessVat - ZXTotalDiscounts)
                ThreadZXRead.Start()
                ThreadlistZXRead.Add(ThreadZXRead)
                For Each t In ThreadlistZXRead
                    t.Join()
                Next
            Else
                ThreadZXRead = New Thread(Sub() ZXDailySales = ZXGross - ZXLessDiscVE - ZXLessVatOthers)
                'ThreadZXRead = New Thread(Sub() ZXDailySales = ZXGross - ZXLessVat - ZXTotalDiscounts)
                ThreadZXRead.Start()
                ThreadlistZXRead.Add(ThreadZXRead)
                For Each t In ThreadlistZXRead
                    t.Join()
                Next
            End If

            Console.WriteLine(ZXDailySales)



            ThreadZXRead = New Thread(Sub() ZXTotalExpenses = sum("total_amount", "loc_expense_list WHERE " & ZReadDateFilter & " AND active = 1"))
            ThreadZXRead.Start()
            ThreadlistZXRead.Add(ThreadZXRead)
            For Each t In ThreadlistZXRead
                t.Join()
            Next

            If S_ZeroRated = "0" Then
                ThreadZXRead = New Thread(Sub() ZXCashTotal = sum("amountdue", "loc_daily_transaction WHERE active = 1 AND " & ZReadDateFilter & " "))
                ThreadZXRead.Start()
                ThreadlistZXRead.Add(ThreadZXRead)
                For Each t In ThreadlistZXRead
                    t.Join()
                Next
            Else
                ThreadZXRead = New Thread(Sub() ZXCashTotal = ZXLessVatOthers + sum("amountdue", "loc_daily_transaction WHERE active = 1 AND " & ZReadDateFilter & " "))
                ThreadZXRead.Start()
                ThreadlistZXRead.Add(ThreadZXRead)
                For Each t In ThreadlistZXRead
                    t.Join()
                Next
            End If



            ThreadZXRead = New Thread(Sub() ZXGiftCard = sum("coupon_total", "loc_coupon_data WHERE " & ZReadDateFilter & " AND coupon_type = 'Fix-1' "))
            ThreadZXRead.Start()
            ThreadlistZXRead.Add(ThreadZXRead)
            For Each t In ThreadlistZXRead
                t.Join()
            Next

            ThreadZXRead = New Thread(Sub() ZXNetSales = sum("amountdue", "loc_daily_transaction WHERE active = 1 AND " & ZReadDateFilter & " ") + ZXGiftCard)
            ThreadZXRead.Start()
            ThreadlistZXRead.Add(ThreadZXRead)
            For Each t In ThreadlistZXRead
                t.Join()
            Next

            ThreadZXRead = New Thread(Sub() ZXCashlessTotal = sum("amountdue", "loc_daily_transaction WHERE active IN (1,3) AND " & ZReadDateFilter & " AND transaction_type NOT IN ('Walk-in' , 'Complimentary Expenses')"))
            ThreadZXRead.Start()
            ThreadlistZXRead.Add(ThreadZXRead)
            For Each t In ThreadlistZXRead
                t.Join()
            Next

            ThreadZXRead = New Thread(Sub() ZXGcash = sum("amountdue", "loc_daily_transaction WHERE active = 1 AND " & ZReadDateFilter & " AND transaction_type = 'Gcash' "))
            ThreadZXRead.Start()
            ThreadlistZXRead.Add(ThreadZXRead)
            For Each t In ThreadlistZXRead
                t.Join()
            Next

            ThreadZXRead = New Thread(Sub() ZXPaymaya = sum("amountdue", "loc_daily_transaction WHERE active = 1 AND " & ZReadDateFilter & " AND transaction_type = 'Paymaya' "))
            ThreadZXRead.Start()
            ThreadlistZXRead.Add(ThreadZXRead)
            For Each t In ThreadlistZXRead
                t.Join()
            Next

            ThreadZXRead = New Thread(Sub() ZXGrabFood = sum("amountdue", "loc_daily_transaction WHERE active = 1 AND " & ZReadDateFilter & " AND transaction_type = 'Grab' "))
            ThreadZXRead.Start()
            ThreadlistZXRead.Add(ThreadZXRead)
            For Each t In ThreadlistZXRead
                t.Join()
            Next

            ThreadZXRead = New Thread(Sub() ZXFoodPanda = sum("amountdue", "loc_daily_transaction WHERE active = 1 AND " & ZReadDateFilter & " AND transaction_type = 'Food Panda' "))
            ThreadZXRead.Start()
            ThreadlistZXRead.Add(ThreadZXRead)
            For Each t In ThreadlistZXRead
                t.Join()
            Next

            ThreadZXRead = New Thread(Sub() ZXShopeePay = sum("amountdue", "loc_daily_transaction WHERE active = 1 AND " & ZReadDateFilter & " AND transaction_type = 'Shopee' "))
            ThreadZXRead.Start()
            ThreadlistZXRead.Add(ThreadZXRead)
            For Each t In ThreadlistZXRead
                t.Join()
            Next

            ThreadZXRead = New Thread(Sub() ZXRepExpense = sum("amountdue", "loc_daily_transaction WHERE active = 3 AND " & ZReadDateFilter & " AND transaction_type = 'Complimentary Expenses' "))
            ThreadZXRead.Start()
            ThreadlistZXRead.Add(ThreadZXRead)
            For Each t In ThreadlistZXRead
                t.Join()
            Next

            ThreadZXRead = New Thread(Sub() ZXCashlessOthers = sum("amountdue", "loc_daily_transaction WHERE active = 3 AND " & ZReadDateFilter & " AND transaction_type = 'Others' "))
            ThreadZXRead.Start()
            ThreadlistZXRead.Add(ThreadZXRead)
            For Each t In ThreadlistZXRead
                t.Join()
            Next


            'ZXCreditCard  N/A
            'ZXDebitCard  N/A
            'ZXMiscCheques  N/A



            ThreadZXRead = New Thread(Sub() ZXGiftCardSum = sum("gc_value", "loc_coupon_data WHERE " & ZReadDateFilter & " AND coupon_type = 'Fix-1'"))
            ThreadZXRead.Start()
            ThreadlistZXRead.Add(ThreadZXRead)
            For Each t In ThreadlistZXRead
                t.Join()
            Next
            '
            'ZXAR N/A
            'ZXCardOthers N/A

            ThreadZXRead = New Thread(Sub() ZXDeposits = sum("amount", "loc_deposit WHERE date(transaction_date) >= '" & S_Zreading & "' AND date(transaction_date) <= '" & S_Zreading & "' "))
            ThreadZXRead.Start()
            ThreadlistZXRead.Add(ThreadZXRead)
            For Each t In ThreadlistZXRead
                t.Join()
            Next


            'ThreadZXRead = New Thread(Sub() ZXCashInDrawer = sum("amountdue", "loc_daily_transaction WHERE active = 1 AND " & ZReadDateFilter & " AND transaction_type IN ('Walk-in','Registered')") + Double.Parse(ZXBegBalance) - ZXTotalExpenses - ZXDeposits)
            'ThreadZXRead.Start()
            'ThreadlistZXRead.Add(ThreadZXRead)
            'For Each t In ThreadlistZXRead
            '    t.Join()
            'Nextf
            If S_ZeroRated = "0" Then
                ZXCashInDrawer = ZXGross - ZXCashlessTotal - ZXLessDiscVE - ZXLessVat - ZXTotalExpenses - ZXDeposits + Double.Parse(ZXBegBalance)
            Else
                ZXCashInDrawer = ZXGross - ZXCashlessTotal - ZXLessDiscVE - ZXTotalExpenses - ZXLessVatOthers - ZXDeposits + Double.Parse(ZXBegBalance)
            End If


            ThreadZXRead = New Thread(Sub() ZXSeniorCitizen = sum("coupon_total", "loc_coupon_data WHERE coupon_name = 'Senior Discount 20%' AND " & ZReadDateFilter & " AND status = '1' "))
            ThreadZXRead.Start()
            ThreadlistZXRead.Add(ThreadZXRead)
            For Each t In ThreadlistZXRead
                t.Join()
            Next

            ThreadZXRead = New Thread(Sub() ZXPWD = sum("coupon_total", "loc_coupon_data WHERE coupon_name = 'PWD Discount 20%' AND " & ZReadDateFilter & " AND status = '1' "))
            ThreadZXRead.Start()
            ThreadlistZXRead.Add(ThreadZXRead)
            For Each t In ThreadlistZXRead
                t.Join()
            Next

            ThreadZXRead = New Thread(Sub() ZXAthlete = sum("coupon_total", "loc_coupon_data WHERE coupon_name = 'Sports Discount 20%' AND " & ZReadDateFilter & " AND status = '1' "))
            ThreadZXRead.Start()
            ThreadlistZXRead.Add(ThreadZXRead)
            For Each t In ThreadlistZXRead
                t.Join()
            Next

            ThreadZXRead = New Thread(Sub() ZXSingleParent = sum("coupon_total", "loc_coupon_data WHERE coupon_name = 'Single Parent 20%' AND " & ZReadDateFilter & " AND status = '1' "))
            ThreadZXRead.Start()
            ThreadlistZXRead.Add(ThreadZXRead)
            For Each t In ThreadlistZXRead
                t.Join()
            Next

            ThreadZXRead = New Thread(Sub() ZXReturnsExchange = sum("quantity", "loc_daily_transaction_details WHERE active = 2 AND " & ZReadDateFilter & " "))
            ThreadZXRead.Start()
            ThreadlistZXRead.Add(ThreadZXRead)
            For Each t In ThreadlistZXRead
                t.Join()
            Next

            ZXItemVoidEC = ZXReturnsExchange
            ZXTransactionVoid = ZXReturnsExchange
            ZXTransactionCancel = ZXReturnsExchange

            'ZXTakeOutCharge N/A
            'ZXDeliveryCharge N/A

            ThreadZXRead = New Thread(Sub() ZXReturnsRefund = sum("total", "loc_daily_transaction_details WHERE active = 2 AND " & ZReadDateFilter & " "))
            ThreadZXRead.Start()
            ThreadlistZXRead.Add(ThreadZXRead)
            For Each t In ThreadlistZXRead
                t.Join()
            Next

            ThreadZXRead = New Thread(Sub() ZXTotalQTYSold = sum("quantity", "loc_daily_transaction_details WHERE " & ZReadDateFilter & " AND active = 1"))
            ThreadZXRead.Start()
            ThreadlistZXRead.Add(ThreadZXRead)
            For Each t In ThreadlistZXRead
                t.Join()
            Next

            ThreadZXRead = New Thread(Sub() ZXTotalTransactionCount = count("transaction_id", "loc_daily_transaction WHERE " & ZReadDateFilter & " "))
            ThreadZXRead.Start()
            ThreadlistZXRead.Add(ThreadZXRead)
            For Each t In ThreadlistZXRead
                t.Join()
            Next

            ZXTotalGuess = ZXTotalTransactionCount
            ZXCurrentTotalSales = ZXNetSales
            ZXOldGrandTotalSales = S_OLDGRANDTOTAL

            ThreadZXRead = New Thread(Sub() ZXNewGrandtotalSales = sum("amountdue", "loc_daily_transaction WHERE active = 1 AND " & ZReadDateFilter & "") + ZXGiftCard)
            ThreadZXRead.Start()
            ThreadlistZXRead.Add(ThreadZXRead)
            For Each t In ThreadlistZXRead
                t.Join()
            Next

            If XREADORZREAD = "Z-READ" Then
                Dim ResetCounter = 0
                Dim ConnectionLocal As MySqlConnection = LocalhostConn()
                Dim Query = " SELECT counter_value FROM tbcountertable WHERE counter_id = 1"
                Dim Cmd As MySqlCommand = New MySqlCommand(Query, ConnectionLocal)
                Using reader As MySqlDataReader = Cmd.ExecuteReader
                    If reader.HasRows Then
                        While reader.Read
                            ResetCounter = reader("counter_value")
                        End While
                    End If
                End Using
                ZXResetCounter = ResetCounter
                ZXZreadCounter = My.Settings.zcounter
            Else
                ZXCashier = returnfullname(ClientCrewID)
            End If

            ThreadZXRead = New Thread(Sub() ZXSimplyPerfect = sum("quantity", "loc_daily_transaction_details WHERE " & ZReadDateFilter & " AND product_category = 'Simply Perfect'"))
            ThreadZXRead.Start()
            ThreadlistZXRead.Add(ThreadZXRead)
            For Each t In ThreadlistZXRead
                t.Join()
            Next

            ThreadZXRead = New Thread(Sub() ZXPerfectCombination = sum("quantity", "loc_daily_transaction_details WHERE " & ZReadDateFilter & " AND product_category = 'Perfect Combination'"))
            ThreadZXRead.Start()
            ThreadlistZXRead.Add(ThreadZXRead)
            For Each t In ThreadlistZXRead
                t.Join()
            Next

            ThreadZXRead = New Thread(Sub() ZXSavoury = sum("quantity", "loc_daily_transaction_details WHERE " & ZReadDateFilter & " AND product_category = 'Savory'"))
            ThreadZXRead.Start()
            ThreadlistZXRead.Add(ThreadZXRead)
            For Each t In ThreadlistZXRead
                t.Join()
            Next

            ThreadZXRead = New Thread(Sub() ZXCombo = sum("quantity", "loc_daily_transaction_details WHERE " & ZReadDateFilter & " AND product_category = 'Combo'"))
            ThreadZXRead.Start()
            ThreadlistZXRead.Add(ThreadZXRead)
            For Each t In ThreadlistZXRead
                t.Join()
            Next

            ThreadZXRead = New Thread(Sub() ZXFamousBlends = sum("quantity", "loc_daily_transaction_details WHERE " & ZReadDateFilter & " AND product_category = 'Famous Blends'"))
            ThreadZXRead.Start()
            ThreadlistZXRead.Add(ThreadZXRead)
            For Each t In ThreadlistZXRead
                t.Join()
            Next

            ThreadZXRead = New Thread(Sub() ZXAddOns = sum("quantity", "loc_daily_transaction_details WHERE " & ZReadDateFilter & " AND product_category = 'Add-Ons'"))
            ThreadZXRead.Start()
            ThreadlistZXRead.Add(ThreadZXRead)
            For Each t In ThreadlistZXRead
                t.Join()
            Next
            Dim CashBreakDownDatatable As DataTable = ReturnCashBreakdown(Fromdate, ToDate)
            With CashBreakDownDatatable
                For i As Integer = 0 To .Rows.Count - 1 Step +1
                    ZXThousandQty += CashBreakDownDatatable(i)(18)
                    ZXFiveHundredQty += CashBreakDownDatatable(i)(19)
                    ZXTwoHundredQty += CashBreakDownDatatable(i)(20)
                    ZXOneHundredQty += CashBreakDownDatatable(i)(21)
                    ZXFiftyQty += CashBreakDownDatatable(i)(22)
                    ZXTwentyQty += CashBreakDownDatatable(i)(23)
                    ZXTenQty += CashBreakDownDatatable(i)(24)
                    ZXFiveQty += CashBreakDownDatatable(i)(25)
                    ZXOneQty += CashBreakDownDatatable(i)(26)
                    ZXPointTwentyFiveQty += CashBreakDownDatatable(i)(27)
                    ZXPointFiveQty += CashBreakDownDatatable(i)(28)
                Next
            End With

            ThreadZXRead = New Thread(Sub() ZXVatExemptSales = sum("vatexemptsales", "loc_daily_transaction WHERE zreading = '" & S_Zreading & "' AND active = 1"))
            ThreadZXRead.Start()
            ThreadlistZXRead.Add(ThreadZXRead)
            For Each t In ThreadlistZXRead
                t.Join()
            Next

            'ZXReprintCount N/A

            ThreadZXRead = New Thread(Sub() ZXPremium = sum("quantity", "loc_daily_transaction_details WHERE zreading = '" & S_Zreading & "' AND product_category = 'Premium'"))
            ThreadZXRead.Start()
            ThreadlistZXRead.Add(ThreadZXRead)
            For Each t In ThreadlistZXRead
                t.Join()
            Next

            ZXThousandTotal = ZXThousandQty * 1000
            ZXFiveHundredTotal = ZXFiveHundredQty * 500
            ZXTwoHundredTotal = ZXTwoHundredQty * 200
            ZXOneHundredTotal = ZXOneHundredQty * 100
            ZXFiftyTotal = ZXFiftyQty * 50
            ZXTwentyTotal = ZXTwentyQty * 20
            ZXTenTotal = ZXTenQty * 10
            ZXFiveTotal = ZXFiveQty * 5
            ZXOneTotal = ZXOneQty * 1
            ZXPointTwentyFiveTotal = ZXPointTwentyFiveQty * 0.25
            ZXPointFiveTotal = ZXPointFiveQty * 0.05
            ZXdate = S_Zreading
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Reports: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        Finally
            If XREADORZREAD = "Z-READ" Then
                InsertZReadXRead()
            End If
        End Try
    End Sub

    Private Sub FIllZReadReprint(DateFrom, DateTo)
        Try
            Dim ConnectionLocal As MySqlConnection = LocalhostConn()
            Dim Query As String = ""
            Dim Command As MySqlCommand

            ZXBegSINo = returnselect("ZXBegSINo", "`loc_zread_table` WHERE ZXdate = '" & DateFrom & "' AND status = 1 Order by id ASC limit 1")
            ZXEndSINo = returnselect("ZXEndSINo", "`loc_zread_table` WHERE ZXdate = '" & DateTo & "' AND status = 1 Order by id ASC limit 1")
            ZXBegTransNo = returnselect("ZXBegTransNo", "`loc_zread_table` WHERE ZXdate = '" & DateFrom & "' AND status = 1 Order by id ASC limit 1")
            ZXEndTransNo = returnselect("ZXEndTransNo", "`loc_zread_table` WHERE ZXdate = '" & DateTo & "' AND status = 1 Order by id ASC limit 1")

            'Dim BegSi As Integer = Val(ZXBegSINo)
            'ZXBegSINo = BegSi.ToString(S_SIFormat)

            'Dim EndSi As Integer = Val(ZXEndSINo)
            'ZXEndSINo = EndSi.ToString(S_SIFormat)

            Dim ResetCounter = 0
            Query = " SELECT counter_value FROM tbcountertable WHERE counter_id = 1"
            Dim Cmd As MySqlCommand = New MySqlCommand(Query, ConnectionLocal)
            Using reader As MySqlDataReader = Cmd.ExecuteReader
                If reader.HasRows Then
                    While reader.Read
                        ResetCounter = reader("counter_value")
                    End While
                End If
            End Using
            ZXResetCounter = ResetCounter
            ZXZreadCounter = My.Settings.zcounter
            ZXCashier = returnfullname(ClientCrewID)


            Query = "SELECT " & ZReadTableFieldsSum & " FROM loc_zread_table WHERE ZXdate >= '" & DateFrom & "' AND ZXdate <= '" & DateTo & "' AND status = 1"
            Console.WriteLine(Query)
            Command = New MySqlCommand(Query, ConnectionLocal)
            Using Reader As MySqlDataReader = Command.ExecuteReader
                If Reader.HasRows Then
                    While Reader.Read

                        Dim CZXBegBalance = Reader("ZXBegBalance")
                        ZXBegBalance = If(CZXBegBalance.ToString <> "", Reader("ZXBegBalance"), 0)
                        Dim CZXCashTotal = Reader("ZXCashTotal")
                        ZXCashTotal = If(CZXCashTotal.ToString <> "", Reader("ZXCashTotal"), 0)
                        Dim CZXGross = Reader("ZXGross")
                        ZXGross = If(CZXGross.ToString <> "", Reader("ZXGross"), 0)
                        Dim CZXLessVat = Reader("ZXLessVat")
                        ZXLessVat = If(CZXLessVat.ToString <> "", Reader("ZXLessVat"), 0)
                        Dim CZXLessVatDiplomat = Reader("ZXLessVatDiplomat")
                        ZXLessVatDiplomat = If(CZXLessVatDiplomat.ToString <> "", Reader("ZXLessVatDiplomat"), 0)
                        Dim CZXLessVatOthers = Reader("ZXLessVatOthers")
                        ZXLessVatOthers = If(CZXLessVatOthers.ToString <> "", Reader("ZXLessVatOthers"), 0)
                        Dim CZXAdditionalVat = Reader("ZXAdditionalVat")
                        ZXAdditionalVat = If(CZXAdditionalVat.ToString <> "", Reader("ZXAdditionalVat"), 0)
                        Dim CZXVatAmount = Reader("ZXVatAmount")
                        ZXVatAmount = If(CZXVatAmount.ToString <> "", Reader("ZXVatAmount"), 0)
                        Dim CZXLocalGovTax = Reader("ZXLocalGovTax")
                        ZXLocalGovTax = If(CZXLocalGovTax.ToString <> "", Reader("ZXLocalGovTax"), 0)
                        Dim CZXVatableSales = Reader("ZXVatableSales")
                        ZXVatableSales = If(CZXVatableSales.ToString <> "", Reader("ZXVatableSales"), 0)
                        Dim CZXZeroRatedSales = Reader("ZXZeroRatedSales")
                        ZXZeroRatedSales = If(CZXZeroRatedSales.ToString <> "", Reader("ZXZeroRatedSales"), 0)
                        Dim CZXDailySales = Reader("ZXDailySales")
                        ZXDailySales = If(CZXDailySales.ToString <> "", Reader("ZXDailySales"), 0)
                        Dim CZXNetSales = Reader("ZXNetSales")
                        ZXNetSales = If(CZXNetSales.ToString <> "", Reader("ZXNetSales"), 0)
                        Dim CZXCashlessTotal = Reader("ZXCashlessTotal")
                        ZXCashlessTotal = If(CZXCashlessTotal.ToString <> "", Reader("ZXCashlessTotal"), 0)
                        Dim CZXGcash = Reader("ZXGcash")
                        ZXGcash = If(CZXGcash.ToString <> "", Reader("ZXGcash"), 0)
                        Dim CZXPaymaya = Reader("ZXPaymaya")
                        ZXPaymaya = If(CZXPaymaya.ToString <> "", Reader("ZXPaymaya"), 0)
                        Dim CZXGrabFood = Reader("ZXGrabFood")
                        ZXGrabFood = If(CZXGrabFood.ToString <> "", Reader("ZXGrabFood"), 0)
                        Dim CZXFoodPanda = Reader("ZXFoodPanda")
                        ZXFoodPanda = If(CZXFoodPanda.ToString <> "", Reader("ZXFoodPanda"), 0)
                        Dim CZXShopeePay = Reader("ZXShopeePay")
                        ZXShopeePay = If(CZXShopeePay.ToString <> "", Reader("ZXShopeePay"), 0)
                        Dim CZXCashlessOthers = Reader("ZXCashlessOthers")
                        ZXCashlessOthers = If(CZXCashlessOthers.ToString <> "", Reader("ZXCashlessOthers"), 0)
                        Dim CZXRepExpense = Reader("ZXRepExpense")
                        ZXRepExpense = If(CZXRepExpense.ToString <> "", Reader("ZXRepExpense"), 0)
                        Dim CZXCreditCard = Reader("ZXCreditCard")
                        ZXCreditCard = If(CZXCreditCard.ToString <> "", Reader("ZXCreditCard"), 0)
                        Dim CZXDebitCard = Reader("ZXDebitCard")
                        ZXDebitCard = If(CZXDebitCard.ToString <> "", Reader("ZXDebitCard"), 0)
                        Dim CZXMiscCheques = Reader("ZXMiscCheques")
                        ZXMiscCheques = If(CZXMiscCheques.ToString <> "", Reader("ZXMiscCheques"), 0)
                        Dim CZXGiftCard = Reader("ZXGiftCard")
                        ZXGiftCard = If(CZXGiftCard.ToString <> "", Reader("ZXGiftCard"), 0)
                        Dim CZXGiftCardSum = Reader("ZXGiftCardSum")
                        ZXGiftCardSum = If(CZXGiftCardSum.ToString <> "", Reader("ZXGiftCardSum"), 0)
                        Dim CZXAR = Reader("ZXAR")
                        ZXAR = If(CZXAR.ToString <> "", Reader("ZXAR"), 0)
                        Dim CZXTotalExpenses = Reader("ZXTotalExpenses")
                        ZXTotalExpenses = If(CZXTotalExpenses.ToString <> "", Reader("ZXTotalExpenses"), 0)
                        Dim CZXCardOthers = Reader("ZXCardOthers")
                        ZXCardOthers = If(CZXCardOthers.ToString <> "", Reader("ZXCardOthers"), 0)
                        Dim CZXDeposits = Reader("ZXDeposits")
                        ZXDeposits = If(CZXDeposits.ToString <> "", Reader("ZXDeposits"), 0)
                        Dim CZXCashInDrawer = Reader("ZXCashInDrawer")
                        ZXCashInDrawer = If(CZXCashInDrawer.ToString <> "", Reader("ZXCashInDrawer"), 0)
                        Dim CZXTotalDiscounts = Reader("ZXTotalDiscounts")
                        ZXTotalDiscounts = If(CZXTotalDiscounts.ToString <> "", Reader("ZXTotalDiscounts"), 0)
                        Dim CZXSeniorCitizen = Reader("ZXSeniorCitizen")
                        ZXSeniorCitizen = If(CZXSeniorCitizen.ToString <> "", Reader("ZXSeniorCitizen"), 0)
                        Dim CZXPWD = Reader("ZXPWD")
                        ZXPWD = If(CZXPWD.ToString <> "", Reader("ZXPWD"), 0)
                        Dim CZXAthlete = Reader("ZXAthlete")
                        ZXAthlete = If(CZXAthlete.ToString <> "", Reader("ZXAthlete"), 0)
                        Dim CZXSingleParent = Reader("ZXSingleParent")
                        ZXSingleParent = If(CZXSingleParent.ToString <> "", Reader("ZXSingleParent"), 0)
                        Dim CZXItemVoidEC = Reader("ZXItemVoidEC")
                        ZXItemVoidEC = If(CZXItemVoidEC.ToString <> "", Reader("ZXItemVoidEC"), 0)
                        Dim CZXTransactionVoid = Reader("ZXTransactionVoid")
                        ZXTransactionVoid = If(CZXTransactionVoid.ToString <> "", Reader("ZXTransactionVoid"), 0)
                        Dim CZXTransactionCancel = Reader("ZXTransactionCancel")
                        ZXTransactionCancel = If(CZXTransactionCancel.ToString <> "", Reader("ZXTransactionCancel"), 0)
                        Dim CZXTakeOutCharge = Reader("ZXTakeOutCharge")
                        ZXTakeOutCharge = If(CZXTakeOutCharge.ToString <> "", Reader("ZXTakeOutCharge"), 0)
                        Dim CZXDeliveryCharge = Reader("ZXDeliveryCharge")
                        ZXDeliveryCharge = If(CZXDeliveryCharge.ToString <> "", Reader("ZXDeliveryCharge"), 0)
                        Dim CZXReturnsExchange = Reader("ZXReturnsExchange")
                        ZXReturnsExchange = If(CZXReturnsExchange.ToString <> "", Reader("ZXReturnsExchange"), 0)
                        Dim CZXReturnsRefund = Reader("ZXReturnsRefund")
                        ZXReturnsRefund = If(CZXReturnsRefund.ToString <> "", Reader("ZXReturnsRefund"), 0)
                        Dim CZXTotalQTYSold = Reader("ZXTotalQTYSold")
                        ZXTotalQTYSold = If(CZXTotalQTYSold.ToString <> "", Reader("ZXTotalQTYSold"), 0)
                        Dim CZXTotalTransactionCount = Reader("ZXTotalTransactionCount")
                        ZXTotalTransactionCount = If(CZXTotalTransactionCount.ToString <> "", Reader("ZXTotalTransactionCount"), 0)
                        Dim CZXTotalGuess = Reader("ZXTotalGuess")
                        ZXTotalGuess = If(CZXTotalGuess.ToString <> "", Reader("ZXTotalGuess"), 0)
                        Dim CZXCurrentTotalSales = Reader("ZXCurrentTotalSales")
                        ZXCurrentTotalSales = If(CZXCurrentTotalSales.ToString <> "", Reader("ZXCurrentTotalSales"), 0)
                        Dim CZXOldGrandTotalSales = Reader("ZXOldGrandTotalSales")
                        ZXOldGrandTotalSales = If(CZXOldGrandTotalSales.ToString <> "", Reader("ZXOldGrandTotalSales"), 0)
                        Dim CZXNewGrandtotalSales = Reader("ZXNewGrandtotalSales")
                        ZXNewGrandtotalSales = If(CZXNewGrandtotalSales.ToString <> "", Reader("ZXNewGrandtotalSales"), 0)
                        Dim CZXSimplyPerfect = Reader("ZXSimplyPerfect")
                        ZXSimplyPerfect = If(CZXSimplyPerfect.ToString <> "", Reader("ZXSimplyPerfect"), 0)
                        Dim CZXPerfectCombination = Reader("ZXPerfectCombination")
                        ZXPerfectCombination = If(CZXPerfectCombination.ToString <> "", Reader("ZXPerfectCombination"), 0)
                        Dim CZXSavoury = Reader("ZXSavoury")
                        ZXSavoury = If(CZXSavoury.ToString <> "", Reader("ZXSavoury"), 0)
                        Dim CZXCombo = Reader("ZXCombo")
                        ZXCombo = If(CZXCombo.ToString <> "", Reader("ZXCombo"), 0)
                        Dim CZXFamousBlends = Reader("ZXFamousBlends")
                        ZXFamousBlends = If(CZXFamousBlends.ToString <> "", Reader("ZXFamousBlends"), 0)
                        Dim CZXAddOns = Reader("ZXAddOns")
                        ZXAddOns = If(CZXAddOns.ToString <> "", Reader("ZXAddOns"), 0)
                        Dim CZXThousandQty = Reader("ZXThousandQty")
                        ZXThousandQty = If(CZXThousandQty.ToString <> "", Reader("ZXThousandQty"), 0)
                        Dim CZXFiveHundredQty = Reader("ZXFiveHundredQty")
                        ZXFiveHundredQty = If(CZXFiveHundredQty.ToString <> "", Reader("ZXFiveHundredQty"), 0)
                        Dim CZXTwoHundredQty = Reader("ZXTwoHundredQty")
                        ZXTwoHundredQty = If(CZXTwoHundredQty.ToString <> "", Reader("ZXTwoHundredQty"), 0)
                        Dim CZXOneHundredQty = Reader("ZXOneHundredQty")
                        ZXOneHundredQty = If(CZXOneHundredQty.ToString <> "", Reader("ZXOneHundredQty"), 0)
                        Dim CZXFiftyQty = Reader("ZXFiftyQty")
                        ZXFiftyQty = If(CZXFiftyQty.ToString <> "", Reader("ZXFiftyQty"), 0)
                        Dim CZXTwentyQty = Reader("ZXTwentyQty")
                        ZXTwentyQty = If(CZXTwentyQty.ToString <> "", Reader("ZXTwentyQty"), 0)
                        Dim CZXTenQty = Reader("ZXTenQty")
                        ZXTenQty = If(CZXTenQty.ToString <> "", Reader("ZXTenQty"), 0)
                        Dim CZXFiveQty = Reader("ZXFiveQty")
                        ZXFiveQty = If(CZXFiveQty.ToString <> "", Reader("ZXFiveQty"), 0)
                        Dim CZXOneQty = Reader("ZXOneQty")
                        ZXOneQty = If(CZXOneQty.ToString <> "", Reader("ZXOneQty"), 0)
                        Dim CZXPointTwentyFiveQty = Reader("ZXPointTwentyFiveQty")
                        ZXPointTwentyFiveQty = If(CZXPointTwentyFiveQty.ToString <> "", Reader("ZXPointTwentyFiveQty"), 0)
                        Dim CZXPointFiveQty = Reader("ZXPointFiveQty")
                        ZXPointFiveQty = If(CZXPointFiveQty.ToString <> "", Reader("ZXPointFiveQty"), 0)
                        Dim CZXThousandTotal = Reader("ZXThousandTotal")
                        ZXThousandTotal = If(CZXThousandTotal.ToString <> "", Reader("ZXThousandTotal"), 0)
                        Dim CZXFiveHundredTotal = Reader("ZXFiveHundredTotal")
                        ZXFiveHundredTotal = If(CZXFiveHundredTotal.ToString <> "", Reader("ZXFiveHundredTotal"), 0)
                        Dim CZXTwoHundredTotal = Reader("ZXTwoHundredTotal")
                        ZXTwoHundredTotal = If(CZXTwoHundredTotal.ToString <> "", Reader("ZXTwoHundredTotal"), 0)
                        Dim CZXOneHundredTotal = Reader("ZXOneHundredTotal")
                        ZXOneHundredTotal = If(CZXOneHundredTotal.ToString <> "", Reader("ZXOneHundredTotal"), 0)
                        Dim CZXFiftyTotal = Reader("ZXFiftyTotal")
                        ZXFiftyTotal = If(CZXFiftyTotal.ToString <> "", Reader("ZXFiftyTotal"), 0)
                        Dim CZXTwentyTotal = Reader("ZXTwentyTotal")
                        ZXTwentyTotal = If(CZXTwentyTotal.ToString <> "", Reader("ZXTwentyTotal"), 0)
                        Dim CZXTenTotal = Reader("ZXTenTotal")
                        ZXTenTotal = If(CZXTenTotal.ToString <> "", Reader("ZXTenTotal"), 0)
                        Dim CZXFiveTotal = Reader("ZXFiveTotal")
                        ZXFiveTotal = If(CZXFiveTotal.ToString <> "", Reader("ZXFiveTotal"), 0)
                        Dim CZXOneTotal = Reader("ZXOneTotal")
                        ZXOneTotal = If(CZXOneTotal.ToString <> "", Reader("ZXOneTotal"), 0)
                        Dim CZXPointTwentyFiveTotal = Reader("ZXPointTwentyFiveTotal")
                        ZXPointTwentyFiveTotal = If(CZXPointTwentyFiveTotal.ToString <> "", Reader("ZXPointTwentyFiveTotal"), 0)
                        Dim CZXPointFiveTotal = Reader("ZXPointFiveTotal")
                        ZXPointFiveTotal = If(CZXPointFiveTotal.ToString <> "", Reader("ZXPointFiveTotal"), 0)
                        Dim CZXVatExemptSales = Reader("ZXVatExemptSales")
                        ZXVatExemptSales = If(CZXVatExemptSales.ToString <> "", Reader("ZXVatExemptSales"), 0)
                        Dim CZXPremium = Reader("ZXPremium")
                        ZXPremium = If(CZXPremium.ToString <> "", Reader("ZXPremium"), 0)

                        If DateFrom = DateTo Then
                            Dim CZXReprintCount = Reader("ZXReprintCount")
                            ZXReprintCount = If(CZXReprintCount.ToString <> "", Reader("ZXReprintCount"), 0)
                        Else
                            ZXReprintCount = 0
                        End If

                        Dim CZXLessDiscVE = Reader("ZXLessDiscVE")
                        ZXLessDiscVE = If(CZXLessDiscVE.ToString <> "", Reader("ZXLessDiscVE"), 0)


                    End While
                End If
            End Using
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Reports: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub



    Private Sub ButtonAdvancedCustomReports_Click(sender As Object, e As EventArgs) Handles ButtonAdvancedCustomReports.Click
        AdvancedCustomReport.Show()
        Enabled = False
    End Sub

    Private Sub Reports_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If Application.OpenForms().OfType(Of AdvancedCustomReport).Any Then
            AdvancedCustomReport.Close()
        End If
    End Sub

    Private Sub ToolStripComboBoxStatus_TextChanged(sender As Object, e As EventArgs) Handles ToolStripComboBoxStatus.SelectedIndexChanged
        ToolStripButton6.PerformClick()
    End Sub
    Dim sfd As SaveFileDialog = New SaveFileDialog()
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles ButtonXREAD.Click
        Try
            AuditTrail.LogToAuditTral("System", "Reports/Others: Accessed X-READ, " & ClientCrewID, "Normal")

            XREADORZREAD = "X-READ"
            FillZreadData(" zreading = '" & S_Zreading & "'", S_Zreading, S_Zreading)
            printdocZRead.DefaultPageSettings.PaperSize = New PaperSize("Custom", ReturnPrintSize(), 1020)

            Dim XMLName As String = "XREAD" & FullDateFormatForSaving().ToString & ".xml"
            XML_Writer = New XmlTextWriter(pdfSharpMod.XML_Path & XMLName, Encoding.UTF8)
            XML_Writer.WriteStartDocument(True)
            XML_Writer.Formatting = Formatting.Indented
            XML_Writer.Indentation = 2
            XML_Writer.WriteStartElement("Table")

            If S_Print_XZRead = "YES" Then
                printdocZRead.Print()
            Else
                PrintPreviewDialogZread.Document = printdocZRead
                PrintPreviewDialogZread.ShowDialog()
            End If

            XML_Writer.WriteEndElement()
            XML_Writer.WriteEndDocument()
            XML_Writer.Close()
            InsertIntoEJournal()
            SaveXMLInfo(XMLName)
        Catch ex As Exception
            MsgBox(ex.ToString)
            SendErrorReport(ex.ToString)
        Finally
            SystemLogDesc = "X-Reading : " & FullDate24HR() & " Crew : " & returnfullname(ClientCrewID)
            SystemLogType = "X-READ"
            GLOBAL_SYSTEM_LOGS(SystemLogType, SystemLogDesc)
        End Try
    End Sub
    Private Sub printdocZRead_PrintPage_one()

    End Sub
    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles ButtonZReading.Click
        Try
            ReprintZRead = False
            Dim msg = MessageBox.Show("Are you sure you want to generate Z-READ ? Press Yes to continue or No to cancel", "Z-reading", MessageBoxButtons.YesNo, MessageBoxIcon.Information)

            If msg = DialogResult.Yes Then
                AuditTrail.LogToAuditTral("System", "Reports/Others: Accessed Z-READ, " & ClientCrewID, "Normal")

                My.Settings.zcounter += 1
                Dim ConnectionLocal As MySqlConnection = LocalhostConn()
                'Fill dgv inv
                GLOBAL_SELECT_ALL_FUNCTION("loc_pos_inventory", "*", DataGridViewZreadInventory)
                'Update inventory
                MainInventorySub()
                'Fill again
                GLOBAL_SELECT_ALL_FUNCTION("loc_pos_inventory", "*", DataGridViewZreadInventory)
                'Print zread
                XREADORZREAD = "Z-READ"
                FillZreadData(" zreading = '" & S_Zreading & "'", S_Zreading, S_Zreading)
                printdocZRead.DefaultPageSettings.PaperSize = New PaperSize("Custom", ReturnPrintSize(), 1020)

                Dim XMLName As String = "ZREAD" & FullDateFormatForSaving().ToString & ".xml"
                XML_Writer = New XmlTextWriter(pdfSharpMod.XML_Path & XMLName, Encoding.UTF8)
                XML_Writer.WriteStartDocument(True)
                XML_Writer.Formatting = Formatting.Indented
                XML_Writer.Indentation = 2
                XML_Writer.WriteStartElement("Table")

                If S_Print_XZRead = "YES" Then
                    printdocZRead.Print()
                Else
                    PrintPreviewDialogZread.Document = printdocZRead
                    PrintPreviewDialogZread.ShowDialog()
                End If

                XML_Writer.WriteEndElement()
                XML_Writer.WriteEndDocument()
                XML_Writer.Close()

                InsertIntoEJournal()
                GetOldGrandtotal()
                SaveXMLInfo(XMLName)
                'Update Zread
                S_Zreading = Format(DateAdd("d", 1, S_Zreading), "yyyy-MM-dd")
                sql = "UPDATE loc_settings SET S_Zreading = '" & S_Zreading & "'"
                cmd = New MySqlCommand(sql, ConnectionLocal)
                cmd.ExecuteNonQuery()
                cmd.Dispose()

                sql = "UPDATE loc_pos_inventory SET zreading = '" & S_Zreading & "'"
                LocalhostConn.Close()
                cmd = New MySqlCommand(sql, ConnectionLocal)
                cmd.ExecuteNonQuery()

                cmd.Dispose()
                ConnectionLocal.Close()
                'Insert to local zread inv
                XZreadingInventory(S_Zreading)

                LabelDate.Text = "Z-READ DATE: " & Format(StringToDate(S_Zreading), "MMMM dd, yyyy").ToString.ToUpper

                If S_Zreading = Format(Now().AddDays(1), "yyyy-MM-dd") Then
                    'ButtonZread.Enabled = False
                    ButtonZReading.Enabled = False
                    'ButtonZreadAdmin.Enabled = False
                End If


            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
            AuditTrail.LogToAuditTral("System", "Reports: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        Finally
            SystemLogDesc = "Z-Reading : " & FullDate24HR() & " Crew : " & returnfullname(ClientCrewID)
            SystemLogType = "Z-READ"
            GLOBAL_SYSTEM_LOGS(SystemLogType, SystemLogDesc)
        End Try
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles ButtonZREADREPRINT.Click

        Try
            ReprintZRead = True
            Dim FromDate, ToDate As String
            FromDate = Format(DateTimePickerZXreading.Value, "yyyy-MM-dd")
            ToDate = Format(DateTimePickerZXreadingTo.Value, "yyyy-MM-dd")

            XREADORZREAD = "Z-READ"
            AuditTrail.LogToAuditTral("System", "Reports/Others: Accessed Z-READ REPRINT, " & ClientCrewID, "Normal")

            If FromDate = ToDate Then
                Dim ConnectionLocal As MySqlConnection = LocalhostConn()
                Dim Query As String = "UPDATE loc_zread_table SET ZXReprintCount = @1 WHERE ZXdate = '" & FromDate & "'"
                Dim Command As MySqlCommand = New MySqlCommand(Query, ConnectionLocal)
                Dim TotalReprint = ZXReprintCount + 1
                Command.Parameters.Add("@1", MySqlDbType.Text).Value = TotalReprint
                Command.ExecuteNonQuery()
            End If

            FIllZReadReprint(FromDate, ToDate)

            printdocZRead.DefaultPageSettings.PaperSize = New PaperSize("Custom", ReturnPrintSize(), 1050)

            Dim XMLName As String = "ZREADREP" & FullDateFormatForSaving().ToString & ".xml"

            XML_Writer = New XmlTextWriter(pdfSharpMod.XML_Path & XMLName, Encoding.UTF8)
            XML_Writer.WriteStartDocument(True)
            XML_Writer.Formatting = Formatting.Indented
            XML_Writer.Indentation = 2
            XML_Writer.WriteStartElement("Table")


            If S_Print_XZRead = "YES" Then
                printdocZRead.Print()
            Else
                PrintPreviewDialogZread.Document = printdocZRead
                PrintPreviewDialogZread.ShowDialog()
            End If

            XML_Writer.WriteEndElement()
            XML_Writer.WriteEndDocument()
            XML_Writer.Close()
            SaveXMLInfo(XMLName)
            InsertIntoEJournal()
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Reports: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        Finally
            SystemLogDesc = "Z-Reading Reprint : " & FullDate24HR() & " Crew : " & returnfullname(ClientCrewID)
            SystemLogType = "Z-READ REPRINT"
            GLOBAL_SYSTEM_LOGS(SystemLogType, SystemLogDesc)
        End Try
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles ButtonEJournal.Click
        Try
            GenerateEJournalV2()
            AuditTrail.LogToAuditTral("System", "Reports/Others: Accessed E-JOURNAL, " & ClientCrewID, "Normal")

        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub

    Private Sub GenerateEJournalV2()
        Try
            Dim Denom As String = ""

            Dim connectionlocal As MySqlConnection = LocalhostConn()
            Dim sql As String = ""
            Dim cmd As MySqlCommand
            Dim dt As DataTable = New DataTable
            Dim da As MySqlDataAdapter

            Dim CompleteDirectoryPath As String = ""

            If Not Directory.Exists(My.Computer.FileSystem.SpecialDirectories.Desktop & "\E-Journal") Then
                Directory.CreateDirectory(My.Computer.FileSystem.SpecialDirectories.Desktop & "\E-journal")
                CompleteDirectoryPath = My.Computer.FileSystem.SpecialDirectories.Desktop & "\E-journal\" & FullDateFormatForSaving()
                Directory.CreateDirectory(CompleteDirectoryPath)
            Else
                CompleteDirectoryPath = My.Computer.FileSystem.SpecialDirectories.Desktop & "\E-journal\" & FullDateFormatForSaving()
                Directory.CreateDirectory(CompleteDirectoryPath)
            End If

            Dim GrandTotalLines As Integer = 0
            Dim WholeContentLine As String = ""
            sql = "SELECT totallines, content FROM loc_e_journal WHERE (zreading BETWEEN '" & Format(DateTimePickerZXreading.Value, "yyyy-MM-dd") & "' AND '" & Format(DateTimePickerZXreadingTo.Value, "yyyy-MM-dd") & "') ORDER by id DESC"
            cmd = New MySqlCommand(sql, connectionlocal)
            da = New MySqlDataAdapter(cmd)
            da.Fill(dt)

            For i As Integer = 0 To dt.Rows.Count - 1 Step +1
                GrandTotalLines += dt(i)(0)
                WholeContentLine &= dt(i)(1)
            Next

            Dim TotalDgvRows As Integer = GrandTotalLines
            Dim TxtFileLine(TotalDgvRows) As String
            Dim a As Integer = 0

            Dim strArr() As String
            Dim count As Integer

            strArr = WholeContentLine.Split("/n")
            For count = 0 To strArr.Length - 1
                TxtFileLine(a) = strArr(count)
                a += 1
            Next

            Dim CompletePath As String = CompleteDirectoryPath & "\ejournal" & FullDateFormatForSaving() & ".txt"

            AuditTrail.LogToAuditTral("Report", "Reports/Others: E-Journal Generated, " & CompleteDirectoryPath, "Normal")

            File.WriteAllLines(CompletePath, TxtFileLine, Encoding.UTF8)

            MsgBox("Complete")
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Reports: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles ButtonAuditTrail.Click
        MDIFORM.newMDIchildReports.Enabled = False
        MDIFORM.Enabled = False
        AuditTrail.LogToAuditTral("System", "Reports: Accessed AUDIT TRAIL, " & ClientCrewID, "Normal")
        AuditTrail.Show()
    End Sub

    Private Sub ButtonEJournalPDF_Click(sender As Object, e As EventArgs) Handles ButtonEJournalPDF.Click
        Try
            'zreading >= @Fromdate AND zreading <= @ToDate
            Dim ConnectionLocal As MySqlConnection = LocalhostConn()
            Dim Query As String = "SELECT xml_name FROM loc_xml_ref WHERE DATE(zreading) BETWEEN DATE(@FromDate) AND DATE(@ToDate) ORDER BY created_at DESC"
            Dim Command As MySqlCommand = New MySqlCommand(Query, ConnectionLocal)
            Command.Parameters.Clear()
            Command.Parameters.Add("@FromDate", MySqlDbType.Date).Value = Format(DateTimePickerZXreading.Value, "yyyy-MM-dd")
            Command.Parameters.Add("@ToDate", MySqlDbType.Date).Value = Format(DateTimePickerZXreadingTo.Value, "yyyy-MM-dd")
            Dim ListOfXml As New List(Of String)
            Using Reader As MySqlDataReader = Command.ExecuteReader
                While Reader.Read
                    If Reader.HasRows Then
                        ListOfXml.Add(Reader("xml_name").ToString)
                    End If
                End While
            End Using

            Dim f As New EJOURNAL_PDF.EJOURNAL_PDF(ListOfXml)
            f.Show()

            AuditTrail.LogToAuditTral("Report", "Reports/Others: E-Journal PDF Generated, " & ClientCrewID, "Normal")
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub


End Class