Imports MySql.Data.MySqlClient
Public Class StockAdjustment
    Dim IngredientIndex As Integer = 0
    Private Sub StockAdjustment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.TopMost = True
            LoadReasonCategories()
            LoadReasonCategoriesDeactivated()
            countingredients()
            loadcomboboxingredients()
            TabControl3.TabPages(0).Text = "Stock Adjustment (Add/Deduct/Transfer)"
            TabControl3.TabPages(1).Text = "Stock Adjustment (Settings)"
            TabControl4.TabPages(0).Text = "Active"
            TabControl4.TabPages(1).Text = "Deactivated"
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Stock Adjustment: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Sub loadcomboboxingredients()
        Try
            Dim sql = "SELECT product_ingredients FROM loc_pos_inventory WHERE main_inventory_id = 0 AND stock_status = 1  AND main_inventory_id = 0 ORDER BY product_ingredients ASC"
            Dim cmd As MySqlCommand = New MySqlCommand(sql, LocalhostConn)
            Dim da As MySqlDataAdapter = New MySqlDataAdapter(cmd)
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            For i As Integer = 0 To dt.Rows.Count - 1 Step +1
                ComboBoxDESC.Items.Add(dt(i)(0))
            Next
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Stock Adjustment: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub


    Private Sub LoadOutlets()
        Try
            GLOBAL_SELECT_ALL_FUNCTION_COMBOBOX("admin_outlets WHERE user_guid = '" & ClientGuid & "' AND store_id NOT IN(" & ClientStoreID & ")", "store_name", ComboBoxtransfer, False)
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Stock Adjustment: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Public Sub FillComboboxReason()
        Try
            GLOBAL_SELECT_ALL_FUNCTION_COMBOBOX("loc_transfer_data WHERE active = 1", "transfer_cat", ComboBoxDeduction, True)
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Stock Adjustment: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Public Sub LoadReasonCategories()
        Try
            GLOBAL_SELECT_ALL_FUNCTION("`loc_transfer_data` WHERE active = 1", "`transfer_id`, `transfer_cat`, `crew_id`, `created_at`, `created_by`, `updated_at`", DataGridViewReasonCategories)
            With DataGridViewReasonCategories
                .Columns(0).Visible = False
                .Columns(1).HeaderText = "Category"
                .Columns(2).HeaderText = "Crew"
                .Columns(3).HeaderText = "Date Created"
                .Columns(4).HeaderText = "Created By"
                .Columns(5).HeaderText = "Updated At"
            End With
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Stock Adjustment: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub

    Private Sub countingredients()
        Label7.Text = "(" & count(table:="loc_pos_inventory", tocount:="inventory_id") & ") record(s) count"
    End Sub
    Private Sub ButtonSave_Click(sender As Object, e As EventArgs) Handles ButtonSave.Click
        Try
            Dim SQL = ""
            Dim Ingredient = ComboBoxDESC.Text
            Dim Origin As String = ""
            Dim ID As Integer = 0

            If Val(TextBoxFormulaID.Text) = 0 Then
                Origin = "Server"
                ID = TextBoxServerInventoryID.Text
                SQL = "SELECT `stock_primary`, `stock_secondary`, `stock_no_of_servings` FROM  `loc_pos_inventory` WHERE `server_inventory_id` = " & ID
            Else
                Origin = "Local"
                ID = TextBox1.Text
                SQL = "SELECT `stock_primary`, `stock_secondary`, `stock_no_of_servings` FROM  `loc_pos_inventory` WHERE `inventory_id` = " & ID
            End If


            If ComboBoxAction.Text <> "" Then
                If TextboxIsEmpty(Panel23) Then
                    Dim Primary As Double = Double.Parse(TextBoxIPQuantity.Text) * Double.Parse(TextBoxFPrimaryVal.Text)
                    Dim Secondary As Double = Double.Parse(TextBoxIPQuantity.Text) * Double.Parse(TextBoxFSecondary.Text)
                    Dim NoOfServings As Double = Double.Parse(TextBoxIPQuantity.Text) * Double.Parse(TextBoxFnoofservings.Text)

                    Dim SQLCmd As MySqlCommand = New MySqlCommand(SQL, LocalhostConn)
                    Dim SQlDa As MySqlDataAdapter = New MySqlDataAdapter(SQLCmd)
                    Dim InvDT As DataTable = New DataTable
                    SQlDa.Fill(InvDT)
                    Dim InvPrimary As Double = 0
                    Dim InvSecondary As Double = 0
                    Dim InvServings As Double = 0
                    For Each row As DataRow In InvDT.Rows
                        InvPrimary = row("stock_primary")
                        InvSecondary = row("stock_secondary")
                        InvServings = row("stock_no_of_servings")
                    Next
                    LocalhostConn.Close()
                    If ComboBoxAction.Text = "ADD" Then
                        Dim TotalPrimary As Double = Primary + InvPrimary
                        Dim TotalSecondary As Double = Secondary + InvSecondary
                        Dim TotalNoOfServings As Double = NoOfServings + InvServings

                        SystemLogType = "NEW STOCK ADDED"
                        SystemLogDesc = "Adding stock of: " & Ingredient & " ,Quantity(Primary): " & TextBoxIPQuantity.Text & " ,Reason: " & TextBoxIReason.Text
                        Dim table = "loc_pos_inventory"
                        Dim fields = "`stock_primary`=" & TotalPrimary & ",`stock_secondary`= " & TotalSecondary & " , `stock_no_of_servings`= " & TotalNoOfServings & ", `date_modified` = '" & FullDate24HR() & "'"
                        Dim where = ""
                        If Origin = "Local" Then
                            where = "formula_id = " & ID
                        Else
                            where = "server_inventory_id = " & ID
                        End If
                        AuditTrail.LogToAuditTral("Transaction", "Menu/Inventory/Stock Adjustment: New Stock " & SystemLogDesc, "Normal")

                        GLOBAL_FUNCTION_UPDATE(table, fields, where)
                    ElseIf ComboBoxAction.Text = "TRANSFER" Then

                        Dim TotalPrimary As Double = InvPrimary - Primary
                        Dim TotalSecondary As Double = InvSecondary - Secondary
                        Dim TotalNoOfServings As Double = InvServings - NoOfServings

                        SystemLogType = "STOCK TRANSFER"
                        SystemLogDesc = "Transfer stock to: " & ComboBoxtransfer.Text & " ,Item: " & Ingredient & " ,Quantity(Primary): " & TextBoxIPQuantity.Text & " ,Reason: " & TextBoxIReason.Text

                        Dim table = "loc_pos_inventory"
                        Dim fields = "`stock_primary`=" & TotalPrimary & ",`stock_secondary`= " & TotalSecondary & " , `stock_no_of_servings`= " & TotalNoOfServings & ", `date_modified` = '" & FullDate24HR() & "'"
                        Dim where = ""
                        If Origin = "Local" Then
                            where = "formula_id = " & ID
                        Else
                            where = "server_inventory_id = " & ID
                        End If
                        AuditTrail.LogToAuditTral("Transaction", "Menu/Inventory/Stock Adjustment: Stock Transfer " & SystemLogDesc, "Normal")

                        GLOBAL_FUNCTION_UPDATE(table, fields, where)
                    ElseIf ComboBoxAction.Text = "DEDUCT" Then
                        Dim TotalPrimary As Double = InvPrimary - Primary
                        Dim TotalSecondary As Double = InvSecondary - Secondary
                        Dim TotalNoOfServings As Double = InvServings - NoOfServings

                        SystemLogType = "STOCK REMOVAL"
                        SystemLogDesc = "Removing stock of: " & Ingredient & " ,Quantity(Primary): " & TextBoxIPQuantity.Text & " ,Reason: (" & ComboBoxDeduction.Text & ") " & TextBoxIReason.Text

                        Dim table = "loc_pos_inventory"
                        Dim fields = "`stock_primary`=" & TotalPrimary & ",`stock_secondary`= " & TotalSecondary & " , `stock_no_of_servings`= " & TotalNoOfServings & ", `date_modified` = '" & FullDate24HR() & "'"

                        Dim where = ""
                        If Origin = "Local" Then
                            where = "formula_id = " & ID
                        Else
                            where = "server_inventory_id = " & ID
                        End If
                        AuditTrail.LogToAuditTral("Transaction", "Menu/Inventory/Stock Adjustment: Stock Remove " & SystemLogDesc, "Normal")

                        GLOBAL_FUNCTION_UPDATE(table, fields, where)
                    End If
                    TextBoxIPQuantity.Clear()
                    GLOBAL_SYSTEM_LOGS(SystemLogType, SystemLogDesc)

                    MDIFORM.newMDIchildInventory.loadinventory()
                    MDIFORM.newMDIchildInventory.loadstockadjustmentreport(False)
                    MDIFORM.newMDIchildInventory.loadcriticalstocks()
                    MDIFORM.LabelTotalAvailStock.Text = roundsum("stock_primary", "loc_pos_inventory WHERE store_id = " & ClientStoreID & " AND guid = '" & ClientGuid & "'", "P")
                    MDIFORM.LabelTotalCrititems.Text = count(table:="loc_pos_inventory WHERE stock_status = 1 AND critical_limit >= stock_primary AND store_id ='" & ClientStoreID & "' AND guid = '" & ClientGuid & "'", tocount:="inventory_id")
                Else
                    MessageBox.Show("Fill up all empty fields", "Empty Fields", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
            Else
                MessageBox.Show("Select action first", "No Action Selected", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Stock Adjustment: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub

    Private Sub ComboBoxAction_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxAction.SelectedIndexChanged
        Try
            If ComboBoxAction.Text = "DEDUCT" Then
                ComboBoxDeduction.Enabled = True
                ComboBoxtransfer.Enabled = False
                FillComboboxReason()
            ElseIf ComboBoxAction.Text = "TRANSFER" Then
                If CheckForInternetConnection() = True Then
                    ComboBoxDeduction.Enabled = False
                    ComboBoxtransfer.Enabled = True
                    LoadOutlets()
                Else
                    MsgBox("Internet connection not available.")
                    ComboBoxAction.SelectedIndex = 0
                End If
            Else
                ComboBoxDeduction.Enabled = False
                ComboBoxtransfer.Enabled = False
            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Stock Adjustment: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub

    Public AddOrUpdate As Boolean = False
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Enabled = False
        AddOrUpdate = False
        PanelReasonCat.Show()
    End Sub
    Private Sub ButtonDeleteProducts_Click(sender As Object, e As EventArgs) Handles ButtonDeleteProducts.Click
        Try
            If DataGridViewReasonCategories.SelectedRows(0).Cells(2).Value <> "Server" Then
                If DataGridViewReasonCategories.SelectedRows.Count > 0 Then
                    Dim msg = MessageBox.Show("Are you sure do you want to deactivate this category ?", "Deactivation", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                    If msg = DialogResult.Yes Then
                        Dim sql = "UPDATE loc_transfer_data SET active = 0, updated_at = '" & FullDate24HR() & "' WHERE transfer_id = " & DataGridViewReasonCategories.SelectedRows(0).Cells(0).Value
                        Dim cmd As MySqlCommand = New MySqlCommand(sql, LocalhostConn)
                        cmd.ExecuteNonQuery()
                        LoadReasonCategories()
                        LoadReasonCategoriesDeactivated()
                    End If
                Else
                    MsgBox("Select Category first")
                End If
            Else
                MsgBox("Server category cannot be edited")
            End If

        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Stock Adjustment: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click

        Try
            If DataGridViewReasonCategories.SelectedRows(0).Cells(2).Value <> "Server" Then
                If DataGridViewReasonCategories.Rows.Count > 0 Then
                    AddOrUpdate = True
                    PanelReasonCat.Show()
                    PanelReasonCat.TextBoxReasonsCat.Text = DataGridViewReasonCategories.SelectedRows(0).Cells(1).Value
                    PanelReasonCat.TransferID = DataGridViewReasonCategories.SelectedRows(0).Cells(0).Value
                Else
                    MsgBox("Select category first")
                End If
            Else
                MsgBox("Server category cannot be edited")
            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Stock Adjustment: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        Try
            If DataGridViewDeactivatedReasonCat.SelectedRows.Count = 1 Then
                Dim msg = MessageBox.Show("Are you sure do you want to activate this category ?", "Activation", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                If msg = DialogResult.Yes Then
                    Dim sql = "UPDATE `loc_transfer_data` SET `active`=@1 , `updated_at`=@2 WHERE transfer_id = " & DataGridViewDeactivatedReasonCat.SelectedRows(0).Cells(0).Value
                    cmd = New MySqlCommand(sql, LocalhostConn)
                    cmd.Parameters.Add("@1", MySqlDbType.Text).Value = "1"
                    cmd.Parameters.Add("@2", MySqlDbType.Text).Value = FullDate24HR()
                    cmd.ExecuteNonQuery()
                    GLOBAL_SYSTEM_LOGS("REASON CATEGORY ACTIVATED", "Category Name: " & DataGridViewDeactivatedReasonCat.SelectedRows(0).Cells(1).Value & " ,Activated By: " & ClientCrewID)
                    LoadReasonCategories()
                    LoadReasonCategoriesDeactivated()
                    FillComboboxReason()
                End If
            ElseIf DataGridViewDeactivatedReasonCat.SelectedRows.Count > 1 Then
                MsgBox("Select one category only.")
            Else
                MsgBox("Select category Category first")
            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Stock Adjustment: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Public Sub LoadReasonCategoriesDeactivated()
        Try
            GLOBAL_SELECT_ALL_FUNCTION("`loc_transfer_data` WHERE active = 0", "`transfer_id`, `transfer_cat`, `crew_id`, `created_at`, `created_by`, `updated_at`", DataGridViewDeactivatedReasonCat)
            With DataGridViewDeactivatedReasonCat
                .Columns(0).Visible = False
                .Columns(1).HeaderText = "Category"
                .Columns(2).HeaderText = "Crew"
                .Columns(3).HeaderText = "Date Created"
                .Columns(4).HeaderText = "Created By"
                .Columns(5).HeaderText = "Updated At"
            End With
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Stock Adjustment: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub

    Private Sub StockAdjustment_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        MDIFORM.newMDIchildInventory.Enabled = True
        If Application.OpenForms().OfType(Of PanelReasonCat).Any Then
            PanelReasonCat.Close()
        End If
    End Sub

    Private Sub ButtonKeyboard_Click(sender As Object, e As EventArgs) Handles ButtonKeyboard.Click
        ShowKeyboard()
    End Sub

    Private Sub TextBoxIPQuantity_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBoxIPQuantity.KeyPress
        Try
            Numeric(sender, e)
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub

    Private Sub TextBoxIReason_KeyPress(sender As Object, e As KeyPressEventArgs)
        Try
            If InStr(DisallowedCharacters, e.KeyChar) > 0 Then
                e.Handled = True
            End If
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub

    Private Sub ComboBoxDESC_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxDESC.SelectedIndexChanged
        Try
            Dim sql = "SELECT inventory_id, stock_primary, stock_secondary, origin, server_inventory_id FROM loc_pos_inventory WHERE product_ingredients = '" & ComboBoxDESC.Text & "'"
            Dim cmd As MySqlCommand = New MySqlCommand(sql, LocalhostConn)
            Dim da As MySqlDataAdapter = New MySqlDataAdapter(cmd)
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            TextBoxIPrimaryVal.Text = dt(0)(1)
            TextBoxISecondaryTotal.Text = dt(0)(2)
            TextBox1.Text = dt(0)(0)
            TextBoxFormulaID.Text = dt(0)(3)
            TextBoxServerInventoryID.Text = dt(0)(4)
            SelectFormulaEntry(dt(0)(0), dt(0)(3))
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Stock Adjustment: " & ex.ToString, "Critical")

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
                TextBoxFPrimaryVal.Text = row("primary_value")
                TextBoxIPrimaryUnit.Text = row("primary_unit")

                TextBoxFSecondary.Text = row("secondary_value")
                TextBoxFSecondaryUnit.Text = row("secondary_unit")

                TextBoxServingValue.Text = row("serving_value")
                TextBoxServingUnit.Text = row("serving_unit")
                TextBoxFnoofservings.Text = row("no_servings")
            Next
            LocalhostConn.Close()
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Stock Adjustment: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub

End Class