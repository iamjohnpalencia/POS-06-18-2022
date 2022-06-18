Imports MySql.Data.MySqlClient
Imports System.Threading
Public Class SettingsForm
    Public AddOrEdit As Boolean
    Dim Partners As Boolean = False
    Dim Formula As Boolean = False
    Dim Returns As Boolean = False
    Dim Coupons As Boolean = False
    Dim Updates As Boolean = False

    Dim AutoBackupBoolean As Boolean = False
    Dim PrintOptionsBoolean As Boolean = False

    Private Shared _instance As SettingsForm

    Public ReadOnly Property Instance As SettingsForm
        Get
            Return _instance
        End Get
    End Property

    Private Sub SettingsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            _instance = Me

            LabelCheckingUpdates.Text = "Last Checked: " & My.Settings.Updatedatetime

            TabControl1.TabPages(0).Text = "General Settings"
            TabControl1.TabPages(1).Text = "Partners Transaction"
            TabControl1.TabPages(2).Text = "Formula"
            TabControl1.TabPages(3).Text = "Item Refund"
            TabControl1.TabPages(4).Text = "Coupon Settings"
            TabControl1.TabPages(5).Text = "Reset"
            TabControl1.TabPages(6).Text = "Updates"

            TabControl2.TabPages(0).Text = "Connection Settings"
            TabControl2.TabPages(1).Text = "Database Settings"
            TabControl1.TabPages(7).Text = "Other Settings"
            TabControl1.TabPages(8).Text = "Receipt"

            TabControl4.TabPages(0).Text = "Create Coupon"
            TabControl4.TabPages(1).Text = "Coupon List"

            TabControl5.TabPages(0).Text = "Available Coupon"
            TabControl5.TabPages(1).Text = "Pending (For admin approval)"
            LoadConn()
            LoadCloudConn()
            LoadAdditionalSettings()
            LoadDevInfo()
            LoadAutoBackup()
            LoadPrintOptions()
            LoadTrainingMode()
            LoadLedDisplaySettings()
            LabelResetStatus.Text = LoadSystemResetStatus()
            If LabelResetStatus.Text = "" Then
                LabelResetStatus.Text = "0"
            End If

            With DataGridViewITEMRETURN1
                .Columns(0).HeaderText = "Sales Invoice #"
                .Columns(1).HeaderText = "Cash"
                .Columns(2).HeaderText = "Discount"
                .Columns(3).HeaderText = "Change"
                .Columns(4).HeaderText = "Crew"
                .Columns(0).Width = 100
                .Columns(5).Visible = False
                .Columns(6).Visible = False
                .Columns(7).Visible = False
                .Columns(8).Visible = False
                .Columns(9).Visible = False
            End With

            If ClientRole = "Admin" Then
                ButtonTestLocCon.Enabled = True
                ButtonTestCloudCon.Enabled = True
                ButtonImport.Enabled = True
                ButtonExport.Enabled = True
            End If
            If ClientRole <> "Admin" Then
                TabControl1.TabPages.Remove(TabControl1.TabPages(5))
                TabControl1.TabPages.Remove(TabControl1.TabPages(7))
                AutoBackupBoolean = False
                PrintOptionsBoolean = False
                GroupBox19.Enabled = False
                RadioButtonYES.Enabled = False
                RadioButtonNO.Enabled = False
                NumericUpDownPrintCount.Enabled = False
                ComboBoxPrintSize.Enabled = False
                TextBoxS_ZeroRated.Enabled = False
                ButtonSaveZeroRated.Enabled = False
            Else
                GroupBox19.Enabled = True
                AutoBackupBoolean = True
                PrintOptionsBoolean = True
                RadioButtonYES.Enabled = True
                RadioButtonNO.Enabled = True
            End If
            If ClientRole = "Crew" Then
                ButtonPartnersPrio.Visible = False
                ButtonAddBank.Visible = False
                ButtonDeactivateBank.Visible = False
                ButtonEditBank.Visible = False
                ButtonPTActivate.Visible = False
                ButtonChangeFormula.Visible = False

                ButtonMaintenance.Visible = False
                ButtonOptimizeDB.Visible = False
                ButtonImport.Enabled = False
                ButtonExport.Enabled = False
                ToolStripButtonSelectP.Visible = False
                ToolStripButtonClear.Visible = False
                ButtonCHelp.Visible = False
                ButtonSaveCoupon.Visible = False
            End If
            FillDgvReset()

            With DataGridViewReset
                '.ColumnHeadersVisible = False
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

            TextBoxSyncInterval.Text = My.Settings.S_Sync_Interval
            ComboBoxPrintSize.SelectedItem = My.Settings.PrintSize
            If My.Settings.S_Auto_Sync Then
                RadioButtonAutoSyncOn.Checked = True
            Else
                RadioButtonAutoSyncOff.Checked = True
            End If

            If My.Settings.Auto_Update Then
                RadioButtonAutoUpdateOn.Checked = True
            Else
                RadioButtonAutoUpdateOff.Checked = True
            End If
            TextBoxS_ZeroRated.Text = S_ZeroRated_Tax
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Settings Form: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub FillDgvReset()
        Try
            DataGridViewReset.ColumnCount = 1
            DataGridViewReset.Columns(0).HeaderText = "Category"
            DataGridViewReset.Columns(0).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            Dim row As String() = New String() {"Sales  /Senior citizen  data/ Transaction mode details"}
            DataGridViewReset.Rows.Add(row)
            row = New String() {"Deposits data"}
            DataGridViewReset.Rows.Add(row)
            row = New String() {"Expenses"}
            DataGridViewReset.Rows.Add(row)
            row = New String() {"Fast moving stocks"}
            DataGridViewReset.Rows.Add(row)
            row = New String() {"Categories"}
            DataGridViewReset.Rows.Add(row)
            row = New String() {"Message"}
            DataGridViewReset.Rows.Add(row)
            row = New String() {"Inventory temp data"}
            DataGridViewReset.Rows.Add(row)

            row = New String() {"Pending orders"}
            DataGridViewReset.Rows.Add(row)
            row = New String() {"Inventory"}
            DataGridViewReset.Rows.Add(row)
            row = New String() {"Price request data"}
            DataGridViewReset.Rows.Add(row)
            row = New String() {"Formula"}
            DataGridViewReset.Rows.Add(row)
            row = New String() {"Returns"}
            DataGridViewReset.Rows.Add(row)
            row = New String() {"Error logs"}
            DataGridViewReset.Rows.Add(row)
            row = New String() {"Stock adjustment categories"}
            DataGridViewReset.Rows.Add(row)
            row = New String() {"System logs"}
            DataGridViewReset.Rows.Add(row)
            row = New String() {"Transfer inventory data"}
            DataGridViewReset.Rows.Add(row)

            row = New String() {"Users/ Triggers"}
            DataGridViewReset.Rows.Add(row)
            row = New String() {"Zread inventory"}
            DataGridViewReset.Rows.Add(row)
            row = New String() {"Coupons"}
            DataGridViewReset.Rows.Add(row)
            row = New String() {"Products/ Triggers	"}
            DataGridViewReset.Rows.Add(row)
            row = New String() {"Coupon data"}
            DataGridViewReset.Rows.Add(row)
            row = New String() {"Partners transaction list"}
            DataGridViewReset.Rows.Add(row)

            Dim chk As New DataGridViewCheckBoxColumn()
            DataGridViewReset.Columns.Add(chk)
            chk.Name = "ChkBox"
            chk.HeaderText = "Select"
            chk.Width = 100

        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Settings Form: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub LoadLedDisplaySettings()
        Try
            GetPorts(ComboBoxComPort)
            If My.Settings.LedDisplayTrue Then
                TextBoxBaudRate.Text = My.Settings.SpBaudrate
            End If
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub SettingsForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            POS.Enabled = True
            If BackgroundWorkerLocalConnection.IsBusy Or BackgroundWorkerCloudConnection.IsBusy Then
                If BackgroundWorkerLocalConnection.IsBusy Then
                    BackgroundWorkerLocalConnection.CancelAsync()
                End If
                If BackgroundWorkerCloudConnection.IsBusy Then
                    BackgroundWorkerCloudConnection.CancelAsync()
                End If
                CancelTestLocalCon = True
                CancelTestCloudCon = True
            End If
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        If TabControl1.SelectedIndex = 1 Then
            If Partners = False Then
                TabControl3.TabPages(0).Text = "Available Partners"
                TabControl3.TabPages(1).Text = "Deactivated Partners"
                LoadPartners()
                LoadPartnersDeact()
                Partners = True
            End If
        ElseIf TabControl1.SelectedIndex = 2 Then
            If Formula = False Then
                loadformula()
                Formula = True
            End If
        ElseIf TabControl1.SelectedIndex = 3 Then
            If Returns = False Then
                loaditemreturn(True)
                loadindexdgv()
                Returns = True
            End If
        ElseIf TabControl1.SelectedIndex = 4 Then
            If Coupons = False Then
                ShowAllCoupons()
                ShowAllCouponsPending()
                FillComboboxSearch()
                ToolStripComboBoxCategorySearch.SelectedIndex = 0
                ShowAllProducts("All")
                Coupons = True
            End If
        ElseIf TabControl1.SelectedIndex = 5 Then
            If My.Settings.Updatedatetime = "" Then
                LabelCheckingUpdates.Text = "Last Checked:  2020-06-01 11:12:30"
            Else
                LabelCheckingUpdates.Text = "Last Checked: " & My.Settings.Updatedatetime
            End If

        ElseIf TabControl1.SelectedIndex = 8 Then
            LoadHeader()
            LoadFooter()
            LoadOthers()
            LoadValidity()
            LoadRefundFooter()
        End If
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Try
            If CheckForInternetConnection() = True Then
                If POS.POSISUPDATING = False Then
                    HASUPDATE = False
                    PictureBox1.Visible = True
                    Timer1.Start()
                    DataGridView5.Rows.Clear()
                    Dim Products = count("product_id", "loc_admin_products")
                    Dim Category = count("category_id", "loc_admin_category")
                    Dim Inventory = count("inventory_id", "loc_pos_inventory")
                    Dim Formula = count("formula_id", "loc_product_formula")
                    Dim Coupons = count("ID", "tbcoupon")
                    Dim Partners = count("id", "loc_partners_transaction")

                    DataGridView5.Rows.Add(Products)
                    DataGridView5.Rows.Add(Category)
                    DataGridView5.Rows.Add(Inventory)
                    DataGridView5.Rows.Add(Formula)
                    DataGridView5.Rows.Add(Coupons)
                    DataGridView5.Rows.Add(Partners)

                    LabelCountAllRows.Text = SumOfColumnsToInt(DataGridView5, 0)
                    LabelCheckingUpdates.Text = "Checking for updates."
                    ProgressBar1.Maximum = Val(LabelCountAllRows.Text)
                    ProgressBar1.Value = 0
                    LabelNewRows.Text = 0
                    BackgroundWorker1.WorkerReportsProgress = True
                    BackgroundWorker1.WorkerSupportsCancellation = True
                    BackgroundWorker1.RunWorkerAsync()
                    Button4.Enabled = False
                    HASUPDATE = False
                Else
                    MsgBox("Updates is still on process please wait.")
                End If
            Else
                MsgBox("Internet connection is not available")
            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Settings Form: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If LabelCheckingUpdates.Text = "Checking for updates." Then
            LabelCheckingUpdates.Text = "Checking for updates.."
        ElseIf LabelCheckingUpdates.Text = "Checking for updates.." Then
            LabelCheckingUpdates.Text = "Checking for updates..."
        ElseIf LabelCheckingUpdates.Text = "Checking for updates..." Then
            LabelCheckingUpdates.Text = "Checking for updates"
        ElseIf LabelCheckingUpdates.Text = "Checking for updates" Then
            LabelCheckingUpdates.Text = "Checking for updates."
        End If
    End Sub
#Region "Partners"

    Public Sub LoadPartners()
        Try
            Dim ActiveRow
            Dim PartnersTableActive = AsDatatable("loc_partners_transaction WHERE active = 1 ORDER BY arrid ASC", "*", DataGridViewPartners)
            For Each row As DataRow In PartnersTableActive.Rows
                If row("active") = 1 Then
                    ActiveRow = "Active"
                Else
                    ActiveRow = "Deactivated"
                End If
                DataGridViewPartners.Rows.Add(row("id"), row("arrid"), row("bankname"), row("date_modified"), row("crew_id"), row("store_id"), row("guid"), ActiveRow, row("synced"))
            Next
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Settings Form: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Public Sub LoadPartnersDeact()
        Try
            Dim ActiveRow
            Dim PartnersTableDeactive = AsDatatable("loc_partners_transaction WHERE active = 0 ORDER BY arrid ASC", "*", DataGridViewPartnersDeact)
            For Each row As DataRow In PartnersTableDeactive.Rows
                If row("active") = 1 Then
                    ActiveRow = "Active"
                Else
                    ActiveRow = "Deactivated"
                End If
                DataGridViewPartnersDeact.Rows.Add(row("id"), row("arrid"), row("bankname"), row("date_modified"), row("crew_id"), row("store_id"), row("guid"), ActiveRow, row("synced"))
            Next
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Settings Form: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub ButtonDeleteProducts_Click(sender As Object, e As EventArgs) Handles ButtonDeactivateBank.Click
        Try
            If DataGridViewPartners.SelectedRows.Count < 1 Then
                MsgBox("Select bank first")
            ElseIf DataGridViewPartners.SelectedRows.Count > 1 Then
                MsgBox("Select one bank at a time")
            Else
                Dim BankID = DataGridViewPartners.SelectedRows(0).Cells(0).Value.ToString
                GLOBAL_FUNCTION_UPDATE("loc_partners_transaction", "active = 0", "id = " & BankID)
                LoadPartners()
                LoadPartnersDeact()
            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Settings Form: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub Button18_Click(sender As Object, e As EventArgs) Handles ButtonPTActivate.Click
        Try
            If DataGridViewPartnersDeact.SelectedRows.Count < 1 Then
                MsgBox("Select bank first")
            ElseIf DataGridViewPartnersDeact.SelectedRows.Count > 1 Then
                MsgBox("Select one bank at a time")
            Else
                Dim BankID = DataGridViewPartnersDeact.SelectedRows(0).Cells(0).Value.ToString
                GLOBAL_FUNCTION_UPDATE("loc_partners_transaction", "active = 1", "id = " & BankID)
                LoadPartners()
                LoadPartnersDeact()
            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Settings Form: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles ButtonPartnersPrio.Click
        Try
            If DataGridViewPartners.SelectedRows.Count < 1 Then
                MsgBox("Select bank first")
            ElseIf DataGridViewPartners.SelectedRows.Count > 1 Then
                MsgBox("Select one bank at a time")
            Else
                Dim ID = GLOBAL_RETURN_FUNCTION("loc_partners_transaction WHERE arrid = 1", "bankname", "bankname", True)
                Dim Arrid = GLOBAL_RETURN_FUNCTION("loc_partners_transaction WHERE id = " & DataGridViewPartners.SelectedRows(0).Cells(0).Value, "bankname", "bankname", True)
                table = "loc_partners_transaction"
                fields = "arrid = 1, synced = 'Unsynced'"
                where = "bankname = '" & Arrid & "'"
                GLOBAL_FUNCTION_UPDATE(table, fields, where)
                fields = "arrid = " & DataGridViewPartners.SelectedRows(0).Cells(1).Value & ", synced = 'Unsynced'"
                where = "bankname = '" & ID & "'"
                GLOBAL_FUNCTION_UPDATE(table, fields, where)
                LoadPartners()
            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Settings Form: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles ButtonAddBank.Click
        AddOrEdit = True
        Enabled = False
        AddBank.Show()
    End Sub
    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles ButtonEditBank.Click
        If DataGridViewPartners.SelectedRows.Count < 1 Then
            MsgBox("Select bank first")
        ElseIf DataGridViewPartners.SelectedRows.Count > 1 Then
            MsgBox("Select one bank at a time")
        Else
            AddOrEdit = False
            AddBank.TextBoxBankName.Text = DataGridViewPartners.SelectedRows(0).Cells(2).Value.ToString
            Enabled = False
            AddBank.Show()
        End If
    End Sub
