Public Class CashBreakdown
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Dim msg = MessageBox.Show("Are you sure you want to submit cash break-down?", "NOTICE", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If msg = DialogResult.Yes Then
                ResetTransactionVariables()
                Dim Table = "loc_cash_breakdown"
                Dim Fields = "(`1000`, `500`, `200`, `100`, `50`, `20`, `10`, `5`, `1`, `.25`, `.05`, `created_at`, `crew_id`, `status`, `zreading`, `synced`)"
                Dim Values = "(" & Val(TextBox1000.Text) & "," & Val(TextBox500.Text) & "," & Val(TextBox200.Text) & "," & Val(TextBox100.Text) & ",
                " & Val(TextBox50.Text) & "," & Val(TextBox20.Text) & "," & Val(TextBox10.Text) & "," & Val(TextBox5.Text) & "," & Val(TextBox1.Text) & ",
                " & Val(TextBoxPoint25.Text) & "," & Val(TextBoxZeroPoint5.Text) & ",'" & FullDate24HR() & "', '" & ClientCrewID & "' , '1', '" & S_Zreading & "', 'Unsynced')"
                GLOBAL_INSERT_FUNCTION(Table, Fields, Values)
                AuditTrail.LogToAuditTral("User", "Cash Break Down: Total " & Label23.Text & " User: " & ClientCrewID, "Normal")
                HASUPDATE = False
                BegBalanceBool = False
                FormIsOpen()
                SystemLogDesc = "User Logout: " & returnfullname(where:=ClientCrewID)
                SystemLogType = "LOG OUT"
                GLOBAL_SYSTEM_LOGS(SystemLogType, SystemLogDesc)

                SystemLogDesc = "Cash Breakdown - " & returnfullname(where:=ClientCrewID)
                SystemLogType = "CB"
                GLOBAL_SYSTEM_LOGS(SystemLogType, SystemLogDesc)
                EndBalance()
                MDIFORM.Close()
                POS.Close()
                Dispose()
                Login.Show()
            End If

        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Cash Breakdown: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub

    Private Sub TextBox1000_TextChanged(sender As Object, e As EventArgs) Handles TextBoxZeroPoint5.TextChanged, TextBoxPoint25.TextChanged, TextBox500.TextChanged, TextBox50.TextChanged, TextBox5.TextChanged, TextBox200.TextChanged, TextBox20.TextChanged, TextBox1000.TextChanged, TextBox100.TextChanged, TextBox10.TextChanged, TextBox1.TextChanged
        Try
            ReturnZero(Me)
            Dim OneThousand As Decimal = Val(TextBox1000.Text) * Val(Label1.Text)
            Dim FiveHundred As Decimal = Val(TextBox500.Text) * Val(Label2.Text)
            Dim TwoHundred As Decimal = Val(TextBox200.Text) * Val(Label3.Text)
            Dim OneHundred As Decimal = Val(TextBox100.Text) * Val(Label4.Text)
            Dim Fifty As Decimal = Val(TextBox50.Text) * Val(Label5.Text)
            Dim Twenty As Decimal = Val(TextBox20.Text) * Val(Label6.Text)
            Dim Ten As Decimal = Val(TextBox10.Text) * Val(Label7.Text)
            Dim Five As Decimal = Val(TextBox5.Text) * Val(Label8.Text)
            Dim One As Decimal = Val(TextBox1.Text) * Val(Label9.Text)
            Dim TwentyFiveCents As Decimal = Val(TextBoxPoint25.Text) * Val(Label10.Text)
            Dim ZeroPoint5Cents As Decimal = Val(TextBoxZeroPoint5.Text) * Val(Label11.Text)
            Label23.Text = OneThousand + FiveHundred + TwoHundred + OneHundred + Fifty + Twenty + Ten + Five + One + TwentyFiveCents + ZeroPoint5Cents
        Catch ex As Exception
            SendErrorReport(ex.ToString)
            End Try
    End Sub

    Private Sub TextBox1000_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBoxZeroPoint5.KeyPress, TextBoxPoint25.KeyPress, TextBox500.KeyPress, TextBox50.KeyPress, TextBox5.KeyPress, TextBox200.KeyPress, TextBox20.KeyPress, TextBox1000.KeyPress, TextBox100.KeyPress, TextBox10.KeyPress, TextBox1.KeyPress
        Try
            Numeric(sender, e)
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub

    Private Sub CashBreakdown_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

        If LOGOUTFROMPOS Then
            POS.Enabled = True
        Else
            MDIFORM.Enabled = True
        End If
    End Sub

