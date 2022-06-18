Imports MySql.Data.MySqlClient
Public Class PendingOrders
    Private Sub PendingOrders_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AMPpopulatecategory()
        pendingloadorders()
    End Sub
    Private Sub PendingOrders_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        posandpendingenter = False
        POS.Enabled = True
    End Sub
    Public Sub AMPpopulatecategory()
        Try
            Dim command As New MySqlCommand("SELECT DISTINCT customer_name FROM loc_pending_orders WHERE guid = '" & ClientGuid & "'", LocalhostConn())
            Dim adapter As New MySqlDataAdapter(command)
            Dim table As New DataTable()
            adapter.Fill(table)
            Me.ComboBoxCustomerName.DataSource = Nothing
            Me.ComboBoxCustomerName.DataSource = table
            Me.ComboBoxCustomerName.DisplayMember = "customer_name"
            Me.ComboBoxCustomerName.ValueMember = "customer_name"
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Pending Orders: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Public Sub pendingloadorders()
        Try
            Dim PendingOrders = AsDatatable("loc_pending_orders", "`product_name`, `product_quantity`, `product_price`, `product_total`, `created_at`, `guid`, `active`, `increment`", DataGridView1)
            With DataGridView1
                .Rows.Clear()
                For Each row As DataRow In PendingOrders.Rows
                    .Rows.Add(row("product_name"), row("product_quantity"), row("product_price"), row("product_total"), row("created_at"), row("guid"), row("active"), row("increment"))
                Next
            End With
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Pending Orders: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Public Sub populatedatagridvieworders()
        Try
            Dim command As New MySqlCommand("SELECT * FROM `loc_pending_orders` WHERE customer_name = '" & ComboBoxCustomerName.Text & "' AND guid = '" & ClientGuid & "'", LocalhostConn())
            Dim adapter As New MySqlDataAdapter(command)
            Dim table As New DataTable()
            adapter.Fill(table)
            For Each row As DataRow In table.Rows
                POS.DataGridViewOrders.Rows.Add(row("product_name"), row("product_quantity"), row("product_price"), row("product_total"), row("increment"), row("product_id"), row("product_sku"), row("product_category"), row("product_addon_id"), row("ColumnSumID"), row("ColumnInvID"), row("Upgrade"), row("Origin"), row("addontype"), 0, 0, 0, 0, 0, 0, 0, 0)
            Next row
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Pending Orders: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
        Try
            Dim command As New MySqlCommand("SELECT `hold_id`, `sr_total`, `f_id`, `qty`, `id`, `nm`, `org_serve`, `cog`, `ocog`, `prd.addid`, `origin` FROM loc_hold_inventory WHERE name = '" & ComboBoxCustomerName.Text & "'", LocalhostConn())
            Dim adapter As New MySqlDataAdapter(command)
            Dim table As New DataTable()
            adapter.Fill(table)
            For Each row As DataRow In table.Rows
                POS.DataGridViewInv.Rows.Add(row("sr_total"), row("f_id"), row("qty"), row("id"), row("nm"), row("org_serve"), row("cog"), row("ocog"), row("prd.addid"), row("origin"))
            Next row
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Pending Orders: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub ButtonSelectCustomer_Click(sender As Object, e As EventArgs) Handles ButtonSelectCustomer.Click
        If POS.DataGridViewOrders.RowCount = 0 Then
            If DataGridView1.RowCount > 0 Then
                populatedatagridvieworders()
                GLOBAL_DELETE_ALL_FUNCTION(tablename:="loc_pending_orders", where:=" customer_name = '" & ComboBoxCustomerName.Text & "'")
                GLOBAL_DELETE_ALL_FUNCTION(tablename:="loc_hold_inventory", where:=" name = '" & ComboBoxCustomerName.Text & "'")
                AMPpopulatecategory()
                pendingloadorders()
                posandpendingenter = False
                With POS
                    .Label76.Text = SumOfColumnsToDecimal(.DataGridViewOrders, 3)
                    .TextBoxQTY.Text = 0
                    .Buttonholdoder.Enabled = True
                    .ButtonPayMent.Enabled = True
                    .ButtonPendingOrders.Enabled = False
                End With
                SystemLogType = "PENDING ORDERS"
                SystemLogDesc = "Selected by :" & returnfullname(ClientCrewID) & " Customer Name: " & ComboBoxCustomerName.Text
                GLOBAL_SYSTEM_LOGS(SystemLogType, SystemLogDesc)
                Me.Close()
            End If
        Else
            MessageBox.Show("Hold Order(s) or end the transaction first!", "Pending Orders", MessageBoxButtons.OK, MessageBoxIcon.Information)

        End If
    End Sub
    Private Sub ComboBoxCustomerName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxCustomerName.SelectedIndexChanged
        pendingloadorders()
    End Sub

    Private Sub ButtonKeyboard_Click(sender As Object, e As EventArgs) Handles ButtonKeyboard.Click
        ShowKeyboard()
    End Sub
End Class