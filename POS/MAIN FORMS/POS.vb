Imports MySql.Data.MySqlClient
Imports System.Drawing.Printing
Imports System.Threading
Imports System.Data
Imports System.Linq
Imports System.Xml
Imports System.Text

Public Class POS
    Private WithEvents printdoc As PrintDocument = New PrintDocument
    'Private WithEvents printdoc2 As PrintDocument = New PrintDocument
    Private PrintPreviewDialog1 As New PrintPreviewDialog
    'Private PrintPreviewDialog2 As New PrintPreviewDialog
    Private Location_control As New Point(0, 0)

    Public ButtonClickCount As Integer = 0

    Public SUPERAMOUNTDUE
    Public WaffleUpgrade As Boolean = False

    Private Shared _instance As POS

    Dim Font1Bold As New Font("Tahoma", 6, FontStyle.Bold)
    Dim Font2Bold As New Font("Tahoma", 7, FontStyle.Bold)
    Dim FontDefault As New Font("Tahoma", 6)
    Dim FontAddOn As New Font("Tahoma", 5)
    Public ReadOnly Property Instance As POS
        Get
            Return _instance
        End Get
    End Property
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _instance = Me
        LabelFOOTER.Text = My.Settings.Footer
        Try
            If Application.OpenForms().OfType(Of SynctoCloud).Any Then
                SynctoCloud.BringToFront()
            End If

            LabelStorename.Text = ClientStorename
            Label11.Focus()
            Timer1.Start()

            Enabled = False
            BegBalance.Show()
            BegBalance.TopMost = True

        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "POS: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub

    Public Sub LoadCategory()
        Try
            Panel3.Controls.Clear()
            Location_control = New Point(0, 0)
            Dim ConnectionLocal As MySqlConnection = LocalhostConn()
            Dim sql = "SELECT category_name FROM loc_admin_category WHERE status = 1"
            Dim cmd As MySqlCommand = New MySqlCommand(sql, ConnectionLocal)
            Dim da As MySqlDataAdapter = New MySqlDataAdapter(cmd)
            Dim dt As DataTable = New DataTable()
            da.Fill(dt)
            With cmd
                For Each row As DataRow In dt.Rows
                    Dim buttonname As String = row("category_name")
                    Dim new_Button As New Button
                    Dim panellocation As New Panel
                    With new_Button
                        .Name = buttonname
                        .Text = buttonname
                        .TextImageRelation = TextImageRelation.ImageBeforeText
                        .TextAlign = ContentAlignment.MiddleCenter
                        .ForeColor = Color.White
                        .Font = New Font("Tahoma", 9, FontStyle.Bold)
                        .FlatStyle = FlatStyle.Flat
                        .FlatAppearance.BorderSize = 0
                        .Location = New Point(Location_control.X, Location_control.Y)
                        .Width = 120
                        .Height = 53
                        .Cursor = Cursors.Hand
                        Location_control.X += .Height + 65
                        AddHandler .Click, AddressOf new_Button_click_category
                    End With
                    Panel3.Controls.Add(new_Button)
                Next
            End With
        Catch ex As Exception
            SendErrorReport(ex.ToString)
            AuditTrail.LogToAuditTral("System", "POS/Load Category: " & ex.ToString, "Critical")
        End Try
    End Sub
    Dim Partners As String = ""
    Public Sub new_Button_click_category(ByVal sender As Object, ByVal e As EventArgs)
        Try
            'NEW BUTTON ON CLICK EVENT 
            If TypeOf sender Is Button Then
                Dim btn = sender
                Dim name = btn.name
                Partners = name
                btnformcolor(changecolor:=sender)
                btndefaut(defaultcolor:=sender, form:=Me)

                If name = "Others" Or name = "Famous Blends" Or name = "Add-Ons" Then
                    WaffleUpgrade = False
                    ButtonWaffleUpgrade.Text = "Brownie Upgrade"
                    ButtonWaffleUpgrade.BackColor = Color.FromArgb(221, 114, 46)
                    ButtonWaffleUpgrade.Enabled = False
                Else
                    ButtonWaffleUpgrade.Enabled = True
                End If
                listviewproductsshow(name, ComboBoxPartners.Text)
            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "POS: " & ex.ToString, "Critical")
        End Try
    End Sub

    Private Sub ButtonLogout_Click(sender As Object, e As EventArgs) Handles ButtonLogout.Click
        'LOGOUT
        If SyncIsOnProcess = True Then
            MessageBox.Show("Sync is on process please wait.", "Syncing", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            If MessageBox.Show("Are you sure you really want to Logout ?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbYes Then
                AuditTrail.LogToAuditTral("User", "User Logout: " & ClientCrewID, "Normal")

                Enabled = False
                LOGOUTFROMPOS = True
                CashBreakdown.Show()
            End If
        End If
    End Sub
    Private Sub ButtonSettings_Click(sender As Object, e As EventArgs) Handles ButtonSettings.Click
        SettingsForm.Show()
        Enabled = False
    End Sub
    Private Sub ButtonExpense_Click(sender As Object, e As EventArgs) Handles ButtonExpense.Click
        Enabled = False
        Dim newMDIchild As New Addexpense()
        If Application.OpenForms().OfType(Of Addexpense).Any Then
            Addexpense.BringToFront()
        Else
            Addexpense.Show()
            Addexpense.Focus()
        End If
        'VIEW EXPENSE FORM
    End Sub
    Private Sub ButtonMenu_Click(sender As Object, e As EventArgs) Handles ButtonMenu.Click
        'VIEW MENU FORM
        'messageboxappearance = False
        SystemLogType = "MENU FORM"
        SystemLogDesc = "Accessed by :" & returnfullname(ClientCrewID) & " : " & ClientRole
        GLOBAL_SYSTEM_LOGS(SystemLogType, SystemLogDesc)
        Enabled = False
        MDIFORM.Show()
    End Sub
    Private Sub ButtonPromo_Click(sender As Object, e As EventArgs) Handles ButtonPromo.Click
        'VIEW PROMO FORM
        Me.Enabled = False
        If Application.OpenForms().OfType(Of CouponCode).Any Then
            CouponCode.BringToFront()
        Else
            CouponCode.Show()
            CouponCode.ButtonSubmit.Enabled = False
        End If
    End Sub

    Private Sub Button38_Click(sender As Object, e As EventArgs) Handles ButtonEnter.Click
        Try
            If payment = False Then
                If Val(TextBoxQTY.Text) > 0 Then

                    Dim TotalProductPrice As Double = 0
                    Dim productprice = DataGridViewOrders.SelectedRows(0).Cells(2).Value
                    'Procedure: 1 Product Qty
                    If DataGridViewOrders.Rows.Count > 0 Then
                        If S_ZeroRated = "0" Then
                            'Price not / by 1.12
                            If WaffleUpgrade Then
                                'Price plus waffle upgrade price
                                Dim TotalPrice As Integer = 0
                                TotalPrice = Val(TextBoxQTY.Text) * Val(productprice)
                                Dim TotalUpgrade As Integer = 0
                                TotalUpgrade = Val(TextBoxQTY.Text) * Val(S_Upgrade_Price)
                                TotalProductPrice = TwoDecimalPlaces(TotalPrice + TotalUpgrade)
                                DataGridViewOrders.SelectedRows(0).Cells(11).Value = TextBoxQTY.Text
                            Else
                                Dim TotalPrice As Integer = 0
                                TotalPrice = Val(TextBoxQTY.Text) * Val(productprice)
                                TotalProductPrice = TwoDecimalPlaces(TotalPrice)
                            End If
                            DataGridViewOrders.SelectedRows(0).Cells(1).Value = TextBoxQTY.Text
                            DataGridViewOrders.SelectedRows(0).Cells(3).Value = TotalProductPrice

                            Compute()

                            For i As Integer = 0 To DataGridViewInv.Rows.Count - 1 Step +1
                                If DataGridViewOrders.SelectedRows(0).Cells(0).Value = DataGridViewInv.Rows(i).Cells(4).Value Then
                                    DataGridViewInv.Rows(i).Cells(2).Value = Val(TextBoxQTY.Text)
                                    DataGridViewInv.Rows(i).Cells(0).Value = DataGridViewInv.Rows(i).Cells(2).Value * DataGridViewInv.Rows(i).Cells(5).Value
                                    DataGridViewInv.Rows(i).Cells(6).Value = DataGridViewInv.Rows(i).Cells(2).Value * DataGridViewInv.Rows(i).Cells(7).Value
                                End If
                            Next

                        Else
                            Dim Tax = 1 + Val(S_ZeroRated)
                            If WaffleUpgrade Then
                                Dim TotalPrice As Integer = 0
                                TotalPrice = Val(TextBoxQTY.Text) * Val(productprice)
                                Dim TotalUgrade As Integer = 0
                                TotalUgrade = Val(TextBoxQTY.Text) * Val(S_Upgrade_Price)
                                Dim WaffleAddPriceTotal = TotalPrice + TotalUgrade
                                TotalProductPrice = TwoDecimalPlaces(WaffleAddPriceTotal / Tax)
                                DataGridViewOrders.SelectedRows(0).Cells(11).Value = TextBoxQTY.Text
                            Else
                                Dim TotalPrice As Integer = 0
                                TotalPrice = Val(TextBoxQTY.Text) * Val(productprice)
                                TotalProductPrice = TwoDecimalPlaces(TotalPrice / Tax)
                            End If
                            DataGridViewOrders.SelectedRows(0).Cells(1).Value = TextBoxQTY.Text
                            DataGridViewOrders.SelectedRows(0).Cells(3).Value = TotalProductPrice

                            Compute()

                            For i As Integer = 0 To DataGridViewInv.Rows.Count - 1 Step +1
                                If DataGridViewOrders.Rows(0).Cells(0).Value = DataGridViewInv.Rows(i).Cells(4).Value Then
                                    DataGridViewInv.Rows(i).Cells(2).Value = Val(TextBoxQTY.Text)
                                    DataGridViewInv.Rows(i).Cells(0).Value = DataGridViewInv.Rows(i).Cells(2).Value * DataGridViewInv.Rows(i).Cells(5).Value
                                    DataGridViewInv.Rows(i).Cells(6).Value = DataGridViewInv.Rows(i).Cells(2).Value * DataGridViewInv.Rows(i).Cells(7).Value
                                End If
                            Next
                        End If

                    End If
                End If
                TextBoxQTY.Text = 0
            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "POS: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles ButtonPendingOrders.Click
        Dim newMDIchild As New PendingOrders()
        If Application.OpenForms().OfType(Of PendingOrders).Any Then
            PendingOrders.BringToFront()
        Else
            PendingOrders.Show()
            posandpendingenter = True
            Me.Enabled = False
        End If
    End Sub
    Private Sub Buttonholdoder_Click(sender As Object, e As EventArgs) Handles Buttonholdoder.Click
        If Application.OpenForms().OfType(Of HoldOrder).Any Then
            HoldOrder.BringToFront()
        Else
            HoldOrder.Show()
            Me.Enabled = False
        End If
    End Sub
    Private Sub ButtonPay_Click(sender As Object, e As EventArgs) Handles ButtonPayMent.Click
        Try
            'MsgBox(PromoApplied)
            'MsgBox(DiscAppleid)
            'MsgBox(PromoName)
            'MsgBox(PromoDesc)
            If Double.Parse(TextBoxGRANDTOTAL.Text) <= 999999999.99 Then
                If ButtonPayMent.Text = "Checkout" Then
                    'If Shift = "" Then
                    '    MessageBox.Show("Input cashier balance first", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    'Else
                    If S_Zreading = Format(Now(), "yyyy-MM-dd") Or S_Zreading = Format(Now().AddDays(1), "yyyy-MM-dd") Then
                        Enabled = False
                        PaymentForm.Show()
                        Application.DoEvents()
                        PaymentForm.TextBoxMONEY.Focus()
                        PaymentForm.TextBoxTOTALPAY.Text = TextBoxGRANDTOTAL.Text
                        PaymentForm.Focus()
                    Else
                        MessageBox.Show("Z-read first", "Z-Reading", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                    'If S_Zreading <> Format(Now(), "yyyy-MM-dd") Or Format(Now(), "yyyy-MM-dd") > Format(ReturnStringToDate(S_Zreading).AddDays(1), "yyyy-MM-dd") Then
                    'Else

                    'End If
                    'If S_Zreading <> Format(Now(), "yyyy-MM-dd") Then
                    '    
                    'Else

                    'End If
                    'End If
                Else
                    BackgroundWorkerInventory.WorkerReportsProgress = True
                    BackgroundWorkerInventory.WorkerSupportsCancellation = True
                    BackgroundWorkerInventory.RunWorkerAsync()
                End If
            Else
                MsgBox("Maximum sales capacity already reached. Please contact your administrator for immediate solution.")
            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "POS: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub ButtonWaffleUpgrade_Click(sender As Object, e As EventArgs) Handles ButtonWaffleUpgrade.Click
        Try
            If WaffleUpgrade = False Then
                WaffleUpgrade = True
                ButtonWaffleUpgrade.Text = "Classic Waffle"
                ButtonWaffleUpgrade.BackColor = Color.Brown
            Else
                WaffleUpgrade = False
                ButtonWaffleUpgrade.Text = "Brownie Upgrade"
                ButtonWaffleUpgrade.BackColor = Color.FromArgb(221, 114, 46)

            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub Mix()
        Try
            Dim inventory_id As Integer = 0
            Dim totalQuantity As Integer = 0
            Dim Ingredient As String = ""
            Dim Query As String = ""
            Dim SqlCommand As MySqlCommand
            Dim SqlAdapter As MySqlDataAdapter
            Dim SqlDt As DataTable = New DataTable

            Dim FORMPrimaryval As Double = 0
            Dim FORMSecondaryval As Double = 0
            Dim FORMServingval As Double = 0
            Dim FORMNoofservings As Double = 0

            Dim TotalPrimaryVal As Double = 0
            Dim TotalSecondaryVal As Double = 0
            Dim TotalNoOfServings As Double = 0

            Dim RetStockSec As Double = 0
            Dim RetStockPrim As Double = 0
            Dim RetNoServ As Double = 0
            Dim ProductID As Integer = 0
            With DataGridViewOrders
                For i As Integer = 0 To .Rows.Count - 1 Step +1
                    ProductID = .Rows(i).Cells(5).Value
                    inventory_id = .Rows(i).Cells(10).Value
                    totalQuantity = .Rows(i).Cells(1).Value
                    Ingredient = .Rows(i).Cells(0).Value
                    'Console.WriteLine("INV ID - " & inventory_id)
                    If .Rows(i).Cells(14).Value > 0 Then
                        Query = "SELECT `primary_value`, `secondary_value`, `serving_value`, `no_servings` FROM loc_product_formula WHERE server_formula_id = " & inventory_id
                        ' Console.WriteLine("HALF BATCH" & Query)
                        SqlCommand = New MySqlCommand(Query, LocalhostConn)
                        SqlAdapter = New MySqlDataAdapter(SqlCommand)
                        SqlDt = New DataTable
                        SqlAdapter.Fill(SqlDt)
                        For Each row As DataRow In SqlDt.Rows
                            FORMPrimaryval = row("primary_value") / 2
                            FORMSecondaryval = row("secondary_value") / 2
                            FORMServingval = row("serving_value") / 2
                            FORMNoofservings = row("no_servings") / 2

                            '  Console.WriteLine("INVENTORY PV - " & FORMPrimaryval & ", SV - " & FORMSecondaryval & ", SERVAL - " & FORMServingval & ", NO. SERV - " & FORMNoofservings)
                        Next
                    Else
                        Query = "SELECT `primary_value`, `secondary_value`, `serving_value`, `no_servings` FROM loc_product_formula WHERE server_formula_id = " & inventory_id
                        SqlCommand = New MySqlCommand(Query, LocalhostConn)
                        SqlAdapter = New MySqlDataAdapter(SqlCommand)
                        SqlDt = New DataTable
                        SqlAdapter.Fill(SqlDt)
                        For Each row As DataRow In SqlDt.Rows
                            FORMPrimaryval = row("primary_value")
                            FORMSecondaryval = row("secondary_value")
                            FORMServingval = row("serving_value")
                            FORMNoofservings = row("no_servings")

                            '  Console.WriteLine("INVENTORY PV - " & FORMPrimaryval & ", SV - " & FORMSecondaryval & ", SERVAL - " & FORMServingval & ", NO. SERV - " & FORMNoofservings)
                        Next
                    End If



                    TotalPrimaryVal = totalQuantity * FORMPrimaryval
                    TotalSecondaryVal = FORMSecondaryval * totalQuantity
                    TotalNoOfServings = FORMNoofservings * totalQuantity

                    '  Console.WriteLine("TOTAL PV - " & TotalPrimaryVal & ", TOTAL SEC - " & TotalSecondaryVal & ", TOTAL NO. SERV - " & TotalNoOfServings)

                    If .Rows(i).Cells(14).Value > 0 Then
                        Query = "SELECT `stock_primary`,`stock_secondary`,`stock_no_of_servings` FROM `loc_pos_inventory` WHERE server_inventory_id = " & inventory_id
                    Else
                        Query = "SELECT `stock_primary`,`stock_secondary`,`stock_no_of_servings` FROM `loc_pos_inventory` WHERE server_inventory_id = " & inventory_id
                    End If

                    SqlCommand = New MySqlCommand(Query, LocalhostConn)
                    SqlAdapter = New MySqlDataAdapter(SqlCommand)
                    SqlDt = New DataTable
                    SqlAdapter.Fill(SqlDt)
                    For Each row As DataRow In SqlDt.Rows
                        RetStockPrim = row("stock_primary")
                        RetStockSec = row("stock_secondary")
                        RetNoServ = row("stock_no_of_servings")

                        ' Console.WriteLine("RetStockPrim - " & RetStockPrim & ", RetStockSec - " & RetStockSec & ", RetNoServ - " & RetNoServ)
                    Next

                    Dim TotalPrimary As Double = RetStockPrim + TotalPrimaryVal
                    Dim Secondary As Double = RetStockSec + TotalSecondaryVal
                    Dim ServingValue As Double = RetNoServ + TotalNoOfServings


                    ' Console.WriteLine("TOTAL Primary - " & TotalPrimary & ", TOTAL Secondary - " & Secondary & ", TOTAL ServingValue - " & ServingValue)

                    If .Rows(i).Cells(14).Value > 0 Then
                        Query = "UPDATE loc_pos_inventory SET `stock_secondary` = " & Secondary & " , `stock_no_of_servings` = " & ServingValue & " , `stock_primary` = " & TotalPrimary & ", `date_modified` = '" & FullDate24HR() & "' WHERE `server_inventory_id` = " & inventory_id
                    Else
                        Query = "UPDATE loc_pos_inventory SET `stock_secondary` = " & Secondary & " , `stock_no_of_servings` = " & ServingValue & " , `stock_primary` = " & TotalPrimary & ", `date_modified` = '" & FullDate24HR() & "' WHERE `server_inventory_id` = " & inventory_id
                    End If
                    ' Console.WriteLine(Query)
                    SqlCommand = New MySqlCommand(Query, LocalhostConn())
                    SqlCommand.ExecuteNonQuery()
                    GLOBAL_SYSTEM_LOGS("MIX", "MIXED : " & Ingredient & ", Crew : " & ClientCrewID)
                Next


            End With
        Catch ex As Exception
            SendErrorReport(ex.ToString)
            AuditTrail.LogToAuditTral("System", "POS/MIX : " & ex.ToString, "Critical")
        End Try
    End Sub
    Dim secondary_value As Double = 0
    Dim stock_secondary As Double = 0
    'Private Sub UpdateInventory(returnVoid As Boolean)
    '    Dim SqlCommand As MySqlCommand
    '    Dim SqlAdapter As MySqlDataAdapter
    '    Dim SqlDt As DataTable
    '    Dim UpdateInventoryCon As MySqlConnection = LocalhostConn()
    '    Try
    '        Dim Query As String = ""
    '        If returnVoid Then
    '            'With ConfirmRefund.DataGridViewInv
    '            '    For i As Integer = 0 To .Rows.Count - 1 Step +1
    '            '        Dim TotalQuantity As Double = 0
    '            '        Dim TotalServingValue As Double = 0
    '            '        Dim Secondary As Double = 0
    '            '        Dim ServingValue As Double = 0
    '            '        Dim TotalPrimary As Double = 0
    '            '        TotalQuantity = .Rows(i).Cells(2).Value
    '            '        TotalServingValue = Double.Parse(.Rows(i).Cells(0).Value.ToString)
    '            '        If .Rows(i).Cells(9).Value.ToString = "Server" Then
    '            '            Query = "SELECT `secondary_value` FROM `loc_product_formula` WHERE server_formula_id = " & .Rows(i).Cells(1).Value
    '            '        Else
    '            '            Query = "SELECT `secondary_value` FROM `loc_product_formula` WHERE formula_id = " & .Rows(i).Cells(1).Value

    '            '        End If
    '            '        SqlCommand = New MySqlCommand(Query, UpdateInventoryCon)
    '            '        SqlAdapter = New MySqlDataAdapter(SqlCommand)
    '            '        SqlDt = New DataTable
    '            '        SqlAdapter.Fill(SqlDt)
    '            '        For Each row As DataRow In SqlDt.Rows
    '            '            secondary_value = row("secondary_value")
    '            '        Next
    '            '        If .Rows(i).Cells(9).Value.ToString = "Server" Then
    '            '            Query = "SELECT `stock_secondary` FROM `loc_pos_inventory` WHERE server_inventory_id = " & .Rows(i).Cells(1).Value
    '            '        Else
    '            '            Query = "SELECT `stock_secondary` FROM `loc_pos_inventory` WHERE inventory_id = " & .Rows(i).Cells(1).Value
    '            '        End If
    '            '        SqlCommand = New MySqlCommand(Query, UpdateInventoryCon)
    '            '        SqlAdapter = New MySqlDataAdapter(SqlCommand)
    '            '        SqlDt = New DataTable
    '            '        SqlAdapter.Fill(SqlDt)
    '            '        For Each row As DataRow In SqlDt.Rows
    '            '            stock_secondary = row("stock_secondary")
    '            '        Next

    '            '        'Console.WriteLine(Double.Parse(.Rows(i).Cells(5).Value.ToString) - secondary_value)
    '            '        Secondary = stock_secondary + TotalServingValue ' 93+93 = 186
    '            '        ServingValue = Secondary / Double.Parse(.Rows(i).Cells(5).Value.ToString)
    '            '        TotalPrimary = Secondary / secondary_value

    '            '        'Console.WriteLine("TOTAL SECONDARY " & Secondary)
    '            '        'Console.WriteLine("TOTAL SERVING " & ServingValue)
    '            '        ' Console.WriteLine("TOTAL PRIMARY  " & TotalPrimary)

    '            '        If .Rows(i).Cells(9).Value.ToString = "Server" Then
    '            '            Query = "UPDATE loc_pos_inventory SET stock_secondary = " & Secondary & " , stock_no_of_servings = " & ServingValue & " , stock_primary = " & TotalPrimary & ", date_modified = '" & FullDate24HR() & "' WHERE server_inventory_id = " & .Rows(i).Cells(1).Value
    '            '        Else
    '            '            Query = "UPDATE loc_pos_inventory SET stock_secondary = " & Secondary & " , stock_no_of_servings = " & ServingValue & " , stock_primary = " & TotalPrimary & ", date_modified = '" & FullDate24HR() & "' WHERE inventory_id = " & .Rows(i).Cells(1).Value
    '            '        End If
    '            '        SqlCommand = New MySqlCommand(Query, UpdateInventoryCon)
    '            '        SqlCommand.ExecuteNonQuery()
    '            '    Next
    '            '    UpdateInventoryCon.Close()
    '            'End With
    '        Else
    '            MsgBox("POS")
    '            With DataGridViewInv
    '                For i As Integer = 0 To .Rows.Count - 1 Step +1
    '                    MsgBox("POS")

    '                    Dim TotalQuantity As Double = 0
    '                    Dim TotalServingValue As Double = 0
    '                    Dim Secondary As Double = 0
    '                    Dim ServingValue As Double = 0
    '                    Dim TotalPrimary As Double = 0
    '                    TotalQuantity = .Rows(i).Cells(2).Value
    '                    TotalServingValue = Double.Parse(.Rows(i).Cells(0).Value.ToString)
    '                    If .Rows(i).Cells(9).Value.ToString = "Server" Then
    '                        Query = "SELECT `secondary_value` FROM `loc_product_formula` WHERE server_formula_id = " & .Rows(i).Cells(1).Value
    '                    Else
    '                        Query = "SELECT `secondary_value` FROM `loc_product_formula` WHERE formula_id = " & .Rows(i).Cells(1).Value

    '                    End If
    '                    SqlCommand = New MySqlCommand(Query, UpdateInventoryCon)
    '                    SqlAdapter = New MySqlDataAdapter(SqlCommand)
    '                    SqlDt = New DataTable
    '                    SqlAdapter.Fill(SqlDt)
    '                    For Each row As DataRow In SqlDt.Rows
    '                        secondary_value = row("secondary_value")
    '                    Next
    '                    If .Rows(i).Cells(9).Value.ToString = "Server" Then
    '                        Query = "SELECT `stock_secondary` FROM `loc_pos_inventory` WHERE server_inventory_id = " & .Rows(i).Cells(1).Value
    '                    Else
    '                        Query = "SELECT `stock_secondary` FROM `loc_pos_inventory` WHERE inventory_id = " & .Rows(i).Cells(1).Value
    '                    End If
    '                    SqlCommand = New MySqlCommand(Query, UpdateInventoryCon)
    '                    SqlAdapter = New MySqlDataAdapter(SqlCommand)
    '                    SqlDt = New DataTable
    '                    SqlAdapter.Fill(SqlDt)
    '                    For Each row As DataRow In SqlDt.Rows
    '                        stock_secondary = row("stock_secondary")
    '                    Next

    '                    'Console.WriteLine(Double.Parse(.Rows(i).Cells(5).Value.ToString) - secondary_value)
    '                    Secondary = stock_secondary - TotalServingValue
    '                    ServingValue = Secondary / Double.Parse(.Rows(i).Cells(5).Value.ToString)
    '                    TotalPrimary = Secondary / secondary_value

    '                    'Console.WriteLine("TOTAL SECONDARY " & Secondary)
    '                    'Console.WriteLine("TOTAL SERVING " & ServingValue)
    '                    ' Console.WriteLine("TOTAL PRIMARY  " & TotalPrimary)
    '                    MsgBox(Query)
    '                    If .Rows(i).Cells(9).Value.ToString = "Server" Then
    '                        Query = "UPDATE loc_pos_inventory SET stock_secondary = " & Secondary & " , stock_no_of_servings = " & ServingValue & " , stock_primary = " & TotalPrimary & ", date_modified = '" & FullDate24HR() & "' WHERE server_inventory_id = " & .Rows(i).Cells(1).Value
    '                    Else
    '                        Query = "UPDATE loc_pos_inventory SET stock_secondary = " & Secondary & " , stock_no_of_servings = " & ServingValue & " , stock_primary = " & TotalPrimary & ", date_modified = '" & FullDate24HR() & "' WHERE inventory_id = " & .Rows(i).Cells(1).Value
    '                    End If
    '                    MsgBox(Query)
    '                    Console.Write(Query)
    '                    SqlCommand = New MySqlCommand(Query, UpdateInventoryCon)
    '                    SqlCommand.ExecuteNonQuery()
    '                Next
    '                UpdateInventoryCon.Close()
    '            End With
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.ToString)
    '        SendErrorReport(ex.ToString)
    '    End Try
    'End Sub

    'Private Sub UpdateInventory()
    '    Dim SqlCommand As MySqlCommand
    '    Dim SqlAdapter As MySqlDataAdapter
    '    Dim SqlDt As DataTable
    '    Dim UpdateInventoryCon As MySqlConnection = LocalhostConn()
    '    Try
    '        Dim Query As String = ""
    '        With DataGridViewInv
    '            For i As Integer = 0 To .Rows.Count - 1 Step +1

    '                Dim GetStock_Primary As Double = 0
    '                Dim GetStock_Secondary As Double = 0
    '                Dim GetStock_NoOfServings As Double = 0

    '                Dim GetFormPrimaryValue As Double = 0
    '                Dim GetFormSecondaryValue As Double = 0
    '                Dim GetFormServingValue As Double = 0

    '                Dim TotalStockPrimary As Double = 0
    '                Dim TotalStockSecondary As Double = 0
    '                Dim TotaStockNoOfServings As Double = 0

    '                Dim FormulaID = .Rows(i).Cells(1).Value

    '                'Dim TotalServingValue As Double = Double.Parse(.Rows(i).Cells(0).Value.ToString)
    '                'Dim ServingValue As Double = 0
    '                'TotalQuantity = .Rows(i).Cells(2).Value

    '                If .Rows(i).Cells(9).Value.ToString = "Server" Then
    '                    Query = "SELECT `primary_value`,`secondary_value`,`serving_value` FROM `loc_product_formula` WHERE server_formula_id = " & .Rows(i).Cells(1).Value
    '                Else
    '                    Query = "SELECT `primary_value`,`secondary_value`,`serving_value` FROM `loc_product_formula` WHERE formula_id = " & .Rows(i).Cells(1).Value
    '                End If

    '                SqlCommand = New MySqlCommand(Query, UpdateInventoryCon)
    '                SqlAdapter = New MySqlDataAdapter(SqlCommand)
    '                SqlDt = New DataTable
    '                SqlAdapter.Fill(SqlDt)

    '                For Each row As DataRow In SqlDt.Rows
    '                    GetFormPrimaryValue = row("primary_value")
    '                    GetFormSecondaryValue = row("secondary_value")
    '                    GetFormServingValue = row("serving_value")
    '                Next
    '                Console.WriteLine("FORMULA PRIMARY " & GetFormPrimaryValue)

    '                If .Rows(i).Cells(9).Value.ToString = "Server" Then
    '                    Query = "SELECT `stock_primary`,`stock_secondary`,`stock_no_of_servings` FROM `loc_pos_inventory` WHERE server_inventory_id = " & FormulaID
    '                Else
    '                    Query = "SELECT `stock_primary`,`stock_secondary`,`stock_no_of_servings` FROM `loc_pos_inventory` WHERE inventory_id = " & FormulaID
    '                End If
    '                SqlCommand = New MySqlCommand(Query, UpdateInventoryCon)
    '                SqlAdapter = New MySqlDataAdapter(SqlCommand)
    '                SqlDt = New DataTable
    '                SqlAdapter.Fill(SqlDt)
    '                For Each row As DataRow In SqlDt.Rows
    '                    GetStock_Primary = row("stock_primary")
    '                    GetStock_Secondary = row("stock_secondary")
    '                    GetStock_NoOfServings = row("stock_no_of_servings")
    '                Next
    '                Console.WriteLine("GET STOCK SEC: " & GetStock_Secondary)
    '                'TotalStockPrimary = GetStock_Primary -
    '                TotalStockSecondary = GetStock_Secondary - Double.Parse(.Rows(i).Cells(0).Value)
    '                'TotaStockNoOfServings =  
    '                TotalStockPrimary = TotalStockSecondary / Double.Parse(.Rows(i).Cells(5).Value.ToString)
    '                'TotaStockNoOfServings
    '                ''
    '                ''    secondary_value = secondary_value / 2
    '                ''    secondary_value = secondary_value / 2
    '                ''End If
    '                'If .Rows(i).Cells(10).Value = 1 Then
    '                '    secondary_value = secondary_value / 2
    '                '    stock_secondary = stock_secondary / 2
    '                'End If

    '                'Secondary = stock_secondary - TotalServingValue
    '                'ServingValue = Secondary / Double.Parse(.Rows(i).Cells(5).Value.ToString)
    '                'TotalPrimary = Secondary / secondary_value



    '                Console.WriteLine("TOTAL SECONDARY " & TotalStockSecondary)
    '                Console.WriteLine("TOTAL SERVING " & TotaStockNoOfServings)
    '                Console.WriteLine("TOTAL PRIMARY  " & TotalStockPrimary)
    '                'If .Rows(i).Cells(9).Value.ToString = "Server" Then
    '                '    Query = "UPDATE loc_pos_inventory SET stock_secondary = " & Secondary & " , stock_no_of_servings = " & ServingValue & " , stock_primary = " & TotalPrimary & ", date_modified = '" & FullDate24HR() & "' WHERE server_inventory_id = " & .Rows(i).Cells(1).Value
    '                'Else
    '                '    Query = "UPDATE loc_pos_inventory SET stock_secondary = " & Secondary & " , stock_no_of_servings = " & ServingValue & " , stock_primary = " & TotalPrimary & ", date_modified = '" & FullDate24HR() & "' WHERE inventory_id = " & .Rows(i).Cells(1).Value
    '                'End If
    '                '' Console.WriteLine(Query)
    '                'SqlCommand = New MySqlCommand(Query, UpdateInventoryCon)
    '                'SqlCommand.ExecuteNonQuery()
    '            Next
    '            UpdateInventoryCon.Close()
    '        End With
    '    Catch ex As Exception
    '        MsgBox(ex.ToString)
    '        SendErrorReport(ex.ToString)
    '    End Try
    'End Sub
    Dim ThreadlistMIX As List(Of Thread) = New List(Of Thread)
    Dim ThreadMix As Thread
    Private Sub BackgroundWorker3_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorkerInventory.DoWork
        Try
            ThreadMix = New Thread(AddressOf Mix)
            ThreadMix.Start()
            ThreadlistMIX.Add(ThreadMix)
            For Each t In ThreadlistMIX
                t.Join()
            Next
            ThreadMix = New Thread(Sub() UpdateInventory(False))
            ThreadMix.Start()
            ThreadlistMIX.Add(ThreadMix)
            For Each t In ThreadlistMIX
                t.Join()
            Next
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "POS: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub

    Private Sub BackgroundWorker3_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorkerInventory.RunWorkerCompleted
        MessageBox.Show("Ingredient Mixed", "Mix Products", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Panel3.Enabled = True
        ButtonPayMent.Enabled = False
        ButtonWaffleUpgrade.Enabled = True
        ButtonPayMent.Text = "Checkout"
        ButtonTransactionMode.Text = "Transaction Type"
        DataGridViewOrders.Rows.Clear()
        DataGridViewInv.Rows.Clear()
        DISABLESERVEROTHERSPRODUCT = False
    End Sub
    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Try

            'ButtonCDISC.PerformClick()
            If DataGridViewOrders.Rows.Count > 0 Then
                Dim datas = DataGridViewOrders.SelectedRows(0).Cells(0).Value.ToString()
                For x As Integer = DataGridViewInv.Rows.Count - 1 To 0 Step -1
                    If DataGridViewInv.Rows(x).Cells("Column10").Value = datas Then
                        DataGridViewInv.Rows.Remove(DataGridViewInv.Rows(x))
                    End If
                Next
                datas = ""
                Deleteitem = True
                Dim dr As DataGridViewRow
                For Each dr In DataGridViewOrders.SelectedRows
                    Dim sum As String = DataGridViewOrders.SelectedRows(0).Cells(3).Value.ToString
                    DataGridViewOrders.Rows.Remove(dr)
                Next

            Else
                TextBoxQTY.Text = 0
                MessageBox.Show("Add item first", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
            If DataGridViewOrders.Rows.Count > 0 Then
                ButtonPayMent.Enabled = True
                ButtonPendingOrders.Enabled = False
                Buttonholdoder.Enabled = True
            Else
                HASOTHERSLOCALPRODUCT = False
                HASOTHERSSERVERPRODUCT = False
                DISABLESERVEROTHERSPRODUCT = False
                Panel3.Enabled = True

                ButtonPayMent.Text = "Checkout"
                ButtonTransactionMode.Text = "Transaction Type"
                ButtonClickCount = 0
                ButtonPayMent.Enabled = False
                Buttonholdoder.Enabled = False
                ButtonPendingOrders.Enabled = True
            End If

            PromoDefault()
            DiscountDefault()
            Compute()


        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "POS: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub Label76_TextChanged(sender As Object, e As EventArgs) Handles Label76.TextChanged
        Try
            If DataGridViewOrders.RowCount > 0 Then
                ButtonApplyCoupon.Enabled = True
                ButtonApplyDiscounts.Enabled = True
            Else
                ButtonApplyCoupon.Enabled = False
                ButtonApplyDiscounts.Enabled = False
            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "POS: " & ex.ToString, "Critical")

            MsgBox(ex.ToString)
        End Try
    End Sub
    Private Sub POS_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Expenses.Dispose()
        Couponisavailable = False
    End Sub
    Public IncrementInterval As Integer = 0
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Try
            Label11.Text = Date.Now.ToString("hh:mm:ss tt").ToUpper
            If Not BackgroundWorkerCheckInternet.IsBusy Then
                BackgroundWorkerCheckInternet.WorkerReportsProgress = True
                BackgroundWorkerCheckInternet.WorkerSupportsCancellation = True
                BackgroundWorkerCheckInternet.RunWorkerAsync()
            End If

            If My.Settings.S_Auto_Sync Then
                IncrementInterval += 1
                If IncrementInterval = My.Settings.S_Sync_Interval Then
                    If Not BackgroundWorkerSyncData.IsBusy Then
                        BackgroundWorkerSyncData.WorkerReportsProgress = True
                        BackgroundWorkerSyncData.WorkerSupportsCancellation = True
                        BackgroundWorkerSyncData.RunWorkerAsync()
                    End If
                End If
            End If


        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "POS: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub

    Public Sub PromoDefault()
        Try

            If SeniorGCDiscount Then
                TextBoxDISCOUNT.Text = NUMBERFORMAT(Val(TextBoxDISCOUNT.Text) - PromoTotal)
                TextBoxGRANDTOTAL.Text = NUMBERFORMAT(Val(TextBoxGRANDTOTAL.Text) + PromoTotal)
                SeniorGCDiscount = False
            Else
                If Not DiscAppleid Then
                    If Not PromoApplied Then
                        Compute()
                    End If
                End If
            End If

            TOTALDISCOUNT = 0
            PromoApplied = False
            PromoDesc = ""
            PromoTotal = 0
            PromoGCValue = 0
            PromoName = ""
            PromoType = "N/A"

        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub

    Public Sub DiscountDefault()
        Try
            If S_ZeroRated = "0" Then
                Dim PRTAX As Double = 1 + Val(S_Tax)
                VATABLESALES = NUMBERFORMAT(Val(Label76.Text) / PRTAX)
                VAT12PERCENT = NUMBERFORMAT(Val(Label76.Text) - VATABLESALES)
                VATEXEMPTSALES = 0
                LESSVAT = 0
            Else
                ZERORATEDSALES = NUMBERFORMAT(Val(Label76.Text))
                ZERORATEDNETSALES = NUMBERFORMAT(Val(Label76.Text))
            End If

            SeniorGCDiscount = False
            DiscAppleid = False
            SeniorDetailsID = ""
            SeniorDetailsName = ""
            SeniorPhoneNumber = ""
            GETNOTDISCOUNTEDAMOUNT = 0
            DiscountName = ""
            DiscountType = ""
            TOTALDISCOUNT = 0

            DISCGUESTCOUNT = 0
            DISCIDCOUNT = 0

            Dim GetDicountValue As Double = 0

            For i As Integer = 0 To DataGridViewOrders.Rows.Count - 1 Step +1
                GetDicountValue += DataGridViewOrders.Rows(i).Cells(15).Value
                DataGridViewOrders.Rows(i).Cells(15).Value = 0
                DataGridViewOrders.Rows(i).Cells(16).Value = 0
                GetDicountValue += DataGridViewOrders.Rows(i).Cells(17).Value
                DataGridViewOrders.Rows(i).Cells(17).Value = 0
                DataGridViewOrders.Rows(i).Cells(18).Value = 0
                GetDicountValue += DataGridViewOrders.Rows(i).Cells(19).Value
                DataGridViewOrders.Rows(i).Cells(19).Value = 0
                DataGridViewOrders.Rows(i).Cells(20).Value = 0
                GetDicountValue += DataGridViewOrders.Rows(i).Cells(21).Value
                DataGridViewOrders.Rows(i).Cells(21).Value = 0
                DataGridViewOrders.Rows(i).Cells(22).Value = 0
            Next

            TextBoxDISCOUNT.Text = NUMBERFORMAT(Val(TextBoxDISCOUNT.Text) - GetDicountValue)
            'MsgBox(PromoApplied)
            Compute(True, GetDicountValue, True)

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub Button1_Click_2(sender As Object, e As EventArgs) Handles ButtonTransactionMode.Click
        If ButtonTransactionMode.Text = "Transaction Type" Then
            Enabled = False
            TransactionType.Show()
        Else
            ButtonCancel.PerformClick()
            ButtonPayMent.Text = "Checkout"
            ButtonTransactionMode.Text = "Transaction Type"
            Panel3.Enabled = True
            ButtonWaffleUpgrade.Enabled = True
        End If
    End Sub
    Private Sub Button1_Click_3(sender As Object, e As EventArgs) Handles Button1.Click
        Enabled = False
        TakeOut.Show()
    End Sub
#Region "Button Functions"
    Private Sub POS_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyData = Keys.Alt + Keys.F4 Then
            e.Handled = True
        End If
        If e.KeyCode = Keys.F9 Then
            ButtonPayMent.PerformClick()
        ElseIf e.KeyCode = Keys.Enter Then
            ButtonEnter.PerformClick()
        ElseIf e.KeyCode = Keys.F10 Then
            ButtonTransactionMode.PerformClick()
        ElseIf e.KeyCode = Keys.F11 Then
            Buttonholdoder.PerformClick()
        ElseIf e.KeyCode = Keys.F12 Then
            ButtonPendingOrders.PerformClick()
        ElseIf e.KeyCode = Keys.Delete Then
            ButtonCancel.PerformClick()
            '=================================
        End If
        If payment = False Then
            If e.KeyCode = Keys.NumPad0 Then
                ButtonNo0.PerformClick()
            ElseIf e.KeyCode = Keys.NumPad1 Then
                ButtonNo1.PerformClick()
            ElseIf e.KeyCode = Keys.NumPad2 Then
                ButtonNo2.PerformClick()
            ElseIf e.KeyCode = Keys.NumPad3 Then
                ButtonNo3.PerformClick()
            ElseIf e.KeyCode = Keys.NumPad4 Then
                ButtonNo4.PerformClick()
            ElseIf e.KeyCode = Keys.NumPad5 Then
                ButtonNo5.PerformClick()
            ElseIf e.KeyCode = Keys.NumPad6 Then
                ButtonNo6.PerformClick()
            ElseIf e.KeyCode = Keys.NumPad7 Then
                ButtonNo7.PerformClick()
            ElseIf e.KeyCode = Keys.NumPad8 Then
                ButtonNo8.PerformClick()
            ElseIf e.KeyCode = Keys.NumPad9 Then
                ButtonNo9.PerformClick()
            ElseIf e.KeyCode = Keys.Back Then
                ButtonClear.PerformClick()
            End If
        End If
    End Sub
    Private Sub ButtonNo9_Click(sender As Object, e As EventArgs) Handles ButtonNo9.Click
        If payment = False Then
            If TextBoxQTY.Text.Length > 6 Then
            Else
                buttonpressedenter(btntext:=ButtonNo9.Text)
            End If
        Else
            If TextBoxQTY.Text.Length > 6 Then
            Else
                buttonpressedenterpayment(btntext:=ButtonNo9.Text)
            End If
        End If
    End Sub
    Private Sub ButtonNo8_Click(sender As Object, e As EventArgs) Handles ButtonNo8.Click
        If payment = False Then
            If TextBoxQTY.Text.Length > 6 Then
            Else
                buttonpressedenter(btntext:=ButtonNo8.Text)
            End If
        Else
            If TextBoxQTY.Text.Length > 6 Then
            Else
                buttonpressedenterpayment(btntext:=ButtonNo8.Text)
            End If
        End If
    End Sub
    Private Sub ButtonNo7_Click(sender As Object, e As EventArgs) Handles ButtonNo7.Click
        If payment = False Then
            If TextBoxQTY.Text.Length > 6 Then
            Else
                buttonpressedenter(btntext:=ButtonNo7.Text)
            End If
        Else
            If TextBoxQTY.Text.Length > 6 Then
            Else
                buttonpressedenterpayment(btntext:=ButtonNo7.Text)
            End If
        End If
    End Sub
    Private Sub ButtonNo6_Click(sender As Object, e As EventArgs) Handles ButtonNo6.Click
        If payment = False Then
            If TextBoxQTY.Text.Length > 6 Then
            Else
                buttonpressedenter(btntext:=ButtonNo6.Text)
            End If
        Else
            If TextBoxQTY.Text.Length > 6 Then
            Else
                buttonpressedenterpayment(btntext:=ButtonNo6.Text)
            End If
        End If
    End Sub
    Private Sub ButtonNo5_Click(sender As Object, e As EventArgs) Handles ButtonNo5.Click
        If payment = False Then
            If TextBoxQTY.Text.Length > 6 Then
            Else
                buttonpressedenter(btntext:=ButtonNo5.Text)
            End If
        Else
            If TextBoxQTY.Text.Length > 6 Then
            Else
                buttonpressedenterpayment(btntext:=ButtonNo5.Text)
            End If
        End If
    End Sub
    Private Sub ButtonNo4_Click(sender As Object, e As EventArgs) Handles ButtonNo4.Click
        If payment = False Then
            If TextBoxQTY.Text.Length > 6 Then
            Else
                buttonpressedenter(btntext:=ButtonNo4.Text)
            End If
        Else
            If TextBoxQTY.Text.Length > 6 Then
            Else
                buttonpressedenterpayment(btntext:=ButtonNo4.Text)
            End If
        End If
    End Sub
    Private Sub ButtonNo3_Click(sender As Object, e As EventArgs) Handles ButtonNo3.Click
        If payment = False Then
            If TextBoxQTY.Text.Length > 6 Then
            Else
                buttonpressedenter(btntext:=ButtonNo3.Text)
            End If
        Else
            If TextBoxQTY.Text.Length > 6 Then
            Else
                buttonpressedenterpayment(btntext:=ButtonNo3.Text)
            End If
        End If
    End Sub
    Private Sub ButtonNo2_Click(sender As Object, e As EventArgs) Handles ButtonNo2.Click
        If payment = False Then
            If TextBoxQTY.Text.Length > 6 Then
            Else
                buttonpressedenter(btntext:=ButtonNo2.Text)
            End If
        Else
            If TextBoxQTY.Text.Length > 6 Then
            Else
                buttonpressedenterpayment(btntext:=ButtonNo2.Text)
            End If
        End If
    End Sub
    Private Sub ButtonNo1_Click(sender As Object, e As EventArgs) Handles ButtonNo1.Click
        If payment = False Then
            If TextBoxQTY.Text.Length > 6 Then
            Else
                buttonpressedenter(btntext:=ButtonNo1.Text)
            End If
        Else
            If TextBoxQTY.Text.Length > 6 Then
            Else
                buttonpressedenterpayment(btntext:=ButtonNo1.Text)
            End If
        End If
    End Sub
    Private Sub ButtonNo0_Click(sender As Object, e As EventArgs) Handles ButtonNo0.Click
        If payment = False Then
            If TextBoxQTY.Text.Length > 6 Then
            Else
                buttonpressedenter(btntext:=ButtonNo0.Text)
            End If
        Else
            If TextBoxQTY.Text.Length > 6 Then
            Else
                buttonpressedenterpayment(btntext:=ButtonNo0.Text)
            End If
        End If
    End Sub
    Private Sub ButtonNo00_Click(sender As Object, e As EventArgs) Handles ButtonNo00.Click
        If payment = False Then
            If TextBoxQTY.Text.Length > 5 Then
            Else
                buttonpressedenter(btntext:=ButtonNo00.Text)
            End If
        Else
            If TextBoxQTY.Text.Length > 5 Then
            Else
                buttonpressedenterpayment(btntext:=ButtonNo00.Text)
            End If
        End If
    End Sub
    Private Sub Buttondot_Click(sender As Object, e As EventArgs) Handles Buttondot.Click
        If payment = False Then
            If Not TextBoxQTY.Text.Contains(".") Then
                TextBoxQTY.Text += "."
            End If
        End If
    End Sub
    Private Sub ButtonClear_Click(sender As Object, e As EventArgs) Handles ButtonClear.Click
        If payment = False Then
            TextBoxQTY.Text = 0
        End If
    End Sub
    Private Sub TextBoxQTY_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBoxQTY.KeyPress
        Try
            If InStr(DisallowedCharacters, e.KeyChar) > 0 Then
                e.Handled = True
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
#End Region
#Region "POS Coupon Application/ Print/ Transaction"

    Dim THREADLIST As List(Of Thread) = New List(Of Thread)
    Dim THREADLISTUPDATE As List(Of Thread) = New List(Of Thread)
    Dim TIMETOINSERT As String
    Dim ACTIVE As Integer = 1

    Public DiscountsDatatable As DataTable
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles ButtonApplyDiscounts.Click
        Try


            TOTALDISCOUNT = 0
            GROSSSALE = 0
            VATEXEMPTSALES = 0
            LESSVAT = 0
            TOTALDISCOUNTEDAMOUNT = 0
            TOTALAMOUNTDUE = 0
            VATABLESALES = 0
            VAT12PERCENT = 0
            ZERORATEDSALES = 0
            ZERORATEDNETSALES = 0

            SeniorDetailsID = ""
            SeniorDetailsName = ""
            SeniorPhoneNumber = ""

            DiscountsDatatable = New DataTable
            DiscountsDatatable.Columns.Add("name")
            DiscountsDatatable.Columns.Add("price")
            DiscountsDatatable.Columns.Add("total")
            DiscountsDatatable.Columns.Add("product_id")

            Dim Tax = 1 + Val(S_ZeroRated_Tax)
            With DataGridViewOrders
                For i As Integer = 0 To DataGridViewOrders.Rows.Count - 1 Step +1
                    Dim LoopQty As Integer = .Rows(i).Cells(1).Value
                    Dim StopUpgradePrice As Integer = 0
                    Dim LoopUpgrade As Integer = 0
                    For a As Integer = 0 To LoopQty - 1 Step +1
                        Dim Prod As DataRow = DiscountsDatatable.NewRow
                        Prod("name") = .Rows(i).Cells(0).Value
                        Prod("price") = .Rows(i).Cells(2).Value

                        LoopUpgrade = .Rows(i).Cells(11).Value

                        If StopUpgradePrice = LoopUpgrade Then

                            If S_ZeroRated = "0" Then
                                Dim TotalPrice As Double = .Rows(i).Cells(2).Value
                                Prod("total") = NUMBERFORMAT(TotalPrice)
                            Else

                                Dim TotalPrice As Double = .Rows(i).Cells(2).Value / Tax
                                Prod("total") = NUMBERFORMAT(TotalPrice)
                            End If

                        Else
                            If S_ZeroRated = "0" Then
                                Dim TotalPrice As Double = .Rows(i).Cells(2).Value + Double.Parse(S_Upgrade_Price)
                                Prod("total") = NUMBERFORMAT(TotalPrice)
                            Else
                                Dim TotalPrice As Double = .Rows(i).Cells(2).Value / Tax
                                TotalPrice += +Double.Parse(S_Upgrade_Price)
                                Prod("total") = NUMBERFORMAT(TotalPrice)
                            End If

                            StopUpgradePrice += 1
                        End If
                        Prod("product_id") = .Rows(i).Cells(5).Value
                        DiscountsDatatable.Rows.Add(Prod)
                    Next
                Next
            End With

            CouponCode.APPLYPROMO = False
            CouponCode.Show()
            CouponCode.ButtonSubmit.Enabled = True
            Enabled = False
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "POS: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles ButtonApplyCoupon.Click
        Try
            MessageBox.Show("Apply coupon after taking all customer orders", "NOTICE", MessageBoxButtons.OK, MessageBoxIcon.Information)
            'If SeniorGCDiscount = False Then
            '    ButtonRemovePromo.PerformClick()
            'End If
            Enabled = False
            CouponCode.APPLYPROMO = True
            CouponCode.Show()
            CouponCode.ButtonSubmit.Enabled = True
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub
#Region "Transaction Process"
    Private Sub InsertCustInfo()
        Try
            Dim ConnectionLocal As MySqlConnection = LocalhostConn()
            Dim cmd As MySqlCommand
            Dim sql = "INSERT INTO loc_customer_info (`transaction_number`, `cust_name`, `cust_tin`,`cust_address`, `cust_business`, `crew_id`, `store_id`, `created_at`, `active`, `synced`)
                       VALUES (@1,@2,@3,@4,@5,@6,@7,@8,@9,@10)"
            cmd = New MySqlCommand(sql, ConnectionLocal)
            cmd.Parameters.Add("@1", MySqlDbType.Text).Value = S_TRANSACTION_NUMBER
            cmd.Parameters.Add("@2", MySqlDbType.Text).Value = CUST_INFO_NAME
            cmd.Parameters.Add("@3", MySqlDbType.Text).Value = CUST_INFO_TIN
            cmd.Parameters.Add("@4", MySqlDbType.Text).Value = CUST_INFO_ADDRESS
            cmd.Parameters.Add("@5", MySqlDbType.Text).Value = CUST_INFO_BUSINESS
            cmd.Parameters.Add("@6", MySqlDbType.Text).Value = ClientCrewID
            cmd.Parameters.Add("@7", MySqlDbType.Text).Value = ClientStoreID
            cmd.Parameters.Add("@8", MySqlDbType.Text).Value = FullDate24HR()
            cmd.Parameters.Add("@9", MySqlDbType.Text).Value = "1"
            cmd.Parameters.Add("@10", MySqlDbType.Text).Value = "Unsynced"
            cmd.ExecuteNonQuery()

        Catch ex As Exception
            SendErrorReport(ex.ToString)
            AuditTrail.LogToAuditTral("System", "POS/Customer Info : " & ex.ToString, "Critical")
        End Try
    End Sub
    Private Sub InsertFMStock()
        Try
            Dim ConnectionLocal As MySqlConnection = LocalhostConn()
            Dim cmd As MySqlCommand
            Dim sql = "INSERT INTO loc_fm_stock (`formula_id`, `stock_primary`, `stock_secondary`,`crew_id`, `store_id`, `guid`, `created_at`, `status`)
                       VALUES (@1,@2,@3,@4,@5,@6,@7,@8)"
            For i As Integer = 0 To DataGridViewInv.Rows.Count - 1 Step +1
                cmd = New MySqlCommand(sql, ConnectionLocal)
                cmd.Parameters.Add("@1", MySqlDbType.VarChar).Value = DataGridViewInv.Rows(i).Cells(1).Value
                cmd.Parameters.Add("@2", MySqlDbType.Decimal).Value = DataGridViewInv.Rows(i).Cells(2).Value
                cmd.Parameters.Add("@3", MySqlDbType.Decimal).Value = DataGridViewInv.Rows(i).Cells(0).Value
                cmd.Parameters.Add("@4", MySqlDbType.VarChar).Value = ClientCrewID
                cmd.Parameters.Add("@5", MySqlDbType.VarChar).Value = ClientStoreID
                cmd.Parameters.Add("@6", MySqlDbType.VarChar).Value = ClientGuid
                cmd.Parameters.Add("@7", MySqlDbType.Text).Value = FullDate24HR()
                cmd.Parameters.Add("@8", MySqlDbType.Int64).Value = 1
                cmd.ExecuteNonQuery()
            Next
        Catch ex As Exception
            SendErrorReport(ex.ToString)
            AuditTrail.LogToAuditTral("System", "POS/Fm Stock : " & ex.ToString, "Critical")
        End Try
    End Sub
    Private Sub InsertDailyTransaction()
        Try

            Dim table As String = "loc_daily_transaction"
            Dim fields As String = " (`transaction_number`, `amounttendered`, `totaldiscount`, `change`, `amountdue`, `vatablesales`, `vatexemptsales`, `zeroratedsales`
                     , `lessvat`, `si_number`, `crew_id`, `guid`, `active`, `store_id`, `created_at`, `transaction_type`, `shift`, `zreading`, `synced`
                     , `discount_type`, `vatpercentage`, `grosssales`, `totaldiscountedamount`) "
            Dim NetSales As Double = 0
            If S_ZeroRated = "0" Then
                NetSales = SUPERAMOUNTDUE
            Else
                NetSales = ZERORATEDNETSALES
            End If
            Dim DiscountTypeTOSave As String = ""
            If SeniorGCDiscount Then
                DiscountTypeTOSave = "Disc + GC"
            Else
                If PromoApplied Then
                    If PromoName = "" Then
                        PromoName = "N/A"
                    End If
                    DiscountTypeTOSave = PromoName
                Else
                    DiscountTypeTOSave = "N/A"
                End If

                If DiscAppleid Then
                    If DiscountName = "" Then
                        DiscountName = "N/A"
                    End If
                    DiscountTypeTOSave = DiscountName
                Else
                    DiscountTypeTOSave = "N/A"
                End If

            End If

            Dim value As String = "('" & S_TRANSACTION_NUMBER & "'," & TEXTBOXMONEYVALUE & "," & TOTALDISCOUNT & "," & TEXTBOXCHANGEVALUE & "," & NetSales & "," & VATABLESALES & "
                     ," & VATEXEMPTSALES & "," & ZERORATEDSALES & "," & LESSVAT & "," & S_SI_NUMBER & ",'" & ClientCrewID & "','" & ClientGuid & "','" & ACTIVE & "','" & ClientStoreID & "'
                     ,'" & INSERTTHISDATE & "','" & TRANSACTIONMODE & "','" & Shift & "','" & S_Zreading & "','Unsynced','" & DiscountTypeTOSave & "'," & VAT12PERCENT & "," & GROSSSALE & "," & TOTALDISCOUNTEDAMOUNT & ")"
            GLOBAL_INSERT_FUNCTION(table, fields, value)
        Catch ex As Exception
            SendErrorReport(ex.ToString)
            AuditTrail.LogToAuditTral("System", "POS/Daily Transaction : " & ex.ToString, "Critical")
        End Try
    End Sub
    Private Sub InsertDailyDetails()
        Try
            Dim ConnectionLocal As MySqlConnection = LocalhostConn()
            Dim cmd As MySqlCommand
            With DataGridViewOrders
                Dim sql = "INSERT INTO loc_daily_transaction_details (`product_id`,`product_sku`,`product_name`,`quantity`,`price`,`total`,`crew_id`,`transaction_number`,`active`,`created_at`,`guid`,`store_id`,`synced`,`total_cost_of_goods`,`product_category`,`zreading`,`transaction_type`,`upgraded`,`addontype`, `seniordisc`, `seniorqty`, `pwddisc`, `pwdqty`, `athletedisc`, `athleteqty`, `spdisc`, `spqty`) 
                       VALUES (@1,@2,@3,@4,@5,@6,@7,@8,@9,@10,@11,@12,@13,@14,@15,@16,@17,@18,@19,@20,@21,@22,@23,@24,@25,@26,@27)"
                For i As Integer = 0 To .Rows.Count - 1 Step +1
                    Dim totalcostofgoods As Decimal
                    For a As Integer = 0 To DataGridViewInv.Rows.Count - 1 Step +1
                        If DataGridViewInv.Rows(a).Cells(4).Value = .Rows(i).Cells(0).Value Then
                            totalcostofgoods += DataGridViewInv.Rows(a).Cells(6).Value
                        End If
                    Next
                    cmd = New MySqlCommand(sql, ConnectionLocal)
                    cmd.Parameters.Add("@1", MySqlDbType.Int64).Value = .Rows(i).Cells(5).Value
                    cmd.Parameters.Add("@2", MySqlDbType.VarChar).Value = .Rows(i).Cells(6).Value
                    cmd.Parameters.Add("@3", MySqlDbType.VarChar).Value = .Rows(i).Cells(0).Value
                    cmd.Parameters.Add("@4", MySqlDbType.Int64).Value = .Rows(i).Cells(1).Value
                    cmd.Parameters.Add("@5", MySqlDbType.Decimal).Value = .Rows(i).Cells(2).Value
                    cmd.Parameters.Add("@6", MySqlDbType.Decimal).Value = .Rows(i).Cells(3).Value
                    cmd.Parameters.Add("@7", MySqlDbType.VarChar).Value = ClientCrewID
                    cmd.Parameters.Add("@8", MySqlDbType.VarChar).Value = S_TRANSACTION_NUMBER
                    cmd.Parameters.Add("@9", MySqlDbType.Int64).Value = ACTIVE
                    cmd.Parameters.Add("@10", MySqlDbType.Text).Value = INSERTTHISDATE
                    cmd.Parameters.Add("@11", MySqlDbType.VarChar).Value = ClientGuid
                    cmd.Parameters.Add("@12", MySqlDbType.VarChar).Value = ClientStoreID
                    cmd.Parameters.Add("@13", MySqlDbType.VarChar).Value = "Unsynced"
                    cmd.Parameters.Add("@14", MySqlDbType.Decimal).Value = totalcostofgoods
                    cmd.Parameters.Add("@15", MySqlDbType.VarChar).Value = .Rows(i).Cells(7).Value
                    cmd.Parameters.Add("@16", MySqlDbType.Text).Value = S_Zreading
                    cmd.Parameters.Add("@17", MySqlDbType.Text).Value = TRANSACTIONMODE
                    cmd.Parameters.Add("@18", MySqlDbType.Int64).Value = .Rows(i).Cells(11).Value
                    cmd.Parameters.Add("@19", MySqlDbType.Text).Value = .Rows(i).Cells(13).Value
                    cmd.Parameters.Add("@20", MySqlDbType.Double).Value = If(.Rows(i).Cells(15).Value > 0, .Rows(i).Cells(15).Value, 0)
                    cmd.Parameters.Add("@21", MySqlDbType.Double).Value = If(.Rows(i).Cells(16).Value > 0, .Rows(i).Cells(16).Value, 0)
                    cmd.Parameters.Add("@22", MySqlDbType.Double).Value = If(.Rows(i).Cells(17).Value > 0, .Rows(i).Cells(17).Value, 0)
                    cmd.Parameters.Add("@23", MySqlDbType.Double).Value = If(.Rows(i).Cells(18).Value > 0, .Rows(i).Cells(18).Value, 0)
                    cmd.Parameters.Add("@24", MySqlDbType.Double).Value = If(.Rows(i).Cells(19).Value > 0, .Rows(i).Cells(19).Value, 0)
                    cmd.Parameters.Add("@25", MySqlDbType.Double).Value = If(.Rows(i).Cells(20).Value > 0, .Rows(i).Cells(20).Value, 0)
                    cmd.Parameters.Add("@26", MySqlDbType.Double).Value = If(.Rows(i).Cells(21).Value > 0, .Rows(i).Cells(21).Value, 0)
                    cmd.Parameters.Add("@27", MySqlDbType.Double).Value = If(.Rows(i).Cells(22).Value > 0, .Rows(i).Cells(22).Value, 0)
                    cmd.ExecuteNonQuery()
                    totalcostofgoods = 0
                Next
            End With

        Catch ex As Exception
            SendErrorReport(ex.ToString)
            AuditTrail.LogToAuditTral("System", "POS/Daily Transaction Details: " & ex.ToString, "Critical")
        End Try
    End Sub
    Private Sub InsertModeofTransaction()
        Try

            Dim table As String = "loc_transaction_mode_details"
            Dim fields As String = "(`transaction_type`, `transaction_number`, `fullname`, `reference`, `status`, `synced`, `store_id`, `guid`, `created_at`)"
            Dim value As String = "( '" & TRANSACTIONMODE & "'
                            ,'" & S_TRANSACTION_NUMBER & "'
                            , '" & TEXTBOXFULLNAMEVALUE & "'
                            , '" & TEXTBOXREFERENCEVALUE & "'
                            , " & 1 & "
                            , 'Unsynced'
                            , '" & ClientStoreID & "'
                            , '" & ClientGuid & "'
                            , '" & FullDate24HR() & "')"
            GLOBAL_INSERT_FUNCTION(table:=table, fields:=fields, values:=value)
            ButtonClickCount = 0
        Catch ex As Exception
            SendErrorReport(ex.ToString)
            AuditTrail.LogToAuditTral("System", "POS/Mode of Transaction: " & ex.ToString, "Critical")
        End Try
    End Sub
    Private Sub InsertDiscountData()
        Try
            Dim ConnectionLocal As MySqlConnection = LocalhostConn()
            Dim Query As String = ""
            With DataGridViewOrders
                For i As Integer = 0 To .Rows.Count - 1 Step +1

                    If .Rows(i).Cells(15).Value > 0 Then
                        Dim DiscTotal As Double = NUMBERFORMAT(Math.Round(.Rows(i).Cells(15).Value, 2, MidpointRounding.AwayFromZero))
                        Query = "INSERT INTO loc_coupon_data (`transaction_number`, `coupon_name`, `coupon_type`, `coupon_desc`, `coupon_line`, `coupon_total`, `zreading`, `status`, `synced`) VALUES (@1, @2, @3, @4, @5, @6, @7, @8, @9)"
                        cmd = New MySqlCommand(Query, ConnectionLocal)
                        cmd.Parameters.Clear()
                        cmd.Parameters.Add("@1", MySqlDbType.Text).Value = S_TRANSACTION_NUMBER
                        cmd.Parameters.Add("@2", MySqlDbType.Text).Value = "Senior Discount 20%"
                        cmd.Parameters.Add("@3", MySqlDbType.Text).Value = "Percentage(w/o vat)"
                        cmd.Parameters.Add("@4", MySqlDbType.Text).Value = "N/A"
                        cmd.Parameters.Add("@5", MySqlDbType.Text).Value = PromoLine
                        cmd.Parameters.Add("@6", MySqlDbType.Text).Value = DiscTotal
                        cmd.Parameters.Add("@7", MySqlDbType.Text).Value = S_Zreading
                        cmd.Parameters.Add("@8", MySqlDbType.Text).Value = 1
                        cmd.Parameters.Add("@9", MySqlDbType.Text).Value = "Unsynced"
                        cmd.ExecuteNonQuery()
                    End If
                    If .Rows(i).Cells(17).Value > 0 Then
                        Dim DiscTotal As Double = NUMBERFORMAT(Math.Round(.Rows(i).Cells(17).Value, 2, MidpointRounding.AwayFromZero))
                        Query = "INSERT INTO loc_coupon_data (`transaction_number`, `coupon_name`, `coupon_type`, `coupon_desc`, `coupon_line`, `coupon_total`, `zreading`, `status`, `synced`) VALUES (@1, @2, @3, @4, @5, @6, @7, @8, @9)"
                        cmd = New MySqlCommand(Query, ConnectionLocal)
                        cmd.Parameters.Clear()
                        cmd.Parameters.Add("@1", MySqlDbType.Text).Value = S_TRANSACTION_NUMBER
                        cmd.Parameters.Add("@2", MySqlDbType.Text).Value = "PWD Discount 20%"
                        cmd.Parameters.Add("@3", MySqlDbType.Text).Value = "Percentage(w/o vat)"
                        cmd.Parameters.Add("@4", MySqlDbType.Text).Value = "N/A"
                        cmd.Parameters.Add("@5", MySqlDbType.Text).Value = PromoLine
                        cmd.Parameters.Add("@6", MySqlDbType.Text).Value = DiscTotal
                        cmd.Parameters.Add("@7", MySqlDbType.Text).Value = S_Zreading
                        cmd.Parameters.Add("@8", MySqlDbType.Text).Value = 1
                        cmd.Parameters.Add("@9", MySqlDbType.Text).Value = "Unsynced"
                        cmd.ExecuteNonQuery()
                    End If
                    If .Rows(i).Cells(19).Value > 0 Then
                        Dim DiscTotal As Double = NUMBERFORMAT(Math.Round(.Rows(i).Cells(19).Value, 2, MidpointRounding.AwayFromZero))
                        Query = "INSERT INTO loc_coupon_data (`transaction_number`, `coupon_name`, `coupon_type`, `coupon_desc`, `coupon_line`, `coupon_total`, `zreading`, `status`, `synced`) VALUES (@1, @2, @3, @4, @5, @6, @7, @8, @9)"
                        cmd = New MySqlCommand(Query, ConnectionLocal)
                        cmd.Parameters.Clear()
                        cmd.Parameters.Add("@1", MySqlDbType.Text).Value = S_TRANSACTION_NUMBER
                        cmd.Parameters.Add("@2", MySqlDbType.Text).Value = "Sports Discount 20%"
                        cmd.Parameters.Add("@3", MySqlDbType.Text).Value = "Percentage(w/o vat)"
                        cmd.Parameters.Add("@4", MySqlDbType.Text).Value = "N/A"
                        cmd.Parameters.Add("@5", MySqlDbType.Text).Value = PromoLine
                        cmd.Parameters.Add("@6", MySqlDbType.Text).Value = DiscTotal
                        cmd.Parameters.Add("@7", MySqlDbType.Text).Value = S_Zreading
                        cmd.Parameters.Add("@8", MySqlDbType.Text).Value = 1
                        cmd.Parameters.Add("@9", MySqlDbType.Text).Value = "Unsynced"
                        cmd.ExecuteNonQuery()
                    End If
                    If .Rows(i).Cells(21).Value > 0 Then
                        Dim DiscTotal As Double = NUMBERFORMAT(Math.Round(.Rows(i).Cells(21).Value, 2, MidpointRounding.AwayFromZero))
                        Query = "INSERT INTO loc_coupon_data (`transaction_number`, `coupon_name`, `coupon_type`, `coupon_desc`, `coupon_line`, `coupon_total`, `zreading`, `status`, `synced`) VALUES (@1, @2, @3, @4, @5, @6, @7, @8, @9)"
                        cmd = New MySqlCommand(Query, ConnectionLocal)
                        cmd.Parameters.Clear()
                        cmd.Parameters.Add("@1", MySqlDbType.Text).Value = S_TRANSACTION_NUMBER
                        cmd.Parameters.Add("@2", MySqlDbType.Text).Value = "Single Parent Discount 20%"
                        cmd.Parameters.Add("@3", MySqlDbType.Text).Value = "Percentage(w/o vat)"
                        cmd.Parameters.Add("@4", MySqlDbType.Text).Value = "N/A"
                        cmd.Parameters.Add("@5", MySqlDbType.Text).Value = PromoLine
                        cmd.Parameters.Add("@6", MySqlDbType.Text).Value = DiscTotal
                        cmd.Parameters.Add("@7", MySqlDbType.Text).Value = S_Zreading
                        cmd.Parameters.Add("@8", MySqlDbType.Text).Value = 1
                        cmd.Parameters.Add("@9", MySqlDbType.Text).Value = "Unsynced"
                        cmd.ExecuteNonQuery()
                    End If
                Next



            End With
        Catch ex As Exception
            SendErrorReport(ex.ToString)
            AuditTrail.LogToAuditTral("System", "POS/Discount Data: " & ex.ToString, "Critical")
        End Try
    End Sub
    Private Sub InsertCouponData()
        Try
            'Dim table As String = "loc_coupon_data"
            'Dim fields As String = "(`transaction_number`, `coupon_name`, `coupon_type`, `coupon_desc`, `coupon_line`, `coupon_total`, `gc_value`, `zreading`, `status`, `synced`)"
            'Dim value As String = "( '" & S_TRANSACTION_NUMBER & "'
            '          ,'" & CouponName & "'
            '          , '" & DISCOUNTTYPE & "'
            '          , '" & CouponDesc & "'
            '          , '" & CouponLine & "'
            '          , '" & CouponTotal & "', " & GC_Value & ", '" & S_Zreading & "', '1', 'Unsynced')"
            'GLOBAL_INSERT_FUNCTION(table, fields, value)
            PromoTotal = NUMBERFORMAT(Math.Round(PromoTotal, 2, MidpointRounding.AwayFromZero))
            Dim ConnectionLocal As MySqlConnection = LocalhostConn()
            Dim Query = "INSERT INTO loc_coupon_data (`transaction_number`, `coupon_name`, `coupon_type`, `coupon_desc`, `coupon_line`, `coupon_total`, `gc_value`, `zreading`, `status`, `synced`) VALUES (@1, @2, @3, @4, @5, @6, @7, @8, @9, @10)"
            cmd = New MySqlCommand(Query, ConnectionLocal)
            cmd.Parameters.Clear()
            cmd.Parameters.Add("@1", MySqlDbType.Text).Value = S_TRANSACTION_NUMBER
            cmd.Parameters.Add("@2", MySqlDbType.Text).Value = PromoName
            cmd.Parameters.Add("@3", MySqlDbType.Text).Value = PromoType
            cmd.Parameters.Add("@4", MySqlDbType.Text).Value = PromoDesc
            cmd.Parameters.Add("@5", MySqlDbType.Text).Value = PromoLine
            cmd.Parameters.Add("@6", MySqlDbType.Text).Value = PromoTotal
            cmd.Parameters.Add("@7", MySqlDbType.Text).Value = PromoGCValue
            cmd.Parameters.Add("@8", MySqlDbType.Text).Value = S_Zreading
            cmd.Parameters.Add("@9", MySqlDbType.Text).Value = "1"
            cmd.Parameters.Add("@10", MySqlDbType.Text).Value = "Unsynced"
            cmd.ExecuteNonQuery()

        Catch ex As Exception
            SendErrorReport(ex.ToString)
            AuditTrail.LogToAuditTral("System", "POS/Coupon Data: " & ex.ToString, "Critical")
        End Try
    End Sub
    Private Sub InsertSeniorDetails()
        Try
            Dim table As String = "loc_senior_details"
            Dim fields As String = "(`transaction_number`, `senior_id`, `senior_name`, `active`, `crew_id`, `store_id`, `guid`, `date_created`, `totalguest`, `totalid`,`phone_number`, `synced`)"
            Dim value As String = "( '" & S_TRANSACTION_NUMBER & "'
                      , '" & SeniorDetailsID & "'
                      , '" & SeniorDetailsName & "'
                      , '" & 1 & "'
                      , '" & ClientCrewID & "'
                      , '" & ClientStoreID & "'
                      , '" & ClientGuid & "'
                      , '" & S_Zreading & "'
                      , '" & DISCGUESTCOUNT & "'
                      , '" & DISCIDCOUNT & "'
                      , '" & SeniorPhoneNumber & "'
                      , 'Unsynced')"
            GLOBAL_INSERT_FUNCTION(table, fields, value)
        Catch ex As Exception
            SendErrorReport(ex.ToString)
            AuditTrail.LogToAuditTral("System", "POS/Senior Data: " & ex.ToString, "Critical")
        End Try
    End Sub
