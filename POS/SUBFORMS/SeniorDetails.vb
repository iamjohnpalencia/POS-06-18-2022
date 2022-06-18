Imports MySql.Data.MySqlClient

Public Class SeniorDetails

    Public COUPONNAME As String
    Public COUPONVALUE As Double
    Public NOTDISCOUNTEDAMOUNT As Double

    Private Sub ButtonCANCEL_Click(sender As Object, e As EventArgs) Handles ButtonCANCEL.Click
        DiscAppleid = False
        GETNOTDISCOUNTEDAMOUNT = 0
        NOTDISCOUNTEDAMOUNT = 0
        Close()

    End Sub
    Private Sub SeniorDetails_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Discounts.Enabled = True
        If Not DiscAppleid Then
            GETNOTDISCOUNTEDAMOUNT = 0
            NOTDISCOUNTEDAMOUNT = 0
        End If
    End Sub
    Private Sub ButtonSubmit_Click(sender As Object, e As EventArgs) Handles ButtonSubmit.Click
        Try

            If TextboxIsEmpty(Me) Then
                Dim LimitToOne As Boolean = False
                Dim ConnectionLocal As MySqlConnection = LocalhostConn()
                Dim SQL = "SELECT senior_id FROM `loc_senior_details` WHERE senior_id = '" & Trim(TextBoxSENIORID.Text) & "' AND date_created = '" & S_Zreading & "'"
                Dim Cmd As MySqlCommand = New MySqlCommand(SQL, ConnectionLocal)
                Using reader As MySqlDataReader = Cmd.ExecuteReader
                    If reader.HasRows Then
                        LimitToOne = True
                    Else
                        LimitToOne = False
                    End If
                End Using
                If LimitToOne Then
                    MessageBox.Show("Benefit limit reached for the day. Please use another ID. Thank you", "NOTICE", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    DiscAppleid = True
                    If SeniorDetailsID = "" Then
                        SeniorDetailsID = Trim(TextBoxSENIORID.Text)
                    Else
                        SeniorDetailsID &= "-" & Trim(TextBoxSENIORID.Text)
                    End If
                    If SeniorDetailsName = "" Then
                        SeniorDetailsName = Trim(TextBoxSENIORNAME.Text)
                    Else
                        SeniorDetailsName &= "-" & Trim(TextBoxSENIORNAME.Text)
                    End If
                    If SeniorPhoneNumber = "" Then
                        SeniorPhoneNumber = Trim(TextBoxPhoneNumber.Text)
                    Else
                        SeniorPhoneNumber &= "-" & Trim(TextBoxPhoneNumber.Text)
                    End If
                    DISCGUESTCOUNT = Double.Parse(TextBoxNumberOfGuest.Text)
                    DISCIDCOUNT = Double.Parse(TextBoxNumberOfID.Text)
                    ApplyDiscount()
                    Close()
                    Discounts.Close()
                End If
            Else
                MessageBox.Show("Fill up all the blanks.", "NOTICE", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Senior Details: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub TextBoxNumberOfGuest_KeyPress(sender As Object, e As KeyPressEventArgs)
        Try
            Numeric(sender, e)
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Private Sub ApplyDiscount()
        Try
            Dim SRTTLDiscountedAmount As Double = 0
            Dim SRDiscountValue As Double = COUPONVALUE
            Dim Tax = 1 + Val(S_Tax)

            Dim SRLESSVAT As Double = 0
            Dim TTLDISCOUNTEDAMOUNT As Double = 0
            Dim TTLVATEXEMPT As Double = 0
            Dim TTLLESSVAT As Double = 0
            Dim TTLDISCOUNT As Double = 0

            With Discounts
                For i As Integer = 0 To .DiscountsDatatable.Rows.Count - 1 Step +1
                    SRTTLDiscountedAmount = .DiscountsDatatable(i)(2)

                    Dim SRVATEXEMPTSALES As Double = 0
                    Dim SRTOTALDISCOUNT As Double = 0
                    If S_ZeroRated = "0" Then
                        SRVATEXEMPTSALES = SRTTLDiscountedAmount / Tax
                        SRLESSVAT = SRTTLDiscountedAmount - SRVATEXEMPTSALES
                        SRTOTALDISCOUNT = SRVATEXEMPTSALES * SRDiscountValue
                    Else
                        SRTOTALDISCOUNT = SRTTLDiscountedAmount * SRDiscountValue

                    End If

                    TTLVATEXEMPT += SRVATEXEMPTSALES
                    TTLLESSVAT += SRLESSVAT
                    TTLDISCOUNT += SRTOTALDISCOUNT
                    TTLDISCOUNTEDAMOUNT += SRTTLDiscountedAmount

                    For Each row In POS.DataGridViewOrders.Rows
                        If .DiscountsDatatable(i)(3) = row.Cells("Column12").value Then
                            If COUPONNAME = "Senior Discount 20%" Then

                                row.Cells("seniordisc").value += SRTOTALDISCOUNT
                                row.Cells("seniorqty").value += 1
                            End If
                            If COUPONNAME = "PWD Discount 20%" Then

                                row.Cells("pwddisc").value += SRTOTALDISCOUNT
                                row.Cells("pwdqty").value += 1
                            End If
                            If COUPONNAME = "Sports Discount 20%" Then

                                row.Cells("athletedisc").value += SRTOTALDISCOUNT
                                row.Cells("athleteqty").value += 1
                            End If
                            If COUPONNAME = "Single Parent Discount 20%" Then

                                row.Cells("spdisc").value += SRTOTALDISCOUNT
                                row.Cells("spqty").value += 1
                            End If
                        End If
                    Next
                Next

                If S_ZeroRated = "0" Then
                    Dim SRTOTALDISCPLUSLESSVAT As Double = TTLDISCOUNT + TTLLESSVAT
                    Dim SRTOTALAMOUNTDUE As Double = Math.Round(Double.Parse(POS.TextBoxGRANDTOTAL.Text) - SRTOTALDISCPLUSLESSVAT, 2, MidpointRounding.AwayFromZero)
                    Dim SRVATABLESALES As Double = Math.Round(NOTDISCOUNTEDAMOUNT / Tax, 2, MidpointRounding.AwayFromZero)
                    Dim SRVAT12PERCENT As Double = Math.Round(NOTDISCOUNTEDAMOUNT - SRVATABLESALES, 2, MidpointRounding.AwayFromZero)
                    'MsgBox(SRTOTALAMOUNTDUE)
                    TOTALDISCOUNTEDAMOUNT = TTLDISCOUNT
                    VATEXEMPTSALES += TTLVATEXEMPT
                    LESSVAT += TTLLESSVAT
                    TOTALDISCOUNT += TTLDISCOUNT
                    VATABLESALES = SRVATABLESALES
                    VAT12PERCENT = SRVAT12PERCENT
                    TOTALAMOUNTDUE = SRTOTALAMOUNTDUE
                    ZERORATEDSALES = 0
                    ZERORATEDNETSALES = 0
                    POS.TextBoxGRANDTOTAL.Text = NUMBERFORMAT(TOTALAMOUNTDUE)
                    POS.TextBoxDISCOUNT.Text = Val(POS.TextBoxDISCOUNT.Text) + NUMBERFORMAT(TTLDISCOUNT)
                Else
                    Dim SRTOTALAMOUNTDUE As Double = Double.Parse(POS.TextBoxGRANDTOTAL.Text) - TTLDISCOUNT
                    TOTALDISCOUNTEDAMOUNT = TTLDISCOUNT
                    VATEXEMPTSALES = 0
                    LESSVAT = 0
                    TOTALDISCOUNT = TTLDISCOUNT
                    VATABLESALES = 0
                    VAT12PERCENT = 0
                    TOTALAMOUNTDUE = SRTOTALAMOUNTDUE
                    ZERORATEDSALES = POS.Label76.Text
                    ZERORATEDNETSALES = SRTOTALAMOUNTDUE
                    POS.TextBoxGRANDTOTAL.Text = NUMBERFORMAT(TOTALAMOUNTDUE)
                    POS.TextBoxDISCOUNT.Text = Val(POS.TextBoxDISCOUNT.Text) + NUMBERFORMAT(TTLDISCOUNT)
                End If

            End With

            TransactionVariables.DiscountName = COUPONNAME
            If PromoApplied Then
                SeniorGCDiscount = True
            Else
                DiscountType = COUPONNAME
            End If

        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Senior Details: " & ex.ToString, "Critical")

            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub SeniorDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'MsgBox(COUPONNAME)
        'MsgBox(COUPONVALUE)
        'MsgBox(NOTDISCOUNTEDAMOUNT)
        'MsgBox(GETNOTDISCOUNTEDAMOUNT)
    End Sub

    Private Sub ButtonKeyboard_Click(sender As Object, e As EventArgs) Handles ButtonKeyboard.Click
        ShowKeyboard()
    End Sub

    Private Sub TextBoxPhoneNumber_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBoxPhoneNumber.KeyPress
        Try
            Numeric(sender, e)
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
End Class