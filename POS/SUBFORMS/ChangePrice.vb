Imports MySql.Data.MySqlClient
Public Class ChangePrice
    Public ProductID
    Public PriceFrom
    Public Product
    Private Sub ChangePrice_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.TopMost = True
            TextBoxPriceFrom.Text = PriceFrom
            Me.Text = Product
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub ChangePrice_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            MDIFORM.newMDIchildManageproduct.Enabled = True
            MDIFORM.newMDIchildManageproduct.LoadPriceChange()
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            If Val(TextBoxPriceTo.Text) <> 0 Then
                If SavePriceChange() = True Then

                    MsgBox("Submitted")
                    Close()
                Else
                    MsgBox("Error")
                End If
            Else
                MsgBox("Fill all the fields")
            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Price Change: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Function SavePriceChange() As Boolean
        Dim result = 0
        Try
            Dim ConnectionLocal As MySqlConnection = LocalhostConn()
            Dim sql = "INSERT INTO loc_price_request_change (`server_product_id`, `store_name`, `request_price`, `created_at`, `active`, `store_id`, `crew_id`, `guid`, `synced`) VALUES (@1,@2,@3,@4,@5,@6,@7,@8,@9)"
            Dim cmd As MySqlCommand = New MySqlCommand(sql, ConnectionLocal)
            cmd.Parameters.Add("@1", MySqlDbType.Text).Value = ProductID
            cmd.Parameters.Add("@2", MySqlDbType.Text).Value = ClientStorename
            cmd.Parameters.Add("@3", MySqlDbType.Text).Value = TextBoxPriceTo.Text
            cmd.Parameters.Add("@4", MySqlDbType.Text).Value = FullDate24HR()
            cmd.Parameters.Add("@5", MySqlDbType.Text).Value = 1
            cmd.Parameters.Add("@6", MySqlDbType.Text).Value = ClientStoreID
            cmd.Parameters.Add("@7", MySqlDbType.Text).Value = ClientCrewID
            cmd.Parameters.Add("@8", MySqlDbType.Text).Value = ClientGuid
            cmd.Parameters.Add("@9", MySqlDbType.Text).Value = "Unsynced"
            result = cmd.ExecuteNonQuery()
            AuditTrail.LogToAuditTral("User", "Price Change : " & returnselect("product_name", "loc_admin_products") & ", Changed By: " & ClientCrewID, "Normal")


        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Price Change: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
        If result = 1 Then
            Return True
        Else
            Return False
        End If
    End Function
    Private Sub TextBoxPriceFrom_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBoxPriceTo.KeyPress, TextBoxPriceFrom.KeyPress
        Try
            Numeric(sender, e)
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) 
        Close()
    End Sub

    Private Sub ButtonKeyboard_Click(sender As Object, e As EventArgs) Handles ButtonKeyboard.Click
        ShowKeyboard()
        TextBoxPriceTo.Focus()
    End Sub
End Class