#End Region
    Public INSERTTHISDATE
    Dim ThreadOrder As Thread
    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorkerTransactions.DoWork
        Try

            INSERTTHISDATE = S_Zreading & " " & Format(Now(), "HH:mm:ss")
            SUPERAMOUNTDUE = Convert.ToDecimal(Double.Parse(TextBoxGRANDTOTAL.Text))
            If TRANSACTIONMODE = "Complimentary Expenses" Then
                ACTIVE = 3
            End If
            GROSSSALE = NUMBERFORMAT(Double.Parse(Label76.Text))


            If S_ZeroRated = "0" Then
                If Not PromoApplied And Not DiscAppleid Then
                    VATABLESALES = Math.Round(SUPERAMOUNTDUE / Val(1 + S_Tax), 2, MidpointRounding.AwayFromZero)
                    VAT12PERCENT = Math.Round(SUPERAMOUNTDUE - VATABLESALES, 2, MidpointRounding.AwayFromZero)
                End If
            Else
                VATABLESALES = 0
                Dim TotalPrice As Double = 0
                Dim GrandTotal As Double = 0
                With DataGridViewOrders
                    For i As Integer = 0 To .Rows.Count - 1 Step +1
                        TotalPrice = .Rows(i).Cells(1).Value * .Rows(i).Cells(2).Value
                        GrandTotal += TotalPrice
                    Next
                End With
                If PromoApplied Or DiscAppleid Then
                    Dim SubTotal = Convert.ToDecimal(Double.Parse(TextBoxSUBTOTAL.Text))
                    LESSVAT = Math.Round(GrandTotal - SubTotal, 2, MidpointRounding.AwayFromZero)
                Else
                    LESSVAT = Math.Round(GrandTotal - SUPERAMOUNTDUE, 2, MidpointRounding.AwayFromZero)
                    ZERORATEDNETSALES = GROSSSALE
                    ZERORATEDSALES = GROSSSALE
                End If
            End If
            'MsgBox(PromoApplied)
            'MsgBox(DiscAppleid)
            'MsgBox(VATABLESALES)
            'MsgBox(VAT12PERCENT)

            If S_SI_NUMBER = 0 Then
                SiNumberToString = S_SI_NUMBER.ToString(S_SIFormat)
            Else
                SiNumberToString = S_SI_NUMBER.ToString(S_SIFormat)
            End If

            GLOBAL_FUNCTION_UPDATE("loc_settings", "S_Trn_No = " & S_TRANSACTION_NUMBER, "settings_id = 1")
            GLOBAL_FUNCTION_UPDATE("loc_settings", "S_SI_No = " & S_SI_NUMBER, "settings_id = 1")

            For i = 0 To 100
                BackgroundWorkerTransactions.ReportProgress(i)
                If i = 0 Then
                    WaitFrm.Label1.Text = "Transaction is processing. Please wait."
                    If S_TrainingMode = False Then
                        ThreadOrder = New Thread(AddressOf InsertFMStock)
                        ThreadOrder.Start()
                        THREADLIST.Add(ThreadOrder)
                        For Each t In THREADLIST
                            t.Join()
                        Next

                        ThreadOrder = New Thread(AddressOf FillDatatable)
                        ThreadOrder.Start()
                        THREADLIST.Add(ThreadOrder)
                        For Each t In THREADLIST
                            t.Join()
                        Next

                        ThreadOrder = New Thread(Sub() UpdateInventory(False))
                        ThreadOrder.Start()
                        THREADLIST.Add(ThreadOrder)
                        For Each t In THREADLIST
                            t.Join()
                        Next
                        ThreadOrder = New Thread(AddressOf InsertDailyTransaction)
                        ThreadOrder.Start()
                        THREADLIST.Add(ThreadOrder)
                        For Each t In THREADLIST
                            t.Join()
                        Next
                        ThreadOrder = New Thread(AddressOf InsertDailyDetails)
                        ThreadOrder.Start()
                        THREADLIST.Add(ThreadOrder)
                        For Each t In THREADLIST
                            t.Join()
                        Next

                        If modeoftransaction = True Then
                            ThreadOrder = New Thread(AddressOf InsertModeofTransaction)
                            ThreadOrder.Start()
                            THREADLIST.Add(ThreadOrder)
                            For Each t In THREADLIST
                                t.Join()
                            Next
                        End If

                        If CUST_INFO_FILLED Then
                            ThreadOrder = New Thread(AddressOf InsertCustInfo)
                            ThreadOrder.Start()
                            THREADLIST.Add(ThreadOrder)
                            For Each t In THREADLIST
                                t.Join()
                            Next
                        End If

                        If DiscAppleid Then
                            ThreadOrder = New Thread(AddressOf InsertSeniorDetails)
                            ThreadOrder.Start()
                            THREADLIST.Add(ThreadOrder)
                            For Each t In THREADLIST
                                t.Join()
                            Next

                            ThreadOrder = New Thread(AddressOf InsertDiscountData)
                            ThreadOrder.Start()
                            THREADLIST.Add(ThreadOrder)
                            For Each t In THREADLIST
                                t.Join()
                            Next
                        End If

                        If PromoApplied Then
                            ThreadOrder = New Thread(AddressOf InsertCouponData)
                            ThreadOrder.Start()
                            THREADLIST.Add(ThreadOrder)
                            For Each t In THREADLIST
                                t.Join()
                            Next
                        End If
                    End If
                End If
                Thread.Sleep(10)
            Next
            For Each t In THREADLIST
                t.Join()
            Next

        Catch ex As Exception
            SendErrorReport(ex.ToString)
            AuditTrail.LogToAuditTral("System", "POS/BG Worker 1: " & ex.ToString, "Critical")
        End Try
    End Sub
    Private Sub BackgroundWorker1_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorkerTransactions.ProgressChanged
        With WaitFrm
            .ProgressBar1.Value = e.ProgressPercentage
            If e.ProgressPercentage = 20 Then
                .Label1.Text = "Transaction is processing. Please wait.."
            End If
            If e.ProgressPercentage = 40 Then
                .Label1.Text = "Transaction is processing. Please wait..."
            End If
            If e.ProgressPercentage = 60 Then
                .Label1.Text = "Transaction is processing. Please wait."
            End If
            If e.ProgressPercentage = 80 Then
                .Label1.Text = "Transaction is processing. Please wait.."
            End If
            If e.ProgressPercentage = 100 Then
                .Label1.Text = "Transaction is processing. Please wait..."
            End If
        End With
    End Sub
    Public Reprint As Integer = 1
    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorkerTransactions.RunWorkerCompleted
        Enabled = True
        WaitFrm.Close()
        PaymentForm.Close()



        If DataGridViewOrders.Rows.Count > 0 Then
            Try
                Dim TotalLines As Integer = 0
                Dim BodyLine As Integer = 560
                If DiscAppleid Then
                    BodyLine = 600
                Else
                    BodyLine = 540
                End If
                Dim CountHeaderLine As Integer = count("id", "loc_receipt WHERE type = 'Header' AND status = 1")
                Dim ProductLine As Integer = 0
                Dim CountFooterLine As Integer = count("id", "loc_receipt WHERE type = 'Footer' AND status = 1")
                CountHeaderLine *= 10
                CountFooterLine *= 10
                Dim DiscountLine As Integer = 0
                With DataGridViewOrders
                    For i As Integer = 0 To .Rows.Count - 1 Step +1
                        ProductLine += 10
                        If .Rows(i).Cells(11).Value > 0 Then
                            ProductLine += 10
                        End If
                        If .Rows(i).Cells(15).Value > 0 Then
                            DiscountLine += 10
                        End If
                        If .Rows(i).Cells(17).Value > 0 Then
                            DiscountLine += 10
                        End If
                        If .Rows(i).Cells(19).Value > 0 Then
                            DiscountLine += 10
                        End If
                        If .Rows(i).Cells(21).Value > 0 Then
                            DiscountLine += 10
                        End If
                    Next
                End With

                EJournal.PosWriter = True
                Dim XMLName As String = S_TRANSACTION_NUMBER & FullDateFormatForSaving().ToString & ".xml"
                XML_Writer = New XmlTextWriter(pdfSharpMod.XML_Path & XMLName, Encoding.UTF8)
                XML_Writer.WriteStartDocument(True)
                XML_Writer.Formatting = Formatting.Indented
                XML_Writer.Indentation = 2
                XML_Writer.WriteStartElement("Table")

                ProductLine *= 2
                TotalLines = CountHeaderLine + ProductLine + CountFooterLine + BodyLine + DiscountLine
                printdoc.DefaultPageSettings.PaperSize = New PaperSize("Custom", ReturnPrintSize(), TotalLines)

                If S_Print = "YES" Then
                    For i = 1 To S_PrintCount
                        printdoc.Print()
                        Reprint += 1
                    Next
                    Reprint = 1
                Else
                    For i = 1 To S_PrintCount
                        PrintPreviewDialog1.Document = printdoc
                        PrintPreviewDialog1.ShowDialog()
                        Reprint += 1
                    Next
                    Reprint = 1
                End If

                XML_Writer.WriteEndElement()
                XML_Writer.WriteEndDocument()
                XML_Writer.Close()

                SaveXMLInfo(XMLName)

            Catch ex As Exception
                SendErrorReport(ex.ToString)
                MessageBox.Show("An error occurred while trying to load the " &
                    "document for Print Preview. Make sure you currently have " &
                    "access to a printer. A printer must be localconnected and " &
                    "accessible for Print Preview to work.", Text,
                     MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

            InsertIntoEJournal()

            selectmax(1)

            SystemLogType = "TRANSACTION"
            SystemLogDesc = "Transaction of :" & returnfullname(ClientCrewID) & " Item(s): " & DataGridViewOrders.Rows.Count
            GLOBAL_SYSTEM_LOGS(SystemLogType, SystemLogDesc)

            DataGridViewOrders.Rows.Clear()
            DataGridViewInv.Rows.Clear()
            modeoftransaction = False
            ButtonApplyCoupon.Enabled = False
            ButtonApplyDiscounts.Enabled = False
            ButtonPayMent.Enabled = False
            Buttonholdoder.Enabled = False
            ButtonPendingOrders.Enabled = True
            payment = False

            PromoDefault()
            DiscountDefault()
            ResetTransactionVariables()
            ACTIVE = 1

            DISABLESERVEROTHERSPRODUCT = False
            GETNOTDISCOUNTEDAMOUNT = 0

            Label76.Text = "0.00"
            TextBoxDISCOUNT.Text = "0.00"
            TextBoxSUBTOTAL.Text = "0.00"
            TextBoxGRANDTOTAL.Text = "0.00"
            LabelTransactionType.Text = "Walk-In"
            WaffleUpgrade = False
            ButtonWaffleUpgrade.Text = "Brownie Upgrade"
            ButtonWaffleUpgrade.BackColor = Color.FromArgb(221, 114, 46)
        Else
            MsgBox("Select Transaction First!")
        End If
    End Sub
    Private Sub FillDatatable()
        Try
            INVENTORY_DATATABLE = New DataTable
            With INVENTORY_DATATABLE
                .Columns.Add("SrvT")
                .Columns.Add("FID")
                .Columns.Add("Qty")
                .Columns.Add("ID")
                .Columns.Add("NM")
                .Columns.Add("Srv")
                .Columns.Add("COG")
                .Columns.Add("OCOG")
                .Columns.Add("PrdAddID")
                .Columns.Add("Origin")
                .Columns.Add("HalfBatch")
            End With
            With DataGridViewInv
                For i As Integer = 0 To .Rows.Count - 1 Step +1
                    Dim Prod As DataRow = INVENTORY_DATATABLE.NewRow
                    Prod("SrvT") = .Rows(i).Cells(0).Value
                    Prod("FID") = .Rows(i).Cells(1).Value
                    Prod("Qty") = .Rows(i).Cells(2).Value
                    Prod("ID") = .Rows(i).Cells(3).Value
                    Prod("NM") = .Rows(i).Cells(4).Value
                    Prod("Srv") = .Rows(i).Cells(5).Value
                    Prod("COG") = .Rows(i).Cells(6).Value
                    Prod("OCOG") = .Rows(i).Cells(7).Value
                    Prod("PrdAddID") = .Rows(i).Cells(8).Value
                    Prod("Origin") = .Rows(i).Cells(9).Value
                    Prod("HalfBatch") = .Rows(i).Cells(10).Value
                    INVENTORY_DATATABLE.Rows.Add(Prod)
                Next
            End With
        Catch ex As Exception
            SendErrorReport(ex.ToString)
            AuditTrail.LogToAuditTral("System", "POS/Fill Inventory: " & ex.ToString, "Critical")
        End Try
    End Sub
    Private Sub PrintDocument1_PrintPage(sender As Object, e As PrintPageEventArgs) Handles printdoc.PrintPage
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

            ReceiptHeaderOne(sender, e, False, "", False, True)
            ReceiptBody(sender, e, False, "", False)
            If DiscAppleid Then
                ReceiptBodyFooter(sender, e, False, "", False, True)
            Else
                ReceiptBodyFooter(sender, e, False, "", False, False)
            End If

            ReceiptFooterOne(sender, e, False, True)


            AuditTrail.LogToAuditTral("Transaction", "POS/Transaction Details: " & SiNumberToString, "Normal")


        Catch ex As Exception
            SendErrorReport(ex.ToString)
            AuditTrail.LogToAuditTral("System", "POS/Print Receipt: " & ex.ToString, "Critical")
        End Try
    End Sub
#End Region
#Region "Updates"
#Region "Products Update"

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            UPDATE_WORKER_CANCEL = False
            HASUPDATE = False

            'For i As Integer = 0 To PROMPT_MESSAGE_DATATABLE.Rows.Count - 1 Step +1
            '    MsgBox(PROMPT_MESSAGE_DATATABLE(i)(0))
            'Next

            If OnlineOffline Then
                Enabled = False
                CheckingForUpdates.Show()
                CheckingForUpdates.TopMost = True
                If ValidCloudConnection = True Then

                    BackgroundWorkerUpdates.WorkerReportsProgress = True
                    BackgroundWorkerUpdates.WorkerSupportsCancellation = True
                    BackgroundWorkerUpdates.RunWorkerAsync()
                Else
                    If BegBalanceBool = False Then
                        Enabled = False
                        BegBalance.Show()
                        BegBalance.TopMost = True
                    End If
                End If
            Else
                MsgBox("No internet connection.")
                If BegBalanceBool = False Then
                    BegBalance.Show()
                    BegBalance.TopMost = True
                End If
            End If
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Public POSISUPDATING As Boolean = False

    Dim TestInternetCon As Boolean = False
    Dim ThreadUpdate As Thread
    Private Sub BackgroundWorker2_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorkerUpdates.DoWork
        Try
            If ValidLocalConnection Then
                ThreadUpdate = New Thread(Sub() TestInternetCon = CheckForInternetConnection())
                ThreadUpdate.Start()
                THREADLISTUPDATE.Add(ThreadUpdate)
                For Each t In THREADLISTUPDATE
                    t.Join()
                    If (BackgroundWorkerUpdates.CancellationPending) Then
                        ' Indicate that the task was canceled.
                        e.Cancel = True
                        If UPDATE_WORKER_CANCEL Then
                            Exit For
                        End If
                        Exit Sub
                    End If
                Next
                If TestInternetCon Then
                    ThreadUpdate = New Thread(AddressOf ServerCloudCon)
                    ThreadUpdate.Start()
                    THREADLISTUPDATE.Add(ThreadUpdate)
                    For Each t In THREADLISTUPDATE
                        t.Join()
                        If (BackgroundWorkerUpdates.CancellationPending) Then
                            ' Indicate that the task was canceled.
                            e.Cancel = True
                            If UPDATE_WORKER_CANCEL Then
                                Exit For
                            End If
                            Exit Sub
                        End If
                    Next
                    If ServerCloudCon.State = ConnectionState.Open Then
                        'If UPDATEPRODUCTONLY = False Then
                        'POSISUPDATING = True
                        ThreadUpdate = New Thread(AddressOf CheckPriceChanges)
                        ThreadUpdate.Start()
                        THREADLISTUPDATE.Add(ThreadUpdate)

                        For Each t In THREADLISTUPDATE
                            t.Join()

                            If (BackgroundWorkerUpdates.CancellationPending) Then
                                ' Indicate that the task was canceled.
                                e.Cancel = True
                                If UPDATE_WORKER_CANCEL Then
                                    Exit For
                                End If
                                Exit Sub
                            End If
                        Next

                        ThreadUpdate = New Thread(Sub() GetUpdatesRowCount(1))
                        ThreadUpdate.Start()
                        THREADLISTUPDATE.Add(ThreadUpdate)
                        For Each t In THREADLISTUPDATE
                            t.Join()

                            If (BackgroundWorkerUpdates.CancellationPending) Then
                                ' Indicate that the task was canceled.
                                e.Cancel = True
                                If UPDATE_WORKER_CANCEL Then
                                    Exit For
                                End If
                                Exit Sub
                            End If
                        Next

                        ThreadUpdate = New Thread(Sub() GetCategoryUpdate(1))
                        ThreadUpdate.Start()
                        THREADLISTUPDATE.Add(ThreadUpdate)

                        ThreadUpdate = New Thread(Sub() GetFormulaUpdate(1))
                        ThreadUpdate.Start()
                        THREADLISTUPDATE.Add(ThreadUpdate)

                        ThreadUpdate = New Thread(Sub() GetInventoryUpdate(1))
                        ThreadUpdate.Start()
                        THREADLISTUPDATE.Add(ThreadUpdate)

                        ThreadUpdate = New Thread(Sub() GetCouponsUpdate(1))
                        ThreadUpdate.Start()
                        THREADLISTUPDATE.Add(ThreadUpdate)

                        ThreadUpdate = New Thread(Sub() GetPartnersUpdate(1))
                        ThreadUpdate.Start()
                        THREADLISTUPDATE.Add(ThreadUpdate)

                        ThreadUpdate = New Thread(AddressOf CouponApproval)
                        ThreadUpdate.Start()
                        THREADLISTUPDATE.Add(ThreadUpdate)

                        ThreadUpdate = New Thread(AddressOf CustomProductApproval)
                        ThreadUpdate.Start()
                        THREADLISTUPDATE.Add(ThreadUpdate)

                        ThreadUpdate = New Thread(Sub() GetProductUpdates(1))
                        ThreadUpdate.Start()
                        THREADLISTUPDATE.Add(ThreadUpdate)

                        ThreadUpdate = New Thread(AddressOf PromptMessage)
                        ThreadUpdate.Start()
                        THREADLISTUPDATE.Add(ThreadUpdate)


                        For Each t In THREADLISTUPDATE
                            t.Join()
                            If (BackgroundWorkerUpdates.CancellationPending) Then
                                ' Indicate that the task was canceled.
                                e.Cancel = True
                                If UPDATE_WORKER_CANCEL Then
                                    Exit For
                                End If
                                Exit Sub
                            End If
                        Next

                    End If
                End If

            End If
        Catch ex As Exception
            ValidCloudConnection = False
            BackgroundWorkerUpdates.CancelAsync()
            If UPDATE_WORKER_CANCEL Then
                MsgBox("Cannot fetch data. Please check your internet connection")
            End If
            AuditTrail.LogToAuditTral("System", "POS: Update not successful, " & ex.ToString, "Critical")
            SendErrorReport(ex.ToString)
            Exit Sub
        End Try
    End Sub
    Private Sub BackgroundWorker2_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorkerUpdates.RunWorkerCompleted
        Try
            If UPDATE_WORKER_CANCEL = False Then
                If ValidCloudConnection Then
                    Button3.Enabled = True
                    'UPDATEPRODUCTONLY = False
                    POSISUPDATING = False
                    If UPDATE_CATEGORY_DATATABLE.Rows.Count > 0 Or UPDATE_PRODUCTS_DATATABLE.Rows.Count > 0 Or UPDATE_FORMULA_DATATABLE.Rows.Count > 0 Or UPDATE_INVENTORY_DATATABLE.Rows.Count > 0 Or UPDATE_PRICE_CHANGE_DATATABLE.Rows.Count > 0 Or UPDATE_COUPON_APPROVAL_DATATABLE.Rows.Count > 0 Or UPDATE_CUSTOM_PROD_APP_DATATABLE.Rows.Count Or UPDATE_COUPONS_DATATABLE.Rows.Count > 0 Or UPDATE_PARTNERS_DATATABLE.Rows.Count > 0 Then
                        CheckingForUpdates.CheckingUpdatesUPDATED = True
                        CheckingForUpdates.Close()
                        AuditTrail.LogToAuditTral("System", "POS: Update Detected, ", "Normal")

                        Dim updatemessage = MessageBox.Show("New Updates are available. Would you like to update now ?", "New Updates", MessageBoxButtons.YesNo, MessageBoxIcon.Information)

                        If updatemessage = DialogResult.Yes Then
                            HASUPDATE = True
                            Enabled = False
                            CheckingForUpdates.Show()
                            CheckingForUpdates.TopMost = True
                            CheckingForUpdates.CheckingUpdatesUPDATED = False
                            If UPDATE_WORKER_CANCEL = False Then
                                CheckingForUpdates.Instance.Invoke(Sub()
                                                                       CheckingForUpdates.ProgressBar1.Maximum = 0
                                                                       Dim TotalRows = UPDATE_CATEGORY_DATATABLE.Rows.Count + UPDATE_PRODUCTS_DATATABLE.Rows.Count + UPDATE_FORMULA_DATATABLE.Rows.Count + UPDATE_INVENTORY_DATATABLE.Rows.Count + UPDATE_PRICE_CHANGE_DATATABLE.Rows.Count + UPDATE_COUPON_APPROVAL_DATATABLE.Rows.Count + UPDATE_CUSTOM_PROD_APP_DATATABLE.Rows.Count + UPDATE_PARTNERS_DATATABLE.Rows.Count + UPDATE_COUPONS_DATATABLE.Rows.Count
                                                                       CheckingForUpdates.ProgressBar1.Maximum = TotalRows
                                                                   End Sub)
                            End If
                            BackgroundWorkerInstallUpdates.WorkerReportsProgress = True
                            BackgroundWorkerInstallUpdates.WorkerSupportsCancellation = True
                            BackgroundWorkerInstallUpdates.RunWorkerAsync()
                        Else

                            If BegBalanceBool = False Then
                                BegBalance.Show()
                                BegBalance.TopMost = True
                            Else
                                Enabled = True
                            End If
                        End If
                    Else

                        If BegBalanceBool = False Then
                            BegBalance.Show()
                            BegBalance.TopMost = True
                        Else
                            DisplayInbox()
                            'Enabled = True
                        End If
                        'Enabled = True
                        HASUPDATE = False
                        CheckingForUpdates.CheckingUpdatesUPDATED = True
                        CheckingForUpdates.LabelCheckingUpdates.Text = "Complete Checking! No updates found."
                        CheckingForUpdates.Close()
                    End If
                Else
                    Button3.Enabled = True
                End If
            Else
                If BegBalanceBool = False Then
                    BegBalance.Show()
                    BegBalance.TopMost = True
                Else
                    Enabled = True
                End If
            End If
        Catch ex As Exception
            SendErrorReport(ex.ToString)
            AuditTrail.LogToAuditTral("System", "POS/BG Worker Update Complete: " & ex.ToString, "Critical")
        End Try
    End Sub
    Private Sub BackgroundWorkerInstallUpdates_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorkerInstallUpdates.DoWork
        Try
            If ValidLocalConnection Then
                ThreadUpdate = New Thread(Sub() InstallUpdatesFormula(1))
                ThreadUpdate.Start()
                THREADLISTUPDATE.Add(ThreadUpdate)
                ThreadUpdate = New Thread(Sub() InstallUpdatesInventory(1))
                ThreadUpdate.Start()
                THREADLISTUPDATE.Add(ThreadUpdate)
                ThreadUpdate = New Thread(Sub() InstallUpdatesCategory(1))
                ThreadUpdate.Start()
                THREADLISTUPDATE.Add(ThreadUpdate)
                ThreadUpdate = New Thread(Sub() InstallUpdatesCoupons(1))
                ThreadUpdate.Start()
                THREADLISTUPDATE.Add(ThreadUpdate)
                ThreadUpdate = New Thread(Sub() InstallUpdatesProducts(1))
                ThreadUpdate.Start()
                THREADLISTUPDATE.Add(ThreadUpdate)
                ThreadUpdate = New Thread(Sub() InstallUpdatesPriceChange())
                ThreadUpdate.Start()
                THREADLISTUPDATE.Add(ThreadUpdate)
                ThreadUpdate = New Thread(Sub() InstallCoupons())
                ThreadUpdate.Start()
                THREADLISTUPDATE.Add(ThreadUpdate)
                ThreadUpdate = New Thread(Sub() InstallProducts())
                ThreadUpdate.Start()
                THREADLISTUPDATE.Add(ThreadUpdate)
                ThreadUpdate = New Thread(Sub() InstallUpdatesPartners(1))
                ThreadUpdate.Start()
                THREADLISTUPDATE.Add(ThreadUpdate)
                For Each t In THREADLISTUPDATE
                    t.Join()
                    If (BackgroundWorkerUpdates.CancellationPending) Then
                        e.Cancel = True
                        UPDATE_WORKER_CANCEL = True
                        Exit Sub
                    End If
                Next
            End If

        Catch ex As Exception
            ValidCloudConnection = False
            BackgroundWorkerUpdates.CancelAsync()
            If UPDATE_WORKER_CANCEL Then
                MsgBox("Cannot fetch data. Please check your internet connection")
            End If
            SendErrorReport(ex.ToString)
            AuditTrail.LogToAuditTral("System", "POS/BG Worker Install Update: " & ex.ToString, "Critical")
            Exit Sub
        End Try
    End Sub

    Private Sub BackgroundWorkerInstallUpdates_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorkerInstallUpdates.RunWorkerCompleted
        Try
            CheckingForUpdates.CheckingUpdatesUPDATED = True
            CheckingForUpdates.Close()
            AuditTrail.LogToAuditTral("System", "POS: Update successful, ", "Normal")

            If UPDATE_PRICE_CHANGE_BOOL = True Then
                MsgBox("Product price changes approved")
                UPDATE_PRICE_CHANGE_BOOL = False
            End If
            If UPDATE_COUPON_APPROVAL_BOOL Then
                MsgBox("Coupon Approved")
                UPDATE_COUPON_APPROVAL_BOOL = False
            End If
            If UPDATE_CUSTOM_PROD_APP_BOOL Then
                MsgBox("Products Approved")
                UPDATE_CUSTOM_PROD_APP_BOOL = False
            End If



            BackgroundWorkerContent.WorkerReportsProgress = True
            BackgroundWorkerContent.WorkerSupportsCancellation = True
            BackgroundWorkerContent.RunWorkerAsync()
        Catch ex As Exception
            MsgBox(ex.ToString)
            AuditTrail.LogToAuditTral("System", "POS/BG Worker Install Update Complete: " & ex.ToString, "Critical")
        End Try
    End Sub


#End Region
#Region "Message"


    Private Sub TextBoxGRANDTOTAL_TextChanged(sender As Object, e As EventArgs) Handles TextBoxGRANDTOTAL.TextChanged
        Try
            If My.Settings.LedDisplayTrue Then
                LedDisplay(TextBoxGRANDTOTAL.Text, True)
            End If
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If Button2.Text = "Show" Then
            SynctoCloud.TopMost = True
            SynctoCloud.WindowState = FormWindowState.Normal
            Button2.Text = "Hide"
        Else
            SynctoCloud.TopMost = False
            SynctoCloud.WindowState = FormWindowState.Minimized
            Button2.Text = "Show"
        End If
    End Sub

    Private Sub LabelTransactionType_TextChanged(sender As Object, e As EventArgs) Handles LabelTransactionType.TextChanged
        Try
            LabelTransactionType.Text = LabelTransactionType.Text.ToUpper
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles ButtonRemoveDiscount.Click
        ''MsgBox(TRANSACTIONMODE)
        'MsgBox(PromoApplied)
        DiscountDefault()
    End Sub
    Private Sub ButtonCDISC_Click(sender As Object, e As EventArgs) Handles ButtonRemovePromo.Click
        PromoDefault()
    End Sub
    Dim ThreadCheckInternet As Thread
    Dim ThreadlistCheckInternet As List(Of Thread) = New List(Of Thread)


    Private Sub BackgroundWorkerCheckInternet_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorkerCheckInternet.DoWork
        Try
            ThreadCheckInternet = New Thread(AddressOf PingInternetConnecion)
            ThreadCheckInternet.Start()
            ThreadlistCheckInternet.Add(ThreadCheckInternet)
            For Each t In ThreadlistCheckInternet
                t.Join()
            Next
        Catch ex As Exception
            SendErrorReport(ex.ToString)
            AuditTrail.LogToAuditTral("System", "POS/BG Worker Check Internet: " & ex.ToString, "Critical")
        End Try
    End Sub

    Private Sub BackgroundWorkerCheckInternet_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorkerCheckInternet.RunWorkerCompleted
        Try
            If OnlineOffline Then
                PictureBox1.BackgroundImage = Base64ToImage(OnlineImage)
                PictureBox1.BackgroundImageLayout = ImageLayout.Stretch
                LabelOnlineOffline.Text = "Online  • "
                POSWorkerCanceled = False
                If Not Button3.Enabled Then
                    Button3.Enabled = True
                End If
            Else
                PictureBox1.BackgroundImage = Base64ToImage(OfflineImage)
                PictureBox1.BackgroundImageLayout = ImageLayout.Stretch
                LabelOnlineOffline.Text = "Offline  • "
                If BackgroundWorkerSyncData.IsBusy Then
                    BackgroundWorkerSyncData.CancelAsync()

                    POSWorkerCanceled = True
                    If Button3.Enabled Then
                        Button3.Enabled = False
                    End If
                End If

                If BackgroundWorkerUpdates.IsBusy Then
                    BackgroundWorkerUpdates.CancelAsync()
                    UPDATE_WORKER_CANCEL = True
                End If
            End If
        Catch ex As Exception
            SendErrorReport(ex.ToString)
            AuditTrail.LogToAuditTral("System", "POS/BG Worker Check Internet Complete: " & ex.ToString, "Critical")
        End Try
    End Sub
    Dim ThreadAutoSyncData As Thread
    Dim ThreadlistAutoSyncData As List(Of Thread) = New List(Of Thread)
    Public POSWorkerCanceled As Boolean
    Private Sub BackgroundWorkerSyncData_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorkerSyncData.DoWork
        Try
            ThreadAutoSyncData = New Thread(AddressOf AutoSyncSales)
            ThreadAutoSyncData.Start()
            ThreadlistAutoSyncData.Add(ThreadAutoSyncData)

            ThreadAutoSyncData = New Thread(AddressOf AutoSyncSalesDetails)
            ThreadAutoSyncData.Start()
            ThreadlistAutoSyncData.Add(ThreadAutoSyncData)

            ThreadAutoSyncData = New Thread(AddressOf AutoSyncInventory)
            ThreadAutoSyncData.Start()
            ThreadlistAutoSyncData.Add(ThreadAutoSyncData)

            For Each t In ThreadlistAutoSyncData
                t.Join()
                If (BackgroundWorkerSyncData.CancellationPending) Then
                    ' Indicate that the task was canceled.
                    POSWorkerCanceled = True
                    e.Cancel = True
                    Exit For
                End If
            Next
        Catch ex As Exception
            SendErrorReport(ex.ToString)
            AuditTrail.LogToAuditTral("System", "POS/BG Worker Sync Data: " & ex.ToString, "Critical")
        End Try
    End Sub

    Private Sub BackgroundWorkerSyncData_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorkerSyncData.RunWorkerCompleted
        IncrementInterval = 0
    End Sub

    Private Sub ComboBoxPartners_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxPartners.SelectedIndexChanged
        Try
            listviewproductsshow(Partners, ComboBoxPartners.Text)
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Dim ThreadListContent As List(Of Thread) = New List(Of Thread)
    Dim ThreadContent As Thread
    Private Sub BackgroundWorker1_DoWork_1(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorkerContent.DoWork
        Try
            ThreadContent = New Thread(Sub() selectmax(1))
            ThreadContent.Start()
            ThreadListContent.Add(ThreadContent)
            For Each t In ThreadListContent
                t.Join()
            Next
            ThreadContent = New Thread(Sub() LoadContent())
            ThreadContent.Start()
            ThreadListContent.Add(ThreadContent)
            For Each t In ThreadListContent
                t.Join()
            Next
        Catch ex As Exception
            SendErrorReport(ex.ToString)
            AuditTrail.LogToAuditTral("System", "POS/BG Worker Content: " & ex.ToString, "Critical")
        End Try
    End Sub

    Private Sub BackgroundWorkerContent_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorkerContent.RunWorkerCompleted
        Try
            For i As Integer = 0 To SELECT_DISCTINCT_PARTNERS_DT.Rows.Count - 1 Step +1
                ComboBoxPartners.Items.Add(SELECT_DISCTINCT_PARTNERS_DT(i)(0))
            Next
            If ComboBoxPartners.Items.Count > 0 Then
                ComboBoxPartners.SelectedIndex = 0
            End If
            LoadCategory()
            DisplayInbox()
        Catch ex As Exception
            SendErrorReport(ex.ToString)
            AuditTrail.LogToAuditTral("System", "POS/BG Worker Content Complete: " & ex.ToString, "Critical")
        End Try
    End Sub


#End Region
#End Region
End Class


