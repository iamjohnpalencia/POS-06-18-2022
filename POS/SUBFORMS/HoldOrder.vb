Imports MySql.Data.MySqlClient
Public Class HoldOrder
    Dim RowsReturned As Integer
    Private Sub HoldOrder_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        POS.Enabled = True
    End Sub

    Private Sub ButtonHoldOrder_Click(sender As Object, e As EventArgs) Handles ButtonHoldOrder.Click
        If String.IsNullOrWhiteSpace(TextBoxCustomerName.Text) Then
            MsgBox("Input customer name first")
            TextBoxCustomerName.Clear()
        Else
            sql = "SELECT * FROM loc_pending_orders WHERE customer_name = '" & TextBoxCustomerName.Text & "'"
            cmd = New MySqlCommand(sql, LocalhostConn())
            RowsReturned = cmd.ExecuteScalar
            If RowsReturned > 0 Then
                MsgBox("Customer Exist")
            Else
                Try
                    For i As Integer = 0 To POS.DataGridViewOrders.Rows.Count - 1 Step +1
                        table = "loc_pending_orders"
                        fields = "(`crew_id`,`customer_name`,`product_name`,`product_quantity`,`product_price`,`product_total`,`product_id`,`product_sku`,`guid`,`active`, `increment`, `product_category`, `product_addon_id`, `ColumnSumID`, `ColumnInvID`, `Upgrade`, `Origin`, `addontype`)"
                        value = "('" & ClientCrewID & "'
                                , '" & Me.TextBoxCustomerName.Text & "'
                                , '" & POS.DataGridViewOrders.Rows(i).Cells(0).Value & "'
                                , " & POS.DataGridViewOrders.Rows(i).Cells(1).Value & "
                                , " & POS.DataGridViewOrders.Rows(i).Cells(2).Value & "
                                , " & POS.DataGridViewOrders.Rows(i).Cells(3).Value & "
                                , " & POS.DataGridViewOrders.Rows(i).Cells(5).Value & "
                                , '" & POS.DataGridViewOrders.Rows(i).Cells(6).Value & "'
                                , '" & ClientGuid & "'
                                , " & 1 & "
                                , " & POS.DataGridViewOrders.Rows(i).Cells(4).Value & "
                                , '" & POS.DataGridViewOrders.Rows(i).Cells(7).Value & "'
                                , " & POS.DataGridViewOrders.Rows(i).Cells(8).Value & "
                                , '" & POS.DataGridViewOrders.Rows(i).Cells(9).Value & "'
                                , " & POS.DataGridViewOrders.Rows(i).Cells(10).Value & "
                                , " & POS.DataGridViewOrders.Rows(i).Cells(11).Value & "
                                , '" & POS.DataGridViewOrders.Rows(i).Cells(12).Value & "'
                                , '" & POS.DataGridViewOrders.Rows(i).Cells(13).Value & "')"
                        GLOBAL_INSERT_FUNCTION(table:=table, fields:=fields, values:=value)
                    Next
                Catch ex As Exception
                    AuditTrail.LogToAuditTral("System", "Hold Order: " & ex.ToString, "Critical")

                    SendErrorReport(ex.ToString)
                End Try
                Try
                    For a As Integer = 0 To POS.DataGridViewInv.Rows.Count - 1 Step +1
                        table = "loc_hold_inventory"
                        fields = "(`sr_total`, `f_id`, `qty`, `id`, `nm`, `org_serve`, `name`, `cog`, `ocog`, `prd.addid`, `origin`)"
                        value = "(" & POS.DataGridViewInv.Rows(a).Cells(0).Value & " 
                        , " & POS.DataGridViewInv.Rows(a).Cells(1).Value & "
                        , " & POS.DataGridViewInv.Rows(a).Cells(2).Value & "
                        , " & POS.DataGridViewInv.Rows(a).Cells(3).Value & "
                        , '" & POS.DataGridViewInv.Rows(a).Cells(4).Value & "'
                        , " & POS.DataGridViewInv.Rows(a).Cells(5).Value & "
                        , '" & TextBoxCustomerName.Text & "'
                        , " & POS.DataGridViewInv.Rows(a).Cells(6).Value & "
                        , " & POS.DataGridViewInv.Rows(a).Cells(7).Value & "
                        , " & POS.DataGridViewInv.Rows(a).Cells(8).Value & "
                        , '" & POS.DataGridViewInv.Rows(a).Cells(9).Value & "')"
                        GLOBAL_INSERT_FUNCTION(table:=table, fields:=fields, values:=value)
                    Next
                Catch ex As Exception
                    AuditTrail.LogToAuditTral("System", "Hold Order: " & ex.ToString, "Critical")

                    SendErrorReport(ex.ToString)
                End Try
                Try
                    SystemLogType = "HOLD ORDER"
                    SystemLogDesc = "Customer name: " & TextBoxCustomerName.Text & " Item(s): " & POS.DataGridViewOrders.Rows.Count
                    GLOBAL_SYSTEM_LOGS(SystemLogType, SystemLogDesc)
                Catch ex As Exception
                    SendErrorReport(ex.ToString)
                End Try
                MsgBox("success")
                Me.Close()
                Me.TextBoxCustomerName.Clear()
                With POS
                    .DataGridViewInv.Rows.Clear()
                    .DataGridViewOrders.Rows.Clear()
                    .Buttonholdoder.Enabled = False
                    .ButtonPayMent.Enabled = False
                    .ButtonPendingOrders.Enabled = True
                End With
            End If
        End If
    End Sub
    Private Sub ButtonKeyboard_Click(sender As Object, e As EventArgs) Handles ButtonKeyboard.Click
        ShowKeyboard()
        Application.DoEvents()
        TextBoxCustomerName.Focus()
    End Sub

    Private Sub TextBoxCustomerName_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBoxCustomerName.KeyPress
        If InStr(DisallowedCharacters, e.KeyChar) > 0 Then
            e.Handled = True
        End If
    End Sub
End Class