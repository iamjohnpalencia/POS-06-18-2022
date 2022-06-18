Imports MySql.Data.MySqlClient
Public Class Changeproductformula
    Public intTop As Integer = 1
    Dim strText As String
    Dim cmb As ComboBox
    Private Sub Changeproductformula_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        SettingsForm.Enabled = True
    End Sub
    Private Sub Changeproductformula_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadProducts()
        Dim arg = New DataGridViewCellEventArgs(0, 0)
        DataGridViewProducts_CellClick(sender, arg)
    End Sub
    Private Sub LoadProducts()
        Try
            GLOBAL_SELECT_ALL_FUNCTION("loc_admin_products WHERE product_status = 1", "`product_id`,`product_name`, `formula_id`, `origin`", DataGridViewProducts)
            DataGridViewProducts.Columns(0).Visible = False
            DataGridViewProducts.Columns(2).Visible = False
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Change Formula: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Public IamTheChoosenButton As String = ""
    Private Sub DataGridViewProducts_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridViewProducts.CellClick
        Try
            Dim formulaid = DataGridViewProducts.SelectedRows(0).Cells(2).Value
            Dim Origin = DataGridViewProducts.SelectedRows(0).Cells(3).Value.ToString
            LoadFormulas(formulaid, Origin)
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub LoadFormulas(formulaid, Origin)
        Try
            intTop = 0
            FlowLayoutPanel1.Controls.Clear()
            Dim sql = ""
            Dim splitformulaids As String() = formulaid.Split(New Char() {","c})
            For Each eachformulaidword In splitformulaids
                If Origin = "Server" Then
                    sql = "SELECT product_ingredients FROM loc_product_formula WHERE server_formula_id = " & eachformulaidword
                Else
                    sql = "SELECT product_ingredients FROM loc_product_formula WHERE formula_id = " & eachformulaidword
                End If
                Dim cmd As MySqlCommand = New MySqlCommand(sql, LocalhostConn)
                Dim result = cmd.ExecuteScalar()
                Dim new_Button As New Button
                With new_Button
                    .Name = result & intTop
                    .Text = result
                    .TextAlign = ContentAlignment.MiddleCenter
                    .ForeColor = Color.Black
                    .Font = New Font("Tahoma", 9, FontStyle.Bold)
                    .FlatStyle = FlatStyle.Standard
                    .FlatAppearance.BorderSize = 0
                    .Width = 130
                    .Height = 53
                    .Cursor = Cursors.Hand
                    intTop = intTop + 1
                    AddHandler .Click, AddressOf new_Button_click
                End With
                FlowLayoutPanel1.Controls.Add(new_Button)
                LocalhostConn.Close()
            Next
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Change Formula: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub new_Button_click(ByVal sender As Object, ByVal e As EventArgs)
        If TypeOf sender Is Button Then
            Dim btn = sender
            Dim name = btn.name
            Dim text = btn.text
            IamTheChoosenButton = name
            Enabled = False
            SelectNewFormula.Show()
        End If
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles ButtonArrangeFormulaIDs.Click
        Try
            Dim sql = ""
            Dim cmd As MySqlCommand
            TextBox1.Clear()

            For Each btn As Button In FlowLayoutPanel1.Controls.OfType(Of Button)()
                If DataGridViewProducts.SelectedRows(0).Cells(3).Value.ToString = "Server" Then
                    sql = "SELECT server_formula_id FROM loc_product_formula WHERE product_ingredients = '" & btn.Text & "'"
                Else
                    sql = "SELECT formula_id FROM loc_product_formula WHERE product_ingredients = '" & btn.Text & "'"
                End If
                cmd = New MySqlCommand(sql, LocalhostConn)
                Dim result = cmd.ExecuteScalar
                TextBox1.Text += result & ","
            Next
            TextBox1.Text = TextBox1.Text.TrimEnd(",")
            Dim MSG = MessageBox.Show("Are you sure you want to save the changes?", "Update Product", MessageBoxButtons.YesNo)
            If MSG = DialogResult.Yes Then
                sql = "UPDATE loc_admin_products SET formula_id = @0 WHERE product_id = " & DataGridViewProducts.SelectedRows(0).Cells(0).Value
                cmd = New MySqlCommand(sql, LocalhostConn)
                cmd.Parameters.Add("@0", MySqlDbType.VarChar).Value = TextBox1.Text
                cmd.ExecuteNonQuery()
                MsgBox("Complete")
                LoadProducts()
            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Change Formula: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
End Class