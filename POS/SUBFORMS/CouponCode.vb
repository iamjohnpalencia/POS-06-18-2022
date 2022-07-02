Imports MySql.Data.MySqlClient
Imports System.Threading
Public Class CouponCode
    Dim ThreadList As List(Of Thread) = New List(Of Thread)
    Dim Thread As Thread
    Public APPLYPROMO As Boolean = True

    Private Sub CouponCode_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            CheckForIllegalCrossThreadCalls = False
            BackgroundWorker1.WorkerReportsProgress = True
            BackgroundWorker1.WorkerSupportsCancellation = True
            BackgroundWorker1.RunWorkerAsync()
        Catch ex As Exception
            AuditTrail.LogToAuditTrail("System", "CouponCode/Load: " & ex.ToString, "Critical")
        End Try
    End Sub
    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Try
            Thread = New Thread(AddressOf LoadCoupons)
            Thread.Start()
            ThreadList.Add(Thread)
            For Each t In ThreadList
                t.Join()
                If (BackgroundWorker1.CancellationPending) Then
                    ' Indicate that the task was canceled.
                    e.Cancel = True
                    Exit For
                End If
            Next
        Catch ex As Exception
            AuditTrail.LogToAuditTrail("System", "CouponCode/BackgroundWorker1: " & ex.ToString, "Critical")
        End Try
    End Sub
    Private Sub LoadCoupons(sender As Object)
        Try
            Dim Table As String = "tbcoupon WHERE active = 1 ORDER BY Type ASC"
            If APPLYPROMO Then
                Table = "tbcoupon WHERE active = 1 AND Type <> 'Percentage(w/o vat)' ORDER BY Type ASC"
            Else
                Table = "tbcoupon WHERE active = 1 AND Type = 'Percentage(w/o vat)' ORDER BY Type ASC"
            End If
            Dim LoadCouponTable = AsDatatableFontIncrease(Table, "*", DataGridViewCoupons)
            For Each row As DataRow In LoadCouponTable.Rows
                DataGridViewCoupons.DefaultCellStyle.Font = New Font("Tahoma", 15)
                DataGridViewCoupons.Height = 40
                DataGridViewCoupons.Rows.Add(row("ID"), row("Couponname_"), row("Desc_"), row("Discountvalue_"), row("Referencevalue_"), row("Type"), row("Bundlebase_"), row("BBValue_"), row("Bundlepromo_"), row("BPValue_"), row("Effectivedate"), row("Expirydate"))
            Next
        Catch ex As Exception
            AuditTrail.LogToAuditTrail("System", "CouponCode/LoadCoupons(): " & ex.ToString, "Critical")
        End Try
    End Sub
    Private Sub CouponCode_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        POS.Enabled = True
    End Sub
    Private Sub PromoCodeDefault()
        Try
            PromoApplied = False
            PromoName = ""
            PromoDesc = ""
            PromoLine = 10
            PromoTotal = 0
            PromoGCValue = 0

            With POS
                For i As Integer = 0 To .DataGridViewOrders.Rows.Count - 1 Step +1
                    If .DataGridViewOrders.Rows(i).Cells(11).Value > 0 Then
                        Dim priceadd = .DataGridViewOrders.Rows(i).Cells(11).Value * S_Upgrade_Price
                        If S_ZeroRated = "0" Then
                            .DataGridViewOrders.Rows(i).Cells(3).Value = NUMBERFORMAT(.DataGridViewOrders.Rows(i).Cells(1).Value * .DataGridViewOrders.Rows(i).Cells(2).Value + priceadd)
                        Else
                            Dim TotalPrice As Double = 0
                            Dim Tax = 1 + Val(S_ZeroRated_Tax)
                            Dim Total = Math.Round(.DataGridViewOrders.Rows(i).Cells(1).Value * .DataGridViewOrders.Rows(i).Cells(2).Value + priceadd / Tax, 2, MidpointRounding.AwayFromZero)
                            .DataGridViewOrders.Rows(i).Cells(3).Value = NUMBERFORMAT(Total)
                        End If
                    Else
                        If S_ZeroRated = "0" Then
                            .DataGridViewOrders.Rows(i).Cells(3).Value = NUMBERFORMAT(.DataGridViewOrders.Rows(i).Cells(1).Value * .DataGridViewOrders.Rows(i).Cells(2).Value)
                        Else
                            Dim TotalPrice As Double = 0
                            Dim Tax = 1 + Val(S_ZeroRated_Tax)
                            Dim Total = Math.Round(.DataGridViewOrders.Rows(i).Cells(1).Value * .DataGridViewOrders.Rows(i).Cells(2).Value / Tax, 2, MidpointRounding.AwayFromZero)
                            .DataGridViewOrders.Rows(i).Cells(3).Value = NUMBERFORMAT(Total)
                        End If
                    End If
                Next
            End With
        Catch ex As Exception
            AuditTrail.LogToAuditTrail("System", "CouponCode/PromoCodeDefault(): " & ex.ToString, "Critical")
        End Try
    End Sub
    Private Sub ButtonSubmit_Click(sender As Object, e As EventArgs) Handles ButtonSubmit.Click
        Try

            If APPLYPROMO Then
                Dim CountItem As Integer = 0
                With POS
                    For i As Integer = 0 To .DataGridViewOrders.Rows.Count - 1 Step +1
                        CountItem += .DataGridViewOrders.Rows(i).Cells(1).Value
                    Next
                End With
                If CountItem < 1 Then
                    MsgBox("Cannot apply coupon! Minimum product quantity is 1", vbInformation)
                    Exit Sub
                    'ElseIf Me.DataGridViewCoupons.Item(5, Me.DataGridViewCoupons.CurrentRow.Index).Value.ToString = "Percentage(w/o vat)" Then
                    '    Enabled = False
                    '    SeniorDetails.Show()
                ElseIf Me.DataGridViewCoupons.Item(5, Me.DataGridViewCoupons.CurrentRow.Index).Value.ToString = "Percentage(w/ vat)" Then
                    coupondiscountpercentagewvat()
                ElseIf Me.DataGridViewCoupons.Item(5, Me.DataGridViewCoupons.CurrentRow.Index).Value.ToString = "Fix-1" Then
                    ' MsgBox("Coupon is " & Me.DataGridViewCoupons.Item(5, Me.DataGridViewCoupons.CurrentRow.Index).Value)
                    couponfix1()
                ElseIf Me.DataGridViewCoupons.Item(5, Me.DataGridViewCoupons.CurrentRow.Index).Value.ToString = "Fix-2" Then
                    '  MsgBox("Coupon is " & Me.DataGridViewCoupons.Item(5, Me.DataGridViewCoupons.CurrentRow.Index).Value)
                    couponfix2()
                ElseIf Me.DataGridViewCoupons.Item(5, Me.DataGridViewCoupons.CurrentRow.Index).Value.ToString = "Bundle-1(Fix)" Then
                    ' MsgBox("Coupon is " & Me.DataGridViewCoupons.Item(5, Me.DataGridViewCoupons.CurrentRow.Index).Value)
                    couponbundle1()
                ElseIf Me.DataGridViewCoupons.Item(5, Me.DataGridViewCoupons.CurrentRow.Index).Value.ToString = "Bundle-2(Fix)" Then
                    '   MsgBox("Coupon is " & Me.DataGridViewCoupons.Item(5, Me.DataGridViewCoupons.CurrentRow.Index).Value)
                    couponbundle2()
                ElseIf Me.DataGridViewCoupons.Item(5, Me.DataGridViewCoupons.CurrentRow.Index).Value.ToString = "Bundle-3(%)" Then
                    '  MsgBox("Coupon is " & Me.DataGridViewCoupons.Item(5, Me.DataGridViewCoupons.CurrentRow.Index).Value)
                    couponbundle3()
                ElseIf Me.DataGridViewCoupons.Item(5, Me.DataGridViewCoupons.CurrentRow.Index).Value.ToString = "Bundle-4(Fix)" Then
                    '  MsgBox("Coupon is " & Me.DataGridViewCoupons.Item(5, Me.DataGridViewCoupons.CurrentRow.Index).Value)
                    couponbundle4()
                End If
            Else
                Dim DiscountValue = DataGridViewCoupons.SelectedRows(0).Cells(3).Value / 100
                Discounts.COUPONNAME = Me.DataGridViewCoupons.Item(1, Me.DataGridViewCoupons.CurrentRow.Index).Value.ToString
                Discounts.COUPONVALUE = DiscountValue
                PromoType = Me.DataGridViewCoupons.Item(5, Me.DataGridViewCoupons.CurrentRow.Index).Value.ToString
                Discounts.Show()
                Enabled = False
            End If

            'Compute()
        Catch ex As Exception
            AuditTrail.LogToAuditTrail("System", "CouponCode/ButtonSubmit: " & ex.ToString, "Critical")
        End Try
    End Sub
    Public Sub coupondiscountpercentagewvat()
        Try
            If S_ZeroRated = "0" Then
                PromoCodeDefault()

                With POS
                    Dim PRGROSSSALES As Double = Double.Parse(.Label76.Text)
                    Dim PRTAX As Double = 1 + Val(S_Tax)
                    Dim PRTOTALDISCOUNT As Double = DataGridViewCoupons.SelectedRows(0).Cells(3).Value / 100
                    Dim PRTOTALAMOUNTDUE As Double = 0

                    PRTOTALDISCOUNT = PRGROSSSALES * PRTOTALDISCOUNT
                    PRTOTALAMOUNTDUE = Format(PRGROSSSALES - PRTOTALDISCOUNT, "0.00")

                    Dim PRVATABLESALES As Double = Format(PRGROSSSALES / PRTAX, "0.00")
                    Dim PRVAT12PERCENT As Double = Format(PRVATABLESALES * PRTAX, "0.00")

                    '.GROSSSALE = GROSSSALES
                    TOTALDISCOUNTEDAMOUNT = NUMBERFORMAT(PRGROSSSALES)
                    VATEXEMPTSALES = 0
                    LESSVAT = 0
                    TOTALDISCOUNT = NUMBERFORMAT(PRTOTALDISCOUNT)
                    VATABLESALES = NUMBERFORMAT(PRVATABLESALES)
                    VAT12PERCENT = NUMBERFORMAT(PRVAT12PERCENT)
                    TOTALAMOUNTDUE = NUMBERFORMAT(PRTOTALAMOUNTDUE)
                    .TextBoxGRANDTOTAL.Text = NUMBERFORMAT(PRTOTALAMOUNTDUE)
                    .TextBoxDISCOUNT.Text = NUMBERFORMAT(PRTOTALDISCOUNT)
                    PromoDesc = ""
                    PromoTotal = NUMBERFORMAT(PRTOTALDISCOUNT)
                    PromoApplied = True
                    PromoName = Me.DataGridViewCoupons.Item(1, Me.DataGridViewCoupons.CurrentRow.Index).Value.ToString


                End With
            Else
                PromoCodeDefault()
                With POS
                    Dim PRGROSSSALES As Double = Double.Parse(.Label76.Text)
                    Dim PRTAX As Double = 1 + Val(S_ZeroRated_Tax)
                    Dim PRTOTALDISCOUNT As Double = DataGridViewCoupons.SelectedRows(0).Cells(3).Value / 100
                    Dim PRTOTALAMOUNTDUE As Double = 0

                    PRTOTALDISCOUNT = PRGROSSSALES * PRTOTALDISCOUNT
                    PRTOTALAMOUNTDUE = Format(PRGROSSSALES - PRTOTALDISCOUNT, "0.00")

                    'Dim VATABLESALES As Double = Format(GROSSSALES / TAX, "0.00")
                    'Dim VAT12PERCENT As Double = Format(VATABLESALES * S_Tax, "0.00")

                    '.GROSSSALE = GROSSSALES
                    TOTALDISCOUNTEDAMOUNT = NUMBERFORMAT(PRGROSSSALES)
                    VATEXEMPTSALES = 0
                    LESSVAT = 0
                    TOTALDISCOUNT = NUMBERFORMAT(PRTOTALDISCOUNT)
                    VATABLESALES = NUMBERFORMAT(Double.Parse(.Label76.Text))
                    VAT12PERCENT = 0
                    TOTALAMOUNTDUE = NUMBERFORMAT(PRTOTALAMOUNTDUE)
                    .TextBoxGRANDTOTAL.Text = NUMBERFORMAT(PRTOTALAMOUNTDUE)
                    .TextBoxDISCOUNT.Text = NUMBERFORMAT(PRTOTALDISCOUNT)

                    ZERORATEDSALES = NUMBERFORMAT(Double.Parse(.Label76.Text))
                    ZERORATEDNETSALES = NUMBERFORMAT(Double.Parse(.Label76.Text) - PRTOTALDISCOUNT)
                    .TextBoxGRANDTOTAL.Text = NUMBERFORMAT(Double.Parse(.Label76.Text) - PRTOTALDISCOUNT)

                    PromoDesc = ""
                    PromoTotal = NUMBERFORMAT(PRTOTALDISCOUNT)
                    PromoApplied = True
                    PromoName = Me.DataGridViewCoupons.Item(1, Me.DataGridViewCoupons.CurrentRow.Index).Value.ToString
                    MsgBox("Applied")
                End With
            End If

            PromoType = Me.DataGridViewCoupons.Item(5, Me.DataGridViewCoupons.CurrentRow.Index).Value.ToString

            Close()
        Catch ex As Exception
            AuditTrail.LogToAuditTrail("System", "CouponCode/coupondiscountpercentagewvat(): " & ex.ToString, "Critical")
        End Try
    End Sub
    Private Sub couponfix1()
        Try
            Dim PRGROSSSALES As Double = Double.Parse(POS.TextBoxGRANDTOTAL.Text)
            Dim PRTOTALDISCOUNT As Double = DataGridViewCoupons.SelectedRows(0).Cells(3).Value
            Dim PRDISCOUNTAMOUNT As Double = 0
            Dim PRLESSVAT As Double = 0
            Dim PRVATABLE As Double = 0
            Dim PRTOTALAMOUNTDUE As Double = 0
            Dim PRTax = 1 + Val(S_Tax)
            Dim PRVATABLESALES As Double = 0
            Dim PRVAT12PERCENT As Double = 0
            'SeniorGCDiscount = True
            With POS
                'MsgBox(PRTOTALDISCOUNT)
                'MsgBox(PRGROSSSALES)
                'MsgBox(SeniorGCDiscount)
                If DiscAppleid Then
                    SeniorGCDiscount = True
                End If
                If Double.Parse(PRTOTALDISCOUNT) < PRGROSSSALES Then
                    If SeniorGCDiscount = False Then
                        PromoCodeDefault()
                        If S_ZeroRated = "1" Then
                            PRTOTALAMOUNTDUE = Val(POS.TextBoxGRANDTOTAL.Text) - PRTOTALDISCOUNT
                            PRVATABLESALES = .Label76.Text
                            PRVAT12PERCENT = 0
                        Else
                            PRTOTALAMOUNTDUE = PRGROSSSALES - PRTOTALDISCOUNT
                            PRVATABLESALES = Format(PRGROSSSALES / PRTax, "0.00")
                            PRVAT12PERCENT = Format(PRVATABLESALES * S_Tax, "0.00")
                        End If
                        '.GROSSSALE = GROSSSALES
                        TOTALDISCOUNTEDAMOUNT = NUMBERFORMAT(PRGROSSSALES)
                        VATEXEMPTSALES = 0
                        LESSVAT = 0
                        TOTALDISCOUNT = NUMBERFORMAT(PRTOTALDISCOUNT)
                        VATABLESALES = NUMBERFORMAT(PRVATABLESALES)
                        VAT12PERCENT = NUMBERFORMAT(PRVAT12PERCENT)
                        TOTALAMOUNTDUE = NUMBERFORMAT(PRTOTALAMOUNTDUE)
                        .TextBoxGRANDTOTAL.Text = NUMBERFORMAT(PRTOTALAMOUNTDUE)
                        .TextBoxDISCOUNT.Text = NUMBERFORMAT(PRTOTALDISCOUNT)
                        If S_ZeroRated = "1" Then
                            ZERORATEDSALES = NUMBERFORMAT(PRVATABLESALES)
                            ZERORATEDNETSALES = NUMBERFORMAT(PRTOTALAMOUNTDUE)
                        End If
                        PromoDesc = ""
                        PromoTotal = NUMBERFORMAT(PRTOTALDISCOUNT)
                        PromoApplied = True
                        PromoName = Me.DataGridViewCoupons.Item(1, Me.DataGridViewCoupons.CurrentRow.Index).Value.ToString

                    Else
                        If S_ZeroRated = "1" Then
                            PRTOTALAMOUNTDUE = NUMBERFORMAT(PRGROSSSALES - PRTOTALDISCOUNT)
                            PRVATABLESALES = NUMBERFORMAT(Double.Parse(.Label76.Text))
                            PRVAT12PERCENT = 0
                        Else
                            PRTOTALAMOUNTDUE = NUMBERFORMAT(PRGROSSSALES - PRTOTALDISCOUNT)
                            PRVATABLESALES = Format(PRGROSSSALES / PRTax, "0.00")
                            PRVAT12PERCENT = Format(PRVATABLESALES * S_Tax, "0.00")
                        End If

                        .TextBoxDISCOUNT.Text = Val(.TextBoxDISCOUNT.Text) + PRTOTALDISCOUNT
                        TOTALDISCOUNT = NUMBERFORMAT(TOTALDISCOUNT + PRTOTALDISCOUNT)
                        TOTALAMOUNTDUE = NUMBERFORMAT(TOTALAMOUNTDUE - PRTOTALDISCOUNT)
                        If S_ZeroRated = "1" Then
                            ZERORATEDSALES = NUMBERFORMAT(PRVATABLESALES)
                            ZERORATEDNETSALES = NUMBERFORMAT(PRTOTALAMOUNTDUE)
                        End If
                        .TextBoxGRANDTOTAL.Text = NUMBERFORMAT(Val(.TextBoxGRANDTOTAL.Text) - PRTOTALDISCOUNT)
                        PromoTotal = NUMBERFORMAT(PRTOTALDISCOUNT)
                        PromoDesc = ""
                        PromoApplied = True
                        PromoName = Me.DataGridViewCoupons.Item(1, Me.DataGridViewCoupons.CurrentRow.Index).Value.ToString
                    End If
                Else
                    If SeniorGCDiscount = False Then
                        PromoCodeDefault()
                        If S_ZeroRated = "1" Then
                            PRTOTALAMOUNTDUE = 0
                            PRVAT12PERCENT = 0
                            PRVATABLESALES = Format(PRGROSSSALES, "0.00")
                        Else
                            PRTOTALAMOUNTDUE = NUMBERFORMAT(PRGROSSSALES - PRTOTALDISCOUNT)
                            PRVATABLESALES = Format(PRGROSSSALES / PRTax, "0.00")
                            PRVAT12PERCENT = Format(PRGROSSSALES - PRVATABLESALES, "0.00")
                        End If
                        PRTOTALDISCOUNT = NUMBERFORMAT(Val(POS.TextBoxGRANDTOTAL.Text))
                        PRTOTALAMOUNTDUE = 0
                        GROSSSALE = NUMBERFORMAT(PRGROSSSALES)
                        TOTALDISCOUNTEDAMOUNT = NUMBERFORMAT(PRGROSSSALES)
                        VATEXEMPTSALES = 0
                        LESSVAT = 0
                        TOTALDISCOUNT = NUMBERFORMAT(PRTOTALDISCOUNT)
                        VATABLESALES = NUMBERFORMAT(PRVATABLESALES)
                        VAT12PERCENT = NUMBERFORMAT(PRVAT12PERCENT)
                        TOTALAMOUNTDUE = NUMBERFORMAT(PRTOTALAMOUNTDUE)
                        .TextBoxGRANDTOTAL.Text = NUMBERFORMAT(0)
                        .TextBoxDISCOUNT.Text = NUMBERFORMAT(PRTOTALDISCOUNT)
                        If S_ZeroRated = "1" Then
                            ZERORATEDSALES = NUMBERFORMAT(PRVATABLESALES)
                            ZERORATEDNETSALES = NUMBERFORMAT(PRTOTALAMOUNTDUE)
                        End If
                        PromoDesc = ""
                        PromoTotal = NUMBERFORMAT(PRTOTALDISCOUNT)
                        PromoApplied = True
                        PromoName = Me.DataGridViewCoupons.Item(1, Me.DataGridViewCoupons.CurrentRow.Index).Value.ToString

                    Else
                        'Dim TotalDiscounted As Double = Double.Parse(.TextBoxDISCOUNT.Text) + PRTOTALDISCOUNT
                        Dim TotalDiscounted As Double = Double.Parse(.TextBoxSUBTOTAL.Text)
                        GROSSSALE = 0
                        TOTALDISCOUNTEDAMOUNT = NUMBERFORMAT(PRGROSSSALES)
                        TOTALDISCOUNT = NUMBERFORMAT(TotalDiscounted)
                        TOTALAMOUNTDUE = 0
                        .TextBoxGRANDTOTAL.Text = 0
                        .TextBoxDISCOUNT.Text = NUMBERFORMAT(TotalDiscounted)
                        PromoDesc = ""
                        PromoTotal = NUMBERFORMAT(TotalDiscounted)
                        PromoApplied = True
                        PromoName = Me.DataGridViewCoupons.Item(1, Me.DataGridViewCoupons.CurrentRow.Index).Value.ToString

                    End If
                End If
            End With
            If Not SeniorGCDiscount Then
                PromoType = Me.DataGridViewCoupons.Item(5, Me.DataGridViewCoupons.CurrentRow.Index).Value.ToString
            Else
                PromoType = Me.DataGridViewCoupons.Item(5, Me.DataGridViewCoupons.CurrentRow.Index).Value.ToString
            End If
            PromoGCValue = DataGridViewCoupons.SelectedRows(0).Cells(3).Value
            Close()
        Catch ex As Exception
            AuditTrail.LogToAuditTrail("System", "CouponCode/couponfix1(): " & ex.ToString, "Critical")
        End Try
    End Sub

    Private Sub couponfix2()
        Try
            With POS
                If Double.Parse(.Label76.Text) < Double.Parse(Me.DataGridViewCoupons.Item(4, Me.DataGridViewCoupons.CurrentRow.Index).Value) Then
                    MsgBox("Condition not meet")
                    PromoApplied = False
                Else
                    Dim PRGROSSSALES As Double = POS.Label76.Text
                    Dim PRTAX As Double = 1 + Val(S_Tax)
                    Dim PRTOTALDISCOUNT As Double = DataGridViewCoupons.SelectedRows(0).Cells(3).Value
                    Dim PRTOTALAMOUNTDUE As Double = 0

                    PRTOTALAMOUNTDUE = Format(PRGROSSSALES - PRTOTALDISCOUNT, "0.00")
                    Dim PRVATABLESALES As Double = 0
                    Dim PRVAT12PERCENT As Double = 0
                    Dim PRLESSVAT As Double = 0

                    If S_ZeroRated = "1" Then
                        Dim PRTotalPrice As Double = 0
                        Dim PRGrandTotal As Double = 0
                        With POS.DataGridViewOrders
                            For i As Integer = 0 To .Rows.Count - 1 Step +1
                                PRTotalPrice = .Rows(i).Cells(1).Value * .Rows(i).Cells(2).Value
                                PRGrandTotal += PRTotalPrice
                            Next
                        End With

                        Dim SubTotal = Convert.ToDecimal(Double.Parse(POS.TextBoxSUBTOTAL.Text))
                        PRLESSVAT = Math.Round(PRGrandTotal - SubTotal, 2, MidpointRounding.AwayFromZero)

                        PRVATABLESALES = 0
                        PRVAT12PERCENT = 0
                        ZERORATEDSALES = .Label76.Text
                        ZERORATEDNETSALES = PRTOTALAMOUNTDUE
                    Else
                        PRVATABLESALES = Format(PRGROSSSALES / PRTAX, "0.00")
                        PRVAT12PERCENT = Format(PRVATABLESALES * S_Tax, "0.00")
                        ZERORATEDSALES = 0
                        ZERORATEDNETSALES = 0

                    End If

                    '.GROSSSALE = GROSSSALES
                    TOTALDISCOUNTEDAMOUNT = NUMBERFORMAT(PRGROSSSALES)
                    VATEXEMPTSALES = 0
                    LESSVAT = NUMBERFORMAT(PRLESSVAT)
                    TOTALDISCOUNT = NUMBERFORMAT(PRTOTALDISCOUNT)
                    VATABLESALES = NUMBERFORMAT(PRVATABLESALES)
                    VAT12PERCENT = NUMBERFORMAT(PRVAT12PERCENT)
                    TOTALAMOUNTDUE = NUMBERFORMAT(PRTOTALAMOUNTDUE)

                    .TextBoxGRANDTOTAL.Text = NUMBERFORMAT(PRTOTALAMOUNTDUE)
                    .TextBoxDISCOUNT.Text = NUMBERFORMAT(PRTOTALDISCOUNT)
                    PromoDesc = ""
                    PromoTotal = NUMBERFORMAT(PRTOTALDISCOUNT)
                    PromoApplied = True
                    PromoName = Me.DataGridViewCoupons.Item(1, Me.DataGridViewCoupons.CurrentRow.Index).Value.ToString
                    MsgBox("Applied")
                End If
            End With
            PromoType = Me.DataGridViewCoupons.Item(5, Me.DataGridViewCoupons.CurrentRow.Index).Value.ToString
            Close()
        Catch ex As Exception
            AuditTrail.LogToAuditTrail("System", "CouponCode/couponfix2(): " & ex.ToString, "Critical")
        End Try
    End Sub
    Private Sub couponbundle1()
        Try
            With POS
                Dim ReferenceExist As Boolean = False
                Dim referenceID As String = Me.DataGridViewCoupons.Item(6, Me.DataGridViewCoupons.CurrentRow.Index).Value.ToString

                Dim refIds As String() = referenceID.Split(New Char() {","c})
                'Total Discount
                Dim PRDISCOUNTEDAMOUNT As Double = 0
                Dim PRGROSSSALES As Double = .Label76.Text
                Dim PRVATABLESALES As Double = 0

                Try
                    For Each getRefids In refIds
                        For i As Integer = 0 To .DataGridViewOrders.Rows.Count - 1 Step +1
                            If .DataGridViewOrders.Rows(i).Cells(5).Value.ToString.Contains(getRefids) = True Then
                                If .DataGridViewOrders.Rows(i).Cells(1).Value >= Me.DataGridViewCoupons.Item(7, Me.DataGridViewCoupons.CurrentRow.Index).Value Then

                                    ReferenceExist = True
                                    Exit Try
                                Else
                                    PromoCodeDefault()
                                End If
                            Else
                                ReferenceExist = False
                            End If
                        Next
                    Next

                Catch ex As Exception

                End Try
                Dim PRTOTALAMOUNTDUE As Double = 0

                Dim SUBTOTAL As Double = 0
                If S_ZeroRated = "1" Then
                    SUBTOTAL = Val(POS.TextBoxGRANDTOTAL.Text)
                Else
                    SUBTOTAL = Val(POS.Label76.Text)
                End If

                Dim QtyCondMeet As Boolean = True
                Dim TotalPrice As Double = 0
                Dim BundlepromoID As String = Me.DataGridViewCoupons.Item(8, Me.DataGridViewCoupons.CurrentRow.Index).Value.ToString
                Dim bundIds As String() = BundlepromoID.Split(New Char() {","c})
                Dim BundleIDExist As Boolean = False
                If ReferenceExist = True Then
                    For Each getBundleids In bundIds
                        For i As Integer = 0 To .DataGridViewOrders.Rows.Count - 1 Step +1
                            If POS.DataGridViewOrders.Rows(i).Cells(5).Value.ToString.Contains(getBundleids) = True Then
                                PRDISCOUNTEDAMOUNT = .DataGridViewOrders.Rows(i).Cells(2).Value
                                TotalPrice += .DataGridViewOrders.Rows(i).Cells(3).Value
                                PRTOTALAMOUNTDUE = SUBTOTAL - PRDISCOUNTEDAMOUNT
                                If POS.DataGridViewOrders.Rows(i).Cells(1).Value >= Me.DataGridViewCoupons.SelectedRows(0).Cells(9).Value Then
                                    Dim GetDiscount As Double = 0
                                    Dim OrgPrice = POS.DataGridViewOrders.Rows(i).Cells(2).Value
                                    Dim Qty = Me.DataGridViewCoupons.SelectedRows(0).Cells(9).Value
                                    Dim TotalLess = OrgPrice * Qty
                                    Dim TotalZeroRatedPrice As Double = 0
                                    Dim PRVAT12PERCENT As Double = 0
                                    If S_ZeroRated = "1" Then
                                        Dim PRTAX As Double = 1 + Val(S_ZeroRated_Tax)
                                        GetDiscount = Math.Round(POS.DataGridViewOrders.Rows(i).Cells(2).Value / PRTAX, 2, MidpointRounding.AwayFromZero)
                                        TotalZeroRatedPrice = GetDiscount * DataGridViewCoupons.SelectedRows(0).Cells(7).Value
                                        ZERORATEDSALES = NUMBERFORMAT(Double.Parse(.Label76.Text))
                                        ZERORATEDNETSALES = NUMBERFORMAT(PRTOTALAMOUNTDUE)
                                        .TextBoxGRANDTOTAL.Text = NUMBERFORMAT(Val(.TextBoxGRANDTOTAL.Text) - TotalZeroRatedPrice)
                                        .TextBoxDISCOUNT.Text = NUMBERFORMAT(GetDiscount)
                                        PRVAT12PERCENT = 0
                                        PRVATABLESALES = 0
                                    Else
                                        Dim PRTAX As Double = 1 + Val(S_Tax)
                                        PRVATABLESALES = NUMBERFORMAT(PRGROSSSALES / PRTAX)
                                        PRVAT12PERCENT = NUMBERFORMAT(PRVATABLESALES * S_Tax)
                                        .TextBoxGRANDTOTAL.Text = NUMBERFORMAT(PRTOTALAMOUNTDUE)
                                        .TextBoxDISCOUNT.Text = NUMBERFORMAT(PRDISCOUNTEDAMOUNT)
                                    End If

                                    '.GROSSSALE = GROSSSALES
                                    TOTALDISCOUNTEDAMOUNT = NUMBERFORMAT(PRDISCOUNTEDAMOUNT)
                                    VATEXEMPTSALES = 0
                                    LESSVAT = 0
                                    TOTALDISCOUNT = NUMBERFORMAT(PRDISCOUNTEDAMOUNT)
                                    VATABLESALES = NUMBERFORMAT(PRVATABLESALES)
                                    VAT12PERCENT = NUMBERFORMAT(PRVAT12PERCENT)
                                    TOTALAMOUNTDUE = NUMBERFORMAT(PRTOTALAMOUNTDUE)

                                    PromoDesc = ""
                                    PromoTotal = NUMBERFORMAT(PRDISCOUNTEDAMOUNT)
                                    PromoApplied = True
                                    BundleIDExist = True
                                    PromoName = Me.DataGridViewCoupons.Item(1, Me.DataGridViewCoupons.CurrentRow.Index).Value.ToString
                                Else
                                    PromoCodeDefault()
                                    PromoApplied = False
                                    BundleIDExist = False
                                    Exit For
                                End If
                            End If
                        Next
                    Next
                    If BundleIDExist = False Then
                        MsgBox("Please select bundle promo")
                    End If
                Else
                    PromoCodeDefault()
                    MsgBox("Condition not meet")
                End If
            End With

            PromoType = Me.DataGridViewCoupons.Item(5, Me.DataGridViewCoupons.CurrentRow.Index).Value.ToString

            Close()
        Catch ex As Exception
            AuditTrail.LogToAuditTrail("System", "CouponCode/couponbundle1(): " & ex.ToString, "Critical")
        End Try
    End Sub
    Private Sub couponbundle2()
        Try
            PromoCodeDefault()
            Dim ReferenceExist As Boolean = False
            Dim PRGROSSSALES As Double = 0
            Dim PRVATABLESALES As Double = 0
            Dim PRVAT12PERCENT As Double = 0
            Dim PRTAX As Double = 1 + Val(S_Tax)
            Dim referenceID As String = DataGridViewCoupons.Item(6, Me.DataGridViewCoupons.CurrentRow.Index).Value.ToString
            Dim PRTOTALDISCOUNT As Double = DataGridViewCoupons.SelectedRows(0).Cells(3).Value
            Dim refIds As String() = referenceID.Split(New Char() {","c})
            With POS
                Try
                    For Each getRefids In refIds
                        For i As Integer = 0 To .DataGridViewOrders.Rows.Count - 1 Step +1
                            If POS.DataGridViewOrders.Rows(i).Cells(5).Value.ToString.Contains(getRefids) = True Then
                                If POS.DataGridViewOrders.Rows(i).Cells(1).Value >= Me.DataGridViewCoupons.Item(7, Me.DataGridViewCoupons.CurrentRow.Index).Value Then
                                    ReferenceExist = True
                                    Exit Try
                                Else
                                    PromoCodeDefault()
                                End If
                            Else
                                ReferenceExist = False
                            End If
                        Next
                    Next
                Catch ex As Exception

                End Try
                Dim PRTOTALAMOUNTDUE As Double = 0
                For i As Integer = 0 To .DataGridViewOrders.Rows.Count - 1 Step +1
                    PRGROSSSALES += .DataGridViewOrders.Rows(i).Cells(3).Value
                Next
                PRTOTALAMOUNTDUE = PRGROSSSALES - PRTOTALDISCOUNT
                PRVATABLESALES = PRGROSSSALES / PRTAX
                Dim QtyCondMeet As Boolean = True
                Dim TotalPrice As Integer = 0
                Dim BundlepromoID As String = Me.DataGridViewCoupons.Item(8, Me.DataGridViewCoupons.CurrentRow.Index).Value.ToString
                Dim bundIds As String() = BundlepromoID.Split(New Char() {","c})
                Dim BundleIDExist As Boolean = False
                If ReferenceExist = True Then
                    Try
                        For Each getBundleids In bundIds
                            For i As Integer = 0 To .DataGridViewOrders.Rows.Count - 1 Step +1
                                If POS.DataGridViewOrders.Rows(i).Cells(5).Value.ToString.Contains(getBundleids) = True Then
                                    If POS.DataGridViewOrders.Rows(i).Cells(1).Value >= Me.DataGridViewCoupons.SelectedRows(0).Cells(9).Value Then


                                        If S_ZeroRated = "1" Then
                                            PRVATABLESALES = NUMBERFORMAT(Double.Parse(.Label76.Text))
                                            ZERORATEDSALES = NUMBERFORMAT(PRVATABLESALES)
                                            ZERORATEDNETSALES = NUMBERFORMAT(PRTOTALAMOUNTDUE)

                                            .TextBoxGRANDTOTAL.Text = NUMBERFORMAT(Val(.TextBoxGRANDTOTAL.Text) - PRTOTALDISCOUNT)
                                            .TextBoxDISCOUNT.Text = NUMBERFORMAT(PRTOTALDISCOUNT)
                                            TOTALAMOUNTDUE = NUMBERFORMAT(Val(.TextBoxGRANDTOTAL.Text))
                                        Else
                                            .TextBoxGRANDTOTAL.Text = NUMBERFORMAT(PRTOTALAMOUNTDUE)
                                            .TextBoxDISCOUNT.Text = NUMBERFORMAT(PRTOTALDISCOUNT)
                                            TOTALAMOUNTDUE = NUMBERFORMAT(PRTOTALAMOUNTDUE)
                                            PRVATABLESALES = NUMBERFORMAT(PRVATABLESALES)
                                            PRVAT12PERCENT = NUMBERFORMAT(PRVATABLESALES * S_Tax)
                                        End If

                                        GROSSSALE = NUMBERFORMAT(PRGROSSSALES)
                                        TOTALDISCOUNTEDAMOUNT = NUMBERFORMAT(PRTOTALAMOUNTDUE)
                                        VATEXEMPTSALES = 0
                                        LESSVAT = 0
                                        TOTALDISCOUNT = NUMBERFORMAT(PRTOTALDISCOUNT)
                                        VATABLESALES = NUMBERFORMAT(PRVATABLESALES)
                                        VAT12PERCENT = NUMBERFORMAT(PRVAT12PERCENT)

                                        PromoDesc = ""
                                        PromoTotal = NUMBERFORMAT(PRTOTALDISCOUNT)
                                        PromoApplied = True
                                        PromoName = Me.DataGridViewCoupons.Item(1, Me.DataGridViewCoupons.CurrentRow.Index).Value.ToString
                                        BundleIDExist = True
                                        Exit Try
                                    Else
                                        PromoApplied = False
                                        BundleIDExist = False
                                    End If
                                Else

                                End If
                            Next
                        Next
                    Catch ex As Exception

                    End Try
                    If BundleIDExist = False Then
                        MsgBox("Please select bundle promo")
                    End If
                Else
                    MsgBox("Condition not meet")
                End If
            End With
            PromoType = Me.DataGridViewCoupons.Item(5, Me.DataGridViewCoupons.CurrentRow.Index).Value.ToString
            Close()
        Catch ex As Exception
            AuditTrail.LogToAuditTrail("System", "CouponCode/couponbundle2(): " & ex.ToString, "Critical")
        End Try
    End Sub
    Private Sub couponbundle3()
        PromoCodeDefault()
        Try
            With POS

                Dim ReferenceExist As Boolean = False
                Dim referenceID As String = Me.DataGridViewCoupons.Item(6, Me.DataGridViewCoupons.CurrentRow.Index).Value.ToString
                Dim refIds As String() = referenceID.Split(New Char() {","c})
                Dim TotalQtyCount As Integer = 0
                Try
                    For Each getRefids In refIds
                        For i As Integer = 0 To .DataGridViewOrders.Rows.Count - 1 Step +1
                            If POS.DataGridViewOrders.Rows(i).Cells(5).Value = getRefids Then
                                TotalQtyCount += POS.DataGridViewOrders.Rows(i).Cells(1).Value
                            End If
                        Next
                    Next
                Catch ex As Exception
                    AuditTrail.LogToAuditTrail("System", ex.ToString, "Critical")
                End Try

                Dim BundlepromoID As String = Me.DataGridViewCoupons.Item(8, Me.DataGridViewCoupons.CurrentRow.Index).Value.ToString
                Dim bundIds As String() = BundlepromoID.Split(New Char() {","c})
                Dim BundpromoID As Boolean = False
                Dim CountQty As Integer = 0
                If TotalQtyCount >= Me.DataGridViewCoupons.Item(7, Me.DataGridViewCoupons.CurrentRow.Index).Value Then
                    Try
                        For Each getBundleids In bundIds
                            For i As Integer = 0 To .DataGridViewOrders.Rows.Count - 1 Step +1
                                If POS.DataGridViewOrders.Rows(i).Cells(5).Value.ToString.Contains(getBundleids) = True Then
                                    If POS.DataGridViewOrders.Rows(i).Cells(1).Value >= Me.DataGridViewCoupons.SelectedRows(0).Cells(9).Value Then
                                        BundpromoID = True
                                        Exit Try
                                    Else
                                        BundpromoID = False
                                    End If
                                End If
                            Next
                        Next
                    Catch ex As Exception
                        AuditTrail.LogToAuditTrail("System", ex.ToString, "Critical")
                    End Try
                    Dim PRGROSSSALES As Double = 0
                    Dim PRDISCOUNTAMOUNT As Double = 0
                    Dim PRPercentage As Double = DataGridViewCoupons.SelectedRows(0).Cells(3).Value / 100
                    Dim PRTOTALDISCOUNT As Double = 0
                    Dim PRVATABLESALES As Double = 0
                    Dim PRTOTALAMOUNTDUE As Double = 0
                    Dim PRVAT12PERCENT As Double = 0
                    Dim PRTAX As Double = 1 + Val(S_Tax)
                    If BundpromoID = True Then
                        For i As Integer = 0 To .DataGridViewOrders.Rows.Count - 1 Step +1
                            PRGROSSSALES += .DataGridViewOrders.Rows(i).Cells(2).Value
                            PRVATABLESALES = PRGROSSSALES / PRTAX
                            If .DataGridViewOrders.Rows(i).Cells(9).Value.ToString = "WAFFLE" Then
                                CountQty += .DataGridViewOrders.Rows(i).Cells(1).Value
                            End If

                            If CountQty = 3 Then

                                If S_ZeroRated = "0" Then

                                    PRVAT12PERCENT = Format(PRVATABLESALES * S_Tax, "0.00")
                                    PRDISCOUNTAMOUNT = .DataGridViewOrders.Rows(i).Cells(2).Value
                                    'PRVATABLESALES = Format(PRTOTALAMOUNTDUE / PRTAX, "0.00")
                                    PRTOTALDISCOUNT = PRPercentage * PRDISCOUNTAMOUNT
                                    .TextBoxDISCOUNT.Text = NUMBERFORMAT(PRTOTALDISCOUNT)
                                    PRTOTALAMOUNTDUE = Double.Parse(POS.TextBoxGRANDTOTAL.Text) - Double.Parse(POS.TextBoxDISCOUNT.Text)
                                Else
                                    Dim PRTAXZeroRated As Double = 1 + Val(S_ZeroRated_Tax)
                                    PRDISCOUNTAMOUNT = .DataGridViewOrders.Rows(i).Cells(2).Value / PRTAXZeroRated
                                    PRVATABLESALES = .Label76.Text
                                    PRTOTALDISCOUNT = PRPercentage * PRDISCOUNTAMOUNT
                                    .TextBoxDISCOUNT.Text = NUMBERFORMAT(PRTOTALDISCOUNT)
                                    PRTOTALAMOUNTDUE = Double.Parse(POS.TextBoxGRANDTOTAL.Text) - Double.Parse(POS.TextBoxDISCOUNT.Text)
                                    ZERORATEDSALES = PRVATABLESALES
                                    ZERORATEDNETSALES = PRTOTALAMOUNTDUE
                                    PRVAT12PERCENT = 0
                                End If

                                GROSSSALE = NUMBERFORMAT(PRGROSSSALES)
                                TOTALDISCOUNTEDAMOUNT = NUMBERFORMAT(PRDISCOUNTAMOUNT)
                                VATEXEMPTSALES = 0
                                LESSVAT = 0
                                TOTALDISCOUNT = NUMBERFORMAT(PRTOTALDISCOUNT)
                                VATABLESALES = NUMBERFORMAT(PRVATABLESALES)
                                VAT12PERCENT = NUMBERFORMAT(PRVAT12PERCENT)
                                TOTALAMOUNTDUE = NUMBERFORMAT(PRTOTALAMOUNTDUE)
                                .TextBoxGRANDTOTAL.Text = NUMBERFORMAT(PRTOTALAMOUNTDUE)

                                PromoDesc = ""
                                PromoTotal = NUMBERFORMAT(PRTOTALDISCOUNT)
                                PromoApplied = True
                                PromoName = Me.DataGridViewCoupons.Item(1, Me.DataGridViewCoupons.CurrentRow.Index).Value.ToString
                            End If

                        Next
                        If CountQty < 3 Then
                            MsgBox("Buy 3 waffles to apply the coupon")
                        End If
                    End If
                Else
                    MsgBox("Condition not meet")
                End If
            End With

            PromoType = Me.DataGridViewCoupons.Item(5, Me.DataGridViewCoupons.CurrentRow.Index).Value.ToString

            Close()
        Catch ex As Exception
            AuditTrail.LogToAuditTrail("System", "CouponCode/couponbundle3(): " & ex.ToString, "Critical")
        End Try
    End Sub
    Private Sub couponbundle4()
        Try
            If S_ZeroRated = "0" Then
                PromoCodeDefault()
                With POS
                    Dim PRGROSSSALES As Double = 0
                    Dim PRTAX As Double = 1 + Val(S_Tax)
                    Dim PRTOTALDISCOUNT As Double = DataGridViewCoupons.SelectedRows(0).Cells(3).Value / 100
                    Dim PRTOTALAMOUNTDUE As Double = 0
                    With .DataGridViewOrders
                        For i As Integer = 0 To .Rows.Count - 1 Step +1
                            PRGROSSSALES += .Rows(i).Cells(3).Value
                        Next
                    End With
                    PRTOTALDISCOUNT = PRGROSSSALES * PRTOTALDISCOUNT
                    PRTOTALAMOUNTDUE = Format(PRGROSSSALES - PRTOTALDISCOUNT, "0.00")

                    Dim PRVATABLESALES As Double = Format(PRGROSSSALES / PRTAX, "0.00")
                    Dim PRVAT12PERCENT As Double = Format(PRVATABLESALES * S_Tax, "0.00")

                    GROSSSALE = NUMBERFORMAT(PRGROSSSALES)
                    TOTALDISCOUNTEDAMOUNT = NUMBERFORMAT(PRGROSSSALES)
                    VATEXEMPTSALES = 0
                    LESSVAT = 0
                    TOTALDISCOUNT = NUMBERFORMAT(PRTOTALDISCOUNT)
                    VATABLESALES = NUMBERFORMAT(PRVATABLESALES)
                    VAT12PERCENT = NUMBERFORMAT(PRVAT12PERCENT)
                    TOTALAMOUNTDUE = NUMBERFORMAT(PRTOTALAMOUNTDUE)
                    .TextBoxGRANDTOTAL.Text = NUMBERFORMAT(PRTOTALAMOUNTDUE)
                    .TextBoxDISCOUNT.Text = NUMBERFORMAT(TOTALDISCOUNT)
                    PromoDesc = ""
                    PromoTotal = NUMBERFORMAT(PRTOTALDISCOUNT)
                    PromoApplied = True
                    PromoName = Me.DataGridViewCoupons.Item(1, Me.DataGridViewCoupons.CurrentRow.Index).Value.ToString
                    MsgBox("Applied")

                End With
            Else
                PromoCodeDefault()
                With POS
                    Dim PRGROSSSALES As Double = Double.Parse(.TextBoxGRANDTOTAL.Text)

                    Dim PRTOTALDISCOUNT As Double = DataGridViewCoupons.SelectedRows(0).Cells(3).Value / 100
                    Dim PRTOTALAMOUNTDUE As Double = 0

                    PRTOTALDISCOUNT = PRGROSSSALES * PRTOTALDISCOUNT
                    PRTOTALAMOUNTDUE = Format(PRGROSSSALES - PRTOTALDISCOUNT, "0.00")

                    GROSSSALE = NUMBERFORMAT(PRGROSSSALES)
                    TOTALDISCOUNTEDAMOUNT = NUMBERFORMAT(PRGROSSSALES)
                    VATEXEMPTSALES = 0
                    LESSVAT = 0
                    TOTALDISCOUNT = NUMBERFORMAT(PRTOTALDISCOUNT)
                    VATABLESALES = NUMBERFORMAT(Double.Parse(.Label76.Text))
                    VAT12PERCENT = 0
                    TOTALAMOUNTDUE = NUMBERFORMAT(PRTOTALAMOUNTDUE)
                    .TextBoxGRANDTOTAL.Text = NUMBERFORMAT(PRTOTALAMOUNTDUE)
                    .TextBoxDISCOUNT.Text = NUMBERFORMAT(TOTALDISCOUNT)

                    ZERORATEDSALES = NUMBERFORMAT(Double.Parse(.Label76.Text))
                    ZERORATEDNETSALES = NUMBERFORMAT(Double.Parse(.Label76.Text) - PRTOTALDISCOUNT)
                    .TextBoxGRANDTOTAL.Text = NUMBERFORMAT(.Label76.Text - PRTOTALDISCOUNT)

                    PromoDesc = ""
                    PromoTotal = NUMBERFORMAT(PRTOTALDISCOUNT)
                    PromoApplied = True
                    PromoName = Me.DataGridViewCoupons.Item(1, Me.DataGridViewCoupons.CurrentRow.Index).Value.ToString
                    MsgBox("Applied")
                End With
            End If

            PromoType = Me.DataGridViewCoupons.Item(5, Me.DataGridViewCoupons.CurrentRow.Index).Value.ToString

            Close()
        Catch ex As Exception
            AuditTrail.LogToAuditTrail("System", "CouponCode/couponbundle4(): " & ex.ToString, "Critical")
        End Try
    End Sub
    'Private Sub DataGridViewCoupons_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridViewCoupons.CellClick
    '    Try
    '        LabelDesc.Text = DataGridViewCoupons.SelectedRows(0).Cells(2).Value.ToString
    '    Catch ex As Exception
    '        AuditTrail.LogToAuditTrail("System", ex.ToString, "Critical")
    '    End Try
    'End Sub
End Class