#End Region
#Region "Formula"
    Public Sub loadformula()
        Try
            fields = "`product_ingredients`, `primary_unit`, `primary_value`, `secondary_unit`, `secondary_value`, `serving_unit`, ROUND(`serving_value`,0) as servval, ROUND(`no_servings`,0) as noofservings"
            GLOBAL_SELECT_ALL_FUNCTION(table:="loc_product_formula WHERE status = 1 AND store_id = '" & ClientStoreID & "' AND guid = '" & ClientGuid & "' ORDER BY product_ingredients ASC", datagrid:=DataGridViewFormula, fields:=fields)
            With DataGridViewFormula
                .Columns(0).HeaderText = "Ingredients"
                .Columns(1).HeaderText = "Primary Unit"
                .Columns(2).HeaderText = "Primary (Value)"
                .Columns(3).HeaderText = "Secondary Unit"
                .Columns(4).HeaderText = "Secondary (Value)"
                .Columns(5).HeaderText = "Srv. Unit"
                .Columns(6).HeaderText = "Srv. (Value)"
                .Columns(7).HeaderText = "No. of Serving"
                .Columns(0).Width = 150
                .Columns(2).Width = 70
            End With
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Settings Form: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
#End Region
#Region "Returns"
    Private Sub rowindex()
        LabelITEMRET.Text = DataGridViewITEMRETURN1.CurrentCell.RowIndex
    End Sub
    Private Sub LoadTrainingMode()
        Try
            If ClientRole <> "Admin" Then
                RadioButtonTrainingOFF.Enabled = False
                RadioButtonTraningON.Enabled = False
                RadioButtonInvResetOff.Enabled = False
                RadioButtonInvResetOn.Enabled = False
            End If
            If S_TrainingMode Then
                RadioButtonTraningON.Checked = True
            Else
                RadioButtonTrainingOFF.Checked = True
            End If
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub loaditemreturn(justload As Boolean)
        Try
            Dim Fields As String = "`transaction_number`, `amounttendered`, `totaldiscount`, `change`, `crew_id`, `vatablesales`, `vatexemptsales`, `zeroratedsales`, `lessvat`, `transaction_type`"
            Dim ReturnDT
            If justload Then
                FlowLayoutPanel1.Controls.Clear()
                ReturnDT = AsDatatable("`loc_daily_transaction` WHERE Date(created_at) = '" & S_Zreading & "' And `active` = 1 ORDER BY `transaction_id` DESC", Fields, DataGridViewITEMRETURN1)
            Else
                If String.IsNullOrWhiteSpace(TextBoxSearchTranNumber.Text) Then
                    FlowLayoutPanel1.Controls.Clear()
                    ReturnDT = AsDatatable("`loc_daily_transaction` WHERE Date(created_at) = '" & S_Zreading & "' And `active` = 1 ORDER BY `transaction_id` DESC", Fields, DataGridViewITEMRETURN1)
                Else
                    FlowLayoutPanel1.Controls.Clear()
                    ReturnDT = AsDatatable("`loc_daily_transaction` WHERE `transaction_number` Like '%" & TextBoxSearchTranNumber.Text & "%'  AND date(created_at) = '" & S_Zreading & "' AND `active` = 1 ORDER BY `transaction_id` DESC", Fields, DataGridViewITEMRETURN1)
                End If
            End If
            With DataGridViewITEMRETURN1
                For Each row As DataRow In ReturnDT.Rows
                    Dim CrewID = returnfullname(row("crew_id"))
                    DataGridViewITEMRETURN1.Rows.Add(row("transaction_number"), row("amounttendered"), row("totaldiscount"), row("change"), CrewID, row("vatablesales"), row("vatexemptsales"), row("zeroratedsales"), row("lessvat"), row("transaction_type"))
                Next

            End With
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Settings Form: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub DataGridViewITEMRETURN1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridViewITEMRETURN1.CellClick
        rowindex()
        loadtransactions()
    End Sub
    Dim productimage As String
    Dim grandtotal As Decimal
    Private Sub loadtransactions()
        Try
            Dim countrow As Integer = 0
            FlowLayoutPanel1.Controls.Clear()
            Dim sql = "SELECT product_id, product_name, quantity, price, total, product_sku FROM loc_daily_transaction_details WHERE transaction_number = '" & DataGridViewITEMRETURN1.SelectedRows(0).Cells(0).Value.ToString & "' AND active = 1"
            Dim query As String = "SELECT amountdue FROM loc_daily_transaction WHERE transaction_number = '" & DataGridViewITEMRETURN1.SelectedRows(0).Cells(0).Value.ToString & "'"
            Dim cmdquery As MySqlCommand = New MySqlCommand(query, LocalhostConn())
            Dim queryda As MySqlDataAdapter = New MySqlDataAdapter(cmdquery)
            Dim dt1 As DataTable = New DataTable
            queryda.Fill(dt1)
            Using readerObj As MySqlDataReader = cmdquery.ExecuteReader
                While readerObj.Read
                    grandtotal = readerObj("amountdue")
                End While
            End Using
            cmd = New MySqlCommand
            da = New MySqlDataAdapter(cmd)
            dt = New DataTable()
            With cmd
                .CommandText = sql
                .Connection = LocalhostConn()
                da.Fill(dt)
                For Each row As DataRow In dt.Rows
                    countrow += 1
                    Dim buttonname As String = row("product_name")
                    sql = "select product_image from loc_admin_products where product_id = " & row("product_id")
                    cmd = New MySqlCommand
                    With cmd
                        .CommandText = sql
                        .Connection = LocalhostConn()
                        Using readerobj As MySqlDataReader = cmd.ExecuteReader
                            While readerobj.Read
                                productimage = readerobj("product_image")
                            End While
                        End Using
                    End With
                    Dim drawproduct As New Panel
                    Dim myname As New Label
                    Dim myqty As New Label
                    Dim myprice As New Label
                    Dim mytotal As New Label
                    Dim myimage As New Button
                    With drawproduct
                        .Name = buttonname
                        .Text = buttonname
                        .BorderStyle = BorderStyle.None
                        .ForeColor = Color.White
                        .Font = New Font("kelson sans normal", 10)
                        .Width = 345
                        .Height = 140
                        .Cursor = Cursors.Hand
                        With myname
                            .Location = New Point(200, 10)
                            .Text = row("product_sku")
                            .ForeColor = Color.Black
                            .Font = New Font("kelson sans normal", 10, FontStyle.Bold)
                            .Width = 200
                        End With
                        With myqty
                            .Font = New Font("kelson sans normal", 10)
                            .Location = New Point(200, 35)
                            .Text = "Quantity: " & row("quantity")
                            .ForeColor = Color.Black
                            .Width = 200
                        End With
                        With myprice
                            .Font = New Font("kelson sans normal", 10)
                            .Location = New Point(200, 60)
                            .Text = "Price: " & row("price")
                            .ForeColor = Color.Black
                            .Width = 200
                        End With
                        With mytotal
                            .Font = New Font("kelson sans normal", 10)
                            .Location = New Point(200, 85)
                            .Text = "Total: " & row("total")
                            .ForeColor = Color.Black
                            .Width = 200
                        End With
                        With myimage
                            .Location = New Point(5, 5)
                            .Width = 166.5
                            .Height = 120
                            .BackgroundImage = Base64ToImage(productimage)
                            .BackgroundImageLayout = ImageLayout.Stretch
                            .FlatStyle = FlatStyle.Flat
                            .FlatAppearance.BorderSize = 0
                            .BackgroundImageLayout = ImageLayout.Stretch
                        End With
                    End With
                    drawproduct.Controls.Add(myname)
                    drawproduct.Controls.Add(myqty)
                    drawproduct.Controls.Add(myprice)
                    drawproduct.Controls.Add(mytotal)
                    drawproduct.Controls.Add(myimage)
                    FlowLayoutPanel1.Controls.Add(drawproduct)
                Next
                With DataGridViewITEMRETURN1
                    LabelIRTRANSNUM.Text = .SelectedRows(0).Cells(0).Value.ToString
                    LabelIRSUBTOTAL.Text = countrow & " item(s)"
                    LabelIRTYPE.Text = .SelectedRows(0).Cells(9).Value.ToString
                    LabelIRTOTAL.Text = grandtotal
                    LabelIRCASH.Text = .SelectedRows(0).Cells(1).Value.ToString
                    LabelIRCHANGE.Text = .SelectedRows(0).Cells(3).Value.ToString
                    LabelIRDISC.Text = .SelectedRows(0).Cells(2).Value.ToString
                    LabelIRVAT.Text = .SelectedRows(0).Cells(5).Value.ToString
                    LabelIRVATEX.Text = .SelectedRows(0).Cells(6).Value.ToString
                    LabelIRZERO.Text = .SelectedRows(0).Cells(7).Value.ToString
                    LabelIRINPUTVAT.Text = .SelectedRows(0).Cells(8).Value.ToString
                    ButtonRefund.Text = "Refund php " & grandtotal
                End With
            End With
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Settings Form: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub TextBoxSearchTranNumber_TextChanged(sender As Object, e As EventArgs) Handles TextBoxSearchTranNumber.TextChanged
        loaditemreturn(False)
        If DataGridViewITEMRETURN1.Rows.Count > 0 Then
            loadtransactions()
        End If
    End Sub
    Public Sub loadindexdgv()
        If DataGridViewITEMRETURN1.RowCount > 0 Then
            DataGridViewITEMRETURN1.ClearSelection()
            DataGridViewITEMRETURN1.CurrentCell = DataGridViewITEMRETURN1.Rows(Val(LabelITEMRET.Text)).Cells(0)
            DataGridViewITEMRETURN1.Rows(Val(LabelITEMRET.Text)).Selected = True
            loadtransactions()
        Else
            FlowLayoutPanel1.Controls.Clear()
        End If
    End Sub
    Private Sub ButtonRefund_Click(sender As Object, e As EventArgs) Handles ButtonRefund.Click
        Try
            If DataGridViewITEMRETURN1.Rows.Count > 0 Then
                Dim transaction_num As String = DataGridViewITEMRETURN1.SelectedRows(0).Cells(0).Value.ToString
                If String.IsNullOrWhiteSpace(TextBoxIRREASON.Text) Then
                    MessageBox.Show("Reason for refund is required!", "Refund", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    sql = "SELECT * FROM loc_daily_transaction WHERE created_at >= Now() - INTERVAL 10 MINUTE AND transaction_number = '" & transaction_num & "'"

                    Dim cmd As MySqlCommand = New MySqlCommand(sql, LocalhostConn)
                    Dim da As MySqlDataAdapter = New MySqlDataAdapter(cmd)
                    Dim dt As DataTable = New DataTable
                    da.Fill(dt)
                    If dt.Rows.Count > 0 Then
                        Dim refund As Integer = MessageBox.Show("Are you sure do you want to refund this transaction?", "Return and Refund", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                        If refund = DialogResult.Yes Then
                            Enabled = False
                            ConfirmRefund.TRANSACTIONNUMBER = transaction_num
                            ConfirmRefund.REFUNDTOTAL = grandtotal
                            ConfirmRefund.Show()
                        End If
                    Else
                        MessageBox.Show("This transaction is non refundable or exceed's the given period of time.", "Refund", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                End If
            Else
                MsgBox("Create transaction first")
            End If

        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Settings Form: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Public Sub INSERTRETURNS(transaction_num As String)
        Try
            fields = "(`transaction_number`, `reason`, `total`, `guid`, `store_id`, `crew_id`, `synced`, `zreading`, `created_at`)"
            value = "('" & transaction_num & "'
                            , '" & TextBoxIRREASON.Text & "'
                            , " & grandtotal & "
                            , '" & ClientGuid & "'
                            , '" & ClientStoreID & "'
                            , '" & ClientCrewID & "'
                            , 'Unsynced'
                            , '" & S_Zreading & "'
                            , '" & FullDate24HR() & "')"
            GLOBAL_INSERT_FUNCTION(table:="`loc_refund_return_details`", fields:=fields, values:=value)
            GLOBAL_FUNCTION_UPDATE("loc_daily_transaction", "active = 2 , synced = 'Unsynced'", "transaction_number = '" & transaction_num & "'")
            GLOBAL_FUNCTION_UPDATE("loc_daily_transaction_details", "active = 2 , synced = 'Unsynced'", "transaction_number = '" & transaction_num & "'")
            GLOBAL_FUNCTION_UPDATE("loc_coupon_data", "status = 2 , synced = 'Unsynced'", "transaction_number = '" & transaction_num & "'")

            'GLOBAL_SYSTEM_LOGS("RETURN", "Reason: " & TextBoxIRREASON.Text & " Trn.Number: " & transaction_num & " Total Amount: " & grandtotal)
            LabelITEMRET.Text = 0
            loaditemreturn(True)

            loadindexdgv()
            TextBoxSearchTranNumber.Clear()
            TextBoxIRREASON.Clear()
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Settings Form: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
#End Region
#Region "Coupons"
    Private Sub FillComboboxSearch()
        Try
            ToolStripComboBoxCategorySearch.Items.Clear()
            Dim FillThisDt As DataTable
            FillThisDt = New DataTable
            FillThisDt.Columns.Add("Category")
            Dim sql = "Select category_name FROM loc_admin_category"
            Dim cmd As MySqlCommand = New MySqlCommand(sql, LocalhostConn)
            Dim dt As DataTable = New DataTable
            Dim da As MySqlDataAdapter = New MySqlDataAdapter(cmd)
            da.Fill(dt)
            FillThisDt.Rows.Add("All")
            For i As Integer = 0 To dt.Rows.Count - 1 Step +1
                Dim Cat As DataRow = FillThisDt.NewRow
                Cat("Category") = dt(i)(0)
                FillThisDt.Rows.Add(Cat)
            Next
            For Each row As DataRow In FillThisDt.Rows
                ToolStripComboBoxCategorySearch.Items.Add(row("Category"))
            Next
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Settings Form: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub ShowAllProducts(Category)
        Try
            DataGridViewProducts.Rows.Clear()
            If Category = "All" Then
                Dim ProductsDatatable = AsDatatable("loc_admin_products WHERE product_status = 1", "product_id, product_name, product_category", DataGridViewProducts)
                For Each row As DataRow In ProductsDatatable.Rows
                    DataGridViewProducts.Rows.Add(row("product_id"), row("product_name"), row("product_category"))
                Next
            Else
                Dim ProductsDatatable = AsDatatable("loc_admin_products WHERE product_status = 1 and product_category = '" & Category & "'", "product_id, product_name, product_category", DataGridViewProducts)
                For Each row As DataRow In ProductsDatatable.Rows
                    DataGridViewProducts.Rows.Add(row("product_id"), row("product_name"), row("product_category"))
                Next
            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Settings Form: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub ShowAllCoupons()
        Try
            GLOBAL_SELECT_ALL_FUNCTION("tbcoupon WHERE active = 1", "`Couponname_`, `Desc_`, `Type`, `Effectivedate`, `Expirydate`", DataGridViewCouponList)
            With DataGridViewCouponList
                .Columns(0).HeaderText = "Coupon Name"
                .Columns(0).HeaderText = "Coupon Descrition"
                .Columns(0).HeaderText = "Coupon Type"
                .Columns(0).HeaderText = "Effective Date"
                .Columns(0).HeaderText = "Expiry Date"
            End With
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Settings Form: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub ShowAllCouponsPending()
        Try
            GLOBAL_SELECT_ALL_FUNCTION("tbcoupon WHERE active = 0", "`Couponname_`, `Desc_`, `Type`, `Effectivedate`, `Expirydate`", DataGridViewCouponPending)
            With DataGridViewCouponPending
                .Columns(0).HeaderText = "Coupon Name"
                .Columns(0).HeaderText = "Coupon Descrition"
                .Columns(0).HeaderText = "Coupon Type"
                .Columns(0).HeaderText = "Effective Date"
                .Columns(0).HeaderText = "Expiry Date"
            End With
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Settings Form: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub SaveCoupon()
        Try
            value = "('" & TextBoxCName.Text & "' , '" & TextBoxCDesc.Text & "', '" & TextBoxCDVal.Text & "', '" & TextBoxCRefVal.Text & "', '" & ComboBoxCType.Text & "' , '" & TextBoxCBBP.Text & "' , '" & TextBoxCBV.Text & "', '" & TextBoxCBP.Text & "', '" & TextBoxCBundVal.Text & "', '" & Format(DateTimePickerCEffectiveDate.Value, "yyyy-MM-dd") & "' , '" & Format(DateTimePickerCExpiryDate.Value, "yyyy-MM-dd") & "', '0', '" & ClientStoreID & "', '" & ClientCrewID & "', '" & ClientGuid & "','Unsynced','Local')"
            GLOBAL_INSERT_FUNCTION("tbcoupon", "(`Couponname_`, `Desc_`, `Discountvalue_`, `Referencevalue_`, `Type`, `Bundlebase_`, `BBValue_`, `Bundlepromo_`, `BPValue_`, `Effectivedate`, `Expirydate`, `active`, `store_id`, `crew_id`, `guid`, `synced`, `origin`)", value)
            GLOBAL_SYSTEM_LOGS("NEW COUPON", "Name : " & TextBoxCName.Text & " Type : " & ComboBoxCType.Text)
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Settings Form: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Dim Checkall As Boolean = False
    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        Try
            For i = 0 To DataGridViewProducts.RowCount - 1
                If Checkall = False Then
                    DataGridViewProducts.Rows(i).Cells(3).Value = True

                Else
                    DataGridViewProducts.Rows(i).Cells(3).Value = False
                End If
            Next
            If Checkall = True Then
                Checkall = False
            Else
                Checkall = True
            End If
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub

    Private Sub ToolStripComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ToolStripComboBoxCategorySearch.SelectedIndexChanged
        Try
            ShowAllProducts(ToolStripComboBoxCategorySearch.Text)
            CheckBox1.Checked = False
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub
    'Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
    '    For i As Integer = 0 To DataGridViewProducts.Rows.Count - 1 Step +1
    '        If TypeOf DataGridViewProducts.Rows(i).Cells(3) Is DataGridViewCheckBoxCell Then
    '            Dim checked As Boolean = DataGridViewProducts.Rows(i).Cells(3).Value
    '            If checked = True Then
    '                TextBoxCBBP.Text += DataGridViewProducts.Rows(i).Cells(0).Value.ToString & ","
    '                TextBoxCBP.Text += DataGridViewProducts.Rows(i).Cells(0).Value.ToString & ","
    '            End If
    '        End If
    '    Next
    '    TextBoxCBBP.Text = TextBoxCBBP.Text.TrimEnd(CChar(","))
    '    TextBoxCBP.Text = TextBoxCBP.Text.TrimEnd(CChar(","))
    'End Sub
    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButtonSelectP.Click
        For i As Integer = 0 To DataGridViewProducts.Rows.Count - 1 Step +1
            If TypeOf DataGridViewProducts.Rows(i).Cells(3) Is DataGridViewCheckBoxCell Then
                Dim checked As Boolean = DataGridViewProducts.Rows(i).Cells(3).Value
                If checked = True Then
                    TextBoxCBBP.Text += DataGridViewProducts.Rows(i).Cells(0).Value.ToString & ","
                    TextBoxCBP.Text += DataGridViewProducts.Rows(i).Cells(0).Value.ToString & ","
                End If
            End If
        Next
        TextBoxCBBP.Text = TextBoxCBBP.Text.TrimEnd(CChar(","))
        TextBoxCBP.Text = TextBoxCBP.Text.TrimEnd(CChar(","))
    End Sub


    Private Sub ComboBoxCType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxCType.SelectedIndexChanged
        If ComboBoxCType.Text = "Percentage(w/o vat)" Then
            TextBoxCDVal.Enabled = True
            TextBoxCRefVal.Enabled = False
            TextBoxCBBP.Enabled = False
            TextBoxCBV.Enabled = False
            TextBoxCBP.Enabled = False
            TextBoxCBundVal.Enabled = False
            ToolStripButtonSelectP.Enabled = False
            ToolStripButtonClear.Enabled = False
            For Each a In TabPage9.Controls
                If TypeOf a Is TextBox Then
                    If a.Enabled = False Then
                        a.Text = "N/A"
                    ElseIf a.Enabled = True Then
                        a.Text = ""
                    End If
                End If
            Next
        ElseIf ComboBoxCType.Text = "Percentage(w/ vat)" Then
            TextBoxCDVal.Enabled = True
            TextBoxCRefVal.Enabled = False
            TextBoxCBBP.Enabled = False
            TextBoxCBV.Enabled = False
            TextBoxCBP.Enabled = False
            TextBoxCBundVal.Enabled = False
            ToolStripButtonSelectP.Enabled = False
            ToolStripButtonClear.Enabled = False
            For Each a In TabPage9.Controls
                If TypeOf a Is TextBox Then
                    If a.Enabled = False Then
                        a.Text = "N/A"
                    ElseIf a.Enabled = True Then
                        a.Text = ""
                    End If
                End If
            Next
        ElseIf ComboBoxCType.Text = "Fix-1" Then
            TextBoxCDVal.Enabled = True
            TextBoxCRefVal.Enabled = False
            TextBoxCBBP.Enabled = False
            TextBoxCBV.Enabled = False
            TextBoxCBP.Enabled = False
            TextBoxCBundVal.Enabled = False
            ToolStripButtonSelectP.Enabled = False
            ToolStripButtonClear.Enabled = False
            For Each a In TabPage9.Controls
                If TypeOf a Is TextBox Then
                    If a.Enabled = False Then
                        a.Text = "N/A"
                    ElseIf a.Enabled = True Then
                        a.Text = ""
                    End If
                End If
            Next
        ElseIf ComboBoxCType.Text = "Fix-2" Then
            TextBoxCDVal.Enabled = True
            TextBoxCRefVal.Enabled = True
            TextBoxCBBP.Enabled = False
            TextBoxCBV.Enabled = False
            TextBoxCBP.Enabled = False
            TextBoxCBundVal.Enabled = False
            ToolStripButtonSelectP.Enabled = False
            ToolStripButtonClear.Enabled = False
            For Each a In TabPage9.Controls
                If TypeOf a Is TextBox Then
                    If a.Enabled = False Then
                        a.Text = "N/A"
                    ElseIf a.Enabled = True Then
                        a.Text = ""
                    End If
                End If
            Next
        ElseIf ComboBoxCType.Text = "Bundle-1(Fix)" Then
            TextBoxCDVal.Enabled = False
            TextBoxCRefVal.Enabled = False
            TextBoxCBBP.Enabled = True
            TextBoxCBV.Enabled = True
            TextBoxCBP.Enabled = True
            TextBoxCBundVal.Enabled = True
            ToolStripButtonSelectP.Enabled = True
            ToolStripButtonClear.Enabled = True
            For Each a In TabPage9.Controls
                If TypeOf a Is TextBox Then
                    If a.Enabled = False Then
                        a.Text = "N/A"
                    ElseIf a.Enabled = True Then
                        a.Text = ""
                    End If
                End If
            Next
        ElseIf ComboBoxCType.Text = "Bundle-2(Fix)" Then
            TextBoxCDVal.Enabled = True
            TextBoxCRefVal.Enabled = False
            TextBoxCBBP.Enabled = True
            TextBoxCBV.Enabled = True
            TextBoxCBP.Enabled = True
            TextBoxCBundVal.Enabled = True
            ToolStripButtonSelectP.Enabled = True
            ToolStripButtonClear.Enabled = True
            For Each a In TabPage9.Controls
                If TypeOf a Is TextBox Then
                    If a.Enabled = False Then
                        a.Text = "N/A"
                    ElseIf a.Enabled = True Then
                        a.Text = ""
                    End If
                End If
            Next
        ElseIf ComboBoxCType.Text = "Bundle-3(%)" Then
            TextBoxCDVal.Enabled = True
            TextBoxCRefVal.Enabled = False
            TextBoxCBBP.Enabled = True
            TextBoxCBV.Enabled = True
            TextBoxCBP.Enabled = True
            TextBoxCBundVal.Enabled = True
            ToolStripButtonSelectP.Enabled = True
            ToolStripButtonClear.Enabled = True
            For Each a In TabPage9.Controls
                If TypeOf a Is TextBox Then
                    If a.Enabled = False Then
                        a.Text = "N/A"
                    ElseIf a.Enabled = True Then
                        a.Text = ""
                    End If
                End If
            Next
        Else
            TextBoxCDVal.Enabled = True
            TextBoxCRefVal.Enabled = True
            TextBoxCBBP.Enabled = True
            TextBoxCBV.Enabled = True
            TextBoxCBP.Enabled = True
            TextBoxCBundVal.Enabled = True
            ToolStripButtonSelectP.Enabled = True
            ToolStripButtonClear.Enabled = True
            For Each a In TabPage9.Controls
                If TypeOf a Is TextBox Then
                    If a.Enabled = False Then
                        a.Text = "N/A"
                    ElseIf a.Enabled = True Then
                        a.Text = ""
                    End If
                End If
            Next
        End If
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButtonClear.Click
        TextBoxCBBP.Clear()
        TextBoxCBP.Clear()
    End Sub

    Private Sub ButtonSaveCoupon_Click(sender As Object, e As EventArgs) Handles ButtonSaveCoupon.Click
        Try
            Dim Required As Boolean = False
            For Each a In TabPage9.Controls
                If TypeOf a Is TextBox Or TypeOf a Is ComboBox Then
                    If a.text = "" Then
                        Required = False
                        Exit For
                    Else
                        Required = True
                    End If
                End If
            Next
            If Required = True Then
                SaveCoupon()
                ShowAllCoupons()
                ShowAllCouponsPending()
                MessageBox.Show("Complete", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("All fields are required", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub
#End Region
#Region "Load"
    Private Sub LoadConn()
        Try
            If My.Settings.LocalConnectionPath <> "" Then
                If System.IO.File.Exists(My.Settings.LocalConnectionPath) Then
                    'The File exists 
                    Dim CreateConnString As String = ""
                    Dim filename As String = String.Empty
                    Dim TextLine As String = ""
                    Dim objReader As New System.IO.StreamReader(My.Settings.LocalConnectionPath)
                    Dim lineCount As Integer
                    Do While objReader.Peek() <> -1
                        TextLine = objReader.ReadLine()
                        If lineCount = 0 Then
                            TextBoxLocalServer.Text = ConvertB64ToString(RemoveCharacter(TextLine, "server="))
                        End If
                        If lineCount = 1 Then
                            TextBoxLocalUsername.Text = ConvertB64ToString(RemoveCharacter(TextLine, "user id="))
                        End If
                        If lineCount = 2 Then
                            TextBoxLocalPassword.Text = ConvertB64ToString(RemoveCharacter(TextLine, "password="))
                        End If
                        If lineCount = 3 Then
                            TextBoxLocalDatabase.Text = ConvertB64ToString(RemoveCharacter(TextLine, "database="))
                        End If
                        If lineCount = 4 Then
                            TextBoxLocalPort.Text = ConvertB64ToString(RemoveCharacter(TextLine, "port="))
                        End If
                        lineCount = lineCount + 1
                    Loop
                    objReader.Close()
                End If
            Else
                Dim path2 = My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\Innovention\user.config"
                If System.IO.File.Exists(path2) Then
                    'The File exists 
                    Dim ConnStr
                    Dim ConnStr2 = ""
                    Dim CreateConnString As String = ""
                    Dim filename As String = String.Empty
                    Dim TextLine As String = ""
                    Dim objReader As New System.IO.StreamReader(path2)
                    Dim lineCount As Integer
                    Do While objReader.Peek() <> -1
                        TextLine = objReader.ReadLine()
                        If lineCount = 0 Then
                            ConnStr = ConvertB64ToString(RemoveCharacter(TextLine, "server="))
                            ConnStr2 = "server=" & ConnStr
                        End If
                        If lineCount = 1 Then
                            ConnStr = ConvertB64ToString(RemoveCharacter(TextLine, "user id="))
                            ConnStr2 += ";user id=" & ConnStr
                        End If
                        If lineCount = 2 Then
                            ConnStr = ConvertB64ToString(RemoveCharacter(TextLine, "password="))
                            ConnStr2 += ";password=" & ConnStr
                        End If
                        If lineCount = 3 Then
                            ConnStr = ConvertB64ToString(RemoveCharacter(TextLine, "database="))
                            ConnStr2 += ";database=" & ConnStr
                        End If
                        If lineCount = 4 Then
                            ConnStr = ConvertB64ToString(RemoveCharacter(TextLine, "port="))
                            ConnStr2 += ";port=" & ConnStr
                        End If
                        If lineCount = 5 Then
                            ConnStr2 += ";" & TextLine
                        End If
                        lineCount = lineCount + 1
                    Loop
                    LocalConnectionString = ConnStr2
                    objReader.Close()
                End If
            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Settings Form: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub LoadCloudConn()
        Try
            If ValidLocalConnection = True Then
                sql = "SELECT C_Server, C_Username, C_Password, C_Database, C_Port FROM loc_settings WHERE settings_id = 1"
                Dim cmd As MySqlCommand = New MySqlCommand(sql, LocalhostConn)
                Dim da As MySqlDataAdapter = New MySqlDataAdapter(cmd)
                Dim dt As DataTable = New DataTable
                da.Fill(dt)
                If dt.Rows.Count > 0 Then
                    TextBoxCloudServer.Text = ConvertB64ToString(dt(0)(0))
                    TextBoxCloudUsername.Text = ConvertB64ToString(dt(0)(1))
                    TextBoxCloudPassword.Text = ConvertB64ToString(dt(0)(2))
                    TextBoxCloudDatabase.Text = ConvertB64ToString(dt(0)(3))
                    TextBoxCloudPort.Text = ConvertB64ToString(dt(0)(4))
                    ValidLocalConnection = True
                Else
                    ValidLocalConnection = False
                End If
            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Settings Form: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub LoadPrintOptions()
        Try
            If ValidLocalConnection = True Then
                sql = "SELECT `printreceipt`, `reprintreceipt`, `printxzread`, `printreturns`, `printcount`, `printsalesreport` FROM loc_settings WHERE settings_id = 1"
                Dim cmd As MySqlCommand = New MySqlCommand(sql, LocalhostConn)
                Dim da As MySqlDataAdapter = New MySqlDataAdapter(cmd)
                Dim dt As DataTable = New DataTable
                da.Fill(dt)
                If dt.Rows.Count > 0 Then
                    If dt(0)(0) <> Nothing Then
                        If dt(0)(0) = "YES" Then
                            RadioButtonPrintReceiptYes.Checked = True
                            S_Print = "YES"
                            If ClientRole <> "Admin" Then
                                RadioButtonPrintReceiptNo.Checked = False
                                RadioButtonPrintReceiptNo.Enabled = False
                            End If
                        Else
                            RadioButtonPrintReceiptNo.Checked = True
                            S_Print = "NO"
                            If ClientRole <> "Admin" Then
                                RadioButtonPrintReceiptYes.Checked = False
                                RadioButtonPrintReceiptYes.Enabled = False
                            End If
                        End If

                    End If
                    If dt(0)(1) <> Nothing Then
                        If dt(0)(1) = "YES" Then
                            RadioButtonRePrintReceiptYes.Checked = True
                            S_Reprint = "YES"
                            If ClientRole <> "Admin" Then
                                RadioButtonRePrintReceiptNo.Checked = False
                                RadioButtonRePrintReceiptNo.Enabled = False
                            End If
                        Else
                            RadioButtonRePrintReceiptNo.Checked = True
                            S_Reprint = "NO"
                            If ClientRole <> "Admin" Then
                                RadioButtonRePrintReceiptYes.Checked = False
                                RadioButtonRePrintReceiptYes.Enabled = False
                            End If
                        End If

                    End If
                    If dt(0)(2) <> Nothing Then
                        If dt(0)(2) = "YES" Then
                            RadioButtonPrintXZReadYes.Checked = True
                            S_Print_XZRead = "YES"
                            If ClientRole <> "Admin" Then
                                RadioButtonPrintXZReadNo.Checked = False
                                RadioButtonPrintXZReadNo.Enabled = False
                            End If
                        Else
                            RadioButtonPrintXZReadNo.Checked = True
                            S_Print_XZRead = "NO"
                            If ClientRole <> "Admin" Then
                                RadioButtonPrintXZReadYes.Checked = False
                                RadioButtonPrintXZReadYes.Enabled = False
                            End If
                        End If
                    End If
                    If dt(0)(3) <> Nothing Then
                        If dt(0)(3) = "YES" Then
                            RadioButtonPrintReturnsYes.Checked = True
                            S_Print_Returns = "YES"
                            If ClientRole <> "Admin" Then
                                RadioButtonPrintReturnsNo.Checked = False
                                RadioButtonPrintReturnsNo.Enabled = False
                            End If
                        Else
                            RadioButtonPrintReturnsNo.Checked = True
                            S_Print_Returns = "NO"
                            If ClientRole <> "Admin" Then
                                RadioButtonPrintReturnsYes.Checked = False
                                RadioButtonPrintReturnsYes.Enabled = False
                            End If
                        End If
                    End If
                    If dt(0)(4) <> Nothing Then
                        NumericUpDownPrintCount.Value = dt(0)(4)
                    End If
                    If dt(0)(5) <> Nothing Then
                        If dt(0)(5) = "YES" Then
                            RadioButtonPrintSRYes.Checked = True
                            S_Print_Sales_Report = "YES"
                            If ClientRole <> "Admin" Then
                                RadioButtonPrintSRNo.Checked = False
                                RadioButtonPrintSRNo.Enabled = False
                            End If
                        Else
                            RadioButtonPrintSRNo.Checked = True
                            S_Print_Sales_Report = "NO"
                            If ClientRole <> "Admin" Then
                                RadioButtonPrintSRYes.Checked = False
                                RadioButtonPrintSRYes.Enabled = False
                            End If
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Settings Form: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub LoadAdditionalSettings()
        Try
            If ValidLocalConnection = True Then
                sql = "SELECT A_Export_Path, A_Tax, A_SIFormat, A_Terminal_No, A_ZeroRated  FROM loc_settings WHERE settings_id = 1"
                Dim cmd As MySqlCommand = New MySqlCommand(sql, LocalhostConn)
                Dim da As MySqlDataAdapter = New MySqlDataAdapter(cmd)
                Dim dt As DataTable = New DataTable
                da.Fill(dt)
                If dt.Rows.Count > 0 Then
                    If dt(0)(0) <> Nothing Then
                        TextBoxExportPath.Text = ConvertB64ToString(dt(0)(0))
                    End If
                    If dt(0)(1) <> Nothing Then
                        TextBoxTax.Text = dt(0)(1) * 100
                    End If
                    If dt(0)(2) <> Nothing Then
                        TextBoxSINumber.Text = dt(0)(2)
                    End If
                    If dt(0)(3) <> Nothing Then
                        TextBoxTerminalNo.Text = dt(0)(3)
                    End If
                    If dt(0)(4) <> Nothing Then
                        If dt(0)(4) = 0 Then
                            RadioButtonNO.Checked = True
                        ElseIf dt(0)(4) = 1 Then
                            RadioButtonYES.Checked = True
                        End If
                    End If

                    If AutoInventoryReset Then
                        RadioButtonInvResetOn.Checked = True
                    Else
                        RadioButtonInvResetOff.Checked = True
                    End If
                End If
                My.Settings.Save()
            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Settings Form: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub LoadDevInfo()
        Try
            If ValidLocalConnection = True Then
                sql = "SELECT Dev_Company_Name, Dev_Address, Dev_Tin, Dev_Accr_No, Dev_Accr_Date_Issued, Dev_Accr_Valid_Until, Dev_PTU_No, Dev_PTU_Date_Issued, Dev_PTU_Valid_Until FROM loc_settings WHERE settings_id = 1"
                Dim cmd As MySqlCommand = New MySqlCommand(sql, LocalhostConn)
                Dim da As MySqlDataAdapter = New MySqlDataAdapter(cmd)
                Dim dt = New DataTable
                da.Fill(dt)
                If dt.Rows.Count > 0 Then
                    If dt(0)(0) <> Nothing Then
                        TextBoxDevname.Text = dt(0)(0)
                    End If
                    If dt(0)(1) <> Nothing Then
                        TextBoxDevAdd.Text = dt(0)(1)
                    End If
                    If dt(0)(2) <> Nothing Then
                        TextBoxDevTIN.Text = dt(0)(2)
                    End If
                    If dt(0)(3) <> Nothing Then
                        TextBoxDevAccr.Text = dt(0)(3)
                    End If
                    If dt(0)(4) <> Nothing Then
                        DateTimePicker1ACCRDI.Value = dt(0)(4)
                    End If
                    If dt(0)(5) <> Nothing Then
                        DateTimePicker2ACCRVU.Value = dt(0)(5)
                    End If
                    If dt(0)(6) <> Nothing Then
                        TextBoxDEVPTU.Text = dt(0)(6)
                    End If
                    If dt(0)(7) <> Nothing Then
                        DateTimePicker4PTUDI.Value = dt(0)(7)
                    End If
                    If dt(0)(8) <> Nothing Then
                        DateTimePickerPTUVU.Value = dt(0)(8)
                    End If
                End If
            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Settings Form: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub

    Private Function LoadSystemResetStatus() As String
        Dim countervalue As String = ""
        Try
            Dim sql = "SELECT counter_value FROM tbcountertable WHERE counter_id = 1"
            Dim cmd As MySqlCommand = New MySqlCommand(sql, LocalhostConn)

            Using reader As MySqlDataReader = cmd.ExecuteReader()
                If reader.HasRows Then
                    While reader.Read
                        countervalue = reader("counter_value")
                    End While
                End If
            End Using

        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Settings Form: " & ex.ToString, "Critical")

            MsgBox(ex.ToString)
        End Try
        Return countervalue
    End Function

    Private Sub LoadAutoBackup()
        Try
            If ValidLocalConnection = True Then
                sql = "SELECT S_BackupInterval, S_BackupDate FROM loc_settings WHERE settings_id = 1"
                Dim cmd As MySqlCommand = New MySqlCommand(sql, LocalhostConn)
                Dim da As MySqlDataAdapter = New MySqlDataAdapter(cmd)
                Dim dt = New DataTable
                da.Fill(dt)
                If dt.Rows.Count > 0 Then
                    If dt(0)(0).ToString = "1" Then
                        RadioButtonDaily.Checked = True
                        '=================================
                        If ClientRole <> "Admin" Then
                            RadioButtonWeekly.Enabled = False
                            RadioButtonMonthly.Enabled = False
                            RadioButtonYearly.Enabled = False
                        End If
                    ElseIf dt(0)(0).ToString = "2" Then
                        RadioButtonWeekly.Checked = True
                        '=================================
                        If ClientRole <> "Admin" Then
                            RadioButtonDaily.Enabled = False
                            RadioButtonMonthly.Enabled = False
                            RadioButtonYearly.Enabled = False
                        End If
                    ElseIf dt(0)(0).ToString = "3" Then
                            RadioButtonMonthly.Checked = True
                        '=================================
                        If ClientRole <> "Admin" Then
                            RadioButtonDaily.Enabled = False
                            RadioButtonWeekly.Enabled = False
                            RadioButtonYearly.Enabled = False
                        End If
                    ElseIf dt(0)(0).ToString = "4" Then
                            RadioButtonYearly.Checked = True
                        '=================================
                        If ClientRole <> "Admin" Then
                            RadioButtonDaily.Enabled = False
                            RadioButtonWeekly.Enabled = False
                            RadioButtonMonthly.Enabled = False
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub ButtonExport_Click(sender As Object, e As EventArgs) Handles ButtonExport.Click
        If ClientRole = "Crew" Then
            MsgBox("You dont have administrator rights.")
        Else
            BackupDatabase()
        End If
    End Sub
    Private Sub BackupDatabase()
        Try
            Dim DatabaseName = "\" & TextBoxLocalDatabase.Text & "-FBW" & ClientStoreID & Format(Now(), "yyyy-MM-dd-HHmmdd") & ".sql"

            Process.Start("cmd.exe", "/k cd C:\xampp\mysql\bin & mysqldump --databases -h " & TextBoxLocalServer.Text & " -u " & TextBoxLocalUsername.Text & " -p " & TextBoxLocalPassword.Text & " " & TextBoxLocalDatabase.Text & " > """ & TextBoxExportPath.Text & DatabaseName & """")
        Catch ex As Exception
            MsgBox(ex.ToString)
            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub ButtonImport_Click(sender As Object, e As EventArgs) Handles ButtonImport.Click
        If ClientRole = "Crew" Then
            MsgBox("You dont have administrator rights.")
        Else
            If (OpenFileDialog1.ShowDialog = DialogResult.OK) Then
                TextBoxLocalRestorePath.Text = OpenFileDialog1.FileName
            End If
        End If
    End Sub
    Private Function Connect() As MySqlConnection
        Dim con As MySqlConnection = New MySqlConnection
        Try
            con.ConnectionString = "server=" & TextBoxLocalServer.Text & ";user name=" & TextBoxLocalUsername.Text & ";password=" & TextBoxLocalPassword.Text
            con.Open()
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Settings Form: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
        Return con
    End Function
    Private Sub RestoreDatabase()
        Try
            Dim sql = "CREATE DATABASE /*!32312 IF NOT EXISTS*/ `" & TextBoxLocalDatabase.Text & "` /*!40100 DEFAULT CHARACTER SET utf8mb4 */;USE `" & TextBoxLocalDatabase.Text & "`;"
            Dim cmd As MySqlCommand = New MySqlCommand(sql, Connect)
            cmd.ExecuteNonQuery()
            Process.Start("cmd.exe", "/k cd C:\xampp\mysql\bin & mysql -h " & TextBoxLocalServer.Text & " -u " & TextBoxLocalUsername.Text & " -p " & TextBoxLocalPassword.Text & " " & TextBoxLocalDatabase.Text & " < """ & TextBoxLocalRestorePath.Text & """")
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Settings Form: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub OpenFileDialog1_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog1.FileOk
        TextBoxLocalRestorePath.Text = System.IO.Path.GetFullPath(OpenFileDialog1.FileName)
        RestoreDatabase()
    End Sub
    Private Sub ButtonMaintenance_Click(sender As Object, e As EventArgs) Handles ButtonMaintenance.Click
        If ClientRole = "Crew" Then
            MsgBox("You dont have administrator rights.")
        Else
            RepairDatabase()
        End If

    End Sub
    Private Sub RepairDatabase()
        Try
            Process.Start("cmd.exe", "/k cd C:\xampp\mysql\bin & mysqlcheck -h " & TextBoxLocalServer.Text & " -u " & TextBoxLocalUsername.Text & " -p " & TextBoxLocalPassword.Text & " --auto-repair -c --databases " & TextBoxLocalDatabase.Text)
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Settings Form: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles ButtonOptimizeDB.Click
        If ClientRole = "Crew" Then
            MsgBox("You dont have administrator rights.")
        Else
            OptimizeDatabase()
        End If
    End Sub
    Private Sub OptimizeDatabase()
        Try
            Process.Start("cmd.exe", "/k cd C:\xampp\mysql\bin & mysqlcheck -h " & TextBoxLocalServer.Text & " -u " & TextBoxLocalUsername.Text & " -p " & TextBoxLocalPassword.Text & " -o --databases " & TextBoxLocalDatabase.Text)
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub

    Private Sub ButtonDatabaseReset_Click(sender As Object, e As EventArgs)
        If ClientRole = "Crew" Then
            MsgBox("You dont have administrator rights.")
        Else

        End If
    End Sub
    Private Function TestDBConnectionLocal(server, username, password, database, port) As MySqlConnection
        Dim testcon As MySqlConnection = New MySqlConnection
        Try
            testcon.ConnectionString = "server=" & server & ";user id=" & username & ";password=" & password & ";database=" & database & ";port=" & port & ""
            testcon.Open()
            If testcon.State = ConnectionState.Open Then
                TestLocalCon = True
            Else
                TestLocalCon = False
            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Settings Form: " & ex.ToString, "Critical")

            TestLocalCon = False
            SendErrorReport(ex.ToString)
        End Try
        Return testcon
    End Function
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles ButtonTestLocCon.Click
        Try
            If Not BackgroundWorkerLocalConnection.IsBusy Then
                BackgroundWorkerLocalConnection.WorkerReportsProgress = True
                BackgroundWorkerLocalConnection.WorkerSupportsCancellation = True
                BackgroundWorkerLocalConnection.RunWorkerAsync()
            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Settings Form: " & ex.ToString, "Critical")
            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Dim threadListConLocal As List(Of Thread) = New List(Of Thread)
    Dim threadConlocal As Thread
    Dim TestLocalCon As Boolean = False
    Dim CancelTestLocalCon As Boolean = False
    Private Sub BackgroundWorkerLocalConnection_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorkerLocalConnection.DoWork
        Try
            For i = 0 To 100
                BackgroundWorkerLocalConnection.ReportProgress(i)
                Thread.Sleep(20)

                If i = 10 Then
                    threadConlocal = New Thread(Sub() TestDBConnectionLocal(TextBoxLocalServer.Text, TextBoxLocalUsername.Text, TextBoxLocalPassword.Text, TextBoxLocalDatabase.Text, TextBoxLocalPort.Text))
                    threadConlocal.Start()
                    threadListConLocal.Add(threadConlocal)
                    For Each t In threadListConLocal
                        t.Join()
                        If (BackgroundWorkerLocalConnection.CancellationPending) Then
                            ' Indicate that the task was canceled.
                            CancelTestLocalCon = True
                            e.Cancel = True
                            Exit For
                        End If
                    Next
                End If
            Next
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Settings Form: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub BackgroundWorkerLocalConnection_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorkerLocalConnection.ProgressChanged
        Try
            If CancelTestLocalCon = False Then
                ToolStripProgressBar1.Value = e.ProgressPercentage
                ToolStripStatusLabel2.Text = "Checking Connection " & e.ProgressPercentage & " %"
            End If

        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Settings Form: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub BackgroundWorkerLocalConnection_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorkerLocalConnection.RunWorkerCompleted
        Try
            If TestLocalCon Then
                ValidLocalConnection = True
                ToolStripStatusLabel2.Text = "Connected Successfully"
            Else
                ValidLocalConnection = False
                ToolStripStatusLabel2.Text = "Cannot connect to server"
            End If
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Function TestDBConnectionCloud(server, username, password, database, port) As MySqlConnection
        Dim testcon As MySqlConnection = New MySqlConnection
        Try
            testcon.ConnectionString = "server=" & server & ";user id=" & username & ";password=" & password & ";database=" & database & ";port=" & port & ""
            testcon.Open()
            If testcon.State = ConnectionState.Open Then
                TestCloudCon = True
            Else
                TestCloudCon = False
            End If
        Catch ex As Exception
            TestCloudCon = False
        End Try
        Return testcon
    End Function


    Private Sub ButtonTestCloudCon_Click(sender As Object, e As EventArgs) Handles ButtonTestCloudCon.Click
        Try
            If Not BackgroundWorkerCloudConnection.IsBusy Then
                BackgroundWorkerCloudConnection.WorkerReportsProgress = True
                BackgroundWorkerCloudConnection.WorkerSupportsCancellation = True
                BackgroundWorkerCloudConnection.RunWorkerAsync()
            End If
        Catch ex As Exception
            ValidCloudConnection = False
            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Dim threadListConCloud As List(Of Thread) = New List(Of Thread)
    Dim threadConCloud As Thread
    Dim TestCloudCon As Boolean = False
    Dim ValidInternetCon As Boolean = False
    Dim CancelTestCloudCon As Boolean = False
    Private Sub BackgroundWorkerCloudConnection_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorkerCloudConnection.DoWork
        Try
            For i = 0 To 100
                BackgroundWorkerCloudConnection.ReportProgress(i)
                Thread.Sleep(20)

                If i = 0 Then
                    threadConCloud = New Thread(Sub() ValidInternetCon = CheckForInternetConnection())
                    threadConCloud.Start()
                    threadListConCloud.Add(threadConCloud)
                End If
                For Each t In threadListConCloud
                    t.Join()
                    If (BackgroundWorkerLocalConnection.CancellationPending) Then
                        ' Indicate that the task was canceled.
                        CancelTestCloudCon = True
                        e.Cancel = True
                        Exit For
                    End If
                Next
                If i = 10 Then
                    If ValidInternetCon Then
                        Console.Write("Has internet")
                        threadConCloud = New Thread(Sub() TestDBConnectionCloud(TextBoxCloudServer.Text, TextBoxCloudUsername.Text, TextBoxCloudPassword.Text, TextBoxCloudDatabase.Text, TextBoxCloudPort.Text))
                        threadConCloud.Start()
                        threadListConCloud.Add(threadConCloud)
                    Else
                        Console.Write("No internet")
                    End If
                End If
                For Each t In threadListConCloud
                    t.Join()
                    If (BackgroundWorkerLocalConnection.CancellationPending) Then
                        ' Indicate that the task was canceled.
                        CancelTestCloudCon = True
                        e.Cancel = True
                        Exit For
                    End If
                Next
            Next
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Settings Form: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub

    Private Sub BackgroundWorkerCloudConnection_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorkerCloudConnection.ProgressChanged
        Try
            If CancelTestCloudCon = False Then
                ToolStripProgressBar2.Value = e.ProgressPercentage
                ToolStripStatusLabel3.Text = "Checking Connection " & e.ProgressPercentage & " %"
            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Settings Form: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub BackgroundWorkerCloudConnection_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorkerCloudConnection.RunWorkerCompleted
        If ValidInternetCon Then
            If TestCloudCon Then
                ValidCloudConnection = True
                POS.Button3.Enabled = True
                ToolStripStatusLabel3.Text = "Connected Successfully"
            Else
                ValidCloudConnection = False
                POS.Button3.Enabled = False
                ToolStripStatusLabel3.Text = "Cannot connect to server"
            End If
        Else
            ValidCloudConnection = False
            POS.Button3.Enabled = False
            ToolStripStatusLabel3.Text = "No internet connection available"
        End If
    End Sub
    Private Sub TextBoxCName_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBoxSearchTranNumber.KeyPress, TextBoxIRREASON.KeyPress, TextBoxCName.KeyPress, TextBoxCDesc.KeyPress, TextBoxCBV.KeyPress, TextBoxCBBP.KeyPress
        Try
            If InStr(DisallowedCharacters, e.KeyChar) > 0 Then
                e.Handled = True
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Dim thread As Thread
    Dim THREADLISTUPDATE As List(Of Thread) = New List(Of Thread)
    Dim WorkerCanceled As Boolean = False
    Dim ValidInternetConn As Boolean = False
    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Try
            If ValidLocalConnection Then
                thread = New Thread(AddressOf ServerCloudCon)
                thread.Start()
                THREADLISTUPDATE.Add(thread)

                For Each t In THREADLISTUPDATE
                    t.Join()
                    If (BackgroundWorker1.CancellationPending) Then
                        ' Indicate that the task was canceled.
                        WorkerCanceled = True
                        e.Cancel = True
                        Exit For
                    End If
                Next

                thread = New Thread(Sub() ValidInternetConn = CheckForInternetConnection())
                thread.Start()
                THREADLISTUPDATE.Add(thread)
                For Each t In THREADLISTUPDATE
                    t.Join()
                Next
                If ValidInternetConn Then
                    If ValidCloudConnection Then
                        thread = New Thread(AddressOf CheckPriceChanges)
                        thread.Start()
                        THREADLISTUPDATE.Add(thread)
                        For Each t In THREADLISTUPDATE
                            t.Join()
                        Next
                        thread = New Thread(AddressOf Function1)
                        thread.Start()
                        THREADLISTUPDATE.Add(thread)
                        thread = New Thread(AddressOf GetProducts)
                        thread.Start()
                        THREADLISTUPDATE.Add(thread)
                        thread = New Thread(AddressOf Function3)
                        thread.Start()
                        THREADLISTUPDATE.Add(thread)
                        thread = New Thread(AddressOf Function4)
                        thread.Start()
                        THREADLISTUPDATE.Add(thread)
                        thread = New Thread(AddressOf Function5)
                        thread.Start()
                        THREADLISTUPDATE.Add(thread)
                        thread = New Thread(AddressOf Function6)
                        thread.Start()
                        THREADLISTUPDATE.Add(thread)
                        thread = New Thread(AddressOf CouponApproval)
                        thread.Start()
                        THREADLISTUPDATE.Add(thread)
                        thread = New Thread(AddressOf CustomProductApproval)
                        thread.Start()
                        THREADLISTUPDATE.Add(thread)
                    End If
                End If
                For Each t In THREADLISTUPDATE
                    t.Join()
                    If (BackgroundWorker1.CancellationPending) Then
                        ' Indicate that the task was canceled.
                        WorkerCanceled = True
                        e.Cancel = True
                        Exit For
                    End If
                Next
            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Settings Form: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        Try
            LabelStatus.Text = "Item(s) " & LabelCountAllRows.Text & " Checked " & ProgressBar1.Value & " of " & LabelCountAllRows.Text
            DataGridView2.DataSource = FillDatagridProduct
            Button4.Enabled = True
            UPDATEPRODUCTONLY = False
            If DataGridView1.Rows.Count > 0 Or DataGridView2.Rows.Count > 0 Or DataGridView3.Rows.Count > 0 Or DataGridView4.Rows.Count > 0 Or PriceChangeDatatabe.Rows.Count > 0 Or DataGridView6.Rows.Count Or DataGridView7.Rows.Count Then
                Dim updatemessage = MessageBox.Show("New Updates are available. Would you like to update now ?", "New Updates", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                If updatemessage = DialogResult.Yes Then
                    HASUPDATE = True
                    InstallUpdatesFormula()
                    InstallUpdatesInventory()
                    InstallUpdatesCategory()
                    InstallUpdatesCoupons()
                    InstallUpdatesPartners()
                    InstallUpdatesProducts()
                    InstallUpdatesPriceChange()
                    If PRICECHANGE = True Then
                        MsgBox("Product price changes approved")
                        PRICECHANGE = False
                    End If
                    POS.LoadCategory()
                    For Each btn As Button In Panel3.Controls.OfType(Of Button)()
                        If btn.Text = "Simply Perfect" Then
                            btn.PerformClick()
                        End If
                    Next
                    LabelCheckingUpdates.Text = "Update Completed : " & FullDate24HR()
                    My.Settings.Updatedatetime = FullDate24HR()
                Else
                    LabelCheckingUpdates.Text = "Completed."
                    My.Settings.Updatedatetime = FullDate24HR()
                End If
            Else
                HASUPDATE = False
                LabelCheckingUpdates.Text = "Complete Checking! No updates found."
                My.Settings.Updatedatetime = FullDate24HR()
            End If
            Timer1.Stop()
            If CloudVersion <> LocalVersion Then
                Dim msg = MessageBox.Show("Software Update found" & vbNewLine & "Version No: " & CloudVersion, "Sofware Update", MessageBoxButtons.OK)
                Process.Start(Application.StartupPath & "\Update\Update.exe")
                Application.Exit()
            Else
                My.Settings.Save()
            End If
            PictureBox1.Visible = False
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Settings Form: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
#End Region
#Region "Updates"
#Region "Check for software update"
    Dim CloudVersion
    Dim LocalVersion
    Private Sub SoftwareUpdate()
        Try
            Dim ConnectionLocal As MySqlConnection = LocalhostConn()
            Dim ConnectionCloud As MySqlConnection = New MySqlConnection
            ConnectionCloud.ConnectionString = "server=" & Trim(TextBoxCloudServer.Text) &
                ";user id=" & Trim(TextBoxCloudUsername.Text) &
                ";password=" & Trim(TextBoxCloudPassword.Text) &
                ";database=" & Trim(TextBoxCloudDatabase.Text) &
                ";port=" & Trim(TextBoxCloudPort.Text)
            ConnectionCloud.Open()
            Dim sql = "SELECT S_Update_Version FROM loc_settings WHERE settings_id = 1"
            Dim cmd As MySqlCommand = New MySqlCommand(sql, ConnectionLocal)
            LocalVersion = cmd.ExecuteScalar
            sql = "SELECT S_Update_Version FROM admin_settings_org WHERE settings_id = 1"
            cmd = New MySqlCommand(sql, ConnectionCloud)
            CloudVersion = cmd.ExecuteScalar
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Settings Form: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
#End Region
#Region "Price Change"
    Dim PRICECHANGE As Boolean = False
    Dim PriceChangeDatatabe As DataTable
    Private Sub CheckPriceChanges()
        Try
            Dim ConnectionServer As MySqlConnection = ServerCloudCon()
            Dim Query = "SELECT * FROM admin_price_request WHERE store_id = '" & ClientStoreID & "' AND guid = '" & ClientGuid & "' AND synced = 'Unsynced' AND active = 2"
            Dim CmdCheck As MySqlCommand = New MySqlCommand(Query, ConnectionServer)
            Dim DaCheck As MySqlDataAdapter = New MySqlDataAdapter(CmdCheck)
            PriceChangeDatatabe = New DataTable
            DaCheck.Fill(PriceChangeDatatabe)
            If PriceChangeDatatabe.Rows.Count > 0 Then
                PRICECHANGE = True
            Else
                PRICECHANGE = False
            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Settings Form: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
#End Region
#Region "Coupon"
    Dim CouponDatatable As DataTable
    Dim CouponApp As Boolean = False
    Private Sub CouponApproval()
        Try
            Dim ConnectionServer As MySqlConnection = ServerCloudCon()
            Dim Query = "SELECT ID FROM admin_custom_coupon WHERE store_id = '" & ClientStoreID & "' AND guid = '" & ClientGuid & "' AND active = 1 AND synced = 'Unsynced'"
            Dim CmdCheck As MySqlCommand = New MySqlCommand(Query, ConnectionServer)
            Dim DaCheck As MySqlDataAdapter = New MySqlDataAdapter(CmdCheck)
            CouponDatatable = New DataTable
            DaCheck.Fill(CouponDatatable)
            If CouponDatatable.Rows.Count > 0 Then
                CouponApp = True
            Else
                CouponApp = False
            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Settings Form: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Dim CustomProductsApproval As DataTable
    Dim CustomProdctsAppBool As Boolean = False
    Private Sub CustomProductApproval()
        Try
            Dim ConnectionServer As MySqlConnection = ServerCloudCon()
            Dim Query = "SELECT loc_product_id FROM loc_product_list WHERE store_id = '" & ClientStoreID & "' AND guid = '" & ClientGuid & "' AND product_status = 1 AND synced = 'Unsynced'"
            Dim CmdCheck As MySqlCommand = New MySqlCommand(Query, ConnectionServer)
            Dim DaCheck As MySqlDataAdapter = New MySqlDataAdapter(CmdCheck)
            CustomProductsApproval = New DataTable
            DaCheck.Fill(CustomProductsApproval)
            If CustomProductsApproval.Rows.Count > 0 Then
                CustomProdctsAppBool = True
            Else
                CustomProdctsAppBool = False
            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Settings Form: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
#End Region
#Region "Categories Update"
    Private Function LoadCategoryLocal() As DataTable
        Dim cmdlocal As MySqlCommand
        Dim dalocal As MySqlDataAdapter
        Dim dtlocal As DataTable = New DataTable
        dtlocal.Columns.Add("updated_at")
        dtlocal.Columns.Add("category_id")
        Dim dtlocal1 As DataTable = New DataTable
        Try
            Dim sql = "SELECT updated_at, category_id FROM loc_admin_category"
            cmdlocal = New MySqlCommand(sql, LocalhostConn())
            dalocal = New MySqlDataAdapter(cmdlocal)
            dalocal.Fill(dtlocal1)
            For i As Integer = 0 To dtlocal1.Rows.Count - 1 Step +1
                Dim Cat As DataRow = dtlocal.NewRow
                Cat("updated_at") = dtlocal1(i)(0).ToString
                Cat("category_id") = dtlocal1(i)(1)
                dtlocal.Rows.Add(Cat)
            Next
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Settings Form: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
        Return dtlocal
    End Function
    Private Sub Function1()
        Try
            Dim Query = "SELECT * FROM loc_admin_category"
            Dim CmdCheck As MySqlCommand = New MySqlCommand(Query, LocalhostConn)
            Dim DaCheck As MySqlDataAdapter = New MySqlDataAdapter(CmdCheck)
            Dim DtCheck As DataTable = New DataTable
            DaCheck.Fill(DtCheck)
            Dim cmdserver As MySqlCommand
            Dim daserver As MySqlDataAdapter
            Dim dtserver As DataTable
            If DtCheck.Rows.Count < 1 Then
                Dim sql = "SELECT `category_id`, `category_name`, `brand_name`, `updated_at`, `origin`, `status` FROM admin_category"
                cmdserver = New MySqlCommand(sql, ServerCloudCon())
                daserver = New MySqlDataAdapter(cmdserver)
                dtserver = New DataTable
                daserver.Fill(dtserver)
                For i As Integer = 0 To dtserver.Rows.Count - 1 Step +1
                    If WorkerCanceled Then
                        Exit Sub
                    End If
                    DataGridView1.Rows.Add(dtserver(i)(0), dtserver(i)(1), dtserver(i)(2), dtserver(i)(3).ToString, dtserver(i)(4), dtserver(i)(5))
                    LabelNewRows.Text += Val(LabelNewRows.Text)
                Next
            Else
                Dim Ids As String = ""
                If ValidCloudConnection = True Then
                    For i As Integer = 0 To LoadCategoryLocal.Rows.Count - 1 Step +1
                        If Ids = "" Then
                            Ids = "" & LoadCategoryLocal(i)(1) & ""
                        Else
                            Ids += "," & LoadCategoryLocal(i)(1) & ""
                        End If
                    Next
                    Dim sql = "SELECT `category_id`, `category_name`, `brand_name`, `updated_at`, `origin`, `status` FROM admin_category WHERE category_id IN (" & Ids & ")"
                    cmdserver = New MySqlCommand(sql, ServerCloudCon())
                    daserver = New MySqlDataAdapter(cmdserver)
                    dtserver = New DataTable
                    daserver.Fill(dtserver)
                    For i As Integer = 0 To dtserver.Rows.Count - 1 Step +1
                        If WorkerCanceled Then
                            Exit Sub
                        End If
                        If LoadCategoryLocal(i)(0).ToString <> dtserver(i)(3).ToString Then
                            DataGridView1.Rows.Add(dtserver(i)(0), dtserver(i)(1), dtserver(i)(2), dtserver(i)(3).ToString, dtserver(i)(4), dtserver(i)(5))
                        End If
                        ProgressBar1.Value = ProgressBar1.Value + 1
                        LabelStatus.Text = "Item(s) " & LabelCountAllRows.Text & " Checked " & ProgressBar1.Value & " of " & LabelCountAllRows.Text
                    Next
                    Dim sql2 = "SELECT `category_id`, `category_name`, `brand_name`, `updated_at`, `origin`, `status` FROM admin_category WHERE category_id NOT IN (" & Ids & ")"
                    cmdserver = New MySqlCommand(sql2, ServerCloudCon())
                    daserver = New MySqlDataAdapter(cmdserver)
                    dtserver = New DataTable
                    daserver.Fill(dtserver)
                    For i As Integer = 0 To dtserver.Rows.Count - 1 Step +1
                        If LoadCategoryLocal(i)(0) <> dtserver(i)(3) Then
                            If WorkerCanceled Then
                                Exit Sub
                            End If
                            DataGridView1.Rows.Add(dtserver(i)(0), dtserver(i)(1), dtserver(i)(2), dtserver(i)(3).ToString, dtserver(i)(4), dtserver(i)(5))
                            LabelNewRows.Text += Val(LabelNewRows.Text)
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Settings Form: " & ex.ToString, "Critical")

            BackgroundWorker1.CancelAsync()
            SendErrorReport(ex.ToString)
            'If table doesnt have data
        End Try
    End Sub
#End Region
#Region "Products Update"
    Dim UPDATEPRODUCTONLY As Boolean = False
    Dim FillDatagridProduct As DataTable
    Private Sub GetProducts()
        Try
            Dim ConnectionLocal As MySqlConnection = LocalhostConn()
            Dim ConnectionServer As MySqlConnection = ServerCloudCon()

            FillDatagridProduct = New DataTable
            FillDatagridProduct.Columns.Add("product_id")
            FillDatagridProduct.Columns.Add("product_sku")
            FillDatagridProduct.Columns.Add("product_name")
            FillDatagridProduct.Columns.Add("formula_id")
            FillDatagridProduct.Columns.Add("product_barcode")
            FillDatagridProduct.Columns.Add("product_category")
            FillDatagridProduct.Columns.Add("product_price")
            FillDatagridProduct.Columns.Add("product_desc")
            FillDatagridProduct.Columns.Add("product_image")
            FillDatagridProduct.Columns.Add("product_status")
            FillDatagridProduct.Columns.Add("origin")
            FillDatagridProduct.Columns.Add("date_modified")
            FillDatagridProduct.Columns.Add("inventory_id")
            FillDatagridProduct.Columns.Add("addontype")
            FillDatagridProduct.Columns.Add("partners")
            FillDatagridProduct.Columns.Add("arrangement")


            Dim Query = "SELECT * FROM loc_admin_products"
            Dim CmdCheck As MySqlCommand = New MySqlCommand(Query, ConnectionLocal)
            Dim DaCheck As MySqlDataAdapter = New MySqlDataAdapter(CmdCheck)
            Dim DtCheck As DataTable = New DataTable
            DaCheck.Fill(DtCheck)
            If DtCheck.Rows.Count < 1 Then
                GetAllProducts()
            Else
                Dim DtCount As DataTable

                Dim SqlCount = "SELECT COUNT(product_id) FROM admin_products_org"
                Dim CmdCount As MySqlCommand = New MySqlCommand(SqlCount, ConnectionServer)
                Dim result As Integer = CmdCount.ExecuteScalar
                Dim DaCount As MySqlDataAdapter
                Dim FillDt As DataTable = New DataTable

                For a = 1 To result
                    If WorkerCanceled Then
                        Exit Sub
                    End If
                    Dim Query1 As String = "SELECT date_modified, price_change FROM loc_admin_products WHERE server_product_id = " & a
                    Dim cmd As MySqlCommand = New MySqlCommand(Query1, ConnectionLocal)
                    DaCount = New MySqlDataAdapter(cmd)
                    FillDt = New DataTable
                    DaCount.Fill(FillDt)
                    Dim Prod As DataRow = FillDatagridProduct.NewRow
                    If FillDt.Rows.Count > 0 Then
                        Dim PriceChange = FillDt(0)(1)
                        'Exist then check for update
                        Query1 = "SELECT * FROM admin_products_org WHERE product_id = " & a
                        cmd = New MySqlCommand(Query1, ConnectionServer)
                        DaCount = New MySqlDataAdapter(cmd)
                        DtCount = New DataTable
                        DaCount.Fill(DtCount)
                        If FillDt(0)(0).ToString <> DtCount(0)(11) Then
                            Prod("product_id") = DtCount(0)(0)
                            Prod("product_sku") = DtCount(0)(1)
                            Prod("product_name") = DtCount(0)(2)
                            Prod("formula_id") = DtCount(0)(3)
                            Prod("product_barcode") = DtCount(0)(4)
                            Prod("product_category") = DtCount(0)(5)
                            If FillDt(0)(1) = 1 Then
                                Dim sql2 = "SELECT product_price FROM loc_admin_products WHERE server_product_id = " & a
                                Dim cmd2 As MySqlCommand = New MySqlCommand(sql2, LocalhostConn)
                                Dim da2 As MySqlDataAdapter = New MySqlDataAdapter(cmd2)
                                Dim dt2 As DataTable = New DataTable
                                da2.Fill(dt2)
                                Prod("product_price") = dt2(0)(0)
                            Else
                                Prod("product_price") = DtCount(0)(6)
                            End If
                            Prod("product_desc") = DtCount(0)(7)
                            Prod("product_image") = DtCount(0)(8)
                            Prod("product_status") = DtCount(0)(9)
                            Prod("origin") = DtCount(0)(10)
                            Prod("date_modified") = DtCount(0)(11)
                            Prod("inventory_id") = DtCount(0)(12)
                            Prod("addontype") = DtCount(0)(13)
                            Prod("half_batch") = DtCount(0)(14)
                            Prod("partners") = DtCount(0)(15)
                            Prod("arrangement") = DtCount(0)(16)
                            FillDatagridProduct.Rows.Add(Prod)
                        End If
                        ProgressBar1.Value = ProgressBar1.Value + 1
                        LabelStatus.Text = "Item(s) " & LabelCountAllRows.Text & " Checked " & ProgressBar1.Value & " of " & LabelCountAllRows.Text
                    Else
                        'Insert new product
                        Query1 = "SELECT * FROM admin_products_org WHERE product_id = " & a
                        cmd = New MySqlCommand(Query1, ConnectionServer)
                        DaCount = New MySqlDataAdapter(cmd)
                        DtCount = New DataTable
                        DaCount.Fill(DtCount)
                        Prod("product_id") = DtCount(0)(0)
                        Prod("product_sku") = DtCount(0)(1)
                        Prod("product_name") = DtCount(0)(2)
                        Prod("formula_id") = DtCount(0)(3)
                        Prod("product_barcode") = DtCount(0)(4)
                        Prod("product_category") = DtCount(0)(5)
                        Prod("product_price") = DtCount(0)(6)
                        Prod("product_desc") = DtCount(0)(7)
                        Prod("product_image") = DtCount(0)(8)
                        Prod("product_status") = DtCount(0)(9)
                        Prod("origin") = DtCount(0)(10)
                        Prod("date_modified") = DtCount(0)(11)
                        Prod("inventory_id") = DtCount(0)(12)
                        Prod("addontype") = DtCount(0)(13)
                        Prod("half_batch") = DtCount(0)(14)
                        Prod("partners") = DtCount(0)(15)
                        Prod("arrangement") = DtCount(0)(16)
                        FillDatagridProduct.Rows.Add(Prod)
                        LabelNewRows.Text += Val(LabelNewRows.Text)
                    End If
                Next
                ConnectionLocal.Close()
                ConnectionServer.Close()
            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Settings Form: " & ex.ToString, "Critical")

            BackgroundWorker1.CancelAsync()
            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub GetAllProducts()
        Try
            Dim Connection As MySqlConnection = ServerCloudCon()
            Dim SqlCount = "SELECT COUNT(product_id) FROM admin_products_org"
            Dim CmdCount As MySqlCommand = New MySqlCommand(SqlCount, Connection)
            Dim result As Integer = CmdCount.ExecuteScalar
            Dim Cmd As MySqlCommand
            FillDatagridProduct = New DataTable
            FillDatagridProduct.Columns.Add("product_id")
            FillDatagridProduct.Columns.Add("product_sku")
            FillDatagridProduct.Columns.Add("product_name")
            FillDatagridProduct.Columns.Add("formula_id")
            FillDatagridProduct.Columns.Add("product_barcode")
            FillDatagridProduct.Columns.Add("product_category")
            FillDatagridProduct.Columns.Add("product_price")
            FillDatagridProduct.Columns.Add("product_desc")
            FillDatagridProduct.Columns.Add("product_image")
            FillDatagridProduct.Columns.Add("product_status")
            FillDatagridProduct.Columns.Add("origin")
            FillDatagridProduct.Columns.Add("date_modified")
            FillDatagridProduct.Columns.Add("inventory_id")
            FillDatagridProduct.Columns.Add("addontype")
            FillDatagridProduct.Columns.Add("half_batch")
            FillDatagridProduct.Columns.Add("partners")
            FillDatagridProduct.Columns.Add("arrangement")

            Dim DaCount As MySqlDataAdapter
            Dim FillDt As DataTable = New DataTable
            For a = 1 To result
                If WorkerCanceled Then
                    Exit Sub
                End If
                Dim Query As String = "SELECT * FROM admin_products_org WHERE product_id = " & a
                Cmd = New MySqlCommand(Query, Connection)
                DaCount = New MySqlDataAdapter(Cmd)
                FillDt = New DataTable
                DaCount.Fill(FillDt)
                For i As Integer = 0 To FillDt.Rows.Count - 1 Step +1
                    Dim Prod As DataRow = FillDatagridProduct.NewRow
                    Prod("product_id") = FillDt(i)(0)
                    Prod("product_sku") = FillDt(i)(1)
                    Prod("product_name") = FillDt(i)(2)
                    Prod("formula_id") = FillDt(i)(3)
                    Prod("product_barcode") = FillDt(i)(4)
                    Prod("product_category") = FillDt(i)(5)
                    Prod("product_price") = FillDt(i)(6)
                    Prod("product_desc") = FillDt(i)(7)
                    Prod("product_image") = FillDt(i)(8)
                    Prod("product_status") = FillDt(i)(9)
                    Prod("origin") = FillDt(i)(10)
                    Prod("date_modified") = FillDt(i)(11)
                    Prod("inventory_id") = FillDt(i)(12)
                    Prod("addontype") = FillDt(i)(13)
                    Prod("half_batch") = FillDt(i)(14)
                    Prod("partners") = FillDt(i)(15)
                    Prod("arrangement") = FillDt(i)(16)
                    FillDatagridProduct.Rows.Add(Prod)
                    LabelNewRows.Text += Val(LabelNewRows.Text)
                Next
            Next
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Settings Form: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub

#End Region
#Region "Formulas Update"
    Private Function LoadFormulaLocal() As DataTable
        Dim cmdlocal As MySqlCommand
        Dim dalocal As MySqlDataAdapter
        Dim dtlocal As DataTable = New DataTable
        dtlocal.Columns.Add("server_date_modified")
        dtlocal.Columns.Add("server_formula_id")
        Dim dtlocal1 As DataTable = New DataTable
        Try
            Dim sql = "SELECT server_date_modified, server_formula_id FROM loc_product_formula"
            cmdlocal = New MySqlCommand(sql, LocalhostConn)
            dalocal = New MySqlDataAdapter(cmdlocal)
            dalocal.Fill(dtlocal1)
            For i As Integer = 0 To dtlocal1.Rows.Count - 1 Step +1
                Dim Cat As DataRow = dtlocal.NewRow
                Cat("server_date_modified") = dtlocal1(i)(0).ToString
                Cat("server_formula_id") = dtlocal1(i)(1)
                dtlocal.Rows.Add(Cat)
            Next
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Settings Form: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
        Return dtlocal
    End Function
    Private Sub Function3()
        Try
            Dim ConnectionLocal As MySqlConnection = LocalhostConn()
            Dim ConnectionServer As MySqlConnection = ServerCloudCon()
            Dim FormulaLocal = LoadFormulaLocal()

            Dim Query = "SELECT * FROM loc_product_formula"
            Dim CmdCheck As MySqlCommand = New MySqlCommand(Query, ConnectionLocal)
            Dim DaCheck As MySqlDataAdapter = New MySqlDataAdapter(CmdCheck)
            Dim DtCheck As DataTable = New DataTable
            DaCheck.Fill(DtCheck)
            Dim cmdserver As MySqlCommand
            Dim daserver As MySqlDataAdapter
            Dim dtserver As DataTable
            If DtCheck.Rows.Count < 1 Then
                Dim sql = "SELECT `server_formula_id`, `product_ingredients`, `primary_unit`, `primary_value`, `secondary_unit`, `secondary_value`, `serving_unit`, `serving_value`, `no_servings`, `status`, `date_modified`, `unit_cost`, `origin` FROM admin_product_formula_org"
                cmdserver = New MySqlCommand(sql, ConnectionServer)
                daserver = New MySqlDataAdapter(cmdserver)
                dtserver = New DataTable
                daserver.Fill(dtserver)
                For i As Integer = 0 To dtserver.Rows.Count - 1 Step +1
                    If WorkerCanceled Then
                        Exit Sub
                    End If
                    DataGridView3.Rows.Add(dtserver(i)(0), dtserver(i)(1), dtserver(i)(2), dtserver(i)(3), dtserver(i)(4), dtserver(i)(5), dtserver(i)(6), dtserver(i)(7), dtserver(i)(8), dtserver(i)(9), dtserver(i)(10).ToString, dtserver(i)(11), dtserver(i)(12))
                    LabelNewRows.Text += Val(LabelNewRows.Text)
                Next
            Else
                Dim Ids As String = ""

                If ValidCloudConnection = True Then
                    For i As Integer = 0 To FormulaLocal.Rows.Count - 1 Step +1
                        If Ids = "" Then
                            Ids = "" & FormulaLocal(i)(1) & ""
                        Else
                            Ids += "," & FormulaLocal(i)(1) & ""
                        End If
                    Next
                    Dim sql = "SELECT `server_formula_id`, `product_ingredients`, `primary_unit`, `primary_value`, `secondary_unit`, `secondary_value`, `serving_unit`, `serving_value`, `no_servings`, `status`, `date_modified`, `unit_cost`, `origin` FROM admin_product_formula_org WHERE server_formula_id IN (" & Ids & ") "
                    cmdserver = New MySqlCommand(sql, ConnectionServer)
                    daserver = New MySqlDataAdapter(cmdserver)
                    dtserver = New DataTable
                    daserver.Fill(dtserver)
                    For i As Integer = 0 To dtserver.Rows.Count - 1 Step +1
                        If WorkerCanceled Then
                            Exit Sub
                        End If
                        If FormulaLocal(i)(0).ToString <> dtserver(i)(10).ToString Then
                            DataGridView3.Rows.Add(dtserver(i)(0), dtserver(i)(1), dtserver(i)(2), dtserver(i)(3), dtserver(i)(4), dtserver(i)(5), dtserver(i)(6), dtserver(i)(7), dtserver(i)(8), dtserver(i)(9), dtserver(i)(10).ToString, dtserver(i)(11), dtserver(i)(12))
                        End If
                        ProgressBar1.Value = ProgressBar1.Value + 1
                        LabelStatus.Text = "Item(s) " & LabelCountAllRows.Text & " Checked " & ProgressBar1.Value & " of " & LabelCountAllRows.Text
                    Next
                    Dim sql2 = "SELECT `server_formula_id`, `product_ingredients`, `primary_unit`, `primary_value`, `secondary_unit`, `secondary_value`, `serving_unit`, `serving_value`, `no_servings`, `status`, `date_modified`, `unit_cost`, `origin` FROM admin_product_formula_org WHERE server_formula_id NOT IN (" & Ids & ") "
                    cmdserver = New MySqlCommand(sql2, ConnectionServer)
                    daserver = New MySqlDataAdapter(cmdserver)
                    dtserver = New DataTable
                    daserver.Fill(dtserver)
                    For i As Integer = 0 To dtserver.Rows.Count - 1 Step +1
                        If WorkerCanceled Then
                            Exit Sub
                        End If
                        If FormulaLocal(i)(0).ToString <> dtserver(i)(10) Then
                            DataGridView3.Rows.Add(dtserver(i)(0), dtserver(i)(1), dtserver(i)(2), dtserver(i)(3), dtserver(i)(4), dtserver(i)(5), dtserver(i)(6), dtserver(i)(7), dtserver(i)(8), dtserver(i)(9), dtserver(i)(10).ToString, dtserver(i)(11), dtserver(i)(12))
                            LabelNewRows.Text += Val(LabelNewRows.Text)
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Settings Form: " & ex.ToString, "Critical")

            BackgroundWorker1.CancelAsync()
            SendErrorReport(ex.ToString)
        End Try
    End Sub
#End Region
#Region "Inventory Update"
    Private Function LoadInventoryLocal() As DataTable
        Dim cmdlocal As MySqlCommand
        Dim dalocal As MySqlDataAdapter
        Dim dtlocal As DataTable = New DataTable
        dtlocal.Columns.Add("server_date_modified")
        dtlocal.Columns.Add("server_inventory_id")
        Dim dtlocal1 As DataTable = New DataTable
        Try
            Dim sql = "SELECT server_date_modified , server_inventory_id FROM loc_pos_inventory"
            cmdlocal = New MySqlCommand(sql, LocalhostConn)
            dalocal = New MySqlDataAdapter(cmdlocal)
            dalocal.Fill(dtlocal)
            For i As Integer = 0 To dtlocal1.Rows.Count - 1 Step +1
                Dim Cat As DataRow = dtlocal.NewRow
                Cat("server_date_modified") = dtlocal1(i)(0).ToString
                Cat("server_inventory_id") = dtlocal1(i)(1)
                dtlocal.Rows.Add(Cat)
            Next
            LocalhostConn.Close()
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Settings Form: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
        Return dtlocal
    End Function
    Private Sub Function4()
        Try
            Dim ConnectionLocal As MySqlConnection = LocalhostConn()
            Dim ConnectionServer As MySqlConnection = ServerCloudCon()
            Dim InventoryLocal = LoadInventoryLocal()

            Dim Query = "SELECT * FROM loc_pos_inventory"
            Dim CmdCheck As MySqlCommand = New MySqlCommand(Query, ConnectionLocal)
            Dim DaCheck As MySqlDataAdapter = New MySqlDataAdapter(CmdCheck)
            Dim DtCheck As DataTable = New DataTable
            DaCheck.Fill(DtCheck)
            Dim cmdserver As MySqlCommand
            Dim daserver As MySqlDataAdapter
            Dim dtserver As DataTable
            If DtCheck.Rows.Count < 1 Then
                Dim sql = "SELECT `server_inventory_id`, `product_ingredients`, `sku`, `stock_primary`, `stock_secondary`, `stock_no_of_servings`, `stock_status`, `critical_limit`, `date_modified`, `main_inventory_id`, `origin`, `show_stockin` FROM admin_pos_inventory_org"
                cmdserver = New MySqlCommand(sql, ConnectionServer)
                daserver = New MySqlDataAdapter(cmdserver)
                dtserver = New DataTable
                daserver.Fill(dtserver)
                For i As Integer = 0 To dtserver.Rows.Count - 1 Step +1
                    If WorkerCanceled Then
                        Exit Sub
                    End If
                    DataGridView4.Rows.Add(dtserver(i)(0), 0, dtserver(i)(1), dtserver(i)(2), dtserver(i)(3), dtserver(i)(4), dtserver(i)(5), dtserver(i)(6), dtserver(i)(7), dtserver(i)(8).ToString, dtserver(i)(9).ToString, dtserver(i)(10).ToString, dtserver(i)(11))
                    LabelNewRows.Text += Val(LabelNewRows.Text)
                Next
            Else
                Dim Ids As String = ""

                If ValidCloudConnection = True Then
                    For i As Integer = 0 To InventoryLocal.Rows.Count - 1 Step +1
                        If Ids = "" Then
                            Ids = "" & InventoryLocal(i)(1) & ""
                        Else
                            Ids += "," & InventoryLocal(i)(1) & ""
                        End If
                    Next
                    Dim sql = "SELECT `server_inventory_id`, `product_ingredients`, `sku`, `stock_primary`, `stock_secondary`, `stock_no_of_servings`, `stock_status`, `critical_limit`, `date_modified`,`main_inventory_id`, `origin`, `show_stockin` FROM admin_pos_inventory_org WHERE server_inventory_id IN (" & Ids & ")"
                    cmdserver = New MySqlCommand(sql, ConnectionServer)
                    daserver = New MySqlDataAdapter(cmdserver)
                    dtserver = New DataTable
                    daserver.Fill(dtserver)
                    For i As Integer = 0 To dtserver.Rows.Count - 1 Step +1
                        If WorkerCanceled Then
                            Exit Sub
                        End If
                        If InventoryLocal(i)(0).ToString <> dtserver(i)(8).ToString Then
                            DataGridView4.Rows.Add(dtserver(i)(0), 0, dtserver(i)(1), dtserver(i)(2), dtserver(i)(3), dtserver(i)(4), dtserver(i)(5), dtserver(i)(6), dtserver(i)(7), dtserver(i)(8).ToString, dtserver(i)(9).ToString, dtserver(i)(10).ToString, dtserver(i)(11))
                        End If
                        ProgressBar1.Value = ProgressBar1.Value + 1
                        LabelStatus.Text = "Item(s) " & LabelCountAllRows.Text & " Checked " & ProgressBar1.Value & " of " & LabelCountAllRows.Text
                    Next
                    Dim sql2 = "SELECT `server_inventory_id`, `product_ingredients`, `sku`, `stock_primary`, `stock_secondary`, `stock_no_of_servings`, `stock_status`, `critical_limit`, `date_modified`,`main_inventory_id`, `origin`, `show_stockin` FROM admin_pos_inventory_org WHERE server_inventory_id NOT IN (" & Ids & ")"
                    cmdserver = New MySqlCommand(sql2, ConnectionServer)
                    daserver = New MySqlDataAdapter(cmdserver)
                    dtserver = New DataTable
                    daserver.Fill(dtserver)
                    For i As Integer = 0 To dtserver.Rows.Count - 1 Step +1
                        If WorkerCanceled Then
                            Exit Sub
                        End If
                        If InventoryLocal(i)(0).ToString <> dtserver(i)(8) Then
                            DataGridView4.Rows.Add(dtserver(i)(0), 0, dtserver(i)(1), dtserver(i)(2), dtserver(i)(3), dtserver(i)(4), dtserver(i)(5), dtserver(i)(6), dtserver(i)(7), dtserver(i)(8).ToString, dtserver(i)(9).ToString, dtserver(i)(10).ToString, dtserver(i)(11))
                            LabelNewRows.Text += Val(LabelNewRows.Text)
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Settings Form: " & ex.ToString, "Critical")

            BackgroundWorker1.CancelAsync()
            SendErrorReport(ex.ToString)
        End Try
    End Sub

#End Region
#Region "Coupons Update"
    Private Function LoadCouponsLocal() As DataTable
        Dim cmdlocal As MySqlCommand
        Dim dalocal As MySqlDataAdapter
        Dim dtlocal As DataTable = New DataTable
        dtlocal.Columns.Add("date_created")
        dtlocal.Columns.Add("ID")
        Dim dtlocal1 As DataTable = New DataTable
        Try
            Dim sql = "SELECT date_created, ID FROM tbcoupon"
            cmdlocal = New MySqlCommand(sql, LocalhostConn())
            dalocal = New MySqlDataAdapter(cmdlocal)
            dalocal.Fill(dtlocal1)
            For i As Integer = 0 To dtlocal1.Rows.Count - 1 Step +1
                Dim Coup As DataRow = dtlocal.NewRow
                Coup("date_created") = dtlocal1(i)(0).ToString
                Coup("ID") = dtlocal1(i)(1)
                dtlocal.Rows.Add(Coup)
            Next
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Settings Form: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
        Return dtlocal
    End Function
    Private Sub Function5()
        Try
            Dim Query = "SELECT * FROM tbcoupon"
            Dim CmdCheck As MySqlCommand = New MySqlCommand(Query, LocalhostConn)
            Dim DaCheck As MySqlDataAdapter = New MySqlDataAdapter(CmdCheck)
            Dim DtCheck As DataTable = New DataTable
            DaCheck.Fill(DtCheck)
            Dim cmdserver As MySqlCommand
            Dim daserver As MySqlDataAdapter
            Dim dtserver As DataTable
            If DtCheck.Rows.Count < 1 Then
                Dim sql = "SELECT `ID`,`Couponname_`,`Desc_`,`Discountvalue_`,`Referencevalue_`,`Type`,`Bundlebase_`,`BBValue_`,`Bundlepromo_`,`BPValue_`,`Effectivedate`,`Expirydate`,`date_created` FROM admin_coupon"
                cmdserver = New MySqlCommand(sql, ServerCloudCon())
                daserver = New MySqlDataAdapter(cmdserver)
                dtserver = New DataTable
                daserver.Fill(dtserver)
                For i As Integer = 0 To dtserver.Rows.Count - 1 Step +1
                    If WorkerCanceled Then
                        Exit Sub
                    End If
                    DataGridView6.Rows.Add(dtserver(i)(0), dtserver(i)(1), dtserver(i)(2), dtserver(i)(3).ToString, dtserver(i)(4), dtserver(i)(5), dtserver(i)(6), dtserver(i)(7), dtserver(i)(8), dtserver(i)(9), dtserver(i)(10), dtserver(i)(11), dtserver(i)(12))
                    LabelNewRows.Text += Val(LabelNewRows.Text)
                Next
            Else
                Dim Ids As String = ""
                If ValidCloudConnection = True Then
                    For i As Integer = 0 To LoadCouponsLocal.Rows.Count - 1 Step +1
                        If Ids = "" Then
                            Ids = "" & LoadCouponsLocal(i)(1) & ""
                        Else
                            Ids += "," & LoadCouponsLocal(i)(1) & ""
                        End If
                    Next
                    Dim sql = "SELECT `ID`,`Couponname_`,`Desc_`,`Discountvalue_`,`Referencevalue_`,`Type`,`Bundlebase_`,`BBValue_`,`Bundlepromo_`,`BPValue_`,`Effectivedate`,`Expirydate`,`date_created` FROM admin_coupon WHERE ID IN (" & Ids & ")"
                    cmdserver = New MySqlCommand(sql, ServerCloudCon())
                    daserver = New MySqlDataAdapter(cmdserver)
                    dtserver = New DataTable
                    daserver.Fill(dtserver)
                    For i As Integer = 0 To dtserver.Rows.Count - 1 Step +1
                        If WorkerCanceled Then
                            Exit Sub
                        End If
                        If LoadCouponsLocal(i)(0).ToString <> dtserver(i)(12).ToString Then
                            DataGridView6.Rows.Add(dtserver(i)(0), dtserver(i)(1), dtserver(i)(2), dtserver(i)(3).ToString, dtserver(i)(4), dtserver(i)(5), dtserver(i)(6), dtserver(i)(7), dtserver(i)(8), dtserver(i)(9), dtserver(i)(10), dtserver(i)(11), dtserver(i)(12))
                        End If
                        ProgressBar1.Value = ProgressBar1.Value + 1
                        LabelStatus.Text = "Item(s) " & LabelCountAllRows.Text & " Checked " & ProgressBar1.Value & " of " & LabelCountAllRows.Text
                    Next
                    Dim sql2 = "SELECT `ID`,`Couponname_`,`Desc_`,`Discountvalue_`,`Referencevalue_`,`Type`,`Bundlebase_`,`BBValue_`,`Bundlepromo_`,`BPValue_`,`Effectivedate`,`Expirydate`,`date_created` FROM admin_coupon WHERE ID NOT IN (" & Ids & ")"
                    cmdserver = New MySqlCommand(sql2, ServerCloudCon())
                    daserver = New MySqlDataAdapter(cmdserver)
                    dtserver = New DataTable
                    daserver.Fill(dtserver)
                    For i As Integer = 0 To dtserver.Rows.Count - 1 Step +1
                        If WorkerCanceled Then
                            Exit Sub
                        End If
                        If LoadCouponsLocal(i)(0) <> dtserver(i)(12) Then
                            DataGridView6.Rows.Add(dtserver(i)(0), dtserver(i)(1), dtserver(i)(2), dtserver(i)(3).ToString, dtserver(i)(4), dtserver(i)(5), dtserver(i)(6), dtserver(i)(7), dtserver(i)(8), dtserver(i)(9), dtserver(i)(10), dtserver(i)(11), dtserver(i)(12))
                            LabelNewRows.Text += Val(LabelNewRows.Text)
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Settings Form: " & ex.ToString, "Critical")

            BackgroundWorker1.CancelAsync()
            SendErrorReport(ex.ToString)
            'If table doesnt have data
        End Try
    End Sub
    Private Function LoadPartnersCategory() As DataTable
        Dim cmdlocal As MySqlCommand
        Dim dalocal As MySqlDataAdapter
        Dim dtlocal As DataTable = New DataTable
        dtlocal.Columns.Add("date_modified")
        dtlocal.Columns.Add("id")
        Dim dtlocal1 As DataTable = New DataTable
        Try
            Dim sql = "SELECT date_modified, id FROM loc_partners_transaction"
            cmdlocal = New MySqlCommand(sql, LocalhostConn())
            dalocal = New MySqlDataAdapter(cmdlocal)
            dalocal.Fill(dtlocal1)
            For i As Integer = 0 To dtlocal1.Rows.Count - 1 Step +1
                Dim Cat As DataRow = dtlocal.NewRow
                Cat("date_modified") = dtlocal1(i)(0).ToString
                Cat("id") = dtlocal1(i)(1)
                dtlocal.Rows.Add(Cat)
            Next
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Settings Form: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
        Return dtlocal
    End Function
    Private Sub Function6()
        Try
            Dim Query = "SELECT * FROM loc_partners_transaction"
            Dim CmdCheck As MySqlCommand = New MySqlCommand(Query, LocalhostConn)
            Dim DaCheck As MySqlDataAdapter = New MySqlDataAdapter(CmdCheck)
            Dim DtCheck As DataTable = New DataTable
            DaCheck.Fill(DtCheck)
            Dim cmdserver As MySqlCommand
            Dim daserver As MySqlDataAdapter
            Dim dtserver As DataTable
            If DtCheck.Rows.Count < 1 Then
                Dim sql = "SELECT `id`, `arrid`, `bankname`, `date_modified`, `active` FROM admin_partners_transaction_org"
                cmdserver = New MySqlCommand(sql, ServerCloudCon())
                daserver = New MySqlDataAdapter(cmdserver)
                dtserver = New DataTable
                daserver.Fill(dtserver)
                For i As Integer = 0 To dtserver.Rows.Count - 1 Step +1
                    If WorkerCanceled Then
                        Exit Sub
                    End If
                    DataGridView7.Rows.Add(dtserver(i)(0), dtserver(i)(1), dtserver(i)(2), dtserver(i)(3).ToString, dtserver(i)(4))
                    LabelNewRows.Text += Val(LabelNewRows.Text)
                Next
            Else
                Dim Ids As String = ""
                If ValidCloudConnection = True Then
                    For i As Integer = 0 To LoadPartnersCategory.Rows.Count - 1 Step +1
                        If Ids = "" Then
                            Ids = "" & LoadPartnersCategory(i)(1) & ""
                        Else
                            Ids += "," & LoadPartnersCategory(i)(1) & ""
                        End If
                    Next
                    Dim sql = "SELECT `id`, `arrid`, `bankname`, `date_modified`, `active` FROM admin_partners_transaction_org WHERE id IN (" & Ids & ")"
                    cmdserver = New MySqlCommand(sql, ServerCloudCon())
                    daserver = New MySqlDataAdapter(cmdserver)
                    dtserver = New DataTable
                    daserver.Fill(dtserver)
                    For i As Integer = 0 To dtserver.Rows.Count - 1 Step +1
                        If WorkerCanceled Then
                            Exit Sub
                        End If
                        If LoadPartnersCategory(i)(0).ToString <> dtserver(i)(3).ToString Then
                            DataGridView7.Rows.Add(dtserver(i)(0), dtserver(i)(1), dtserver(i)(2), dtserver(i)(3).ToString, dtserver(i)(4))
                        End If
                        ProgressBar1.Value = ProgressBar1.Value + 1
                        LabelStatus.Text = "Item(s) " & LabelCountAllRows.Text & " Checked " & ProgressBar1.Value & " of " & LabelCountAllRows.Text
                    Next
                    Dim sql2 = "SELECT `id`, `arrid`, `bankname`, `date_modified`, `active` FROM admin_partners_transaction_org WHERE id NOT IN (" & Ids & ")"
                    cmdserver = New MySqlCommand(sql2, ServerCloudCon())
                    daserver = New MySqlDataAdapter(cmdserver)
                    dtserver = New DataTable
                    daserver.Fill(dtserver)
                    For i As Integer = 0 To dtserver.Rows.Count - 1 Step +1
                        If WorkerCanceled Then
                            Exit Sub
                        End If
                        If LoadPartnersCategory(i)(0) <> dtserver(i)(3) Then
                            DataGridView7.Rows.Add(dtserver(i)(0), dtserver(i)(1), dtserver(i)(2), dtserver(i)(3).ToString, dtserver(i)(4))
                            LabelNewRows.Text += Val(LabelNewRows.Text)
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Settings Form: " & ex.ToString, "Critical")

            BackgroundWorker1.CancelAsync()
            SendErrorReport(ex.ToString)
            'If table doesnt have data
        End Try
    End Sub
#End Region
#End Region
#Region "Install Updates"
    Private Sub InstallUpdatesCoupons()
        Try
            Dim Connection As MySqlConnection = LocalhostConn()
            Dim cmdlocal As MySqlCommand
            With DataGridView6
                For i As Integer = 0 To .Rows.Count - 1 Step +1
                    Dim sql = "SELECT ID FROM tbcoupon WHERE ID = " & .Rows(i).Cells(0).Value
                    cmdlocal = New MySqlCommand(sql, Connection)
                    Dim result As Integer = cmdlocal.ExecuteScalar
                    If result = 0 Then
                        Dim sqlinsert = "INSERT INTO `tbcoupon`(`Couponname_`, `Desc_`, `Discountvalue_`, `Referencevalue_`, `Type`, `Bundlebase_`, `BBValue_`, `Bundlepromo_`, `BPValue_`, `Effectivedate`, `Expirydate`, `active`, `store_id`, `crew_id`, `guid`, `origin`, `synced`, `date_created`) VALUES (@0,@1,@2,@3,@4,@5,@6,@7,@8,@9,@10,@11,@12,@13,@14,@15,@16,@17)"
                        cmdlocal = New MySqlCommand(sqlinsert, Connection)
                        cmdlocal.Parameters.Add("@0", MySqlDbType.Text).Value = .Rows(i).Cells(1).Value.ToString()
                        cmdlocal.Parameters.Add("@1", MySqlDbType.Text).Value = .Rows(i).Cells(2).Value.ToString()
                        cmdlocal.Parameters.Add("@2", MySqlDbType.Text).Value = .Rows(i).Cells(3).Value.ToString()
                        cmdlocal.Parameters.Add("@3", MySqlDbType.Text).Value = .Rows(i).Cells(4).Value.ToString()
                        cmdlocal.Parameters.Add("@4", MySqlDbType.Text).Value = .Rows(i).Cells(5).Value.ToString()
                        cmdlocal.Parameters.Add("@5", MySqlDbType.Text).Value = .Rows(i).Cells(6).Value.ToString()
                        cmdlocal.Parameters.Add("@6", MySqlDbType.Text).Value = .Rows(i).Cells(7).Value.ToString()
                        cmdlocal.Parameters.Add("@7", MySqlDbType.Text).Value = .Rows(i).Cells(8).Value.ToString()
                        cmdlocal.Parameters.Add("@8", MySqlDbType.Text).Value = .Rows(i).Cells(9).Value.ToString()
                        cmdlocal.Parameters.Add("@9", MySqlDbType.Text).Value = .Rows(i).Cells(10).Value.ToString()
                        cmdlocal.Parameters.Add("@10", MySqlDbType.Text).Value = .Rows(i).Cells(11).Value.ToString()
                        cmdlocal.Parameters.Add("@11", MySqlDbType.Text).Value = "1"
                        cmdlocal.Parameters.Add("@12", MySqlDbType.Text).Value = ClientStoreID
                        cmdlocal.Parameters.Add("@13", MySqlDbType.Text).Value = ClientCrewID
                        cmdlocal.Parameters.Add("@14", MySqlDbType.Text).Value = ClientGuid
                        cmdlocal.Parameters.Add("@15", MySqlDbType.Text).Value = "Server"
                        cmdlocal.Parameters.Add("@16", MySqlDbType.Text).Value = "Synced"
                        cmdlocal.Parameters.Add("@17", MySqlDbType.Text).Value = .Rows(i).Cells(12).Value.ToString()
                        cmdlocal.ExecuteNonQuery()
                    Else
                        Dim sqlupdate = "UPDATE `tbcoupon` SET `Couponname_` = @0, `Desc_` = @1, `Discountvalue_` = @2, `Referencevalue_` = @3, `Type` = @4, `Bundlebase_` = @5, `BBValue_` = @6, `Bundlepromo_` = @7, `BPValue_` = @8, `Effectivedate` = @9, `Expirydate` = @10, `active` = @11, `store_id` = @12, `crew_id` = @13, `guid` = @14, `origin` = @15, `synced` = @16, `date_created` = @17 WHERE ID = " & .Rows(i).Cells(0).Value
                        cmdlocal = New MySqlCommand(sqlupdate, Connection)
                        cmdlocal.Parameters.Add("@0", MySqlDbType.Text).Value = .Rows(i).Cells(1).Value.ToString()
                        cmdlocal.Parameters.Add("@1", MySqlDbType.Text).Value = .Rows(i).Cells(2).Value.ToString()
                        cmdlocal.Parameters.Add("@2", MySqlDbType.Text).Value = .Rows(i).Cells(3).Value.ToString()
                        cmdlocal.Parameters.Add("@3", MySqlDbType.Text).Value = .Rows(i).Cells(4).Value.ToString()
                        cmdlocal.Parameters.Add("@4", MySqlDbType.Text).Value = .Rows(i).Cells(5).Value.ToString()
                        cmdlocal.Parameters.Add("@5", MySqlDbType.Text).Value = .Rows(i).Cells(6).Value.ToString()
                        cmdlocal.Parameters.Add("@6", MySqlDbType.Text).Value = .Rows(i).Cells(7).Value.ToString()
                        cmdlocal.Parameters.Add("@7", MySqlDbType.Text).Value = .Rows(i).Cells(8).Value.ToString()
                        cmdlocal.Parameters.Add("@8", MySqlDbType.Text).Value = .Rows(i).Cells(9).Value.ToString()
                        cmdlocal.Parameters.Add("@9", MySqlDbType.Text).Value = .Rows(i).Cells(10).Value.ToString()
                        cmdlocal.Parameters.Add("@10", MySqlDbType.Text).Value = .Rows(i).Cells(11).Value.ToString()
                        cmdlocal.Parameters.Add("@11", MySqlDbType.Text).Value = "1"
                        cmdlocal.Parameters.Add("@12", MySqlDbType.Text).Value = ClientStoreID
                        cmdlocal.Parameters.Add("@13", MySqlDbType.Text).Value = ClientCrewID
                        cmdlocal.Parameters.Add("@14", MySqlDbType.Text).Value = ClientGuid
                        cmdlocal.Parameters.Add("@15", MySqlDbType.Text).Value = "Server"
                        cmdlocal.Parameters.Add("@16", MySqlDbType.Text).Value = "Synced"
                        cmdlocal.Parameters.Add("@17", MySqlDbType.Text).Value = .Rows(i).Cells(12).Value.ToString()
                        cmdlocal.ExecuteNonQuery()
                    End If
                Next
            End With
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Settings Form: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub InstallUpdatesPartners()
        Try
            Dim Connection As MySqlConnection = LocalhostConn()
            Dim cmdlocal As MySqlCommand
            With DataGridView7
                For i As Integer = 0 To .Rows.Count - 1 Step +1
                    Dim sql = "SELECT id FROM loc_partners_transaction WHERE id = " & .Rows(i).Cells(0).Value
                    cmdlocal = New MySqlCommand(sql, Connection)
                    Dim result As Integer = cmdlocal.ExecuteScalar
                    If result = 0 Then
                        Dim sqlinsert = "INSERT INTO `loc_partners_transaction`(`arrid`, `bankname`, `date_modified`, `crew_id`, `store_id`, `guid`, `active`, `synced`) VALUES (@0,@1,@2,@3,@4,@5,@6,@7)"
                        cmdlocal = New MySqlCommand(sqlinsert, Connection)
                        cmdlocal.Parameters.Add("@0", MySqlDbType.Int64).Value = .Rows(i).Cells(1).Value.ToString()
                        cmdlocal.Parameters.Add("@1", MySqlDbType.VarChar).Value = .Rows(i).Cells(2).Value.ToString()
                        cmdlocal.Parameters.Add("@2", MySqlDbType.Text).Value = .Rows(i).Cells(3).Value.ToString()
                        cmdlocal.Parameters.Add("@3", MySqlDbType.VarChar).Value = "Admin"
                        cmdlocal.Parameters.Add("@4", MySqlDbType.VarChar).Value = ClientStoreID
                        cmdlocal.Parameters.Add("@5", MySqlDbType.VarChar).Value = ClientGuid
                        cmdlocal.Parameters.Add("@6", MySqlDbType.Int64).Value = .Rows(i).Cells(4).Value.ToString()
                        cmdlocal.Parameters.Add("@7", MySqlDbType.VarChar).Value = "Synced"
                        cmdlocal.ExecuteNonQuery()
                    Else
                        Dim sqlupdate = "UPDATE `loc_partners_transaction` SET `arrid`=@0,`bankname`=@1,`date_modified`=@2,`crew_id`=@3,`store_id`=@4,`guid`=@5,`active`=@6,`synced`=@7 WHERE id = " & .Rows(i).Cells(0).Value
                        cmdlocal = New MySqlCommand(sqlupdate, Connection)
                        cmdlocal.Parameters.Add("@0", MySqlDbType.Int64).Value = .Rows(i).Cells(1).Value.ToString()
                        cmdlocal.Parameters.Add("@1", MySqlDbType.VarChar).Value = .Rows(i).Cells(2).Value.ToString()
                        cmdlocal.Parameters.Add("@2", MySqlDbType.Text).Value = .Rows(i).Cells(3).Value.ToString()
                        cmdlocal.Parameters.Add("@3", MySqlDbType.VarChar).Value = "Admin"
                        cmdlocal.Parameters.Add("@4", MySqlDbType.VarChar).Value = ClientStoreID
                        cmdlocal.Parameters.Add("@5", MySqlDbType.VarChar).Value = ClientGuid
                        cmdlocal.Parameters.Add("@6", MySqlDbType.Int64).Value = .Rows(i).Cells(4).Value.ToString()
                        cmdlocal.Parameters.Add("@7", MySqlDbType.VarChar).Value = "Synced"
                        cmdlocal.ExecuteNonQuery()
                    End If
                Next
            End With
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Settings Form: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub InstallUpdatesCategory()
        Try
            Dim cmdlocal As MySqlCommand
            With DataGridView1
                For i As Integer = 0 To .Rows.Count - 1 Step +1
                    Dim sql = "SELECT category_id FROM loc_admin_category WHERE category_id = " & .Rows(i).Cells(0).Value
                    cmdlocal = New MySqlCommand(sql, LocalhostConn())
                    Dim result As Integer = cmdlocal.ExecuteScalar
                    If result = 0 Then
                        Dim sqlinsert = "INSERT INTO `loc_admin_category`(`category_name`, `brand_name`, `updated_at`, `origin`, `status`) VALUES (@0,@1,@2,@3,@4)"
                        cmdlocal = New MySqlCommand(sqlinsert, LocalhostConn())
                        cmdlocal.Parameters.Add("@0", MySqlDbType.VarChar).Value = .Rows(i).Cells(1).Value.ToString()
                        cmdlocal.Parameters.Add("@1", MySqlDbType.VarChar).Value = .Rows(i).Cells(2).Value.ToString()
                        cmdlocal.Parameters.Add("@2", MySqlDbType.VarChar).Value = .Rows(i).Cells(3).Value.ToString()
                        cmdlocal.Parameters.Add("@3", MySqlDbType.VarChar).Value = .Rows(i).Cells(4).Value.ToString()
                        cmdlocal.Parameters.Add("@4", MySqlDbType.Int64).Value = .Rows(i).Cells(5).Value.ToString()
                        cmdlocal.ExecuteNonQuery()
                    Else
                        Dim sqlupdate = "UPDATE `loc_admin_category` SET `category_name`=@0,`brand_name`=@1,`updated_at`=@2,`origin`=@3,`status`=@4 WHERE category_id = " & .Rows(i).Cells(0).Value
                        cmdlocal = New MySqlCommand(sqlupdate, LocalhostConn())
                        cmdlocal.Parameters.Add("@0", MySqlDbType.VarChar).Value = .Rows(i).Cells(1).Value.ToString()
                        cmdlocal.Parameters.Add("@1", MySqlDbType.VarChar).Value = .Rows(i).Cells(2).Value.ToString()
                        cmdlocal.Parameters.Add("@2", MySqlDbType.VarChar).Value = .Rows(i).Cells(3).Value.ToString()
                        cmdlocal.Parameters.Add("@3", MySqlDbType.VarChar).Value = .Rows(i).Cells(4).Value.ToString()
                        cmdlocal.Parameters.Add("@4", MySqlDbType.Int64).Value = .Rows(i).Cells(5).Value.ToString()
                        cmdlocal.ExecuteNonQuery()
                    End If
                Next
            End With
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Settings Form: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub InstallUpdatesFormula()
        Try
            Dim cmdlocal As MySqlCommand
            With DataGridView3
                For i As Integer = 0 To .Rows.Count - 1 Step +1
                    Dim sql = "SELECT formula_id FROM loc_product_formula WHERE server_formula_id = " & .Rows(i).Cells(0).Value
                    cmdlocal = New MySqlCommand(sql, LocalhostConn())
                    Dim result As Integer = cmdlocal.ExecuteScalar
                    If result = 0 Then
                        Dim sqlinsert = "INSERT INTO loc_product_formula (`server_formula_id`,`product_ingredients`, `primary_unit`, `primary_value`, `secondary_unit`, `secondary_value`, `serving_unit`, `serving_value`, `no_servings`, `status`, `date_modified`, `unit_cost`, `origin`, `store_id`, `guid`, `crew_id`, `server_date_modified`) VALUES
                                        (@0 ,@1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11 , @12 , @13 , @14, @15, @16)"
                        cmdlocal = New MySqlCommand(sqlinsert, LocalhostConn())
                        cmdlocal.Parameters.Add("@0", MySqlDbType.Int64).Value = .Rows(i).Cells(0).Value.ToString()
                        cmdlocal.Parameters.Add("@1", MySqlDbType.VarChar).Value = .Rows(i).Cells(1).Value.ToString()
                        cmdlocal.Parameters.Add("@2", MySqlDbType.VarChar).Value = .Rows(i).Cells(2).Value.ToString()
                        cmdlocal.Parameters.Add("@3", MySqlDbType.VarChar).Value = .Rows(i).Cells(3).Value.ToString()
                        cmdlocal.Parameters.Add("@4", MySqlDbType.VarChar).Value = .Rows(i).Cells(4).Value.ToString()
                        cmdlocal.Parameters.Add("@5", MySqlDbType.VarChar).Value = .Rows(i).Cells(5).Value.ToString()
                        cmdlocal.Parameters.Add("@6", MySqlDbType.VarChar).Value = .Rows(i).Cells(6).Value.ToString()
                        cmdlocal.Parameters.Add("@7", MySqlDbType.VarChar).Value = .Rows(i).Cells(7).Value.ToString()
                        cmdlocal.Parameters.Add("@8", MySqlDbType.VarChar).Value = .Rows(i).Cells(8).Value.ToString()
                        cmdlocal.Parameters.Add("@9", MySqlDbType.Int64).Value = .Rows(i).Cells(9).Value.ToString()
                        cmdlocal.Parameters.Add("@10", MySqlDbType.VarChar).Value = .Rows(i).Cells(10).Value
                        cmdlocal.Parameters.Add("@11", MySqlDbType.Decimal).Value = .Rows(i).Cells(11).Value.ToString()
                        cmdlocal.Parameters.Add("@12", MySqlDbType.VarChar).Value = .Rows(i).Cells(12).Value.ToString()
                        cmdlocal.Parameters.Add("@13", MySqlDbType.VarChar).Value = ClientStoreID
                        cmdlocal.Parameters.Add("@14", MySqlDbType.VarChar).Value = ClientGuid
                        cmdlocal.Parameters.Add("@15", MySqlDbType.VarChar).Value = "0"
                        cmdlocal.Parameters.Add("@16", MySqlDbType.Text).Value = .Rows(i).Cells(10).Value.ToString()
                        cmdlocal.ExecuteNonQuery()
                    Else
                        Dim sqlupdate = "UPDATE `loc_product_formula` SET `server_formula_id`= @0,`product_ingredients`= @1,`primary_unit`= @2,`primary_value`= @3,`secondary_unit`= @4,`secondary_value`=@5,`serving_unit`=@6,`serving_value`=@7,`no_servings`=@8,`status`=@9,`date_modified`=@10,`unit_cost`=@11,`origin`=@12,`store_id`=@13,`guid`=@14, `crew_id`=@15,`server_date_modified`=@16 WHERE server_formula_id =  " & .Rows(i).Cells(0).Value
                        cmdlocal = New MySqlCommand(sqlupdate, LocalhostConn())
                        cmdlocal.Parameters.Add("@0", MySqlDbType.Int64).Value = .Rows(i).Cells(0).Value.ToString()
                        cmdlocal.Parameters.Add("@1", MySqlDbType.VarChar).Value = .Rows(i).Cells(1).Value.ToString()
                        cmdlocal.Parameters.Add("@2", MySqlDbType.VarChar).Value = .Rows(i).Cells(2).Value.ToString()
                        cmdlocal.Parameters.Add("@3", MySqlDbType.VarChar).Value = .Rows(i).Cells(3).Value.ToString()
                        cmdlocal.Parameters.Add("@4", MySqlDbType.VarChar).Value = .Rows(i).Cells(4).Value.ToString()
                        cmdlocal.Parameters.Add("@5", MySqlDbType.VarChar).Value = .Rows(i).Cells(5).Value.ToString()
                        cmdlocal.Parameters.Add("@6", MySqlDbType.VarChar).Value = .Rows(i).Cells(6).Value.ToString()
                        cmdlocal.Parameters.Add("@7", MySqlDbType.VarChar).Value = .Rows(i).Cells(7).Value.ToString()
                        cmdlocal.Parameters.Add("@8", MySqlDbType.VarChar).Value = .Rows(i).Cells(8).Value.ToString()
                        cmdlocal.Parameters.Add("@9", MySqlDbType.Int64).Value = .Rows(i).Cells(9).Value.ToString()
                        cmdlocal.Parameters.Add("@10", MySqlDbType.VarChar).Value = .Rows(i).Cells(10).Value
                        cmdlocal.Parameters.Add("@11", MySqlDbType.Decimal).Value = .Rows(i).Cells(11).Value.ToString()
                        cmdlocal.Parameters.Add("@12", MySqlDbType.VarChar).Value = .Rows(i).Cells(12).Value.ToString()
                        cmdlocal.Parameters.Add("@13", MySqlDbType.VarChar).Value = ClientStoreID
                        cmdlocal.Parameters.Add("@14", MySqlDbType.VarChar).Value = ClientGuid
                        cmdlocal.Parameters.Add("@15", MySqlDbType.VarChar).Value = "0"
                        cmdlocal.Parameters.Add("@16", MySqlDbType.Text).Value = .Rows(i).Cells(10).Value.ToString()
                        cmdlocal.ExecuteNonQuery()
                    End If
                Next
            End With
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Settings Form: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub InstallUpdatesInventory()
        Try
            Dim cmdlocal As MySqlCommand
            With DataGridView4
                For i As Integer = 0 To .Rows.Count - 1 Step +1
                    Dim sql = "SELECT inventory_id FROM loc_pos_inventory WHERE server_inventory_id = " & .Rows(i).Cells(0).Value
                    cmdlocal = New MySqlCommand(sql, LocalhostConn())
                    Dim result As Integer = cmdlocal.ExecuteScalar
                    If result = 0 Then
                        Dim sqlinsert = "INSERT INTO loc_pos_inventory (`server_inventory_id`,`formula_id`,`product_ingredients`,`sku`,`stock_primary`,`stock_secondary`,`stock_no_of_servings`,`stock_status`,`critical_limit`,`created_at`,`server_date_modified`,`store_id`,`crew_id`,`guid`,`synced`,`main_inventory_id`) VALUES
                                        (@0 ,@1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11, @12, @13, @14, @15)"
                        cmdlocal = New MySqlCommand(sqlinsert, LocalhostConn())
                        cmdlocal.Parameters.Add("@0", MySqlDbType.Int64).Value = .Rows(i).Cells(0).Value.ToString()
                        cmdlocal.Parameters.Add("@1", MySqlDbType.Int64).Value = .Rows(i).Cells(1).Value.ToString()
                        cmdlocal.Parameters.Add("@2", MySqlDbType.VarChar).Value = .Rows(i).Cells(2).Value.ToString()
                        cmdlocal.Parameters.Add("@3", MySqlDbType.VarChar).Value = .Rows(i).Cells(3).Value.ToString()
                        cmdlocal.Parameters.Add("@4", MySqlDbType.Decimal).Value = .Rows(i).Cells(4).Value.ToString()
                        cmdlocal.Parameters.Add("@5", MySqlDbType.Decimal).Value = .Rows(i).Cells(5).Value.ToString()
                        cmdlocal.Parameters.Add("@6", MySqlDbType.Decimal).Value = .Rows(i).Cells(6).Value.ToString()
                        cmdlocal.Parameters.Add("@7", MySqlDbType.Int64).Value = .Rows(i).Cells(7).Value.ToString()
                        cmdlocal.Parameters.Add("@8", MySqlDbType.Int64).Value = .Rows(i).Cells(8).Value.ToString()
                        cmdlocal.Parameters.Add("@9", MySqlDbType.Text).Value = .Rows(i).Cells(9).Value.ToString()
                        cmdlocal.Parameters.Add("@10", MySqlDbType.Text).Value = .Rows(i).Cells(9).Value.ToString()
                        cmdlocal.Parameters.Add("@11", MySqlDbType.VarChar).Value = ClientStoreID
                        cmdlocal.Parameters.Add("@12", MySqlDbType.VarChar).Value = "0"
                        cmdlocal.Parameters.Add("@13", MySqlDbType.VarChar).Value = ClientGuid
                        cmdlocal.Parameters.Add("@14", MySqlDbType.VarChar).Value = "Synced"
                        cmdlocal.Parameters.Add("@15", MySqlDbType.VarChar).Value = .Rows(i).Cells(10).Value.ToString()
                        cmdlocal.ExecuteNonQuery()
                    Else
                        Dim sqlUpdate = "UPDATE `loc_pos_inventory` SET `server_inventory_id`= @0,`formula_id`=@1,`product_ingredients`=@2,`sku`=@3,`stock_primary`=@4,`stock_secondary`=@5,`stock_no_of_servings`=@6,`stock_status`=@7,`critical_limit`=@8,`created_at`=@9,`server_date_modified`=@10,`store_id`=@11,`crew_id`=@12,`guid`=@13,`synced`=@14,`main_inventory_id`=@15 WHERE `server_inventory_id`= " & .Rows(i).Cells(0).Value
                        cmdlocal = New MySqlCommand(sqlUpdate, LocalhostConn())
                        cmdlocal.Parameters.Add("@0", MySqlDbType.Int64).Value = .Rows(i).Cells(0).Value.ToString()
                        cmdlocal.Parameters.Add("@1", MySqlDbType.Int64).Value = .Rows(i).Cells(1).Value.ToString()
                        cmdlocal.Parameters.Add("@2", MySqlDbType.VarChar).Value = .Rows(i).Cells(2).Value.ToString()
                        cmdlocal.Parameters.Add("@3", MySqlDbType.VarChar).Value = .Rows(i).Cells(3).Value.ToString()
                        cmdlocal.Parameters.Add("@4", MySqlDbType.Decimal).Value = .Rows(i).Cells(4).Value.ToString()
                        cmdlocal.Parameters.Add("@5", MySqlDbType.Decimal).Value = .Rows(i).Cells(5).Value.ToString()
                        cmdlocal.Parameters.Add("@6", MySqlDbType.Decimal).Value = .Rows(i).Cells(6).Value.ToString()
                        cmdlocal.Parameters.Add("@7", MySqlDbType.Int64).Value = .Rows(i).Cells(7).Value.ToString()
                        cmdlocal.Parameters.Add("@8", MySqlDbType.Int64).Value = .Rows(i).Cells(8).Value.ToString()
                        cmdlocal.Parameters.Add("@9", MySqlDbType.Text).Value = .Rows(i).Cells(9).Value.ToString()
                        cmdlocal.Parameters.Add("@10", MySqlDbType.Text).Value = .Rows(i).Cells(9).Value.ToString()
                        cmdlocal.Parameters.Add("@11", MySqlDbType.VarChar).Value = ClientStoreID
                        cmdlocal.Parameters.Add("@12", MySqlDbType.VarChar).Value = "0"
                        cmdlocal.Parameters.Add("@13", MySqlDbType.VarChar).Value = ClientGuid
                        cmdlocal.Parameters.Add("@14", MySqlDbType.VarChar).Value = "Synced"
                        cmdlocal.Parameters.Add("@15", MySqlDbType.VarChar).Value = .Rows(i).Cells(10).Value.ToString()
                        cmdlocal.ExecuteNonQuery()
                    End If
                Next
            End With
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Settings Form: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub InstallUpdatesProducts()
        Try
            Dim cmdlocal As MySqlCommand
            With DataGridView2
                For i As Integer = 0 To .Rows.Count - 1 Step +1
                    Dim sql = "SELECT product_id FROM loc_admin_products WHERE server_product_id = " & .Rows(i).Cells(0).Value
                    cmdlocal = New MySqlCommand(sql, LocalhostConn())
                    Dim result As Integer = cmdlocal.ExecuteScalar
                    If result = 0 Then
                        Dim sqlinsert = "INSERT INTO loc_admin_products (`server_product_id`, `product_sku`, `product_name`, `formula_id`, `product_barcode`, `product_category`, `product_price`, `product_desc`, `product_image`, `product_status`, `origin`, `date_modified`, `server_inventory_id`, `guid`, `store_id`, `crew_id`, `synced`) VALUES
                                        (@0 ,@1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11, @12, @13, @14, @15, @16)"
                        cmdlocal = New MySqlCommand(sqlinsert, LocalhostConn())
                        cmdlocal.Parameters.Add("@0", MySqlDbType.Int64).Value = .Rows(i).Cells(0).Value.ToString()
                        cmdlocal.Parameters.Add("@1", MySqlDbType.VarChar).Value = .Rows(i).Cells(1).Value.ToString()
                        cmdlocal.Parameters.Add("@2", MySqlDbType.VarChar).Value = .Rows(i).Cells(2).Value.ToString()
                        cmdlocal.Parameters.Add("@3", MySqlDbType.VarChar).Value = .Rows(i).Cells(3).Value.ToString()
                        cmdlocal.Parameters.Add("@4", MySqlDbType.VarChar).Value = .Rows(i).Cells(4).Value.ToString()
                        cmdlocal.Parameters.Add("@5", MySqlDbType.VarChar).Value = .Rows(i).Cells(5).Value.ToString()
                        cmdlocal.Parameters.Add("@6", MySqlDbType.Int64).Value = .Rows(i).Cells(6).Value.ToString()
                        cmdlocal.Parameters.Add("@7", MySqlDbType.VarChar).Value = .Rows(i).Cells(7).Value.ToString()
                        cmdlocal.Parameters.Add("@8", MySqlDbType.VarChar).Value = .Rows(i).Cells(8).Value.ToString()
                        cmdlocal.Parameters.Add("@9", MySqlDbType.VarChar).Value = .Rows(i).Cells(9).Value.ToString()
                        cmdlocal.Parameters.Add("@10", MySqlDbType.VarChar).Value = .Rows(i).Cells(10).Value.ToString()
                        cmdlocal.Parameters.Add("@11", MySqlDbType.VarChar).Value = .Rows(i).Cells(11).Value.ToString()
                        cmdlocal.Parameters.Add("@12", MySqlDbType.Text).Value = .Rows(i).Cells(12).Value.ToString()

                        cmdlocal.Parameters.Add("@13", MySqlDbType.VarChar).Value = ClientGuid
                        cmdlocal.Parameters.Add("@14", MySqlDbType.Int64).Value = ClientStoreID
                        cmdlocal.Parameters.Add("@15", MySqlDbType.VarChar).Value = "0"
                        cmdlocal.Parameters.Add("@16", MySqlDbType.VarChar).Value = "Synced"
                        cmdlocal.ExecuteNonQuery()
                    Else
                        ',`formula_id`=@3
                        Dim sqlupdate = "UPDATE `loc_admin_products` SET `server_product_id`=@0,`product_sku`=@1,`product_name`=@2,`product_barcode`=@4,`product_category`=@5,`product_price`=@6,`product_desc`=@7,`product_image`=@8,`product_status`=@9,`origin`=@10,`date_modified`=@11,`server_inventory_id`=@12,`guid`=@13,`store_id`=@14,`crew_id`=@15,`synced`=@16 WHERE server_product_id =  " & .Rows(i).Cells(0).Value
                        cmdlocal = New MySqlCommand(sqlupdate, LocalhostConn())
                        cmdlocal.Parameters.Add("@0", MySqlDbType.Int64).Value = .Rows(i).Cells(0).Value.ToString()
                        cmdlocal.Parameters.Add("@1", MySqlDbType.VarChar).Value = .Rows(i).Cells(1).Value.ToString()
                        cmdlocal.Parameters.Add("@2", MySqlDbType.VarChar).Value = .Rows(i).Cells(2).Value.ToString()
                        'cmdlocal.Parameters.Add("@3", MySqlDbType.VarChar).Value = .Rows(i).Cells(3).Value.ToString()
                        cmdlocal.Parameters.Add("@4", MySqlDbType.VarChar).Value = .Rows(i).Cells(4).Value.ToString()
                        cmdlocal.Parameters.Add("@5", MySqlDbType.VarChar).Value = .Rows(i).Cells(5).Value.ToString()
                        cmdlocal.Parameters.Add("@6", MySqlDbType.Int64).Value = .Rows(i).Cells(6).Value.ToString()
                        cmdlocal.Parameters.Add("@7", MySqlDbType.VarChar).Value = .Rows(i).Cells(7).Value.ToString()
                        cmdlocal.Parameters.Add("@8", MySqlDbType.VarChar).Value = .Rows(i).Cells(8).Value.ToString()
                        cmdlocal.Parameters.Add("@9", MySqlDbType.VarChar).Value = .Rows(i).Cells(9).Value.ToString()
                        cmdlocal.Parameters.Add("@10", MySqlDbType.VarChar).Value = .Rows(i).Cells(10).Value.ToString()
                        cmdlocal.Parameters.Add("@11", MySqlDbType.VarChar).Value = .Rows(i).Cells(11).Value.ToString()
                        cmdlocal.Parameters.Add("@12", MySqlDbType.Text).Value = .Rows(i).Cells(12).Value.ToString()

                        cmdlocal.Parameters.Add("@13", MySqlDbType.VarChar).Value = ClientGuid
                        cmdlocal.Parameters.Add("@14", MySqlDbType.Int64).Value = ClientStoreID
                        cmdlocal.Parameters.Add("@15", MySqlDbType.VarChar).Value = "0"
                        cmdlocal.Parameters.Add("@16", MySqlDbType.VarChar).Value = "Synced"
                        cmdlocal.ExecuteNonQuery()
                    End If
                Next
            End With
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Settings Form: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub InstallUpdatesPriceChange()
        Try
            Dim ConnectionLocal As MySqlConnection = LocalhostConn()
            Dim ConnectionServer As MySqlConnection = ServerCloudCon()
            Dim CmdCheck As MySqlCommand
            For i As Integer = 0 To PriceChangeDatatabe.Rows.Count - 1 Step +1
                Dim sql = "UPDATE loc_admin_products SET product_price = " & PriceChangeDatatabe(i)(4) & ", price_change = 1 WHERE server_product_id = " & PriceChangeDatatabe(i)(3) & ""
                CmdCheck = New MySqlCommand(sql, ConnectionLocal)
                CmdCheck.ExecuteNonQuery()
                Dim sql2 = "UPDATE loc_price_request_change SET active = " & PriceChangeDatatabe(i)(6) & " WHERE request_id = " & PriceChangeDatatabe(i)(0) & ""
                CmdCheck = New MySqlCommand(sql2, ConnectionLocal)
                CmdCheck.ExecuteNonQuery()
                Dim sq3 = "UPDATE admin_price_request SET synced = 'Synced' WHERE request_id = " & PriceChangeDatatabe(i)(0) & ""
                CmdCheck = New MySqlCommand(sq3, ConnectionServer)
                CmdCheck.ExecuteNonQuery()
            Next
            ConnectionLocal.Close()
            ConnectionServer.Close()
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Settings Form: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub InstallCoupons()
        Try
            Dim ConnectionLocal As MySqlConnection = LocalhostConn()
            Dim ConnectionServer As MySqlConnection = ServerCloudCon()
            Dim CmdCheck As MySqlCommand
            For i As Integer = 0 To CouponDatatable.Rows.Count - 1 Step +1
                Dim sql = "UPDATE tbcoupon SET active = 1 WHERE ID = " & CouponDatatable(i)(0) & ""
                CmdCheck = New MySqlCommand(sql, ConnectionLocal)
                CmdCheck.ExecuteNonQuery()
                Dim sql2 = "UPDATE admin_custom_coupon SET synced = 'Synced' WHERE ID = " & CouponDatatable(i)(0) & ""
                CmdCheck = New MySqlCommand(sql2, ConnectionServer)
                CmdCheck.ExecuteNonQuery()
            Next
            ConnectionLocal.Close()
            ConnectionServer.Close()
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Settings Form: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
#End Region
    Private Sub Button3_Click_1(sender As Object, e As EventArgs) Handles ButtonChangeFormula.Click
        Enabled = False
        Changeproductformula.Show()
    End Sub
    Private Sub ButtonKeyboard_Click(sender As Object, e As EventArgs) Handles ButtonKeyboard.Click, Button3.Click
        ShowKeyboard()
    End Sub

    Private Sub TextBoxCRefVal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBoxCRefVal.KeyPress, TextBoxCDVal.KeyPress, TextBoxCBundVal.KeyPress, TextBoxCBP.KeyPress
        Numeric(sender, e)
    End Sub
#Region "Reset"
    '
    'Private Sub TruncateTable(ToTruncate)
    '    Try
    '        Query += "TRUNCATE TABLE " & ToTruncate & " ;"
    '    Catch ex As Exception
    '        SendErrorReport(ex.ToString)
    '    End Try
    'End Sub


    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Dim Query As String = ""
            Dim array() As String = {"loc_coupon_data", "loc_daily_transaction", "loc_daily_transaction_details", "loc_deposit", "loc_expense_details", "loc_expense_list",
            "loc_fm_stock", "loc_hold_inventory", "loc_inv_temp_data", "loc_pending_orders", "loc_refund_return_details", "loc_senior_details",
            "loc_system_logs", "loc_send_bug_report", "loc_transaction_mode_details", "loc_transfer_data", "loc_zread_inventory", "loc_admin_category", "loc_admin_products",
            "loc_partners_transaction", "loc_pos_inventory", "loc_product_formula", "tbcoupon", "loc_cash_breakdown", "loc_customer_info"}

            With DataGridViewReset
                If .Rows(0).Cells(1).Selected Then
                    TruncateTableAll("loc_daily_transaction")
                    TruncateTableAll("loc_daily_transaction_details")
                    TruncateTableAll("loc_senior_details")
                    TruncateTableAll("loc_transaction_mode_details")
                    TruncateTableAll("loc_coupon_data")
                End If
                If .Rows(1).Cells(1).Selected Then
                    TruncateTableAll("loc_deposit")
                End If
                If .Rows(2).Cells(1).Selected Then
                    TruncateTableAll("loc_expense_details")
                    TruncateTableAll("loc_expense_list")
                End If
                If .Rows(3).Cells(1).Selected Then
                    TruncateTableAll("loc_fm_stock")
                End If
                If .Rows(4).Cells(1).Selected Then
                    TruncateTableAll("loc_admin_category")
                End If
                If .Rows(5).Cells(1).Selected Then
                    TruncateTableAll("loc_inbox_messages")
                End If
                If .Rows(6).Cells(1).Selected Then
                    TruncateTableAll("loc_inv_temp_data")
                End If
                If .Rows(7).Cells(1).Selected Then
                    TruncateTableAll("loc_pending_orders")
                End If
                If .Rows(8).Cells(1).Selected Then
                    TruncateTableAll("loc_pos_inventory")
                End If
                If .Rows(9).Cells(1).Selected Then
                    TruncateTableAll("loc_price_request_change")
                End If
                If .Rows(10).Cells(1).Selected Then
                    TruncateTableAll("loc_product_formula")
                End If
                If .Rows(11).Cells(1).Selected Then
                    TruncateTableAll("loc_refund_return_details")
                End If
                If .Rows(12).Cells(1).Selected Then
                    TruncateTableAll("loc_send_bug_report")
                End If
                If .Rows(13).Cells(1).Selected Then
                    TruncateTableAll("loc_stockadjustment_cat")
                End If
                If .Rows(14).Cells(1).Selected Then
                    TruncateTableAll("loc_system_logs")
                End If
                If .Rows(15).Cells(1).Selected Then
                    TruncateTableAll("loc_transfer_data")
                End If
                If .Rows(16).Cells(1).Selected Then
                    TruncateTableAll("loc_users")
                    TruncateTableAll("triggers_loc_users")
                End If
                If .Rows(17).Cells(1).Selected Then
                    TruncateTableAll("loc_zread_inventory")
                End If
                If .Rows(18).Cells(1).Selected Then
                    TruncateTableAll("tbcoupon")
                End If
                If .Rows(19).Cells(1).Selected Then
                    TruncateTableAll("loc_admin_products")
                    TruncateTableAll("triggers_loc_admin_products")
                End If
                If .Rows(20).Cells(1).Selected Then
                    TruncateTableAll("loc_coupon_data")
                End If
                If .Rows(21).Cells(1).Selected Then
                    TruncateTableAll("loc_partners_transaction")
                End If
            End With
            If CheckBoxAll.Checked Then

            End If
            Dim msg = MessageBox.Show("Are you sure you want to truncate all selected table(s)?", "NOTICE", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If msg = DialogResult.Yes Then

                If CheckBoxAll.Checked Then

                    Dim counterValue = 0
                    Dim ConnectionLocal As MySqlConnection = LocalhostConn()
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
                    'Else
                    '    If ReturnBool Then
                    '        counterValue += 1
                    '        Query = "UPDATE `tbcountertable` SET counter_value = '" & counterValue & "' WHERE counter_id = 1"
                    '        cmd = New MySqlCommand(Query, ConnectionLocal)
                    '        cmd.ExecuteNonQuery()
                    '    Else
                    '        counterValue = 1
                    '        Query = "INSERT INTO `tbcountertable` (counter_value, date_created) VALUES ('" & counterValue & "', '" & FullDate24HR() & "')"
                    '        cmd = New MySqlCommand(Query, ConnectionLocal)
                    '        cmd.ExecuteNonQuery()
                    '    End If
                    LabelResetStatus.Text = counterValue
                    My.Settings.zcounter = 0
                    My.Settings.Save()
                    ConnectionLocal.Close()
                    cmd.Dispose()
                End If

                FormIsOpen()
                SystemLogDesc = "User Logout: " & returnfullname(where:=ClientCrewID)
                SystemLogType = "LOG OUT"
                GLOBAL_SYSTEM_LOGS(SystemLogType, SystemLogDesc)
                AuditTrail.LogToAuditTral("System", "System Recalibrated, Reset initialized. Success!", "Critical")
                POS.Close()
                EndBalance()
                Login.Show()
                Dispose()
            End If
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub

    Dim Autobackup As Boolean
    Private Sub RadioButtonYearly_Click(sender As Object, e As EventArgs) Handles RadioButtonYearly.Click, RadioButtonWeekly.Click, RadioButtonMonthly.Click, RadioButtonDaily.Click
        Try
            If AutoBackupBoolean Then
                If ValidLocalConnection Then
                    Dim Conn = LocalhostConn()
                    Dim Interval As Integer = 0
                    Dim IntervalName As String = ""
                    If RadioButtonDaily.Checked = True Then
                        Interval = 1
                        IntervalName = "Daily"
                    ElseIf RadioButtonWeekly.Checked = True Then
                        Interval = 2
                        IntervalName = "Weekly"
                    ElseIf RadioButtonMonthly.Checked = True Then
                        Interval = 3
                        IntervalName = "Monthly"
                    ElseIf RadioButtonYearly.Checked = True Then
                        Interval = 4
                        IntervalName = "Yearly"
                    End If
                    Dim sql = "SELECT `S_BackupInterval` , `S_BackupDate` FROM loc_settings WHERE settings_id = 1"
                    Dim cmd As MySqlCommand = New MySqlCommand(sql, Conn)
                    Dim da As MySqlDataAdapter = New MySqlDataAdapter(cmd)
                    Dim dt As DataTable = New DataTable
                    da.Fill(dt)
                    If dt.Rows.Count > 0 Then
                        sql = "UPDATE loc_settings SET `S_BackupInterval` = " & Interval & " , `S_BackupDate` = '" & Format(Now(), "yyyy-MM-dd") & "'"
                        cmd = New MySqlCommand(sql, Conn)
                        cmd.ExecuteNonQuery()
                        Autobackup = True
                    Else
                        sql = "INSERT INTO loc_settings (`S_BackupInterval` , `S_BackupDate`) VALUES ('" & Interval & "','" & Format(Now(), "yyyy-MM-dd") & "')"
                        cmd = New MySqlCommand(sql, Conn)
                        cmd.ExecuteNonQuery()
                        Autobackup = True
                    End If
                    MsgBox("Automatic system backup set to " & IntervalName & " backup")
                End If
            End If
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub

    Private Sub RadioButtonPrintReceiptYes_Click(sender As Object, e As EventArgs) Handles RadioButtonPrintReceiptYes.Click, RadioButtonPrintReceiptNo.Click
        Dim PrintOptionIsSet As Boolean = False
        Dim PrintOption As String = ""

        Try

            Dim table = "`loc_settings`"
            Dim Conn = LocalhostConn()
            If PrintOptionsBoolean Then
                If ValidLocalConnection Then
                    If RadioButtonPrintReceiptYes.Checked Then
                        PrintOption = "YES"
                        PrintOptionIsSet = True
                    ElseIf RadioButtonPrintReceiptNo.Checked Then
                        PrintOption = "NO"
                        PrintOptionIsSet = True
                    Else
                        PrintOptionIsSet = False
                        PrintOption = ""
                    End If
                    If PrintOptionIsSet Then
                        Dim sql = "SELECT `printreceipt` FROM " & table & " WHERE `settings_id` = 1"
                        Dim cmd As MySqlCommand = New MySqlCommand(sql, Conn)
                        Dim da As MySqlDataAdapter = New MySqlDataAdapter(cmd)
                        Dim dt As DataTable = New DataTable
                        da.Fill(dt)
                        If dt.Rows.Count > 0 Then
                            Dim fields = "`printreceipt` = '" & PrintOption & "' "
                            sql = "UPDATE " & table & " SET " & fields & " WHERE `settings_id` = 1"
                            cmd = New MySqlCommand(sql, Conn)
                            cmd.ExecuteNonQuery()

                        Else
                            Dim fields = "`printreceipt`"
                            Dim value = "'" & PrintOption & "'"
                            sql = "INSERT INTO " & table & " (" & fields & ") VALUES (" & value & ")"
                            cmd = New MySqlCommand(sql, Conn)
                            cmd.ExecuteNonQuery()

                        End If
                        S_Print = PrintOption
                    Else
                        MsgBox("Select option first")
                        PrintOptionIsSet = False
                        PrintOption = ""
                    End If
                Else
                    MsgBox("Connection must be valid first")
                    PrintOptionIsSet = False
                    PrintOption = ""
                End If
            End If
        Catch ex As Exception

            PrintOptionIsSet = False
            PrintOption = ""
            SendErrorReport(ex.ToString)
        End Try
    End Sub

    Private Sub RadioButtonRePrintReceiptYes_Click(sender As Object, e As EventArgs) Handles RadioButtonRePrintReceiptYes.Click, RadioButtonRePrintReceiptNo.Click
        Dim RePrintOptionIsSet As Boolean = False
        Dim RePrintOption As String = ""
        Try
            Dim table = "`loc_settings`"
            Dim Conn = LocalhostConn()
            If PrintOptionsBoolean Then
                If ValidLocalConnection Then
                    If RadioButtonRePrintReceiptYes.Checked Then
                        RePrintOption = "YES"
                        RePrintOptionIsSet = True
                    ElseIf RadioButtonRePrintReceiptNo.Checked Then
                        RePrintOption = "NO"
                        RePrintOptionIsSet = True
                    Else
                        RePrintOption = ""
                        RePrintOptionIsSet = False
                    End If
                    If RePrintOptionIsSet Then
                        Dim sql = "Select `reprintreceipt` FROM " & table & " WHERE `settings_id` = 1"
                        Dim cmd As MySqlCommand = New MySqlCommand(sql, Conn)
                        Dim da As MySqlDataAdapter = New MySqlDataAdapter(cmd)
                        Dim dt As DataTable = New DataTable
                        da.Fill(dt)
                        If dt.Rows.Count > 0 Then
                            Dim fields = "`reprintreceipt` = '" & RePrintOption & "' "
                            sql = "UPDATE " & table & " SET " & fields & " WHERE `settings_id` = 1"
                            cmd = New MySqlCommand(sql, Conn)
                            cmd.ExecuteNonQuery()

                        Else
                            Dim fields = "`reprintreceipt`"
                            Dim value = "'" & RePrintOption & "'"
                            sql = "INSERT INTO " & table & " (" & fields & ") VALUES (" & value & ")"
                            cmd = New MySqlCommand(sql, Conn)
                            cmd.ExecuteNonQuery()

                        End If
                        S_Reprint = RePrintOption
                    Else
                        MsgBox("Select option first")
                        RePrintOptionIsSet = False
                        RePrintOption = ""
                    End If
                Else
                    MsgBox("Connection must be valid first")
                    RePrintOption = ""
                    RePrintOptionIsSet = False
                End If
            End If

        Catch ex As Exception

            RePrintOption = ""
            RePrintOptionIsSet = False
            SendErrorReport(ex.ToString)
        End Try
    End Sub

    Private Sub RadioButtonPrintXZReadNo_Click(sender As Object, e As EventArgs) Handles RadioButtonPrintXZReadYes.Click, RadioButtonPrintXZReadNo.Click
        Dim PrintXZRead As Boolean = False
        Dim PrintXZReadOption As String = ""
        Try

            Dim table = "`loc_settings`"
            Dim Conn = LocalhostConn()
            If PrintOptionsBoolean Then
                If ValidLocalConnection Then
                    If RadioButtonPrintXZReadYes.Checked Then
                        PrintXZReadOption = "YES"
                        PrintXZRead = True
                    ElseIf RadioButtonPrintXZReadNo.Checked Then
                        PrintXZReadOption = "NO"
                        PrintXZRead = True
                    Else
                        PrintXZReadOption = ""
                        PrintXZRead = False
                    End If
                    If PrintXZRead Then
                        Dim sql = "Select `printxzread` FROM " & table & " WHERE `settings_id` = 1"
                        Dim cmd As MySqlCommand = New MySqlCommand(sql, Conn)
                        Dim da As MySqlDataAdapter = New MySqlDataAdapter(cmd)
                        Dim dt As DataTable = New DataTable
                        da.Fill(dt)
                        If dt.Rows.Count > 0 Then
                            Dim fields = "`printxzread` = '" & PrintXZReadOption & "' "
                            sql = "UPDATE " & table & " SET " & fields & " WHERE `settings_id` = 1"
                            cmd = New MySqlCommand(sql, Conn)
                            cmd.ExecuteNonQuery()

                        Else
                            Dim fields = "`printxzread`"
                            Dim value = "'" & PrintXZReadOption & "'"
                            sql = "INSERT INTO " & table & " (" & fields & ") VALUES (" & value & ")"
                            cmd = New MySqlCommand(sql, Conn)
                            cmd.ExecuteNonQuery()

                        End If
                        S_Print_XZRead = PrintXZReadOption
                    Else
                        MsgBox("Select option first")
                        PrintXZRead = False
                        PrintXZReadOption = ""
                    End If
                Else
                    MsgBox("Connection must be valid first")
                    PrintXZReadOption = ""
                    PrintXZRead = False
                End If
            End If

        Catch ex As Exception
            PrintXZReadOption = ""
            PrintXZRead = False
            SendErrorReport(ex.ToString)
        End Try
    End Sub

    Private Sub RadioButtonPrintReturnsNo_Click(sender As Object, e As EventArgs) Handles RadioButtonPrintReturnsYes.Click, RadioButtonPrintReturnsNo.Click
        Dim PrintReturns = ""
        Dim PrintReturnsBool As Boolean = False
        Try
            Dim table = "`loc_settings`"
            Dim Conn = LocalhostConn()
            If ValidLocalConnection Then
                If RadioButtonPrintReturnsYes.Checked Then
                    PrintReturns = "YES"
                    PrintReturnsBool = True
                ElseIf RadioButtonPrintReturnsNo.Checked Then
                    PrintReturns = "NO"
                    PrintReturnsBool = True
                Else
                    PrintReturns = ""
                    PrintReturnsBool = False
                End If
                If PrintReturnsBool Then
                    Dim sql = "Select `printreturns` FROM " & table & " WHERE `settings_id` = 1"
                    Dim cmd As MySqlCommand = New MySqlCommand(sql, Conn)
                    Dim da As MySqlDataAdapter = New MySqlDataAdapter(cmd)
                    Dim dt As DataTable = New DataTable
                    da.Fill(dt)
                    If dt.Rows.Count > 0 Then
                        Dim fields = "`printreturns` = '" & PrintReturns & "' "
                        sql = "UPDATE " & table & " SET " & fields & " WHERE `settings_id` = 1"
                        cmd = New MySqlCommand(sql, Conn)
                        cmd.ExecuteNonQuery()

                    Else
                        Dim fields = "`printreturns`"
                        Dim value = "'" & PrintReturns & "'"
                        sql = "INSERT INTO " & table & " (" & fields & ") VALUES (" & value & ")"
                        cmd = New MySqlCommand(sql, Conn)
                        cmd.ExecuteNonQuery()

                    End If
                    S_Print_Returns = PrintReturns
                Else
                    MsgBox("Select option first")
                    PrintReturnsBool = False
                    PrintReturns = ""
                End If
            Else
                MsgBox("Connection must be valid first")
                PrintReturns = ""
                PrintReturnsBool = False
            End If

        Catch ex As Exception
            PrintReturns = ""
            PrintReturnsBool = False
            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub RadioButtonPrintSRYes_Click(sender As Object, e As EventArgs) Handles RadioButtonPrintSRYes.Click, RadioButtonPrintSRNo.Click
        Dim PrintSalesReport = ""
        Dim PrintSalesReportBool As Boolean = False
        Try
            Dim table = "`loc_settings`"
            Dim Conn = LocalhostConn()
            If ValidLocalConnection Then
                If RadioButtonPrintSRYes.Checked Then
                    PrintSalesReport = "YES"
                    PrintSalesReportBool = True
                ElseIf RadioButtonPrintSRNo.Checked Then
                    PrintSalesReport = "NO"
                    PrintSalesReportBool = True
                Else
                    PrintSalesReport = ""
                    PrintSalesReportBool = False
                End If
                If PrintSalesReportBool Then
                    Dim sql = "Select `printsalesreport` FROM " & table & " WHERE `settings_id` = 1"
                    Dim cmd As MySqlCommand = New MySqlCommand(sql, Conn)
                    Dim da As MySqlDataAdapter = New MySqlDataAdapter(cmd)
                    Dim dt As DataTable = New DataTable
                    da.Fill(dt)
                    If dt.Rows.Count > 0 Then
                        Dim fields = "`printsalesreport` = '" & PrintSalesReport & "' "
                        sql = "UPDATE " & table & " SET " & fields & " WHERE `settings_id` = 1"
                        cmd = New MySqlCommand(sql, Conn)
                        cmd.ExecuteNonQuery()

                    Else
                        Dim fields = "`printsalesreport`"
                        Dim value = "'" & PrintSalesReport & "'"
                        sql = "INSERT INTO " & table & " (" & fields & ") VALUES (" & value & ")"
                        cmd = New MySqlCommand(sql, Conn)
                        cmd.ExecuteNonQuery()

                    End If
                    S_Print_Sales_Report = PrintSalesReport
                Else
                    MsgBox("Select option first")
                    PrintSalesReportBool = False
                    PrintSalesReport = ""
                End If
            Else
                MsgBox("Connection must be valid first")
                PrintSalesReport = ""
                PrintSalesReportBool = False
            End If

        Catch ex As Exception
            PrintSalesReport = ""
            PrintSalesReportBool = False
            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub RadioButtonTrainingOFF_Click(sender As Object, e As EventArgs) Handles RadioButtonTraningON.Click, RadioButtonTrainingOFF.Click
        Try
            If RadioButtonTraningON.Checked Then
                S_TrainingMode = True
                MessageBox.Show("Training mode is ON", "NOTICE", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ElseIf RadioButtonTrainingOFF.Checked Then
                S_TrainingMode = False
                MessageBox.Show("Training mode is OFF", "NOTICE", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub RadioButtonInvResetOff_Click(sender As Object, e As EventArgs) Handles RadioButtonInvResetOn.Click, RadioButtonInvResetOff.Click
        Try
            If RadioButtonInvResetOn.Checked Then
                AutoInventoryReset = True
                Dim ConnectionLocal = LocalhostConn()
                Dim Sql = "UPDATE loc_settings SET autoresetinv = 1 WHERE settings_id = 1"
                Dim cmd As MySqlCommand = New MySqlCommand(sql, ConnectionLocal)
                Dim res = cmd.ExecuteNonQuery
                MessageBox.Show("Monthly inventory reset is on", "NOTICE", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ElseIf RadioButtonInvResetOff.Checked Then
                AutoInventoryReset = False
                Dim ConnectionLocal = LocalhostConn()
                Dim Sql = "UPDATE loc_settings SET autoresetinv = 0 WHERE settings_id = 1"
                Dim cmd As MySqlCommand = New MySqlCommand(Sql, ConnectionLocal)
                Dim res = cmd.ExecuteNonQuery
                MessageBox.Show("Monthly inventory reset is off", "NOTICE", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub

    Private Sub ButtonApplyLedSettings_Click(sender As Object, e As EventArgs) Handles ButtonApplyLedSettings.Click
        Try
            If My.Settings.LedDisplayTrue Then
                My.Settings.SpPort = ComboBoxComPort.Text
                My.Settings.SpBaudrate = Val(TextBoxBaudRate.Text)
                My.Settings.Save()
            Else
                MsgBox("Fill up all the fields first!")
            End If
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub

    Private Sub ButtonRefreshPort_Click(sender As Object, e As EventArgs) Handles ButtonRefreshPort.Click
        Try
            GetPorts(ComboBoxComPort)
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles ButtonTestDisplay.Click
        Try
            If ComboBoxComPort.SelectedIndex <> -1 And ComboBoxComPort.Text <> "" Then
                If Val(TextBoxBaudRate.Text) > 0 Then
                    If TextBoxTest.Text <> "" Then
                        LedConfig(TextBoxTest.Text, ComboBoxComPort.Text, Val(TextBoxBaudRate.Text))
                    Else
                        MsgBox("Input sample text display first")
                    End If
                Else
                    MsgBox("Input baudrate first.")
                End If
            Else
                MsgBox("Select available Serial Port first.")
            End If
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub


    Private Sub RadioButtonYES_Click(sender As Object, e As EventArgs) Handles RadioButtonYES.Click, RadioButtonNO.Click
        Try
            Dim ZeroRatedVal As Integer = 0
            Dim ConnectionLocal As MySqlConnection = LocalhostConn()
            If RadioButtonYES.Checked Then
                ZeroRatedVal = 1
                S_ZeroRated = ZeroRatedVal
            Else
                ZeroRatedVal = 0
                S_ZeroRated = ZeroRatedVal
            End If
            Dim Query As String = "UPDATE loc_settings SET A_ZeroRated = '" & S_ZeroRated & "' WHERE settings_id = 1"
            Dim cmd As MySqlCommand = New MySqlCommand(Query, ConnectionLocal)
            cmd.ExecuteNonQuery()

        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub NumericUpDownPrintCount_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDownPrintCount.ValueChanged
        Try
            If NumericUpDownPrintCount.Value <> 0 Then
                S_PrintCount = NumericUpDownPrintCount.Value
                Dim ConnectionLocal As MySqlConnection = LocalhostConn()
                Dim Query As String = "UPDATE loc_settings SET printcount = " & S_PrintCount & " WHERE settings_id = 1"
                Dim cmd As MySqlCommand = New MySqlCommand(Query, ConnectionLocal)
                cmd.ExecuteNonQuery()
            Else
                MsgBox("Print count cannot be 0")
                NumericUpDownPrintCount.Value = 1
                S_PrintCount = 1
                Dim ConnectionLocal As MySqlConnection = LocalhostConn()
                Dim Query As String = "UPDATE loc_settings SET printcount = " & S_PrintCount & " WHERE settings_id = 1"
                Dim cmd As MySqlCommand = New MySqlCommand(Query, ConnectionLocal)
                cmd.ExecuteNonQuery()
            End If
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub

    Private Sub ComboBoxPrintSize_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxPrintSize.SelectedIndexChanged
        Try
            If ComboBoxPrintSize.Text = "57mm" Then
                My.Settings.PrintSize = "57mm"
                My.Settings.Save()
            Else
                My.Settings.PrintSize = "80mm"
                My.Settings.Save()
            End If
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub
#End Region
#Region "Receipt"
    Private Sub LoadHeader()
        Try
            Dim ConnectionLocal As MySqlConnection = LocalhostConn()
            Dim Sql As String = "SELECT * FROM loc_receipt WHERE status = 1 AND type = 'Header'"
            Dim Cmd As MySqlCommand = New MySqlCommand(Sql, ConnectionLocal)
            Dim da As MySqlDataAdapter = New MySqlDataAdapter(Cmd)
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            DataGridViewHeader.Rows.Clear()
            For Each row As DataRow In dt.Rows
                DataGridViewHeader.Rows.Add(row("id"), row("description"))
            Next
            'DataGridViewHeader
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Settings Form: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub

    Private Sub AddHeaderDetails(Desc)
        Try
            If Not String.IsNullOrEmpty(TextBoxHeaderDesc.Text) Then
                Dim ConnectionLocal As MySqlConnection = LocalhostConn()
                Dim Sql As String = "INSERT INTO loc_receipt (`type`,`description`, `created_by`, `created_at`, `status`) VALUES (@0, @1, @2, @3, @4)"
                Dim Cmd As MySqlCommand = New MySqlCommand(Sql, ConnectionLocal)
                Cmd.Parameters.Add("@0", MySqlDbType.Text).Value = "Header"
                Cmd.Parameters.Add("@1", MySqlDbType.Text).Value = Desc
                Cmd.Parameters.Add("@2", MySqlDbType.Text).Value = ClientCrewID
                Cmd.Parameters.Add("@3", MySqlDbType.Text).Value = FullDate24HR()
                Cmd.Parameters.Add("@4", MySqlDbType.Text).Value = 1
                Cmd.ExecuteNonQuery()
                ToolStripLabelHeaderStatus.Text = "Added successfully!"
                ToolStripLabelHeaderStatus.ForeColor = Color.DarkGreen
                ConnectionLocal.Close()
                Cmd.Dispose()
                LoadHeader()
            Else
                ToolStripLabelHeaderStatus.Text = "Description is empty!"
                ToolStripLabelHeaderStatus.ForeColor = Color.DarkRed
            End If

        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Settings Form: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub UpdateHeaderDetails(Desc)
        Try
            If DataGridViewHeader.SelectedRows.Count = 1 Then
                If Not String.IsNullOrEmpty(TextBoxHeaderDesc.Text) Then
                    Dim ConnectionLocal As MySqlConnection = LocalhostConn()
                    Dim Sql As String = "UPDATE loc_receipt SET `description` = @1, `updated_by` = @2, `updated_at` = @3 WHERE id = " & DataGridViewHeader.SelectedRows(0).Cells(0).Value
                    Dim Cmd As MySqlCommand = New MySqlCommand(Sql, ConnectionLocal)
                    Cmd.Parameters.Add("@1", MySqlDbType.Text).Value = Desc
                    Cmd.Parameters.Add("@2", MySqlDbType.Text).Value = ClientCrewID
                    Cmd.Parameters.Add("@3", MySqlDbType.Text).Value = FullDate24HR()
                    Cmd.ExecuteNonQuery()
                    ToolStripLabelHeaderStatus.Text = "Updated successfully!"
                    ToolStripLabelHeaderStatus.ForeColor = Color.DarkGreen
                    ConnectionLocal.Close()
                    Cmd.Dispose()
                    LoadHeader()
                Else
                    ToolStripLabelHeaderStatus.Text = "Description is empty!"
                    ToolStripLabelHeaderStatus.ForeColor = Color.DarkRed
                End If
            Else
                ToolStripLabelHeaderStatus.Text = "Select one only"
                ToolStripLabelHeaderStatus.ForeColor = Color.DarkRed
            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Settings Form: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub

    Private Sub DeleteHeader()
        Try
            For Each dgvr As DataGridViewRow In DataGridViewHeader.SelectedRows
                Dim ConnectionLocal As MySqlConnection = LocalhostConn()
                Dim Sql As String = "UPDATE loc_receipt SET `updated_by` = @1, `updated_at` = @2, `status` = @3 WHERE id = " & dgvr.Cells("Column57").Value
                Dim Cmd As MySqlCommand = New MySqlCommand(Sql, ConnectionLocal)
                Cmd.Parameters.Add("@1", MySqlDbType.Text).Value = ClientCrewID
                Cmd.Parameters.Add("@2", MySqlDbType.Text).Value = FullDate24HR()
                Cmd.Parameters.Add("@3", MySqlDbType.Text).Value = 0
                Cmd.ExecuteNonQuery()
                ToolStripLabelHeaderStatus.Text = "Deleted successfully!"
                ToolStripLabelHeaderStatus.ForeColor = Color.DarkGreen
                ConnectionLocal.Close()
                Cmd.Dispose()
            Next
            LoadHeader()
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Settings Form: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub

    Private Sub ToolStripButton1_Click_1(sender As Object, e As EventArgs) Handles ToolStripButtonAddHeader.Click
        AddHeaderDetails(Trim(TextBoxHeaderDesc.Text))
    End Sub

    Private Sub ToolStripButton3_Click(sender As Object, e As EventArgs) Handles ToolStripButtonUpdateHeader.Click
        UpdateHeaderDetails(Trim(TextBoxHeaderDesc.Text))
    End Sub

    Private Sub ToolStripButton2_Click_1(sender As Object, e As EventArgs) Handles ToolStripButtonDeleteHeader.Click
        DeleteHeader()
    End Sub

    Private Sub LoadFooter()
        Try
            Dim ConnectionLocal As MySqlConnection = LocalhostConn()
            Dim Sql As String = "SELECT * FROM loc_receipt WHERE status = 1 And type = 'Footer'"
            Dim Cmd As MySqlCommand = New MySqlCommand(Sql, ConnectionLocal)
            Dim da As MySqlDataAdapter = New MySqlDataAdapter(Cmd)
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            DataGridViewFooter.Rows.Clear()
            For Each row As DataRow In dt.Rows
                DataGridViewFooter.Rows.Add(row("id"), row("description"))
            Next
            'DataGridViewHeader
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub AddFooterDetails(Desc)
        Try
            If Not String.IsNullOrEmpty(TextBoxFooterDesc.Text) Then
                Dim ConnectionLocal As MySqlConnection = LocalhostConn()
                Dim Sql As String = "INSERT INTO loc_receipt (`type`,`description`, `created_by`, `created_at`, `status`) VALUES (@0, @1, @2, @3, @4)"
                Dim Cmd As MySqlCommand = New MySqlCommand(Sql, ConnectionLocal)
                Cmd.Parameters.Add("@0", MySqlDbType.Text).Value = "Footer"
                Cmd.Parameters.Add("@1", MySqlDbType.Text).Value = Desc
                Cmd.Parameters.Add("@2", MySqlDbType.Text).Value = ClientCrewID
                Cmd.Parameters.Add("@3", MySqlDbType.Text).Value = FullDate24HR()
                Cmd.Parameters.Add("@4", MySqlDbType.Text).Value = 1
                Cmd.ExecuteNonQuery()
                ToolStripLabelFooterStatus.Text = "Added successfully!"
                ToolStripLabelFooterStatus.ForeColor = Color.DarkGreen
                ConnectionLocal.Close()
                Cmd.Dispose()
                LoadFooter()
            Else
                ToolStripLabelFooterStatus.Text = "Description is empty!"
                ToolStripLabelFooterStatus.ForeColor = Color.DarkRed
            End If

        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Settings Form: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub UpdateFooterDetails(Desc)
        Try
            If DataGridViewFooter.SelectedRows.Count = 1 Then
                If Not String.IsNullOrEmpty(TextBoxFooterDesc.Text) Then
                    Dim ConnectionLocal As MySqlConnection = LocalhostConn()
                    Dim Sql As String = "UPDATE loc_receipt SET `description` = @1, `updated_by` = @2, `updated_at` = @3 WHERE id = " & DataGridViewFooter.SelectedRows(0).Cells(0).Value
                    Dim Cmd As MySqlCommand = New MySqlCommand(Sql, ConnectionLocal)
                    Cmd.Parameters.Add("@1", MySqlDbType.Text).Value = Desc
                    Cmd.Parameters.Add("@2", MySqlDbType.Text).Value = ClientCrewID
                    Cmd.Parameters.Add("@3", MySqlDbType.Text).Value = FullDate24HR()
                    Cmd.ExecuteNonQuery()
                    ToolStripLabelFooterStatus.Text = "Updated successfully!"
                    ToolStripLabelFooterStatus.ForeColor = Color.DarkGreen
                    ConnectionLocal.Close()
                    Cmd.Dispose()
                    LoadFooter()
                Else
                    ToolStripLabelFooterStatus.Text = "Description is empty!"
                    ToolStripLabelFooterStatus.ForeColor = Color.DarkRed
                End If
            Else
                ToolStripLabelFooterStatus.Text = "Select one only"
                ToolStripLabelFooterStatus.ForeColor = Color.DarkRed
            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Settings Form: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub

    Private Sub DeleteFooter()
        Try
            For Each dgvr As DataGridViewRow In DataGridViewFooter.SelectedRows
                Dim ConnectionLocal As MySqlConnection = LocalhostConn()
                Dim Sql As String = "UPDATE loc_receipt SET `updated_by` = @1, `updated_at` = @2, `status` = @3 WHERE id = " & dgvr.Cells("DataGridViewTextBoxColumn29").Value
                Dim Cmd As MySqlCommand = New MySqlCommand(Sql, ConnectionLocal)
                Cmd.Parameters.Add("@1", MySqlDbType.Text).Value = ClientCrewID
                Cmd.Parameters.Add("@2", MySqlDbType.Text).Value = FullDate24HR()
                Cmd.Parameters.Add("@3", MySqlDbType.Text).Value = 0
                Cmd.ExecuteNonQuery()
                ToolStripLabelFooterStatus.Text = "Deleted successfully!"
                ToolStripLabelFooterStatus.ForeColor = Color.DarkGreen
                ConnectionLocal.Close()
                Cmd.Dispose()
            Next
            LoadFooter()
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Settings Form: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub

    Private Sub ToolStripButtonAddFooter_Click(sender As Object, e As EventArgs) Handles ToolStripButtonAddFooter.Click
        AddFooterDetails(Trim(TextBoxFooterDesc.Text))
    End Sub

    Private Sub ToolStripButtonUpdateFooter_Click(sender As Object, e As EventArgs) Handles ToolStripButtonUpdateFooter.Click
        UpdateFooterDetails(Trim(TextBoxFooterDesc.Text))
    End Sub

    Private Sub ToolStripButtonDeleteFooter_Click(sender As Object, e As EventArgs) Handles ToolStripButtonDeleteFooter.Click
        DeleteFooter()
    End Sub
    Private Sub LoadOthers()
        Try
            Dim ConnectionLocal As MySqlConnection = LocalhostConn()
            Dim Sql As String = "SELECT * FROM loc_receipt WHERE status = 1 And type IN ('SALES-INVOICE','REFUND-HEADER','REFUND-FOOTER','OFFICIAL-INVOICE','OFFICIAL-REFUND')"
            Dim Cmd As MySqlCommand = New MySqlCommand(Sql, ConnectionLocal)
            Dim da As MySqlDataAdapter = New MySqlDataAdapter(Cmd)
            Dim dt As DataTable = New DataTable
            da.Fill(dt)

            For Each row As DataRow In dt.Rows
                If row("type") = "SALES-INVOICE" Then
                    TextBoxSIOthers.Text = row("description")
                ElseIf row("type") = "REFUND-HEADER" Then
                    TextBoxRefundHeader.Text = row("description")
                ElseIf row("type") = "OFFICIAL-INVOICE" Then
                    TextBoxOfficialInvoice.Text = row("description")
                ElseIf row("type") = "OFFICIAL-REFUND" Then
                    TextBoxOfficialRefund.Text = row("description")
                End If
            Next

        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Settings Form: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub LoadValidity()
        Try
            Dim ConnectionLocal As MySqlConnection = LocalhostConn()
            Dim Sql As String = "SELECT * FROM loc_receipt WHERE status = 1 And type = 'VALIDITY'"
            Dim Cmd As MySqlCommand = New MySqlCommand(Sql, ConnectionLocal)
            Dim da As MySqlDataAdapter = New MySqlDataAdapter(Cmd)
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            DataGridViewVALIDITY.Rows.Clear()
            For Each row As DataRow In dt.Rows
                DataGridViewVALIDITY.Rows.Add(row("id"), row("description"))
            Next
            'DataGridViewHeader
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Settings Form: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub AddValidityDetails(Desc)
        Try
            If Not String.IsNullOrEmpty(TextBoxValidity.Text) Then
                Dim ConnectionLocal As MySqlConnection = LocalhostConn()
                Dim Sql As String = "INSERT INTO loc_receipt (`type`,`description`, `created_by`, `created_at`, `status`) VALUES (@0, @1, @2, @3, @4)"
                Dim Cmd As MySqlCommand = New MySqlCommand(Sql, ConnectionLocal)
                Cmd.Parameters.Add("@0", MySqlDbType.Text).Value = "VALIDITY"
                Cmd.Parameters.Add("@1", MySqlDbType.Text).Value = Trim(TextBoxValidity.Text)
                Cmd.Parameters.Add("@2", MySqlDbType.Text).Value = ClientCrewID
                Cmd.Parameters.Add("@3", MySqlDbType.Text).Value = FullDate24HR()
                Cmd.Parameters.Add("@4", MySqlDbType.Text).Value = 1
                Cmd.ExecuteNonQuery()
                ToolStripLabelValidityStatus.Text = "Added successfully!"
                ToolStripLabelValidityStatus.ForeColor = Color.DarkGreen
                ConnectionLocal.Close()
                Cmd.Dispose()
                LoadValidity()
            Else
                ToolStripLabelValidityStatus.Text = "Description is empty!"
                ToolStripLabelValidityStatus.ForeColor = Color.DarkRed
            End If

        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Settings Form: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub UpdateVALIDITYDetails(Desc)
        Try
            If DataGridViewFooter.SelectedRows.Count = 1 Then
                If Not String.IsNullOrEmpty(TextBoxValidity.Text) Then
                    Dim ConnectionLocal As MySqlConnection = LocalhostConn()
                    Dim Sql As String = "UPDATE loc_receipt SET `description` = @1, `updated_by` = @2, `updated_at` = @3 WHERE id = " & DataGridViewVALIDITY.SelectedRows(0).Cells(0).Value
                    Dim Cmd As MySqlCommand = New MySqlCommand(Sql, ConnectionLocal)
                    Cmd.Parameters.Add("@1", MySqlDbType.Text).Value = Desc
                    Cmd.Parameters.Add("@2", MySqlDbType.Text).Value = ClientCrewID
                    Cmd.Parameters.Add("@3", MySqlDbType.Text).Value = FullDate24HR()
                    Cmd.ExecuteNonQuery()
                    ToolStripLabelValidityStatus.Text = "Updated successfully!"
                    ToolStripLabelValidityStatus.ForeColor = Color.DarkGreen
                    ConnectionLocal.Close()
                    Cmd.Dispose()
                    LoadValidity()
                Else
                    ToolStripLabelValidityStatus.Text = "Description is empty!"
                    ToolStripLabelValidityStatus.ForeColor = Color.DarkRed
                End If
            Else
                ToolStripLabelValidityStatus.Text = "Select one only"
                ToolStripLabelValidityStatus.ForeColor = Color.DarkRed
            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Settings Form: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub

    Private Sub DeleteValidity()
        Try
            For Each dgvr As DataGridViewRow In DataGridViewVALIDITY.SelectedRows
                Dim ConnectionLocal As MySqlConnection = LocalhostConn()
                Dim Sql As String = "UPDATE loc_receipt SET `updated_by` = @1, `updated_at` = @2, `status` = @3 WHERE id = " & dgvr.Cells("DataGridViewTextBoxColumn31").Value
                Dim Cmd As MySqlCommand = New MySqlCommand(Sql, ConnectionLocal)
                Cmd.Parameters.Add("@1", MySqlDbType.Text).Value = ClientCrewID
                Cmd.Parameters.Add("@2", MySqlDbType.Text).Value = FullDate24HR()
                Cmd.Parameters.Add("@3", MySqlDbType.Text).Value = 0
                Cmd.ExecuteNonQuery()
                ToolStripLabelValidityStatus.Text = "Deleted successfully!"
                ToolStripLabelValidityStatus.ForeColor = Color.DarkGreen
                ConnectionLocal.Close()
                Cmd.Dispose()
            Next
            LoadValidity()
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Settings Form: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub ToolStripButtonValidity_Click(sender As Object, e As EventArgs) Handles ToolStripButtonValidity.Click
        AddValidityDetails(Trim(TextBoxValidity.Text))
    End Sub

    Private Sub ToolStripButtonUpdateValidity_Click(sender As Object, e As EventArgs) Handles ToolStripButtonUpdateValidity.Click
        UpdateVALIDITYDetails(Trim(TextBoxValidity.Text))
    End Sub

    Private Sub ToolStripButtonDeleteValidity_Click(sender As Object, e As EventArgs) Handles ToolStripButtonDeleteValidity.Click
        DeleteValidity()
    End Sub
    Private Sub LoadRefundFooter()
        Try
            Dim ConnectionLocal As MySqlConnection = LocalhostConn()
            Dim Sql As String = "SELECT * FROM loc_receipt WHERE status = 1 And type = 'REFUND-FOOTER'"
            Dim Cmd As MySqlCommand = New MySqlCommand(Sql, ConnectionLocal)
            Dim da As MySqlDataAdapter = New MySqlDataAdapter(Cmd)
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            DataGridViewRefundExchange.Rows.Clear()
            For Each row As DataRow In dt.Rows
                DataGridViewRefundExchange.Rows.Add(row("id"), row("description"))
            Next
            'DataGridViewHeader
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Settings Form: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub AddRefundFooterDetails(Desc)
        Try
            If Not String.IsNullOrEmpty(TextBoxRefundFooter.Text) Then
                Dim ConnectionLocal As MySqlConnection = LocalhostConn()
                Dim Sql As String = "INSERT INTO loc_receipt (`type`,`description`, `created_by`, `created_at`, `status`) VALUES (@0, @1, @2, @3, @4)"
                Dim Cmd As MySqlCommand = New MySqlCommand(Sql, ConnectionLocal)
                Cmd.Parameters.Add("@0", MySqlDbType.Text).Value = "REFUND-FOOTER"
                Cmd.Parameters.Add("@1", MySqlDbType.Text).Value = Trim(TextBoxRefundFooter.Text)
                Cmd.Parameters.Add("@2", MySqlDbType.Text).Value = ClientCrewID
                Cmd.Parameters.Add("@3", MySqlDbType.Text).Value = FullDate24HR()
                Cmd.Parameters.Add("@4", MySqlDbType.Text).Value = 1
                Cmd.ExecuteNonQuery()
                ToolStripLabelRFooterStatus.Text = "Added successfully!"
                ToolStripLabelRFooterStatus.ForeColor = Color.DarkGreen
                ConnectionLocal.Close()
                Cmd.Dispose()
                LoadRefundFooter()
            Else
                ToolStripLabelRFooterStatus.Text = "Description is empty!"
                ToolStripLabelRFooterStatus.ForeColor = Color.DarkRed
            End If

        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Settings Form: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub UpdateRefundFooterDetails(Desc)
        Try
            If DataGridViewRefundExchange.SelectedRows.Count = 1 Then
                If Not String.IsNullOrEmpty(TextBoxRefundFooter.Text) Then
                    Dim ConnectionLocal As MySqlConnection = LocalhostConn()
                    Dim Sql As String = "UPDATE loc_receipt SET `description` = @1, `updated_by` = @2, `updated_at` = @3 WHERE id = " & DataGridViewRefundExchange.SelectedRows(0).Cells(0).Value
                    Dim Cmd As MySqlCommand = New MySqlCommand(Sql, ConnectionLocal)
                    Cmd.Parameters.Add("@1", MySqlDbType.Text).Value = Desc
                    Cmd.Parameters.Add("@2", MySqlDbType.Text).Value = ClientCrewID
                    Cmd.Parameters.Add("@3", MySqlDbType.Text).Value = FullDate24HR()
                    Cmd.ExecuteNonQuery()
                    ToolStripLabelRFooterStatus.Text = "Updated successfully!"
                    ToolStripLabelRFooterStatus.ForeColor = Color.DarkGreen
                    ConnectionLocal.Close()
                    Cmd.Dispose()
                    LoadRefundFooter()
                Else
                    ToolStripLabelRFooterStatus.Text = "Description is empty!"
                    ToolStripLabelRFooterStatus.ForeColor = Color.DarkRed
                End If
            Else
                ToolStripLabelRFooterStatus.Text = "Select one only"
                ToolStripLabelRFooterStatus.ForeColor = Color.DarkRed
            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Settings Form: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub

    Private Sub DeleteRefundFooter()
        Try
            For Each dgvr As DataGridViewRow In DataGridViewRefundExchange.SelectedRows
                Dim ConnectionLocal As MySqlConnection = LocalhostConn()
                Dim Sql As String = "UPDATE loc_receipt SET `updated_by` = @1, `updated_at` = @2, `status` = @3 WHERE id = " & dgvr.Cells("DataGridViewTextBoxColumn33").Value
                Dim Cmd As MySqlCommand = New MySqlCommand(Sql, ConnectionLocal)
                Cmd.Parameters.Add("@1", MySqlDbType.Text).Value = ClientCrewID
                Cmd.Parameters.Add("@2", MySqlDbType.Text).Value = FullDate24HR()
                Cmd.Parameters.Add("@3", MySqlDbType.Text).Value = 0
                Cmd.ExecuteNonQuery()
                ToolStripLabelRFooterStatus.Text = "Deleted successfully!"
                ToolStripLabelRFooterStatus.ForeColor = Color.DarkGreen
                ConnectionLocal.Close()
                Cmd.Dispose()
            Next
            LoadRefundFooter()
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Settings Form: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub ToolStripButton1_Click_2(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        AddRefundFooterDetails(Trim(TextBoxRefundFooter.Text))
    End Sub

    Private Sub ToolStripButton2_Click_2(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        UpdateRefundFooterDetails(Trim(TextBoxRefundFooter.Text))
    End Sub

    Private Sub ToolStripButton3_Click_1(sender As Object, e As EventArgs) Handles ToolStripButton3.Click
        DeleteRefundFooter()
    End Sub
    Private Sub SaveOthers(SALES, RHeader, OfficialInvoiceBody, OfficialRefundBody)
        Try
            Dim ConnectionLocal As MySqlConnection = LocalhostConn()
            Dim Sql As String = ""
            Dim Cmd As MySqlCommand
            If Not String.IsNullOrEmpty(SALES) Then
                Sql = "SELECT * FROM loc_receipt WHERE status = 1 AND type = 'SALES-INVOICE'"
                Cmd = New MySqlCommand(Sql, ConnectionLocal)
                Using reader As MySqlDataReader = Cmd.ExecuteReader
                    If reader.HasRows Then
                        reader.Dispose()
                        Sql = "UPDATE loc_receipt SET description = @1 WHERE type = 'SALES-INVOICE'"
                        Cmd = New MySqlCommand(Sql, ConnectionLocal)
                        Cmd.Parameters.Add("@1", MySqlDbType.Text).Value = SALES
                        Cmd.ExecuteNonQuery()
                        ToolStripLabelOthersStatus.Text = "Updated successfully!"
                    Else
                        reader.Dispose()
                        Sql = "INSERT INTO loc_receipt (`type`,`description`, `created_by`, `created_at`, `status`) VALUES (@1, @2, @3, @4, @5)"
                        Cmd = New MySqlCommand(Sql, ConnectionLocal)
                        Cmd.Parameters.Add("@1", MySqlDbType.Text).Value = "SALES-INVOICE"
                        Cmd.Parameters.Add("@2", MySqlDbType.Text).Value = SALES
                        Cmd.Parameters.Add("@3", MySqlDbType.Text).Value = ClientCrewID
                        Cmd.Parameters.Add("@4", MySqlDbType.Text).Value = FullDate24HR()
                        Cmd.Parameters.Add("@5", MySqlDbType.Text).Value = 1
                        Cmd.ExecuteNonQuery()
                        ToolStripLabelOthersStatus.Text = "Added successfully!"
                    End If
                End Using
            End If

            If Not String.IsNullOrEmpty(RHeader) Then
                Sql = "SELECT * FROM loc_receipt WHERE status = 1 AND type = 'REFUND-HEADER'"
                Cmd = New MySqlCommand(Sql, ConnectionLocal)
                Using reader As MySqlDataReader = Cmd.ExecuteReader
                    If reader.HasRows Then
                        reader.Dispose()
                        Sql = "UPDATE loc_receipt SET description = @1 WHERE type = 'REFUND-HEADER'"
                        Cmd = New MySqlCommand(Sql, ConnectionLocal)
                        Cmd.Parameters.Add("@1", MySqlDbType.Text).Value = RHeader
                        Cmd.ExecuteNonQuery()
                        ToolStripLabelOthersStatus.Text = "Updated successfully!"
                    Else
                        reader.Dispose()
                        Sql = "INSERT INTO loc_receipt (`type`,`description`, `created_by`, `created_at`, `status`) VALUES (@1, @2, @3, @4, @5)"
                        Cmd = New MySqlCommand(Sql, ConnectionLocal)
                        Cmd.Parameters.Add("@1", MySqlDbType.Text).Value = "REFUND-HEADER"
                        Cmd.Parameters.Add("@2", MySqlDbType.Text).Value = RHeader
                        Cmd.Parameters.Add("@3", MySqlDbType.Text).Value = ClientCrewID
                        Cmd.Parameters.Add("@4", MySqlDbType.Text).Value = FullDate24HR()
                        Cmd.Parameters.Add("@5", MySqlDbType.Text).Value = 1
                        Cmd.ExecuteNonQuery()
                        ToolStripLabelOthersStatus.Text = "Added successfully!"
                    End If
                End Using
            End If

            'If Not String.IsNullOrEmpty(RFooter) Then
            '    Sql = "SELECT * FROM loc_receipt WHERE status = 1 AND type = 'REFUND-FOOTER'"
            '    Cmd = New MySqlCommand(Sql, ConnectionLocal)
            '    Using reader As MySqlDataReader = Cmd.ExecuteReader
            '        If reader.HasRows Then
            '            reader.Dispose()
            '            Sql = "UPDATE loc_receipt SET description = @1 WHERE type = 'REFUND-FOOTER'"
            '            Cmd = New MySqlCommand(Sql, ConnectionLocal)
            '            Cmd.Parameters.Add("@1", MySqlDbType.Text).Value = RFooter
            '            Cmd.ExecuteNonQuery()
            '            ToolStripLabelOthersStatus.Text = "Updated successfully!"
            '        Else
            '            reader.Dispose()
            '            Sql = "INSERT INTO loc_receipt (`type`,`description`, `created_by`, `created_at`, `status`) VALUES (@1, @2, @3, @4, @5)"
            '            Cmd = New MySqlCommand(Sql, ConnectionLocal)
            '            Cmd.Parameters.Add("@1", MySqlDbType.Text).Value = "REFUND-FOOTER"
            '            Cmd.Parameters.Add("@2", MySqlDbType.Text).Value = RFooter
            '            Cmd.Parameters.Add("@3", MySqlDbType.Text).Value = ClientCrewID
            '            Cmd.Parameters.Add("@4", MySqlDbType.Text).Value = FullDate24HR()
            '            Cmd.Parameters.Add("@5", MySqlDbType.Text).Value = 1
            '            Cmd.ExecuteNonQuery()
            '            ToolStripLabelOthersStatus.Text = "Added successfully!"
            '        End If
            '    End Using
            'End If

            If Not String.IsNullOrEmpty(OfficialInvoiceBody) Then
                Sql = "SELECT * FROM loc_receipt WHERE status = 1 AND type = 'OFFICIAL-INVOICE'"
                Cmd = New MySqlCommand(Sql, ConnectionLocal)
                Using reader As MySqlDataReader = Cmd.ExecuteReader
                    If reader.HasRows Then
                        reader.Dispose()
                        Sql = "UPDATE loc_receipt SET description = @1 WHERE type = 'OFFICIAL-INVOICE'"
                        Cmd = New MySqlCommand(Sql, ConnectionLocal)
                        Cmd.Parameters.Add("@1", MySqlDbType.Text).Value = OfficialInvoiceBody
                        Cmd.ExecuteNonQuery()
                        ToolStripLabelOthersStatus.Text = "Updated successfully!"
                    Else
                        reader.Dispose()
                        Sql = "INSERT INTO loc_receipt (`type`,`description`, `created_by`, `created_at`, `status`) VALUES (@1, @2, @3, @4, @5)"
                        Cmd = New MySqlCommand(Sql, ConnectionLocal)
                        Cmd.Parameters.Add("@1", MySqlDbType.Text).Value = "OFFICIAL-INVOICE"
                        Cmd.Parameters.Add("@2", MySqlDbType.Text).Value = OfficialInvoiceBody
                        Cmd.Parameters.Add("@3", MySqlDbType.Text).Value = ClientCrewID
                        Cmd.Parameters.Add("@4", MySqlDbType.Text).Value = FullDate24HR()
                        Cmd.Parameters.Add("@5", MySqlDbType.Text).Value = 1
                        Cmd.ExecuteNonQuery()
                        ToolStripLabelOthersStatus.Text = "Added successfully!"
                    End If
                End Using
            End If

            If Not String.IsNullOrEmpty(OfficialRefundBody) Then
                Sql = "SELECT * FROM loc_receipt WHERE status = 1 AND type = 'OFFICIAL-REFUND'"
                Cmd = New MySqlCommand(Sql, ConnectionLocal)
                Using reader As MySqlDataReader = Cmd.ExecuteReader
                    If reader.HasRows Then
                        reader.Dispose()
                        Sql = "UPDATE loc_receipt SET description = @1 WHERE type = 'OFFICIAL-REFUND'"
                        Cmd = New MySqlCommand(Sql, ConnectionLocal)
                        Cmd.Parameters.Add("@1", MySqlDbType.Text).Value = OfficialRefundBody
                        Cmd.ExecuteNonQuery()
                        ToolStripLabelOthersStatus.Text = "Updated successfully!"
                    Else
                        reader.Dispose()
                        Sql = "INSERT INTO loc_receipt (`type`,`description`, `created_by`, `created_at`, `status`) VALUES (@1, @2, @3, @4, @5)"
                        Cmd = New MySqlCommand(Sql, ConnectionLocal)
                        Cmd.Parameters.Add("@1", MySqlDbType.Text).Value = "OFFICIAL-REFUND"
                        Cmd.Parameters.Add("@2", MySqlDbType.Text).Value = OfficialRefundBody
                        Cmd.Parameters.Add("@3", MySqlDbType.Text).Value = ClientCrewID
                        Cmd.Parameters.Add("@4", MySqlDbType.Text).Value = FullDate24HR()
                        Cmd.Parameters.Add("@5", MySqlDbType.Text).Value = 1
                        Cmd.ExecuteNonQuery()
                        ToolStripLabelOthersStatus.Text = "Added successfully!"
                    End If
                End Using
            End If

        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Settings Form: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub ToolStripButtonUpdateOthers_Click(sender As Object, e As EventArgs) Handles ToolStripButtonUpdateOthers.Click
        SaveOthers(Trim(TextBoxSIOthers.Text), Trim(TextBoxRefundHeader.Text), Trim(TextBoxOfficialInvoice.Text), Trim(TextBoxOfficialRefund.Text))
    End Sub



    Private Sub RadioButtonAutoSyncOn_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonAutoSyncOn.CheckedChanged, RadioButtonAutoSyncOff.CheckedChanged
        If RadioButtonAutoSyncOn.Checked Then
            My.Settings.S_Auto_Sync = True
            My.Settings.Save()
        Else
            My.Settings.S_Auto_Sync = False
            My.Settings.Save()
        End If
    End Sub

    Private Sub TextBoxSyncInterval_TextChanged(sender As Object, e As EventArgs) Handles TextBoxSyncInterval.TextChanged
        Try
            My.Settings.S_Sync_Interval = TextBoxSyncInterval.Text
            My.Settings.Save()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub TextBoxSyncInterval_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBoxSyncInterval.KeyPress, TextBoxS_ZeroRated.KeyPress
        Try
            Numeric(sender, e)
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub TextBoxHeaderDesc_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBoxValidity.KeyPress, TextBoxSIOthers.KeyPress, TextBoxRefundHeader.KeyPress, TextBoxRefundFooter.KeyPress, TextBoxOfficialRefund.KeyPress, TextBoxOfficialInvoice.KeyPress, TextBoxHeaderDesc.KeyPress, TextBoxFooterDesc.KeyPress
        Try
            If InStr(DisallowedCharactersCustom, e.KeyChar) > 0 Then
                e.Handled = True
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonAutoUpdateOff.CheckedChanged, RadioButtonAutoUpdateOn.CheckedChanged
        If RadioButtonAutoUpdateOn.Checked Then
            My.Settings.Auto_Update = True
            My.Settings.Save()
        Else
            My.Settings.Auto_Update = False
            My.Settings.Save()
        End If
    End Sub

    Private Sub ButtonSaveZeroRated_Click(sender As Object, e As EventArgs) Handles ButtonSaveZeroRated.Click
        Try
            If Double.Parse(TextBoxS_ZeroRated.Text) > 0 Then
                Dim ConnectionLocal As MySqlConnection = LocalhostConn()
                Dim Query As String = "UPDATE loc_settings SET S_ZeroRated_Tax = '" & Trim(TextBoxS_ZeroRated.Text) & "' WHERE settings_id = 1"
                Dim Command As MySqlCommand = New MySqlCommand(Query, ConnectionLocal)
                S_ZeroRated_Tax = Trim(TextBoxS_ZeroRated.Text)
                Command.ExecuteNonQuery()
                ConnectionLocal.Close()
                Command.Dispose()
            Else
                MsgBox("Zero rated tax must be greater than zero")
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub



#End Region
End Class