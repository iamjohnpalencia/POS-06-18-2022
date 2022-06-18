Imports MySql.Data.MySqlClient
Public Class TakeOut
    'Dim BAGORSYRUP As Boolean = False
    Private Sub TakeOut_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        POS.Enabled = True
    End Sub
    Private Sub DInventory()
        Try
            Dim Sql
            Dim Cmd As MySqlCommand
            Dim Da As MySqlDataAdapter
            Dim Dt As DataTable

            Dim EXTRAFPrimary As Double = 0
            Dim EXTRAFSecondary As Double = 0
            Dim EXTRAFNServing As Double = 0

            Dim IPrimary As Double = 0
            Dim ISecondary As Double = 0
            Dim INServing As Double = 0

            Dim SPrimary As Double = 0
            Dim SSecondary As Double = 0
            Dim SNServing As Double = 0

            If ComboBoxTakeout.Text = "Sugar Packets" Then
                Dim message = MessageBox.Show("Do you want to add Extra Sugar Syrup?", "Extra Sugar", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                If message = DialogResult.Yes Then
                    Try
                        Sql = "SELECT primary_value ,secondary_value, no_servings FROM loc_product_formula WHERE server_formula_id = " & SugarPackets
                        Cmd = New MySqlCommand(Sql, LocalhostConn)
                        Da = New MySqlDataAdapter(Cmd)
                        Dt = New DataTable
                        Da.Fill(Dt)

                        For Each row As DataRow In Dt.Rows
                            EXTRAFPrimary = row("primary_value")
                            EXTRAFSecondary = row("secondary_value")
                            EXTRAFNServing = row("no_servings")
                        Next

                        Dim TotalExtraP As Double = EXTRAFPrimary * Double.Parse(TextBoxQuantity.Text)
                        Dim TotalExtraS As Double = EXTRAFSecondary * Double.Parse(TextBoxQuantity.Text)
                        Dim TotalExtraN As Double = EXTRAFNServing * Double.Parse(TextBoxQuantity.Text)

                        Sql = "SELECT stock_primary ,stock_secondary, stock_no_of_servings FROM loc_pos_inventory WHERE server_inventory_id = " & SugarPackets
                        Cmd = New MySqlCommand(Sql, LocalhostConn)
                        Da = New MySqlDataAdapter(Cmd)
                        Dt = New DataTable
                        Da.Fill(Dt)

                        For Each row As DataRow In Dt.Rows
                            IPrimary = row("stock_primary")
                            ISecondary = row("stock_secondary")
                            INServing = row("stock_no_of_servings")
                        Next

                        SPrimary = IPrimary - TotalExtraP
                        SSecondary = ISecondary - TotalExtraS
                        SNServing = INServing - TotalExtraN
                        GLOBAL_FUNCTION_UPDATE("loc_pos_inventory", "stock_primary=" & SPrimary & ",stock_secondary=" & SSecondary & ",stock_no_of_servings=" & SNServing & "", "server_inventory_id =  " & SugarPackets)


                        GLOBAL_SYSTEM_LOGS("EXTRA", "SUGAR PACKETS")
                    Catch ex As Exception
                        SendErrorReport(ex.ToString)
                    End Try
                End If
            ElseIf ComboBoxTakeout.Text = "Extra Packaging" Then
                Dim message = MessageBox.Show("Do you want to add Extra packaging?", "Extra packaging", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                If message = DialogResult.Yes Then
                    Try
                        Sql = "SELECT primary_value ,secondary_value, no_servings FROM loc_product_formula WHERE server_formula_id = " & WaffleBagID
                        Cmd = New MySqlCommand(Sql, LocalhostConn)
                        Da = New MySqlDataAdapter(Cmd)
                        Dt = New DataTable
                        Da.Fill(Dt)

                        For Each row As DataRow In Dt.Rows
                            EXTRAFPrimary = row("primary_value")
                            EXTRAFSecondary = row("secondary_value")
                            EXTRAFNServing = row("no_servings")
                        Next

                        Dim TotalExtraP As Double = EXTRAFPrimary * Double.Parse(TextBoxQuantity.Text)
                        Dim TotalExtraS As Double = EXTRAFSecondary * Double.Parse(TextBoxQuantity.Text)
                        Dim TotalExtraN As Double = EXTRAFNServing * Double.Parse(TextBoxQuantity.Text)

                        Sql = "SELECT stock_primary ,stock_secondary, stock_no_of_servings FROM loc_pos_inventory WHERE server_inventory_id = " & WaffleBagID
                        Cmd = New MySqlCommand(Sql, LocalhostConn)
                        Da = New MySqlDataAdapter(Cmd)
                        Dt = New DataTable
                        Da.Fill(Dt)

                        For Each row As DataRow In Dt.Rows
                            IPrimary = row("stock_primary")
                            ISecondary = row("stock_secondary")
                            INServing = row("stock_no_of_servings")
                        Next

                        SPrimary = IPrimary - TotalExtraP
                        SSecondary = ISecondary - TotalExtraS
                        SNServing = INServing - TotalExtraN

                        GLOBAL_FUNCTION_UPDATE("loc_pos_inventory", "stock_primary=" & SPrimary & ",stock_secondary=" & SSecondary & ",stock_no_of_servings=" & SNServing & "", "server_inventory_id = " & WaffleBagID)
                        GLOBAL_SYSTEM_LOGS("EXTRA", "PACKAGING")
                    Catch ex As Exception
                        SendErrorReport(ex.ToString)
                    End Try
                End If
            Else
                MsgBox("Select extra packaging/sugar syrup first")
            End If
            Log()
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Take Out: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub Log()
        Try
            Dim LogType As String = "EXTRA"
            Dim LogDesc As String = ""
            If ComboBoxTakeout.Text = "Waffle bag" Then
                LogDesc = "BAG : Waffle bag" & ClientCrewID
                GLOBAL_SYSTEM_LOGS(LogType, LogDesc)
            ElseIf ComboBoxTakeout.Text = "Sugar syrup" Then
                LogDesc = "SUGAR : Sugar Syrup " & ClientCrewID
                GLOBAL_SYSTEM_LOGS(LogType, LogDesc)
            End If
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub ButtonTakeOut_Click(sender As Object, e As EventArgs) Handles ButtonTakeOut.Click
        If ComboBoxTakeout.Text = "Extra Packaging" Then
            If Val(TextBoxQuantity.Text) > 0 Then
                DInventory()
                Close()
            Else
                MessageBox.Show("Input quantity first", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        ElseIf ComboBoxTakeout.Text = "Sugar Packets" Then
            If Val(TextBoxQuantity.Text) > 0 Then
                DInventory()
                Close()
            Else
                MessageBox.Show("Input quantity first", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Else
            MessageBox.Show("Select extra packaging/sugar syrup first", "Select", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub buttonpressedenter(ByVal btntext As String)
        Try
            TextBoxQuantity.Select()
            If Val(TextBoxQuantity.Text) <> 0 Then
                TextBoxQuantity.Text += btntext
            Else
                TextBoxQuantity.Text = btntext
            End If
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub ButtonNo9_Click(sender As Object, e As EventArgs) Handles ButtonNo9.Click
        If TextBoxQuantity.Text.Length > 6 Then
        Else
            buttonpressedenter(btntext:=ButtonNo9.Text)
        End If
    End Sub
    Private Sub ButtonNo8_Click(sender As Object, e As EventArgs) Handles ButtonNo8.Click
        If TextBoxQuantity.Text.Length > 6 Then
        Else
            buttonpressedenter(btntext:=ButtonNo8.Text)
        End If
    End Sub
    Private Sub ButtonNo7_Click(sender As Object, e As EventArgs) Handles ButtonNo7.Click
        If TextBoxQuantity.Text.Length > 6 Then
        Else
            buttonpressedenter(btntext:=ButtonNo7.Text)
        End If
    End Sub
    Private Sub ButtonNo6_Click(sender As Object, e As EventArgs) Handles ButtonNo6.Click
        If TextBoxQuantity.Text.Length > 6 Then
        Else
            buttonpressedenter(btntext:=ButtonNo6.Text)
        End If
    End Sub
    Private Sub ButtonNo5_Click(sender As Object, e As EventArgs) Handles ButtonNo5.Click
        If TextBoxQuantity.Text.Length > 6 Then
        Else
            buttonpressedenter(btntext:=ButtonNo5.Text)
        End If
    End Sub
    Private Sub ButtonNo4_Click(sender As Object, e As EventArgs) Handles ButtonNo4.Click
        If TextBoxQuantity.Text.Length > 6 Then
        Else
            buttonpressedenter(btntext:=ButtonNo4.Text)
        End If
    End Sub
    Private Sub ButtonNo3_Click(sender As Object, e As EventArgs) Handles ButtonNo3.Click
        If TextBoxQuantity.Text.Length > 6 Then
        Else
            buttonpressedenter(btntext:=ButtonNo3.Text)
        End If
    End Sub
    Private Sub ButtonNo2_Click(sender As Object, e As EventArgs) Handles ButtonNo2.Click
        If TextBoxQuantity.Text.Length > 6 Then
        Else
            buttonpressedenter(btntext:=ButtonNo2.Text)
        End If
    End Sub
    Private Sub ButtonNo1_Click(sender As Object, e As EventArgs) Handles ButtonNo1.Click
        If TextBoxQuantity.Text.Length > 6 Then
        Else
            buttonpressedenter(btntext:=ButtonNo1.Text)
        End If
    End Sub
    Private Sub ButtonNo0_Click(sender As Object, e As EventArgs) Handles ButtonNo0.Click
        If TextBoxQuantity.Text.Length > 6 Then
        Else
            buttonpressedenter(btntext:=ButtonNo0.Text)
        End If
    End Sub
    Private Sub ButtonNo00_Click(sender As Object, e As EventArgs) Handles ButtonNo00.Click
        If TextBoxQuantity.Text.Length > 6 Then
        Else
            buttonpressedenter(btntext:=ButtonNo00.Text)
        End If
    End Sub
    Private Sub TakeOut_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try

            LoadSettings()

        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Dim WaffleBagID
    Dim SugarPackets
    Private Sub LoadSettings()
        Try
            Dim LocalCon As MySqlConnection = LocalhostConn()
            Dim ArrayOfIDS() As Integer = {S_Waffle_Bag, S_Packets}
            WaffleBagID = S_Waffle_Bag
            SugarPackets = S_Packets
            For Each value As Integer In ArrayOfIDS
                sql = "SELECT product_ingredients FROM loc_product_formula WHERE formula_id = " & value
                cmd = New MySqlCommand(sql, LocalCon)
                Dim res = cmd.ExecuteScalar()
                ComboBoxTakeout.Items.Add(res)
            Next
            ComboBoxTakeout.SelectedIndex = 0
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If TextBoxQuantity.Text.Count > 0 Then
            TextBoxQuantity.Text = TextBoxQuantity.Text.Remove(TextBoxQuantity.Text.Count - 1)
        End If
    End Sub
    Private Sub TextBoxWaffleBag_KeyPress(sender As Object, e As KeyPressEventArgs)
        Try
            Numeric(sender, e)
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub
End Class