#Region "ButtonClick"
    Private Sub ButtonKeyboard_Click(sender As Object, e As EventArgs) Handles ButtonKeyboard.Click
        ShowKeyboard()
    End Sub

    Private Sub TextBox1000_Click(sender As Object, e As EventArgs) Handles TextBox1000.Click
        TextBox1000.SelectAll()
    End Sub

    Private Sub TextBox500_Click(sender As Object, e As EventArgs) Handles TextBox500.Click
        TextBox500.SelectAll()
    End Sub

    Private Sub TextBox200_Click(sender As Object, e As EventArgs) Handles TextBox200.Click
        TextBox200.SelectAll()
    End Sub

    Private Sub TextBox100_Click(sender As Object, e As EventArgs) Handles TextBox100.Click
        TextBox100.SelectAll()
    End Sub

    Private Sub TextBox50_Click(sender As Object, e As EventArgs) Handles TextBox50.Click
        TextBox50.SelectAll()
    End Sub

    Private Sub TextBox20_Click(sender As Object, e As EventArgs) Handles TextBox20.Click
        TextBox20.SelectAll()
    End Sub

    Private Sub TextBox10_Click(sender As Object, e As EventArgs) Handles TextBox10.Click
        TextBox10.SelectAll()
    End Sub

    Private Sub TextBox5_Click(sender As Object, e As EventArgs) Handles TextBox5.Click
        TextBox5.SelectAll()
    End Sub

    Private Sub TextBox1_Click(sender As Object, e As EventArgs) Handles TextBox1.Click
        TextBox1.SelectAll()
    End Sub

    Private Sub TextBoxPoint25_Click(sender As Object, e As EventArgs) Handles TextBoxPoint25.Click
        TextBoxPoint25.SelectAll()
    End Sub

    Private Sub TextBoxZeroPoint5_Click(sender As Object, e As EventArgs) Handles TextBoxZeroPoint5.Click
        TextBoxZeroPoint5.SelectAll()
    End Sub
#End Region
#Region "TabStop"
    Private Sub TextBox1000_Enter(sender As Object, e As EventArgs) Handles TextBox1000.Enter
        TextBox1000.SelectAll()
    End Sub

    Private Sub TextBox500_Enter(sender As Object, e As EventArgs) Handles TextBox500.Enter
        TextBox500.SelectAll()
    End Sub

    Private Sub TextBox200_Enter(sender As Object, e As EventArgs) Handles TextBox200.Enter
        TextBox200.SelectAll()
    End Sub

    Private Sub TextBox100_Enter(sender As Object, e As EventArgs) Handles TextBox100.Enter
        TextBox100.SelectAll()
    End Sub

    Private Sub TextBox50_Enter(sender As Object, e As EventArgs) Handles TextBox50.Enter
        TextBox50.SelectAll()
    End Sub

    Private Sub TextBox20_Enter(sender As Object, e As EventArgs) Handles TextBox20.Enter
        TextBox20.SelectAll()
    End Sub

    Private Sub TextBox10_Enter(sender As Object, e As EventArgs) Handles TextBox10.Enter
        TextBox10.SelectAll()
    End Sub

    Private Sub TextBox5_Enter(sender As Object, e As EventArgs) Handles TextBox5.Enter
        TextBox5.SelectAll()
    End Sub

    Private Sub TextBox1_Enter(sender As Object, e As EventArgs) Handles TextBox1.Enter
        TextBox1.SelectAll()
    End Sub

    Private Sub TextBoxPoint25_Enter(sender As Object, e As EventArgs) Handles TextBoxPoint25.Enter
        TextBoxPoint25.SelectAll()
    End Sub

    Private Sub TextBoxZeroPoint5_Enter(sender As Object, e As EventArgs) Handles TextBoxZeroPoint5.Enter
        TextBoxZeroPoint5.SelectAll()
    End Sub


#End Region


End Class