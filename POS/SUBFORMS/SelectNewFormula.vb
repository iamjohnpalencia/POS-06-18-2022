Imports MySql.Data.MySqlClient
Public Class SelectNewFormula
    Private Sub SelectNewFormula_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            GLOBAL_SELECT_ALL_FUNCTION("loc_product_formula", "`product_ingredients`", DataGridView1)
        Catch ex As Exception
            AuditTrail.LogToAuditTrail("System", "SelectNewFormula/Load: " & ex.ToString, "Critical")
        End Try
    End Sub
    Dim ChangeFormula As Boolean = False
    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        ChangeFormula = True
        Close()
    End Sub
    Private Sub SelectNewFormula_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            If ChangeFormula = True Then
                For Each btn As Button In Changeproductformula.FlowLayoutPanel1.Controls.OfType(Of Button)()
                    If btn.Name = Changeproductformula.IamTheChoosenButton Then
                        btn.Text = DataGridView1.SelectedRows(0).Cells(0).Value.ToString
                        btn.Name = DataGridView1.SelectedRows(0).Cells(0).Value.ToString & Changeproductformula.intTop
                        Changeproductformula.intTop += Changeproductformula.intTop
                    End If
                Next
            End If
            Changeproductformula.Enabled = True
        Catch ex As Exception
            AuditTrail.LogToAuditTrail("System", "SelectNewFormula/FormClosing: " & ex.ToString, "Critical")
        End Try
    End Sub
End Class