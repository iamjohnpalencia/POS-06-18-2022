Imports MySql.Data.MySqlClient
Public Class NewStockEntry
    Private Sub NewStockEntry_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loadcomboboxingredients()
        TopMost = True
    End Sub
    Private Sub ButtonENTRYADDSTOCK_Click(sender As Object, e As EventArgs) Handles ButtonENTRYADDSTOCK.Click
        Try

            If Val(TextBoxEQuantity.Text) > 0 Then
                Dim Primary As Double = Double.Parse(TextBoxEQuantity.Text) * Double.Parse(TextBoxEFPrimaryVal.Text)
                Dim Secondary As Double = Double.Parse(TextBoxEQuantity.Text) * Double.Parse(TextBoxEFSecondVal.Text)
                Dim NoOfServings As Double = Double.Parse(TextBoxEQuantity.Text) * Double.Parse(TextBoxENoServings.Text)
                Dim ConnectionLocal As MySqlConnection = LocalhostConn()
                Dim SQL = "SELECT `stock_primary`, `stock_secondary`, `stock_no_of_servings`, `formula_id` FROM  `loc_pos_inventory` WHERE `inventory_id` = " & TextBox1.Text
                Dim SQLCmd As MySqlCommand = New MySqlCommand(SQL, ConnectionLocal)
                Dim SQlDa As MySqlDataAdapter = New MySqlDataAdapter(SQLCmd)
                Dim InvDT As DataTable = New DataTable
                SQlDa.Fill(InvDT)

                Dim InvPrimary As Double = 0
                Dim InvSecondary As Double = 0
                Dim InvServings As Double = 0
                Dim formula_id As Integer = 0
                For Each row As DataRow In InvDT.Rows
                    InvPrimary = row("stock_primary")
                    InvSecondary = row("stock_secondary")
                    InvServings = row("stock_no_of_servings")
                    formula_id = row("formula_id")
                Next

                Dim TotalPrimary As Double = Primary + InvPrimary
                Dim TotalSecondary As Double = Secondary + InvSecondary
                Dim TotalNoOfServings As Double = NoOfServings + InvServings

                SystemLogType = "STOCK ENTRY"
                SystemLogDesc = "Adding stock of: " & ComboBoxDESC.Text & ", Quantity(Primary): " & TextBoxEQuantity.Text & ", Crew : " & ClientCrewID
                GLOBAL_SYSTEM_LOGS(SystemLogType, SystemLogDesc)

                Dim table = "loc_pos_inventory"
                Dim fields = "`stock_primary`=" & TotalPrimary & ",`stock_secondary`= " & TotalSecondary & " , `stock_no_of_servings`= " & TotalNoOfServings & ", `date_modified` = '" & FullDate24HR() & "'"
                Dim where = ""

                If formula_id = 0 Then
                    where = "server_inventory_id = " & TextBox1.Text
                Else
                    where = "formula_id = " & TextBox1.Text
                End If

                AuditTrail.LogToAuditTral("Transaction", "Menu/Inventory/Stock Entry: " & TextBoxEQuantity.Text & " " & ComboBoxDESC.Text, "Normal")

                TextBoxEQuantity.Clear()
                GLOBAL_FUNCTION_UPDATE(table, fields, where)
                MDIFORM.newMDIchildInventory.loadstockentry(False)
                MDIFORM.newMDIchildInventory.loadinventory()
                MDIFORM.newMDIchildInventory.loadstockadjustmentreport(False)
                MDIFORM.newMDIchildInventory.loadcriticalstocks()

            Else
                MsgBox("Quantity(Primary) must be greater than 0")
            End If
            MDIFORM.LabelTotalAvailStock.Text = roundsum("stock_primary", "loc_pos_inventory WHERE store_id = " & ClientStoreID & " AND guid = '" & ClientGuid & "'", "P")
            MDIFORM.LabelTotalCrititems.Text = count(table:="loc_pos_inventory WHERE stock_status = 1 AND critical_limit >= stock_primary AND store_id ='" & ClientStoreID & "' AND guid = '" & ClientGuid & "'", tocount:="inventory_id")

        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "New Stock Entry: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub

    Private Sub ComboBoxDESC_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxDESC.SelectedIndexChanged
        Try
            Dim sql = "SELECT inventory_id, stock_primary, stock_secondary, origin FROM loc_pos_inventory WHERE product_ingredients = '" & ComboBoxDESC.Text & "'"
            Dim cmd As MySqlCommand = New MySqlCommand(sql, LocalhostConn)
            Dim da As MySqlDataAdapter = New MySqlDataAdapter(cmd)
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            TextBoxEPrimary.Text = dt(0)(1)
            TextBoxESecondary.Text = dt(0)(2)
            TextBox1.Text = dt(0)(0)
            SelectFormulaEntry(dt(0)(0), dt(0)(3))
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "New Stock Entry: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub SelectFormulaEntry(FormulaID, Origin)
        Try
            Dim sql As String = ""
            If Origin = "Local" Then
                sql = "SELECT `primary_unit`, `primary_value`, `secondary_unit`, `secondary_value`, `serving_unit`, `serving_value`, `no_servings` FROM loc_product_formula WHERE formula_id = " & FormulaID
            ElseIf Origin = "Server" Then
                sql = "SELECT `primary_unit`, `primary_value`, `secondary_unit`, `secondary_value`, `serving_unit`, `serving_value`, `no_servings` FROM loc_product_formula WHERE server_formula_id = " & FormulaID
            End If
            Dim cmd As MySqlCommand = New MySqlCommand(sql, LocalhostConn)
            Dim da As MySqlDataAdapter = New MySqlDataAdapter(cmd)
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            For Each row As DataRow In dt.Rows
                TextBoxEFPrimaryVal.Text = row("primary_value")
                TextBoxEFPUnit.Text = row("primary_unit")

                TextBoxEFSecondVal.Text = row("secondary_value")
                TextBoxEFSUnit.Text = row("secondary_unit")

                TextBoxEServingValue.Text = row("serving_value")
                TextBoxEServingVal.Text = row("serving_unit")
                TextBoxENoServings.Text = row("no_servings")
            Next
            LocalhostConn.Close()
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "New Stock Entry: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Sub loadcomboboxingredients()
        Try
            Dim sql = "SELECT product_ingredients FROM loc_pos_inventory WHERE main_inventory_id = 0 AND stock_status = 1  AND main_inventory_id = 0 AND show_stockin = 0 ORDER BY product_ingredients ASC"
            Dim cmd As MySqlCommand = New MySqlCommand(sql, LocalhostConn)
            Dim da As MySqlDataAdapter = New MySqlDataAdapter(cmd)
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            For i As Integer = 0 To dt.Rows.Count - 1 Step +1
                ComboBoxDESC.Items.Add(dt(i)(0))
            Next
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "New Stock Entry: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub ButtonKeyboard_Click(sender As Object, e As EventArgs) Handles ButtonKeyboard.Click
        ShowKeyboard()
    End Sub

    Private Sub NewStockEntry_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        MDIFORM.newMDIchildInventory.Enabled = True
    End Sub

    Private Sub TextBoxEQuantity_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBoxEQuantity.KeyPress
        Try
            Numeric(sender, e)
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub
End Class