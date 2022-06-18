Imports MySql.Data.MySqlClient
Public Class BegBalance
    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged, TextBox2.TextChanged, TextBox3.TextChanged, TextBox4.TextChanged, TextBox5.TextChanged, TextBox6.TextChanged, TextBox7.TextChanged, TextBox8.TextChanged, TextBox9.TextChanged, TextBox10.TextChanged
        Try
            Dim OneThousand As Decimal = Val(TextBox1.Text) * Val(Label1.Text)
            Dim FiveHundred As Decimal = Val(TextBox2.Text) * Val(Label2.Text)
            Dim TwoHundred As Decimal = Val(TextBox3.Text) * Val(Label3.Text)
            Dim OneHundred As Decimal = Val(TextBox4.Text) * Val(Label4.Text)
            Dim Fifty As Decimal = Val(TextBox5.Text) * Val(Label5.Text)
            Dim Twenty As Decimal = Val(TextBox6.Text) * Val(Label6.Text)
            Dim Ten As Decimal = Val(TextBox7.Text) * Val(Label7.Text)
            Dim Five As Decimal = Val(TextBox8.Text) * Val(Label8.Text)
            Dim One As Decimal = Val(TextBox9.Text) * Val(Label9.Text)
            Dim TwentyFiveCents As Decimal = Val(TextBox10.Text) * Val(Label10.Text)
            Label12.Text = OneThousand + FiveHundred + TwoHundred + OneHundred + Fifty + Twenty + Ten + Five + One + TwentyFiveCents
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress, TextBox2.KeyPress, TextBox3.KeyPress, TextBox4.KeyPress, TextBox5.KeyPress, TextBox6.KeyPress, TextBox7.KeyPress, TextBox8.KeyPress, TextBox9.KeyPress, TextBox10.KeyPress
        Numeric(sender:=sender, e:=e)
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            If ComboBox1.Text <> "" Then
                If Val(Label12.Text) <> 0 Then
                    BegBalanceBool = True
                    InsertBeginningBalance()

                Else
                    Dim message As Integer = MessageBox.Show("Cash drawer has zero balance. Do you want to proceed ?", "Zero Balance", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                    If message = DialogResult.Yes Then
                        BegBalanceBool = True
                        InsertBeginningBalance()
                    End If
                End If
            Else
                MessageBox.Show("Select shift first.", "Select shift", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub InsertBeginningBalance()
        Try

            POS.BackgroundWorkerContent.WorkerReportsProgress = True
            POS.BackgroundWorkerContent.WorkerSupportsCancellation = True
            POS.BackgroundWorkerContent.RunWorkerAsync()

            If ComboBox1.Text = "First Shift" Then
                SystemLogType = "BG-1"
            ElseIf ComboBox1.Text = "Second Shift" Then
                SystemLogType = "BG-2"
            ElseIf ComboBox1.Text = "Third Shift" Then
                SystemLogType = "BG-3"
            Else
                SystemLogType = "BG-4"
            End If
            Shift = ComboBox1.Text
            BeginningBalance = Val(Label12.Text)
            SystemLogDesc = Label12.Text
            GLOBAL_SYSTEM_LOGS(SystemLogType, SystemLogDesc)
            Dim message As String = GetMonthName(Format(Now(), "yyyy-MM-dd")) & " " & Format(Now(), "dd yyyy") & vbNewLine & Format(Now(), "hh:mm tt")
            'MessageBox.Show(message, "Set Cash Datetime:", MessageBoxButtons.OK, MessageBoxIcon.Information)
            'Settings.Button8.Visible = False
            AllowFormClose = True
            Me.Close()
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Dim AllowFormClose As Boolean = False
    Private Sub BegBalance_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If AllowFormClose = False Then
            If (e.CloseReason = CloseReason.UserClosing) Then
                e.Cancel = True
            End If
        Else
            DisplayInbox()
            If Application.OpenForms().OfType(Of Message).Any Then
                POS.Enabled = False
            Else
                POS.Enabled = True
                If ValidCloudConnection Then
                    POS.Button3.Enabled = True
                End If

            End If
        End If
    End Sub
    Private Sub BegBalance_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ComboBox1.SelectedIndex = 0
    End Sub

    Private Sub ButtonKeyboard_Click(sender As Object, e As EventArgs) Handles ButtonKeyboard.Click
        ShowKeyboard()
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            Dim message As Integer = MessageBox.Show("Are you sure you want to cancel beginning balance?", "NOTICE", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
            If message = DialogResult.Yes Then
                GLOBAL_SYSTEM_LOGS("BEG. CANCEL", "Canceled by: " & ClientCrewID)
                BegBalanceBool = True
                AllowFormClose = True
                POS.BackgroundWorkerContent.WorkerReportsProgress = True
                POS.BackgroundWorkerContent.WorkerSupportsCancellation = True
                POS.BackgroundWorkerContent.RunWorkerAsync()
                Close()
            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "ADV Custom Reports: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
End Class
