Imports System.Management
Imports System.Management.Instrumentation
Imports System
Imports System.IO
Imports MySql.Data.MySqlClient
Module DeleteModule
    Public Sub GLOBAL_DELETE_ALL_FUNCTION(ByVal tablename As String, ByVal where As String)
        Try
            sql = "DELETE FROM " & tablename & " WHERE " & where
            With cmd
                .Connection = LocalhostConn()
                .CommandText = sql
            End With
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Delete Module: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Public Sub TruncateTableAll(ToTruncate)
        Try
            Dim ConnectionLocal As MySqlConnection = LocalhostConn()
            Dim Query As String = "TRUNCATE TABLE  " & ToTruncate & " ;"
            Dim cmd As MySqlCommand = New MySqlCommand(Query, ConnectionLocal)
            cmd.ExecuteNonQuery()
            ConnectionLocal.Close()
            cmd.Dispose()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Public Sub AutoMaticResetPOS()
        Try
            Dim ConnectionLocal As MySqlConnection = LocalhostConn()
            Dim Query As String = "SELECT COUNT(transaction_id) as SalesCount FROM loc_daily_transaction"
            Dim Command As MySqlCommand = New MySqlCommand(Query, ConnectionLocal)
            Dim SalesCount As Double = Command.ExecuteScalar

            If SalesCount = 9999999999.99 Then
                Dim array() As String = {"loc_coupon_data", "loc_daily_transaction", "loc_daily_transaction_details", "loc_deposit", "loc_expense_details", "loc_expense_list",
            "loc_fm_stock", "loc_hold_inventory", "loc_inv_temp_data", "loc_pending_orders", "loc_refund_return_details", "loc_senior_details",
            "loc_system_logs", "loc_send_bug_report", "loc_transaction_mode_details", "loc_transfer_data", "loc_zread_inventory", "loc_admin_category", "loc_admin_products",
            "loc_partners_transaction", "loc_pos_inventory", "loc_product_formula", "tbcoupon", "loc_cash_breakdown", "loc_customer_info"}

                Dim counterValue = 0
                Dim ReturnBool As Boolean = False
                Query = "SELECT counter_value FROM `tbcountertable` WHERE counter_id = 1"
                cmd = New MySqlCommand(Query, ConnectionLocal)
                Using reader As MySqlDataReader = cmd.ExecuteReader
                    If reader.HasRows Then
                        ReturnBool = True
                        While reader.Read
                            counterValue = reader("counter_value")
                        End While
                    Else
                        ReturnBool = False
                    End If
                End Using

                For Each value As String In array
                    TruncateTableAll(value)
                Next

                Query = "UPDATE `loc_settings` SET S_Old_Grand_Total = 0 WHERE settings_id = 1"
                cmd = New MySqlCommand(Query, ConnectionLocal)
                cmd.ExecuteNonQuery()
                S_OLDGRANDTOTAL = 0

                Query = "UPDATE `loc_settings` SET S_Trn_No = 0 WHERE settings_id = 1"
                cmd = New MySqlCommand(Query, ConnectionLocal)
                cmd.ExecuteNonQuery()
                S_TRANSACTION_NUMBER = 0

                Query = "UPDATE `loc_settings` SET S_SI_No = 0 WHERE settings_id = 1"
                cmd = New MySqlCommand(Query, ConnectionLocal)
                cmd.ExecuteNonQuery()
                S_SI_NUMBER = 0

                If ReturnBool Then
                    counterValue += 1
                    Query = "UPDATE `tbcountertable` SET counter_value = '" & counterValue & "' WHERE counter_id = 1"
                    cmd = New MySqlCommand(Query, ConnectionLocal)
                    cmd.ExecuteNonQuery()
                Else
                    counterValue = 1
                    Query = "INSERT INTO `tbcountertable` (counter_value, date_created) VALUES ('" & counterValue & "', '" & FullDate24HR() & "')"
                    cmd = New MySqlCommand(Query, ConnectionLocal)
                    cmd.ExecuteNonQuery()
                End If

                My.Settings.zcounter = 0
                My.Settings.Save()
                ConnectionLocal.Close()
                cmd.Dispose()

                AuditTrail.LogToAuditTral("System", "Automatic Reset POS", "Normal")

                FormIsOpen()

                If Application.OpenForms().OfType(Of MDIFORM).Any Then
                    MDIFORM.Close()
                    If Application.OpenForms().OfType(Of POS).Any Then
                        POS.Close()
                        SystemLogDesc = "User Logout: " & returnfullname(where:=ClientCrewID)
                        SystemLogType = "LOG OUT"
                        GLOBAL_SYSTEM_LOGS(SystemLogType, SystemLogDesc)
                        EndBalance()
                        Login.Show()
                    End If
                End If

                AuditTrail.LogToAuditTral("System", "System Recalibrated, Reset initialized. Success!", "Critical")
            End If

        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Automatic Reset POS: : " & ex.ToString, "Critical")
        End Try
    End Sub
End Module
