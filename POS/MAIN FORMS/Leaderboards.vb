Imports MySql.Data.MySqlClient
Imports System.Threading
Imports System.Windows.Forms.DataVisualization.Charting
Public Class Leaderboards
    Dim thread1 As Thread
    Dim solocloudconn As New MySqlConnection
    Dim hasinternet As Boolean = False
    Dim StopThread As Boolean = False
    Dim BestsellerDataTable As DataTable
    Dim BestsellerDataAdapter As MySqlDataAdapter
    Dim BestSellerCmd As MySqlCommand

    Private Sub ProductList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            TabControl1.TabPages(0).Text = "Sales"
            TabControl1.TabPages(1).Text = "Expenses"
            TabControl1.TabPages(2).Text = "Transfers"
            TabControl1.TabPages(3).Text = "Logs"
            LoadChart("SELECT DATE_FORMAT(zreading, '%Y-%m-%d') as zreading, SUM(total) FROM loc_daily_transaction_details WHERE DATE(CURRENT_DATE) - INTERVAL 7 DAY GROUP BY zreading DESC LIMIT 7", 0)
            LoadProducts()
            loadbestseller()
            LoadTransactions()
            LoadExpenses()
            LoadTransfers()
            LoadLogs()
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Inventory: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub

    Dim threadList As List(Of Thread) = New List(Of Thread)
    Private Sub loadbestseller()
        Try
            GLOBAL_SELECT_ALL_FUNCTION("loc_daily_transaction_details GROUP BY product_name ORDER by SUM(quantity) DESC limit 10", "product_name ,  product_category, SUM(quantity) as Qty, price , Sum(total) as totalprice", DatagridviewTOPSELLER)
            With DatagridviewTOPSELLER
                .Columns(0).HeaderCell.Value = "Product Name"
                .Columns(0).Width = 150
                .Columns(0).HeaderCell.Style.Alignment = DataGridViewContentAlignment.TopLeft
                .Columns(1).HeaderCell.Value = "Category"
                .Columns(1).Width = 150
                .Columns(1).HeaderCell.Style.Alignment = DataGridViewContentAlignment.TopLeft
                .Columns(2).HeaderCell.Value = "Sales Volume"
                .Columns(2).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(3).HeaderCell.Value = "Price"
                .Columns(3).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(4).HeaderCell.Value = "Total Sale"
                .Columns(4).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(2).Width = 120
                .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(3).Width = 100
                .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(4).Width = 100
            End With
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Inventory: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub LoadTransactions()
        Try
            Dim TransacReport = AsDatatable("loc_daily_transaction ORDER BY transaction_id DESC LIMIT 10", "transaction_type , created_at , crew_id , amountdue, active", DataGridViewRecentSales)
            Dim rowActive
            For Each row As DataRow In TransacReport.Rows
                If row("active") = 1 Then
                    rowActive = "Active"
                ElseIf row("active") = 2 Then
                    rowActive = "Returned"
                Else
                    rowActive = "N/A"
                End If
                DataGridViewRecentSales.Rows.Add(row("transaction_type"), row("created_at"), row("crew_id"), row("amountdue"), rowActive)
            Next
            With DataGridViewRecentSales
                .Columns(0).HeaderCell.Value = "Transaction Type"
                .Columns(0).HeaderCell.Style.Alignment = DataGridViewContentAlignment.TopLeft
                .Columns(1).HeaderCell.Value = "Date Created"
                .Columns(1).HeaderCell.Style.Alignment = DataGridViewContentAlignment.TopLeft
                .Columns(2).HeaderCell.Value = "Crew"
                .Columns(2).HeaderCell.Style.Alignment = DataGridViewContentAlignment.TopLeft
                .Columns(3).HeaderCell.Value = "Total Sales"
                .Columns(3).HeaderCell.Style.Alignment = DataGridViewContentAlignment.TopRight
                .Columns(4).HeaderCell.Value = "Status"
            End With
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Inventory: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub LoadExpenses()
        Try
            Dim ExepenseReport = AsDatatable("loc_expense_list GROUP BY expense_id DESC LIMIT 10", "expense_number , created_at AS datetime , crew_id , total_amount, active", DataGridViewRecentExpenses)
            Dim rowActive
            For Each row As DataRow In ExepenseReport.Rows
                If row("active") = 1 Then
                    rowActive = "Active"
                ElseIf row("active") = 2 Then
                    rowActive = "Returned"

                Else
                    rowActive = "N/A"
                End If
                DataGridViewRecentExpenses.Rows.Add(row("expense_number"), row("datetime"), row("crew_id"), row("total_amount"), rowActive)
            Next
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Inventory: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub LoadTransfers()
        Try
            Dim TransferReport = AsDatatable("loc_system_logs WHERE log_type = 'STOCK TRANSFER' GROUP BY log_date_time DESC LIMIT 10", "loc_systemlog_id , log_description  , crew_id , log_date_time", DatagridviewTransfers)

            For Each row As DataRow In TransferReport.Rows
                DatagridviewTransfers.Rows.Add(row("loc_systemlog_id"), row("log_description"), row("crew_id"), row("log_date_time"))
            Next
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Inventory: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub LoadLogs()
        Try
            Dim AsDt = AsDatatable("loc_system_logs WHERE log_type <> 'STOCK TRANSFER'  GROUP BY log_date_time DESC LIMIT 10", "log_type , log_description  , crew_id, log_date_time", DatagridviewLogs)
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
                DatagridviewLogs.Rows.Add(row("log_type"), row("log_description"), row("crew_id"), row("log_date_time"))
            Next
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Inventory: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub LoadProducts()
        Try
            Chart2.Series("Series1").IsVisibleInLegend = False
            Dim sql = "SELECT product_name , Sum(total) as totalprice FROM loc_daily_transaction_details GROUP BY product_name ORDER by COUNT(transaction_number) DESC limit 10"
            Dim cmd As MySqlCommand = New MySqlCommand(sql, LocalhostConn)
            Dim dr As MySqlDataReader
            dr = cmd.ExecuteReader
            While dr.Read
                Chart2.Series("Series1").Points.AddXY(dr.GetString("product_name"), dr.GetInt64("totalprice"))
            End While
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Inventory: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub

    Private Sub LoadChart(sql As String, trueorfalse As Integer)
        Try
            Chart1.Series.Clear()
            Dim Series1 As New DataVisualization.Charting.Series
            With Series1
                .Name = "Series1"
                .ChartType = SeriesChartType.Column
            End With
            Chart1.Series.Add(Series1)
            Chart1.Invalidate()
            Chart1.Series("Series1").IsVisibleInLegend = False
            DataGridView1.Rows.Clear()
            Dim cmd As MySqlCommand = New MySqlCommand(sql, LocalhostConn)
            Dim da As MySqlDataAdapter = New MySqlDataAdapter(cmd)
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            With DataGridView1
                For Each row As DataRow In dt.Rows
                    If trueorfalse = 0 Then
                        .Rows.Add(row("zreading").ToString, row("SUM(total)"))
                    ElseIf trueorfalse = 1 Then
                        .Rows.Add(row("YEAR(zreading)"), row("SUM(total)"))
                    ElseIf trueorfalse = 2 Then
                        .Rows.Add(row("MONTHNAME(zreading)"), row("SUM(total)"))
                    End If
                Next
                For i As Integer = 0 To .Rows.Count - 1 Step +1
                    Me.Chart1.Series("Series1").Points.AddXY(.Rows(i).Cells(0).Value.ToString, .Rows(i).Cells(1).Value.ToString)
                Next
            End With
            LocalhostConn.close()
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Inventory: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub RadioButton3_Click(sender As Object, e As EventArgs) Handles RadioButtonWeek.Click
        Try
            LoadChart("SELECT DATE_FORMAT(zreading, '%Y-%m-%d') as zreading, SUM(total) FROM loc_daily_transaction_details WHERE DATE(CURRENT_DATE) - INTERVAL 7 DAY GROUP BY zreading DESC LIMIT 7", 0)
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Inventory: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub RadioButton2_Click(sender As Object, e As EventArgs) Handles RadioButtonMonth.Click
        Try
            LoadChart("SELECT MONTHNAME(zreading) , SUM(total) FROM `loc_daily_transaction_details` WHERE DATE(zreading) - INTERVAL 1 MONTH GROUP BY MONTHNAME(zreading)", 2)
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Inventory: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub RadioButton4_Click(sender As Object, e As EventArgs) Handles RadioButtonYear.Click
        Try
            LoadChart("SELECT YEAR(zreading), SUM(total) FROM `loc_daily_transaction_details` WHERE YEAR(zreading) GROUP BY YEAR(zreading)", 1)
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Inventory: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub RadioButton5_Click(sender As Object, e As EventArgs) Handles RadioButtonLastYear.Click
        Try
            LoadChart("SELECT YEAR(zreading), SUM(total) FROM `loc_daily_transaction_details` WHERE YEAR(zreading) - INTERVAL 1 YEAR GROUP BY YEAR(zreading)", 1)
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Inventory: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
End Class