Imports MySql.Data.MySqlClient
Module Updatemodule
    Public Sub GLOBAL_FUNCTION_UPDATE(ByVal table, ByVal fields, ByVal where)
        Try
            If LocalhostConn.State <> ConnectionState.Open Then
                LocalhostConn.Open()
            End If
            Dim sql = "UPDATE " + table + " SET " + fields + " WHERE " & where
            Console.WriteLine(sql)
            Dim cmd As MySqlCommand = New MySqlCommand(sql, LocalhostConn)
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Update Module: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        Finally
            LocalhostConn.Close()
            cmd.Dispose()
        End Try
    End Sub

    Public Sub UpdateInventory(returnVoid As Boolean)
        Dim secondary_value As Double = 0
        Dim stock_secondary As Double = 0
        Dim SqlCommand As MySqlCommand
        Dim SqlAdapter As MySqlDataAdapter
        Dim SqlDt As DataTable
        Dim UpdateInventoryCon As MySqlConnection = LocalhostConn()
        Try
            Dim Query As String = ""
            For i As Integer = 0 To INVENTORY_DATATABLE.Rows.Count - 1 Step +1
                Dim TotalQuantity As Double = 0
                Dim TotalServingValue As Double = 0
                Dim Secondary As Double = 0
                Dim ServingValue As Double = 0
                Dim TotalPrimary As Double = 0
                TotalQuantity = INVENTORY_DATATABLE(i)(2)
                TotalServingValue = Double.Parse(INVENTORY_DATATABLE(i)(0).ToString)
                If INVENTORY_DATATABLE(i)(9).ToString = "Server" Then
                    Query = "SELECT `secondary_value` FROM `loc_product_formula` WHERE server_formula_id = " & INVENTORY_DATATABLE(i)(1)
                Else
                    Query = "SELECT `secondary_value` FROM `loc_product_formula` WHERE formula_id = " & INVENTORY_DATATABLE(i)(1)
                End If
                SqlCommand = New MySqlCommand(Query, UpdateInventoryCon)
                SqlAdapter = New MySqlDataAdapter(SqlCommand)
                SqlDt = New DataTable
                SqlAdapter.Fill(SqlDt)
                For Each row As DataRow In SqlDt.Rows
                    secondary_value = row("secondary_value")
                Next
                If INVENTORY_DATATABLE(i)(9).ToString = "Server" Then
                    Query = "SELECT `stock_secondary` FROM `loc_pos_inventory` WHERE server_inventory_id = " & INVENTORY_DATATABLE(i)(1)
                Else
                    Query = "SELECT `stock_secondary` FROM `loc_pos_inventory` WHERE inventory_id = " & INVENTORY_DATATABLE(i)(1)
                End If
                SqlCommand = New MySqlCommand(Query, UpdateInventoryCon)
                SqlAdapter = New MySqlDataAdapter(SqlCommand)
                SqlDt = New DataTable
                SqlAdapter.Fill(SqlDt)
                For Each row As DataRow In SqlDt.Rows
                    stock_secondary = row("stock_secondary")
                Next
                If returnVoid Then
                    Secondary = stock_secondary + TotalServingValue
                Else
                    Secondary = stock_secondary - TotalServingValue
                End If
                ServingValue = Secondary / Double.Parse(INVENTORY_DATATABLE(i)(5).ToString)
                TotalPrimary = Secondary / secondary_value
                If INVENTORY_DATATABLE(i)(9).ToString = "Server" Then
                    Query = "UPDATE loc_pos_inventory SET stock_secondary = " & Secondary & " , stock_no_of_servings = " & ServingValue & " , stock_primary = " & TotalPrimary & ", date_modified = '" & FullDate24HR() & "' WHERE server_inventory_id = " & INVENTORY_DATATABLE(i)(1)
                Else
                    Query = "UPDATE loc_pos_inventory SET stock_secondary = " & Secondary & " , stock_no_of_servings = " & ServingValue & " , stock_primary = " & TotalPrimary & ", date_modified = '" & FullDate24HR() & "' WHERE inventory_id = " & INVENTORY_DATATABLE(i)(1)
                End If

                SqlCommand = New MySqlCommand(Query, UpdateInventoryCon)
                SqlCommand.ExecuteNonQuery()
            Next
            UpdateInventoryCon.Close()
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Update Module: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
End Module
