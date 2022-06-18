Imports MySql.Data.MySqlClient
Module Addmodule
    Public Sub GLOBAL_SYSTEM_LOGS(ByVal logtype As String, ByVal logdesc As String)
        Try
            If ClientCrewID = "" Then
                ClientCrewID = 0
            End If
            Dim ConnectionLocal As MySqlConnection = LocalhostConn()
            Dim Query As String = "INSERT INTO `loc_system_logs`(`log_type`,`crew_id`,`log_description`, `log_store`, `guid`, `loc_systemlog_id`, `synced`, `zreading`, `log_date_time`) VALUES (@1,@2,@3,@4,@5,@6,@7,@8,@9)"
            Dim Command As MySqlCommand = New MySqlCommand(Query, ConnectionLocal)
            Command.Parameters.Add("@1", MySqlDbType.VarChar).Value = logtype
            Command.Parameters.Add("@2", MySqlDbType.VarChar).Value = ClientCrewID
            Command.Parameters.Add("@3", MySqlDbType.Text).Value = logdesc
            Command.Parameters.Add("@4", MySqlDbType.VarChar).Value = ClientStoreID
            Command.Parameters.Add("@5", MySqlDbType.VarChar).Value = ClientGuid
            Command.Parameters.Add("@6", MySqlDbType.VarChar).Value = Format(Now, ("yyyyMMdd-HHmmss"))
            Command.Parameters.Add("@7", MySqlDbType.VarChar).Value = "Unsynced"
            If IsNothing(S_Zreading) Then
                Command.Parameters.Add("@8", MySqlDbType.VarChar).Value = ""
            Else
                Command.Parameters.Add("@8", MySqlDbType.VarChar).Value = S_Zreading
            End If
            Command.Parameters.Add("@9", MySqlDbType.Text).Value = FullDate24HR()
            Command.ExecuteNonQuery()
            ConnectionLocal.Close()
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Dim result As Integer
    Public Sub GLOBAL_INSERT_FUNCTION(ByVal table As String, ByVal fields As String, ByVal values As String)
        Try
            Dim ConnectionLocal As MySqlConnection = LocalhostConn()
            Dim Query As String = "INSERT INTO " + table + fields + " VALUES " + values
            Dim cmd As MySqlCommand = New MySqlCommand(Query, ConnectionLocal)
            cmd.ExecuteNonQuery()
            ConnectionLocal.Close()
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Public Sub SendErrorReport(MSG)

        Dim ConnectionLocal As MySqlConnection = LocalhostConn()
        'If ConnectionLocal.State = ConnectionState.Open Then
        Try
            Dim Query As String = "INSERT INTO `loc_send_bug_report`(`bug_desc`, `crew_id`, `guid`, `store_id`, `date_created`, `synced`) VALUES (@1,@2,@3,@4,@5,@6)"
            Dim Command As MySqlCommand = New MySqlCommand(Query, ConnectionLocal)
            Command.Parameters.Add("@1", MySqlDbType.Text).Value = MSG
            Command.Parameters.Add("@2", MySqlDbType.Text).Value = ClientCrewID
            Command.Parameters.Add("@3", MySqlDbType.Text).Value = ClientGuid
            Command.Parameters.Add("@4", MySqlDbType.Text).Value = ClientStoreID
            Command.Parameters.Add("@5", MySqlDbType.Text).Value = FullDate24HR()
            Command.Parameters.Add("@6", MySqlDbType.Text).Value = "Unsynced"
            Command.ExecuteNonQuery()
            ConnectionLocal.Close()

            MessageBox.Show("An error occured. Please contact the System Administrator", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            Console.WriteLine("Error: " & ex.ToString)
        End Try
        'Else
        '    MsgBox("Localhost connection is not valid.")
        'End If
    End Sub

    Public Sub AutoSyncSales()
        Try
            Dim ConnectionServer As MySqlConnection = New MySqlConnection
            ConnectionServer.ConnectionString = CloudConnectionString
            ConnectionServer.Open()
            Dim CommandServer As MySqlCommand

            Dim Connectionlocal As MySqlConnection = New MySqlConnection
            Connectionlocal.ConnectionString = LocalConnectionString
            Connectionlocal.Open()
            Dim Command As MySqlCommand

            Dim Query As String = "SELECT * FROM loc_daily_transaction WHERE synced = 'Unsynced' AND store_id = " & ClientStoreID & " AND guid = '" & ClientGuid & "'"
            Command = New MySqlCommand(Query, Connectionlocal)
            Dim DaSales As MySqlDataAdapter = New MySqlDataAdapter(command)
            Dim DtSales As DataTable = New DataTable
            DaSales.Fill(DtSales)

            Dim QueryLocal As String = ""


            For i As Integer = 0 To DtSales.Rows.Count - 1 Step +1
                If POS.POSWorkerCanceled = True Then
                    Exit For
                End If

                CommandServer = New MySqlCommand("INSERT INTO `Triggers_admin_daily_transaction`(`loc_transaction_id`, `transaction_number`, `grosssales`, `totaldiscount`, `amounttendered`, `change`, `amountdue`, `vatablesales`, `vatexemptsales`, `zeroratedsales`, `vatpercentage`, `lessvat`, `transaction_type`, `discount_type`, `totaldiscountedamount`, `si_number`, `crew_id`, `guid`, `active`, `store_id`, `created_at`, `shift`, `zreading`)
                                             VALUES (@1,@2,@3,@4,@5,@6,@7,@8,@9,@10,@11,@12,@13,@14,@15,@16,@17,@18,@19,@20,@21,@22,@23)", ConnectionServer)

                CommandServer.Parameters.Add("@1", MySqlDbType.Int64).Value = DtSales(i)(0)
                CommandServer.Parameters.Add("@2", MySqlDbType.VarChar).Value = DtSales(i)(1)
                CommandServer.Parameters.Add("@3", MySqlDbType.Decimal).Value = DtSales(i)(2)
                CommandServer.Parameters.Add("@4", MySqlDbType.Decimal).Value = DtSales(i)(3)
                CommandServer.Parameters.Add("@5", MySqlDbType.Decimal).Value = DtSales(i)(4)
                CommandServer.Parameters.Add("@6", MySqlDbType.Decimal).Value = DtSales(i)(5)
                CommandServer.Parameters.Add("@7", MySqlDbType.Decimal).Value = DtSales(i)(6)
                CommandServer.Parameters.Add("@8", MySqlDbType.Decimal).Value = DtSales(i)(7)
                CommandServer.Parameters.Add("@9", MySqlDbType.Decimal).Value = DtSales(i)(8)
                CommandServer.Parameters.Add("@10", MySqlDbType.Decimal).Value = DtSales(i)(9)
                CommandServer.Parameters.Add("@11", MySqlDbType.Decimal).Value = DtSales(i)(10)
                CommandServer.Parameters.Add("@12", MySqlDbType.Decimal).Value = DtSales(i)(11)
                CommandServer.Parameters.Add("@13", MySqlDbType.VarChar).Value = DtSales(i)(12)
                CommandServer.Parameters.Add("@14", MySqlDbType.Text).Value = DtSales(i)(13)
                CommandServer.Parameters.Add("@15", MySqlDbType.Decimal).Value = DtSales(i)(14)
                CommandServer.Parameters.Add("@16", MySqlDbType.Int64).Value = DtSales(i)(15)
                CommandServer.Parameters.Add("@17", MySqlDbType.VarChar).Value = DtSales(i)(16)
                CommandServer.Parameters.Add("@18", MySqlDbType.VarChar).Value = DtSales(i)(17)
                CommandServer.Parameters.Add("@19", MySqlDbType.VarChar).Value = DtSales(i)(18)
                CommandServer.Parameters.Add("@20", MySqlDbType.VarChar).Value = DtSales(i)(19)
                CommandServer.Parameters.Add("@21", MySqlDbType.Text).Value = DtSales(i)(20)
                CommandServer.Parameters.Add("@22", MySqlDbType.VarChar).Value = DtSales(i)(21)
                CommandServer.Parameters.Add("@23", MySqlDbType.Text).Value = DtSales(i)(22)
                CommandServer.ExecuteNonQuery()

                QueryLocal = "UPDATE loc_daily_transaction SET synced = 'Synced' WHERE transaction_number = '" & DtSales(i)(1) & "'"
                Command = New MySqlCommand(QueryLocal, Connectionlocal)
                Command.ExecuteNonQuery()
            Next

            ConnectionServer.Close()

            Connectionlocal.Close()

        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Add Module: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub

    Public Sub AutoSyncSalesDetails()
        Try
            Dim ConnectionServer As MySqlConnection = New MySqlConnection
            ConnectionServer.ConnectionString = CloudConnectionString
            ConnectionServer.Open()
            Dim CommandServer As MySqlCommand

            Dim Connectionlocal As MySqlConnection = New MySqlConnection
            Connectionlocal.ConnectionString = LocalConnectionString
            Connectionlocal.Open()
            Dim Command As MySqlCommand

            Dim Query As String = "SELECT * FROM loc_daily_transaction_details WHERE synced = 'Unsynced' AND store_id = " & ClientStoreID & " AND guid = '" & ClientGuid & "'"
            Command = New MySqlCommand(Query, Connectionlocal)
            Dim DaSales As MySqlDataAdapter = New MySqlDataAdapter(Command)
            Dim DtSales As DataTable = New DataTable
            DaSales.Fill(DtSales)

            Dim QueryLocal As String = ""


            For i As Integer = 0 To DtSales.Rows.Count - 1 Step +1
                If POS.POSWorkerCanceled = True Then
                    Exit For
                End If
                CommandServer = New MySqlCommand("INSERT INTO Triggers_admin_daily_transaction_details(`loc_details_id`, `product_id`, `product_sku`, `product_name`, `quantity`, `price`, `total`, `crew_id`
                                            , `transaction_number`, `active`, `created_at`, `guid`, `store_id`, `total_cost_of_goods`, `product_category` , `zreading`, `transaction_type`) 
                    VALUES (@a0, @a1, @a2, @a3, @a4, @a5, @a6, @a7, @a8, @a9, @a10, @a11, @a12, @a13, @a14, @a15, @a16)", ConnectionServer)
                CommandServer.Parameters.Add("@a0", MySqlDbType.Int64).Value = DtSales(i)(0)
                CommandServer.Parameters.Add("@a1", MySqlDbType.Int64).Value = DtSales(i)(1)
                CommandServer.Parameters.Add("@a2", MySqlDbType.VarChar).Value = DtSales(i)(2)
                CommandServer.Parameters.Add("@a3", MySqlDbType.VarChar).Value = DtSales(i)(3)
                CommandServer.Parameters.Add("@a4", MySqlDbType.Int64).Value = DtSales(i)(4)
                CommandServer.Parameters.Add("@a5", MySqlDbType.Decimal).Value = DtSales(i)(5)
                CommandServer.Parameters.Add("@a6", MySqlDbType.Decimal).Value = DtSales(i)(6)
                CommandServer.Parameters.Add("@a7", MySqlDbType.VarChar).Value = DtSales(i)(7)
                CommandServer.Parameters.Add("@a8", MySqlDbType.VarChar).Value = DtSales(i)(8)
                CommandServer.Parameters.Add("@a9", MySqlDbType.Int64).Value = DtSales(i)(9)
                CommandServer.Parameters.Add("@a10", MySqlDbType.VarChar).Value = DtSales(i)(10)
                CommandServer.Parameters.Add("@a11", MySqlDbType.VarChar).Value = DtSales(i)(11)
                CommandServer.Parameters.Add("@a12", MySqlDbType.VarChar).Value = DtSales(i)(12)
                CommandServer.Parameters.Add("@a13", MySqlDbType.Decimal).Value = DtSales(i)(13)
                CommandServer.Parameters.Add("@a14", MySqlDbType.VarChar).Value = DtSales(i)(14)
                CommandServer.Parameters.Add("@a15", MySqlDbType.VarChar).Value = DtSales(i)(15)
                CommandServer.Parameters.Add("@a16", MySqlDbType.VarChar).Value = DtSales(i)(16)


                QueryLocal = "UPDATE loc_daily_transaction_details SET synced = 'Synced' WHERE details_id = '" & DtSales(i)(0) & "'"
                Command = New MySqlCommand(QueryLocal, Connectionlocal)
                Command.ExecuteNonQuery()
            Next

            ConnectionServer.Close()

            Connectionlocal.Close()

        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Add Module: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub

    Public Sub AutoSyncInventory()
        Try
            Dim CommandServer As MySqlCommand
            Dim CommandLocal As MySqlCommand

            Dim ConnectionServer As MySqlConnection = New MySqlConnection
            ConnectionServer.ConnectionString = CloudConnectionString
            ConnectionServer.Open()

            Dim ConnectionLocal As MySqlConnection = New MySqlConnection
            ConnectionLocal.ConnectionString = LocalConnectionString
            ConnectionLocal.Open()

            Dim Query As String = "SELECT * FROM loc_pos_inventory WHERE store_id = " & ClientStoreID & " AND guid = '" & ClientGuid & "'"
            CommandLocal = New MySqlCommand(Query, ConnectionLocal)
            Dim DaInventory As MySqlDataAdapter = New MySqlDataAdapter(CommandLocal)
            Dim DtInventory As DataTable = New DataTable
            DaInventory.Fill(DtInventory)

            Dim QueryLocal As String = ""

            With DtInventory
                Dim DateSynced As String = ""
                DateSynced = FullDate24HR()
                For i As Integer = 0 To .Rows.Count - 1 Step +1
                    If POS.POSWorkerCanceled = True Then
                        Exit For
                    End If

                    CommandServer = New MySqlCommand("INSERT INTO Triggers_admin_pos_inventory( `loc_inventory_id`, `store_id`, `formula_id`, `product_ingredients`, `sku`, `stock_primary`, `stock_secondary`, `stock_no_of_servings`, `stock_status`, `critical_limit`, `guid`, `date`)
                                             VALUES (@0, @1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11)", ConnectionServer)
                    CommandServer.Parameters.Add("@0", MySqlDbType.Int64).Value = DtInventory(i)(0)
                    CommandServer.Parameters.Add("@1", MySqlDbType.VarChar).Value = DtInventory(i)(1)
                    CommandServer.Parameters.Add("@2", MySqlDbType.Int64).Value = DtInventory(i)(2)
                    CommandServer.Parameters.Add("@3", MySqlDbType.VarChar).Value = DtInventory(i)(3)
                    CommandServer.Parameters.Add("@4", MySqlDbType.VarChar).Value = DtInventory(i)(4)
                    CommandServer.Parameters.Add("@5", MySqlDbType.Double).Value = DtInventory(i)(5)
                    CommandServer.Parameters.Add("@6", MySqlDbType.Double).Value = DtInventory(i)(6)
                    CommandServer.Parameters.Add("@7", MySqlDbType.Double).Value = DtInventory(i)(7)
                    CommandServer.Parameters.Add("@8", MySqlDbType.Int64).Value = DtInventory(i)(8)
                    CommandServer.Parameters.Add("@9", MySqlDbType.Int64).Value = DtInventory(i)(9)
                    CommandServer.Parameters.Add("@10", MySqlDbType.VarChar).Value = DtInventory(i)(10)
                    CommandServer.Parameters.Add("@11", MySqlDbType.Text).Value = DateSynced
                    CommandServer.ExecuteNonQuery()

                    CommandServer = New MySqlCommand("UPDATE admin_pos_inventory SET `stock_primary` = @1, `stock_secondary`= @2, `stock_no_of_servings` = @3, `stock_status` = @4, `critical_limit`= @5, `date` = @6 WHERE loc_inventory_id = " & DtInventory(i)(0).Value & " AND guid = '" & ClientGuid & "' AND store_id = '" & ClientStoreID & "'", ConnectionServer)
                    CommandServer.Parameters.Add("@1", MySqlDbType.Double).Value = DtInventory(i)(5)
                    CommandServer.Parameters.Add("@2", MySqlDbType.Double).Value = DtInventory(i)(6)
                    CommandServer.Parameters.Add("@3", MySqlDbType.Double).Value = DtInventory(i)(7)
                    CommandServer.Parameters.Add("@4", MySqlDbType.Int64).Value = DtInventory(i)(8)
                    CommandServer.Parameters.Add("@5", MySqlDbType.Int64).Value = DtInventory(i)(9)
                    CommandServer.Parameters.Add("@6", MySqlDbType.Text).Value = DateSynced
                    CommandServer.ExecuteNonQuery()

                    Dim sql = "UPDATE loc_pos_inventory SET `synced`='Synced' WHERE inventory_id = " & DtInventory(i)(0)
                    CommandLocal = New MySqlCommand(sql, ConnectionLocal)
                    CommandLocal.ExecuteNonQuery()
                Next
                ConnectionServer.Close()
                ConnectionLocal.Close()

            End With
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Add Module: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
#Region "Save XML Info"
    Public Sub SaveXMLInfo(xmlName As String)
        Try
            Dim ConnectionLocal As MySqlConnection = LocalhostConn()
            Dim Query As String = "INSERT INTO `loc_xml_ref`(`xml_name`, `zreading`, `created_by`, `created_at`, `status`) VALUES (@1,@2,@3,@4,@5)"
            Dim Command As MySqlCommand = New MySqlCommand(Query, ConnectionLocal)
            Command.Parameters.Add("@1", MySqlDbType.Text).Value = xmlName
            Command.Parameters.Add("@2", MySqlDbType.Text).Value = S_Zreading
            Command.Parameters.Add("@3", MySqlDbType.Text).Value = ClientCrewID
            Command.Parameters.Add("@4", MySqlDbType.Text).Value = FullDate24HR()
            Command.Parameters.Add("@5", MySqlDbType.Text).Value = 1
            Command.ExecuteNonQuery()
            ConnectionLocal.Close()

        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Add Module: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try

    End Sub
#End Region
#Region "Install Updates"
    Public Sub InstallUpdatesCategory(FromPosUpdate As Integer)
        Try
            Dim Connection As MySqlConnection = LocalhostConn()
            Dim cmdlocal As MySqlCommand
            With UPDATE_CATEGORY_DATATABLE
                For i As Integer = 0 To .Rows.Count - 1 Step +1
                    Dim sql = "SELECT category_id FROM loc_admin_category WHERE category_id = " & UPDATE_CATEGORY_DATATABLE(i)(0)
                    cmdlocal = New MySqlCommand(sql, Connection)
                    Dim result As Integer = cmdlocal.ExecuteScalar
                    If result = 0 Then
                        sql = "INSERT INTO `loc_admin_category`(`category_name`, `brand_name`, `updated_at`, `origin`, `status`) VALUES (@0,@1,@2,@3,@4)"
                    Else
                        sql = "UPDATE `loc_admin_category` SET `category_name`=@0,`brand_name`=@1,`updated_at`=@2,`origin`=@3,`status`=@4 WHERE category_id = " & UPDATE_CATEGORY_DATATABLE(i)(0).Value
                    End If
                    cmdlocal = New MySqlCommand(sql, Connection)
                    cmdlocal.Parameters.Add("@0", MySqlDbType.VarChar).Value = UPDATE_CATEGORY_DATATABLE(i)(1)
                    cmdlocal.Parameters.Add("@1", MySqlDbType.VarChar).Value = UPDATE_CATEGORY_DATATABLE(i)(2)
                    cmdlocal.Parameters.Add("@2", MySqlDbType.VarChar).Value = UPDATE_CATEGORY_DATATABLE(i)(3)
                    cmdlocal.Parameters.Add("@3", MySqlDbType.VarChar).Value = UPDATE_CATEGORY_DATATABLE(i)(4)
                    cmdlocal.Parameters.Add("@4", MySqlDbType.Int64).Value = UPDATE_CATEGORY_DATATABLE(i)(5)
                    cmdlocal.ExecuteNonQuery()
                    If FromPosUpdate = 0 Then
                        Loading.Instance.Invoke(Sub()
                                                    Loading.ProgressBar1.Value += 1
                                                    'CheckingForUpdates.Label1.Text = CheckingForUpdates.ProgressBar1.Value
                                                End Sub)
                    ElseIf FromPosUpdate = 1 Then
                        CheckingForUpdates.Instance.Invoke(Sub()
                                                               CheckingForUpdates.ProgressBar1.Value += 1
                                                               CheckingForUpdates.Label1.Text = CheckingForUpdates.ProgressBar1.Value
                                                           End Sub)
                    End If
                Next
            End With
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Add Module: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub

    Public Sub InstallUpdatesPartners(FromPosUpdate As Integer)
        Try
            Dim Connection As MySqlConnection = LocalhostConn()
            Dim cmdlocal As MySqlCommand
            With UPDATE_PARTNERS_DATATABLE
                For i As Integer = 0 To .Rows.Count - 1 Step +1
                    Dim sql = "SELECT id FROM loc_partners_transaction WHERE id = " & UPDATE_PARTNERS_DATATABLE(i)(0)
                    cmdlocal = New MySqlCommand(sql, Connection)
                    Dim result As Integer = cmdlocal.ExecuteScalar
                    If result = 0 Then
                        sql = "INSERT INTO `loc_partners_transaction`(`arrid`, `bankname`, `date_modified`, `crew_id`, `store_id`, `guid`, `active`, `synced`) VALUES (@0,@1,@2,@3,@4,@5,@6,@7)"
                    Else
                        sql = "UPDATE `loc_partners_transaction` SET `arrid`=@0,`bankname`=@1,`date_modified`=@2,`crew_id`=@3,`store_id`=@4,`guid`=@5,`active`=@6,`synced`=@7 WHERE id = " & UPDATE_PARTNERS_DATATABLE(i)(0)
                    End If
                    cmdlocal = New MySqlCommand(sql, Connection)
                    cmdlocal.Parameters.Add("@0", MySqlDbType.Int64).Value = UPDATE_PARTNERS_DATATABLE(i)(1)
                    cmdlocal.Parameters.Add("@1", MySqlDbType.VarChar).Value = UPDATE_PARTNERS_DATATABLE(i)(2)
                    cmdlocal.Parameters.Add("@2", MySqlDbType.Text).Value = UPDATE_PARTNERS_DATATABLE(i)(3)
                    cmdlocal.Parameters.Add("@3", MySqlDbType.VarChar).Value = "Admin"
                    cmdlocal.Parameters.Add("@4", MySqlDbType.VarChar).Value = ClientStoreID
                    cmdlocal.Parameters.Add("@5", MySqlDbType.VarChar).Value = ClientGuid
                    cmdlocal.Parameters.Add("@6", MySqlDbType.Int64).Value = UPDATE_PARTNERS_DATATABLE(i)(4)
                    cmdlocal.Parameters.Add("@7", MySqlDbType.VarChar).Value = "Synced"
                    cmdlocal.ExecuteNonQuery()
                    If FromPosUpdate = 0 Then
                        Loading.Instance.Invoke(Sub()
                                                    Loading.ProgressBar1.Value += 1
                                                    'CheckingForUpdates.Label1.Text = CheckingForUpdates.ProgressBar1.Value
                                                End Sub)
                    ElseIf FromPosUpdate = 1 Then
                        CheckingForUpdates.Instance.Invoke(Sub()
                                                               CheckingForUpdates.ProgressBar1.Value += 1
                                                               CheckingForUpdates.Label1.Text = CheckingForUpdates.ProgressBar1.Value
                                                           End Sub)
                    End If
                Next
            End With
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Add Module: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub

    Public Sub InstallUpdatesCoupons(FromPosUpdate As Integer)
        Try
            Dim Connection As MySqlConnection = LocalhostConn()
            Dim cmdlocal As MySqlCommand
            With UPDATE_COUPONS_DATATABLE
                For i As Integer = 0 To .Rows.Count - 1 Step +1
                    Dim sql = "SELECT ID FROM tbcoupon WHERE ID = " & UPDATE_COUPONS_DATATABLE(i)(0)
                    cmdlocal = New MySqlCommand(sql, Connection)
                    Dim result As Integer = cmdlocal.ExecuteScalar
                    If result = 0 Then
                        sql = "INSERT INTO `tbcoupon`(`Couponname_`, `Desc_`, `Discountvalue_`, `Referencevalue_`, `Type`, `Bundlebase_`, `BBValue_`, `Bundlepromo_`, `BPValue_`, `Effectivedate`, `Expirydate`, `date_created`, `active`, `store_id`, `crew_id`, `guid`, `origin`, `synced`) VALUES (@0,@1,@2,@3,@4,@5,@6,@7,@8,@9,@10,@11,@12,@13,@14,@15,@16,@17)"
                    Else
                        sql = "UPDATE `tbcoupon` SET `Couponname_` = @0, `Desc_` = @1, `Discountvalue_` = @2, `Referencevalue_` = @3, `Type` = @4, `Bundlebase_` = @5, `BBValue_` = @6, `Bundlepromo_` = @7, `BPValue_` = @8, `Effectivedate` = @9, `Expirydate` = @10, `date_created` = @11, `active` = @12, `store_id` = @13, `crew_id` = @14, `guid` = @15, `origin` = @16, `synced` = @17 WHERE ID = " & UPDATE_COUPONS_DATATABLE(i)(0)
                    End If
                    Dim Crew As String = ""
                    If FromPosUpdate = 1 Then
                        Crew = ClientCrewID
                    Else
                        Crew = "0"
                    End If

                    cmdlocal = New MySqlCommand(sql, Connection)
                    cmdlocal.Parameters.Add("@0", MySqlDbType.Text).Value = UPDATE_COUPONS_DATATABLE(i)(1)
                    cmdlocal.Parameters.Add("@1", MySqlDbType.Text).Value = UPDATE_COUPONS_DATATABLE(i)(2)
                    cmdlocal.Parameters.Add("@2", MySqlDbType.Text).Value = UPDATE_COUPONS_DATATABLE(i)(3)
                    cmdlocal.Parameters.Add("@3", MySqlDbType.Text).Value = UPDATE_COUPONS_DATATABLE(i)(4)
                    cmdlocal.Parameters.Add("@4", MySqlDbType.Text).Value = UPDATE_COUPONS_DATATABLE(i)(5)
                    cmdlocal.Parameters.Add("@5", MySqlDbType.Text).Value = UPDATE_COUPONS_DATATABLE(i)(6)
                    cmdlocal.Parameters.Add("@6", MySqlDbType.Text).Value = UPDATE_COUPONS_DATATABLE(i)(7)
                    cmdlocal.Parameters.Add("@7", MySqlDbType.Text).Value = UPDATE_COUPONS_DATATABLE(i)(8)
                    cmdlocal.Parameters.Add("@8", MySqlDbType.Text).Value = UPDATE_COUPONS_DATATABLE(i)(9)
                    cmdlocal.Parameters.Add("@9", MySqlDbType.Text).Value = UPDATE_COUPONS_DATATABLE(i)(10)
                    cmdlocal.Parameters.Add("@10", MySqlDbType.Text).Value = UPDATE_COUPONS_DATATABLE(i)(11)
                    cmdlocal.Parameters.Add("@11", MySqlDbType.Text).Value = UPDATE_COUPONS_DATATABLE(i)(12)
                    cmdlocal.Parameters.Add("@12", MySqlDbType.Text).Value = UPDATE_COUPONS_DATATABLE(i)(13)
                    cmdlocal.Parameters.Add("@13", MySqlDbType.Text).Value = ClientStoreID
                    cmdlocal.Parameters.Add("@14", MySqlDbType.Text).Value = Crew
                    cmdlocal.Parameters.Add("@15", MySqlDbType.Text).Value = ClientGuid
                    cmdlocal.Parameters.Add("@16", MySqlDbType.Text).Value = "Server"
                    cmdlocal.Parameters.Add("@17", MySqlDbType.Text).Value = "Synced"
                    cmdlocal.ExecuteNonQuery()
                    If FromPosUpdate = 0 Then
                        Loading.Instance.Invoke(Sub()
                                                    Loading.ProgressBar1.Value += 1
                                                    'CheckingForUpdates.Label1.Text = CheckingForUpdates.ProgressBar1.Value
                                                End Sub)
                    ElseIf FromPosUpdate = 1 Then
                        CheckingForUpdates.Instance.Invoke(Sub()
                                                               CheckingForUpdates.ProgressBar1.Value += 1
                                                               CheckingForUpdates.Label1.Text = CheckingForUpdates.ProgressBar1.Value
                                                           End Sub)
                    End If
                Next
            End With
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Add Module: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Public Sub InstallUpdatesFormula(FromPosUpdate As Integer)
        Try
            Dim Connection As MySqlConnection = LocalhostConn()
            Dim cmdlocal As MySqlCommand
            With UPDATE_FORMULA_DATATABLE
                For i As Integer = 0 To .Rows.Count - 1 Step +1
                    Dim sql = "SELECT formula_id FROM loc_product_formula WHERE formula_id = " & UPDATE_FORMULA_DATATABLE(i)(0)
                    cmdlocal = New MySqlCommand(sql, Connection)
                    Dim result As Integer = cmdlocal.ExecuteScalar
                    If result = 0 Then
                        sql = "INSERT INTO loc_product_formula (`server_formula_id`,`product_ingredients`, `primary_unit`, `primary_value`, `secondary_unit`, `secondary_value`, `serving_unit`, `serving_value`, `no_servings`, `status`, `date_modified`, `unit_cost`, `origin`, `store_id`, `guid`, `crew_id`, `server_date_modified`) VALUES
                                        (@0 ,@1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11 , @12 , @13 , @14, @15, @16)"
                    Else
                        sql = "UPDATE `loc_product_formula` SET `server_formula_id`= @0,`product_ingredients`= @1,`primary_unit`= @2,`primary_value`= @3,`secondary_unit`= @4,`secondary_value`=@5,`serving_unit`=@6,`serving_value`=@7,`no_servings`=@8,`status`=@9,`date_modified`=@10,`unit_cost`=@11,`origin`=@12,`store_id`=@13,`guid`=@14,`crew_id`=@15,`server_date_modified`=@16 WHERE server_formula_id =  " & UPDATE_FORMULA_DATATABLE(i)(0)
                    End If
                    cmdlocal = New MySqlCommand(sql, Connection)
                    cmdlocal.Parameters.Add("@0", MySqlDbType.Int64).Value = UPDATE_FORMULA_DATATABLE(i)(0)
                    cmdlocal.Parameters.Add("@1", MySqlDbType.VarChar).Value = UPDATE_FORMULA_DATATABLE(i)(1)
                    cmdlocal.Parameters.Add("@2", MySqlDbType.VarChar).Value = UPDATE_FORMULA_DATATABLE(i)(2)
                    cmdlocal.Parameters.Add("@3", MySqlDbType.VarChar).Value = UPDATE_FORMULA_DATATABLE(i)(3)
                    cmdlocal.Parameters.Add("@4", MySqlDbType.VarChar).Value = UPDATE_FORMULA_DATATABLE(i)(4)
                    cmdlocal.Parameters.Add("@5", MySqlDbType.VarChar).Value = UPDATE_FORMULA_DATATABLE(i)(5)
                    cmdlocal.Parameters.Add("@6", MySqlDbType.VarChar).Value = UPDATE_FORMULA_DATATABLE(i)(6)
                    cmdlocal.Parameters.Add("@7", MySqlDbType.VarChar).Value = UPDATE_FORMULA_DATATABLE(i)(7)
                    cmdlocal.Parameters.Add("@8", MySqlDbType.VarChar).Value = UPDATE_FORMULA_DATATABLE(i)(8)
                    cmdlocal.Parameters.Add("@9", MySqlDbType.Int64).Value = UPDATE_FORMULA_DATATABLE(i)(9)
                    cmdlocal.Parameters.Add("@10", MySqlDbType.VarChar).Value = UPDATE_FORMULA_DATATABLE(i)(10)
                    cmdlocal.Parameters.Add("@11", MySqlDbType.Decimal).Value = UPDATE_FORMULA_DATATABLE(i)(11)
                    cmdlocal.Parameters.Add("@12", MySqlDbType.VarChar).Value = UPDATE_FORMULA_DATATABLE(i)(12)
                    cmdlocal.Parameters.Add("@13", MySqlDbType.VarChar).Value = ClientStoreID
                    cmdlocal.Parameters.Add("@14", MySqlDbType.VarChar).Value = ClientGuid
                    cmdlocal.Parameters.Add("@15", MySqlDbType.VarChar).Value = "0"
                    cmdlocal.Parameters.Add("@16", MySqlDbType.Text).Value = UPDATE_FORMULA_DATATABLE(i)(10)
                    cmdlocal.ExecuteNonQuery()
                    If FromPosUpdate = 0 Then
                        Loading.Instance.Invoke(Sub()
                                                    Loading.ProgressBar1.Value += 1
                                                    'CheckingForUpdates.Label1.Text = CheckingForUpdates.ProgressBar1.Value
                                                End Sub)
                    ElseIf FromPosUpdate = 1 Then
                        CheckingForUpdates.Instance.Invoke(Sub()
                                                               CheckingForUpdates.ProgressBar1.Value += 1
                                                               CheckingForUpdates.Label1.Text = CheckingForUpdates.ProgressBar1.Value
                                                           End Sub)
                    End If
                Next
            End With
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Add Module: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Public Sub InstallUpdatesInventory(FromPosUpdate As Integer)
        Try
            Dim Connection As MySqlConnection = LocalhostConn()
            Dim cmdlocal As MySqlCommand
            With UPDATE_INVENTORY_DATATABLE
                For i As Integer = 0 To .Rows.Count - 1 Step +1
                    Dim sql = "SELECT inventory_id FROM loc_pos_inventory WHERE inventory_id = " & UPDATE_INVENTORY_DATATABLE(i)(0)
                    cmdlocal = New MySqlCommand(sql, Connection)
                    Dim result As Integer = cmdlocal.ExecuteScalar
                    If result = 0 Then
                        sql = "INSERT INTO loc_pos_inventory (`server_inventory_id`,`formula_id`,`product_ingredients`,`sku`,`stock_primary`,`stock_secondary`,`stock_no_of_servings`,`stock_status`,`critical_limit`,`date_modified`,`server_date_modified`,`store_id`,`crew_id`,`guid`,`synced`,`main_inventory_id`,`origin`,`show_stockin`) VALUES
                                        (@0 ,@1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11, @12, @13, @14, @15, @16, @17)"
                    Else
                        sql = "UPDATE `loc_pos_inventory` SET `server_inventory_id`= @0,`formula_id`=@1,`product_ingredients`=@2,`sku`=@3,`stock_status`=@7,`critical_limit`=@8,`date_modified`=@9,`server_date_modified`=@10,`store_id`=@11,`crew_id`=@12,`guid`=@13,`synced`=@14,`main_inventory_id`=@15,`origin`=@16,`show_stockin`= @17 WHERE `server_inventory_id`= " & UPDATE_INVENTORY_DATATABLE(i)(0)
                    End If
                    cmdlocal = New MySqlCommand(sql, Connection)
                    cmdlocal.Parameters.Add("@0", MySqlDbType.Int64).Value = UPDATE_INVENTORY_DATATABLE(i)(0)
                    cmdlocal.Parameters.Add("@1", MySqlDbType.Int64).Value = UPDATE_INVENTORY_DATATABLE(i)(1)
                    cmdlocal.Parameters.Add("@2", MySqlDbType.VarChar).Value = UPDATE_INVENTORY_DATATABLE(i)(2)
                    cmdlocal.Parameters.Add("@3", MySqlDbType.VarChar).Value = UPDATE_INVENTORY_DATATABLE(i)(3)
                    cmdlocal.Parameters.Add("@4", MySqlDbType.Decimal).Value = UPDATE_INVENTORY_DATATABLE(i)(4)
                    cmdlocal.Parameters.Add("@5", MySqlDbType.Decimal).Value = UPDATE_INVENTORY_DATATABLE(i)(5)
                    cmdlocal.Parameters.Add("@6", MySqlDbType.Decimal).Value = UPDATE_INVENTORY_DATATABLE(i)(6)
                    cmdlocal.Parameters.Add("@7", MySqlDbType.Int64).Value = UPDATE_INVENTORY_DATATABLE(i)(7)
                    cmdlocal.Parameters.Add("@8", MySqlDbType.Int64).Value = UPDATE_INVENTORY_DATATABLE(i)(8)
                    cmdlocal.Parameters.Add("@9", MySqlDbType.Text).Value = UPDATE_INVENTORY_DATATABLE(i)(9)
                    cmdlocal.Parameters.Add("@10", MySqlDbType.Text).Value = UPDATE_INVENTORY_DATATABLE(i)(9)
                    cmdlocal.Parameters.Add("@11", MySqlDbType.VarChar).Value = ClientStoreID
                    cmdlocal.Parameters.Add("@12", MySqlDbType.VarChar).Value = "0"
                    cmdlocal.Parameters.Add("@13", MySqlDbType.VarChar).Value = ClientGuid
                    cmdlocal.Parameters.Add("@14", MySqlDbType.VarChar).Value = "Synced"
                    cmdlocal.Parameters.Add("@15", MySqlDbType.VarChar).Value = UPDATE_INVENTORY_DATATABLE(i)(10)
                    cmdlocal.Parameters.Add("@16", MySqlDbType.Text).Value = UPDATE_INVENTORY_DATATABLE(i)(11)
                    cmdlocal.Parameters.Add("@17", MySqlDbType.Int64).Value = UPDATE_INVENTORY_DATATABLE(i)(12)
                    cmdlocal.ExecuteNonQuery()
                    If FromPosUpdate = 0 Then
                        Loading.Instance.Invoke(Sub()
                                                    Loading.ProgressBar1.Value += 1
                                                    'CheckingForUpdates.Label1.Text = CheckingForUpdates.ProgressBar1.Value
                                                End Sub)
                    ElseIf FromPosUpdate = 1 Then
                        CheckingForUpdates.Instance.Invoke(Sub()
                                                               CheckingForUpdates.ProgressBar1.Value += 1
                                                               CheckingForUpdates.Label1.Text = CheckingForUpdates.ProgressBar1.Value
                                                           End Sub)
                    End If
                Next
            End With
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Add Module: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Public Sub InstallUpdatesProducts(FromPosUpdate As Integer)
        Try
            Dim Connection As MySqlConnection = LocalhostConn()
            Dim cmdlocal As MySqlCommand
            With UPDATE_PRODUCTS_DATATABLE
                For i As Integer = 0 To .Rows.Count - 1 Step +1
                    Dim sql = "SELECT product_id FROM loc_admin_products WHERE server_product_id = " & UPDATE_PRODUCTS_DATATABLE(i)(0)
                    cmdlocal = New MySqlCommand(sql, Connection)
                    Dim result As Integer = cmdlocal.ExecuteScalar
                    If result = 0 Then
                        sql = "INSERT INTO loc_admin_products (`server_product_id`, `product_sku`, `product_name`, `formula_id`, `product_barcode`, `product_category`, `product_price`, `product_desc`, `product_image`, `product_status`, `origin`, `date_modified`, `server_inventory_id`, `guid`, `store_id`, `crew_id`, `synced`, `addontype`, `half_batch`, `partners`, `arrangement`) VALUES
                                        (@0 ,@1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11, @12, @13, @14, @15, @16, @17, @18, @19, @20)"
                    Else
                        sql = "UPDATE `loc_admin_products` SET `server_product_id`=@0,`product_sku`=@1,`product_name`=@2,`formula_id`=@3,`product_barcode`=@4,`product_category`=@5,`product_price`=@6,`product_desc`=@7,`product_image`=@8,`product_status`=@9,`origin`=@10,`date_modified`=@11,`server_inventory_id`=@12,`guid`=@13,`store_id`=@14,`crew_id`=@15,`synced`=@16,`addontype`=@17,`half_batch`=@18, `partners`=@19, `arrangement`=@20 WHERE server_product_id =  " & UPDATE_PRODUCTS_DATATABLE(i)(0)
                    End If
                    cmdlocal = New MySqlCommand(sql, Connection)
                    cmdlocal.Parameters.Add("@0", MySqlDbType.Int64).Value = UPDATE_PRODUCTS_DATATABLE(i)(0)
                    cmdlocal.Parameters.Add("@1", MySqlDbType.VarChar).Value = UPDATE_PRODUCTS_DATATABLE(i)(1)
                    cmdlocal.Parameters.Add("@2", MySqlDbType.VarChar).Value = UPDATE_PRODUCTS_DATATABLE(i)(2)
                    cmdlocal.Parameters.Add("@3", MySqlDbType.VarChar).Value = UPDATE_PRODUCTS_DATATABLE(i)(3)
                    cmdlocal.Parameters.Add("@4", MySqlDbType.VarChar).Value = UPDATE_PRODUCTS_DATATABLE(i)(4)
                    cmdlocal.Parameters.Add("@5", MySqlDbType.VarChar).Value = UPDATE_PRODUCTS_DATATABLE(i)(5)
                    cmdlocal.Parameters.Add("@6", MySqlDbType.Int64).Value = UPDATE_PRODUCTS_DATATABLE(i)(6)
                    cmdlocal.Parameters.Add("@7", MySqlDbType.VarChar).Value = UPDATE_PRODUCTS_DATATABLE(i)(7)
                    cmdlocal.Parameters.Add("@8", MySqlDbType.VarChar).Value = UPDATE_PRODUCTS_DATATABLE(i)(8)
                    cmdlocal.Parameters.Add("@9", MySqlDbType.VarChar).Value = UPDATE_PRODUCTS_DATATABLE(i)(9)
                    cmdlocal.Parameters.Add("@10", MySqlDbType.VarChar).Value = UPDATE_PRODUCTS_DATATABLE(i)(10)
                    cmdlocal.Parameters.Add("@11", MySqlDbType.VarChar).Value = UPDATE_PRODUCTS_DATATABLE(i)(11)
                    cmdlocal.Parameters.Add("@12", MySqlDbType.Text).Value = UPDATE_PRODUCTS_DATATABLE(i)(12)
                    cmdlocal.Parameters.Add("@13", MySqlDbType.VarChar).Value = ClientGuid
                    cmdlocal.Parameters.Add("@14", MySqlDbType.Int64).Value = ClientStoreID
                    cmdlocal.Parameters.Add("@15", MySqlDbType.VarChar).Value = "0"
                    cmdlocal.Parameters.Add("@16", MySqlDbType.VarChar).Value = "Synced"
                    cmdlocal.Parameters.Add("@17", MySqlDbType.Text).Value = UPDATE_PRODUCTS_DATATABLE(i)(13)
                    cmdlocal.Parameters.Add("@18", MySqlDbType.Int64).Value = UPDATE_PRODUCTS_DATATABLE(i)(14)
                    cmdlocal.Parameters.Add("@19", MySqlDbType.Text).Value = UPDATE_PRODUCTS_DATATABLE(i)(15)
                    cmdlocal.Parameters.Add("@20", MySqlDbType.Text).Value = UPDATE_PRODUCTS_DATATABLE(i)(16)
                    cmdlocal.ExecuteNonQuery()
                    If UPDATE_WORKER_CANCEL = False Then
                        If FromPosUpdate = 0 Then
                            Loading.Instance.Invoke(Sub()
                                                        Loading.ProgressBar1.Value += 1
                                                        'CheckingForUpdates.Label1.Text = CheckingForUpdates.ProgressBar1.Value
                                                    End Sub)
                        ElseIf FromPosUpdate = 1 Then
                            CheckingForUpdates.Instance.Invoke(Sub()
                                                                   CheckingForUpdates.ProgressBar1.Value += 1
                                                                   CheckingForUpdates.Label1.Text = CheckingForUpdates.ProgressBar1.Value
                                                               End Sub)
                        End If
                    End If
                Next
            End With
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Add Module: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Public Sub InstallUpdatesPriceChange()
        Try
            Dim ConnectionLocal As MySqlConnection = LocalhostConn()
            Dim ConnectionServer As MySqlConnection = ServerCloudCon()
            Dim CmdCheck As MySqlCommand
            For i As Integer = 0 To UPDATE_PRICE_CHANGE_DATATABLE.Rows.Count - 1 Step +1
                Dim sql = "UPDATE loc_admin_products SET product_price = " & UPDATE_PRICE_CHANGE_DATATABLE(i)(4) & ", price_change = 1 WHERE server_product_id = " & UPDATE_PRICE_CHANGE_DATATABLE(i)(3) & ""
                CmdCheck = New MySqlCommand(sql, ConnectionLocal)
                CmdCheck.ExecuteNonQuery()
                Dim sql2 = "UPDATE loc_price_request_change SET active = 2 WHERE request_id = " & UPDATE_PRICE_CHANGE_DATATABLE(i)(0) & ""
                CmdCheck = New MySqlCommand(sql2, ConnectionLocal)
                CmdCheck.ExecuteNonQuery()
                Dim sq3 = "UPDATE admin_price_request SET synced = 'Synced' WHERE request_id = " & UPDATE_PRICE_CHANGE_DATATABLE(i)(0) & ""
                CmdCheck = New MySqlCommand(sq3, ConnectionServer)
                CmdCheck.ExecuteNonQuery()
                GLOBAL_SYSTEM_LOGS("UPDATE PRICE CHANGE", "Store ID: " & ClientStoreID & ", Product ID: " & UPDATE_PRICE_CHANGE_DATATABLE(i)(3))
            Next
            ConnectionLocal.Close()
            ConnectionServer.Close()
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Add Module: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Public Sub InstallCoupons()
        Try
            Dim ConnectionLocal As MySqlConnection = LocalhostConn()
            Dim ConnectionServer As MySqlConnection = ServerCloudCon()
            Dim CmdCheck As MySqlCommand
            For i As Integer = 0 To UPDATE_COUPON_APPROVAL_DATATABLE.Rows.Count - 1 Step +1
                Dim sql = "UPDATE tbcoupon SET active = 1 WHERE ID = " & UPDATE_COUPON_APPROVAL_DATATABLE(i)(0) & ""
                CmdCheck = New MySqlCommand(sql, ConnectionLocal)
                CmdCheck.ExecuteNonQuery()
                Dim sql2 = "UPDATE admin_custom_coupon SET synced = 'Synced' WHERE ID = " & UPDATE_COUPON_APPROVAL_DATATABLE(i)(0) & ""
                CmdCheck = New MySqlCommand(sql2, ConnectionServer)
                CmdCheck.ExecuteNonQuery()
                GLOBAL_SYSTEM_LOGS("UPDATE CUSTOM COUPON", "Store ID: " & ClientStoreID & ", Coupon ID: " & UPDATE_COUPON_APPROVAL_DATATABLE(i)(0))
            Next
            ConnectionLocal.Close()
            ConnectionServer.Close()
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Add Module: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Public Sub InstallProducts()
        Try
            Dim ConnectionLocal As MySqlConnection = LocalhostConn()
            Dim ConnectionServer As MySqlConnection = ServerCloudCon()
            Dim CmdCheck As MySqlCommand
            For i As Integer = 0 To UPDATE_CUSTOM_PROD_APP_DATATABLE.Rows.Count - 1 Step +1
                Dim sql = "UPDATE loc_admin_products SET product_status = 1 WHERE product_id = " & UPDATE_CUSTOM_PROD_APP_DATATABLE(i)(0) & ""
                CmdCheck = New MySqlCommand(sql, ConnectionLocal)
                CmdCheck.ExecuteNonQuery()

                Dim sql1 = "UPDATE loc_product_formula SET status = 1 WHERE formula_id = " & UPDATE_CUSTOM_PROD_APP_DATATABLE(i)(1) & ""
                CmdCheck = New MySqlCommand(sql1, ConnectionLocal)
                CmdCheck.ExecuteNonQuery()

                Dim sql0 = "UPDATE loc_pos_inventory SET stock_status = 1 WHERE formula_id = " & UPDATE_CUSTOM_PROD_APP_DATATABLE(i)(1) & ""
                CmdCheck = New MySqlCommand(sql0, ConnectionLocal)
                CmdCheck.ExecuteNonQuery()

                Dim sql2 = "UPDATE loc_product_list SET synced = 'Synced' WHERE loc_product_id = " & UPDATE_CUSTOM_PROD_APP_DATATABLE(i)(0) & ""
                CmdCheck = New MySqlCommand(sql2, ConnectionServer)
                CmdCheck.ExecuteNonQuery()
                GLOBAL_SYSTEM_LOGS("UPDATE CUSTOM PRODUCTS", "Store ID: " & ClientStoreID & ", Product ID: " & UPDATE_CUSTOM_PROD_APP_DATATABLE(i)(0))
            Next
            ConnectionLocal.Close()
            ConnectionServer.Close()
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Add Module: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
#End Region
End Module
