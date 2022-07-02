Public Class TransactionTypeInfo
    Dim Submited As Boolean = False
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TopMost = True
        'If transactionmode = "GCash" Then
        '    TextBoxMARKUP.Text = Val(POS.TextBoxGRANDTOTAL.Text) * 0.15
        'Else
        '    TextBoxMARKUP.Text = "N/A"
        'End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If String.IsNullOrWhiteSpace(TextBoxFULLNAME.Text) Or String.IsNullOrWhiteSpace(TextBoxREFERENCE.Text) Then
            MsgBox("Please fill out all fields.", vbInformation)
        Else
            Submited = True
            TEXTBOXFULLNAMEVALUE = TextBoxFULLNAME.Text
            TEXTBOXREFERENCEVALUE = TextBoxREFERENCE.Text
            Close()
            TransactionType.Close()
        End If
    End Sub
    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ButtonEnableability(TransactionType, True)
        If Submited = False Then
            TRANSACTIONMODE = "Walk-In"
            POS.LabelTransactionType.Text = TRANSACTIONMODE
        End If
    End Sub

    Private Sub TextBoxREFERENCE_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBoxREFERENCE.KeyPress, TextBoxFULLNAME.KeyPress
        Try
            If InStr(DisallowedCharacters, e.KeyChar) > 0 Then
                e.Handled = True
            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTrail("System", "TransactionTypeInfo/DisallowedCharacters: " & ex.ToString, "Critical")
        End Try
    End Sub

    Private Sub ButtonKeyboard_Click(sender As Object, e As EventArgs) Handles ButtonKeyboard.Click
        ShowKeyboard()
    End Sub
End Class