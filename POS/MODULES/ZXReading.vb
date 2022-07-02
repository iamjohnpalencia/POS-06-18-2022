Imports MySql.Data.MySqlClient
Module ZXReading
    Public ZXBegSINo As String
    Public ZXEndSINo As String
    Public ZXBegTransNo As String
    Public ZXEndTransNo As String
    Public ZXBegBalance As String
    Public ZXCashTotal As Double
    Public ZXGross As Double
    Public ZXLessVat As Double
    Public ZXLessVatDiplomat As Double
    Public ZXLessVatOthers As Double
    Public ZXAdditionalVat As Double
    Public ZXVatAmount As Double
    Public ZXLocalGovTax As Double
    Public ZXVatableSales As Double
    Public ZXZeroRatedSales As Double
    Public ZXDailySales As Double
    Public ZXNetSales As Double
    Public ZXCashlessTotal As Double
    Public ZXGcash As Double
    Public ZXPaymaya As Double
    Public ZXGrabFood As Double
    Public ZXFoodPanda As Double
    Public ZXShopeePay As Double
    Public ZXCashlessOthers As Double
    Public ZXRepExpense As Double
    Public ZXCreditCard As Double
    Public ZXDebitCard As Double
    Public ZXMiscCheques As Double
    Public ZXGiftCard As Double
    Public ZXGiftCardSum As Double

    Public ZXAR As Double
    Public ZXTotalExpenses As Double
    Public ZXCardOthers As Double
    Public ZXDeposits As Double
    Public ZXCashInDrawer As Double
    Public ZXTotalDiscounts As Double
    Public ZXSeniorCitizen As Double
    Public ZXPWD As Double
    Public ZXAthlete As Double
    Public ZXSingleParent As Double
    Public ZXItemVoidEC As Double
    Public ZXTransactionVoid As Double
    Public ZXTransactionCancel As Double
    Public ZXTakeOutCharge As Double
    Public ZXDeliveryCharge As Double
    Public ZXReturnsExchange As Double
    Public ZXReturnsRefund As Double
    Public ZXTotalQTYSold As Double
    Public ZXTotalTransactionCount As Double
    Public ZXTotalGuess As Double
    Public ZXCurrentTotalSales As Double
    Public ZXOldGrandTotalSales As Double
    Public ZXNewGrandtotalSales As Double
    Public ZXSimplyPerfect As Double
    Public ZXPerfectCombination As Double
    Public ZXSavoury As Double
    Public ZXCombo As Double
    Public ZXFamousBlends As Double
    Public ZXAddOns As Double
    Public ZXThousandQty As Double
    Public ZXFiveHundredQty As Double
    Public ZXTwoHundredQty As Double
    Public ZXOneHundredQty As Double
    Public ZXFiftyQty As Double
    Public ZXTwentyQty As Double
    Public ZXTenQty As Double
    Public ZXFiveQty As Double
    Public ZXOneQty As Double
    Public ZXPointTwentyFiveQty As Double
    Public ZXPointFiveQty As Double
    Public ZXThousandTotal As Double
    Public ZXFiveHundredTotal As Double
    Public ZXTwoHundredTotal As Double
    Public ZXOneHundredTotal As Double
    Public ZXFiftyTotal As Double
    Public ZXTwentyTotal As Double
    Public ZXTenTotal As Double
    Public ZXFiveTotal As Double
    Public ZXOneTotal As Double
    Public ZXPointTwentyFiveTotal As Double
    Public ZXPointFiveTotal As Double
    Public ZXdate As String
    Public ZXVatExemptSales As Double
    Public ZXPremium As Double
    Public ZXReprintCount As Double

    Public ZXResetCounter As Double
    Public ZXZreadCounter As Double
    Public ZXCashier As String
    Public ZXLessDiscVE As Double

    Dim ZReadTable = "`loc_zread_table`"
    Public ZReadTableFieldsSum As String = "SUM(`ZXBegBalance`) as ZXBegBalance, SUM(`ZXCashTotal`) as ZXCashTotal, SUM(`ZXGross`) as ZXGross
    , SUM(`ZXLessVat`) as ZXLessVat, SUM(`ZXLessVatDiplomat`) as ZXLessVatDiplomat, SUM(`ZXLessVatOthers`) as ZXLessVatOthers, SUM(`ZXAdditionalVat`) as ZXAdditionalVat
    , SUM(`ZXVatAmount`) as ZXVatAmount, SUM(`ZXLocalGovTax`) as ZXLocalGovTax, SUM(`ZXVatableSales`) as ZXVatableSales, SUM(`ZXZeroRatedSales`) as ZXZeroRatedSales, SUM(`ZXDailySales`) as ZXDailySales
    , SUM(`ZXNetSales`) as ZXNetSales, SUM(`ZXCashlessTotal`) as ZXCashlessTotal, SUM(`ZXGcash`) as ZXGcash, SUM(`ZXPaymaya`) as ZXPaymaya, SUM(`ZXGrabFood`) as ZXGrabFood
    , SUM(`ZXFoodPanda`) as ZXFoodPanda, SUM(`ZXShopeePay`) as ZXShopeePay, SUM(`ZXCashlessOthers`) as ZXCashlessOthers, SUM(`ZXRepExpense`) as ZXRepExpense, SUM(`ZXCreditCard`) as ZXCreditCard
    , SUM(`ZXDebitCard`) as ZXDebitCard, SUM(`ZXMiscCheques`) as ZXMiscCheques, SUM(`ZXGiftCard`) as ZXGiftCard, SUM(`ZXGiftCardSum`) as ZXGiftCardSum, SUM(`ZXAR`) as ZXAR
    , SUM(`ZXTotalExpenses`) as ZXTotalExpenses, SUM(`ZXCardOthers`) as ZXCardOthers, SUM(`ZXDeposits`) as ZXDeposits, SUM(`ZXCashInDrawer`) as ZXCashInDrawer
    , SUM(`ZXTotalDiscounts`) as ZXTotalDiscounts, SUM(`ZXSeniorCitizen`) as ZXSeniorCitizen, SUM(`ZXPWD`) as ZXPWD, SUM(`ZXAthlete`) as ZXAthlete, SUM(`ZXSingleParent`) as ZXSingleParent
    , SUM(`ZXItemVoidEC`) as ZXItemVoidEC, SUM(`ZXTransactionVoid`) as ZXTransactionVoid, SUM(`ZXTransactionCancel`) as ZXTransactionCancel, SUM(`ZXTakeOutCharge`) as ZXTakeOutCharge
    , SUM(`ZXDeliveryCharge`) as ZXDeliveryCharge, SUM(`ZXReturnsExchange`) as ZXReturnsExchange, SUM(`ZXReturnsRefund`) as ZXReturnsRefund, SUM(`ZXTotalQTYSold`) as ZXTotalQTYSold
    , SUM(`ZXTotalTransactionCount`) as ZXTotalTransactionCount, SUM(`ZXTotalGuess`) as ZXTotalGuess, SUM(`ZXCurrentTotalSales`) as ZXCurrentTotalSales, SUM(`ZXOldGrandTotalSales`) as ZXOldGrandTotalSales
    , SUM(`ZXNewGrandtotalSales`) as ZXNewGrandtotalSales, SUM(`ZXSimplyPerfect`) as ZXSimplyPerfect, SUM(`ZXPerfectCombination`) as ZXPerfectCombination, SUM(`ZXSavoury`) as ZXSavoury, SUM(`ZXCombo`) as ZXCombo
    , SUM(`ZXFamousBlends`) as ZXFamousBlends, SUM(`ZXAddOns`) as ZXAddOns, SUM(`ZXThousandQty`) as ZXThousandQty, SUM(`ZXFiveHundredQty`) as ZXFiveHundredQty, SUM(`ZXTwoHundredQty`) as ZXTwoHundredQty
    , SUM(`ZXOneHundredQty`) as ZXOneHundredQty, SUM(`ZXFiftyQty`) as ZXFiftyQty, SUM(`ZXTwentyQty`) as ZXTwentyQty, SUM(`ZXTenQty`) as ZXTenQty, SUM(`ZXFiveQty`) as ZXFiveQty, SUM(`ZXOneQty`) as ZXOneQty
    , SUM(`ZXPointTwentyFiveQty`) as ZXPointTwentyFiveQty, SUM(`ZXPointFiveQty`) as ZXPointFiveQty, SUM(`ZXThousandTotal`) as ZXThousandTotal, SUM(`ZXFiveHundredTotal`) as ZXFiveHundredTotal, SUM(`ZXTwoHundredTotal`) as ZXTwoHundredTotal
    , SUM(`ZXOneHundredTotal`) as ZXOneHundredTotal, SUM(`ZXFiftyTotal`) as ZXFiftyTotal, SUM(`ZXTwentyTotal`) as ZXTwentyTotal, SUM(`ZXTenTotal`) as ZXTenTotal, SUM(`ZXFiveTotal`) as ZXFiveTotal, SUM(`ZXOneTotal`) as ZXOneTotal
    , SUM(`ZXPointTwentyFiveTotal`) as ZXPointTwentyFiveTotal, SUM(`ZXPointFiveTotal`) as ZXPointFiveTotal, SUM(`ZXVatExemptSales`) as ZXVatExemptSales, SUM(`ZXPremium`) as ZXPremium, `ZXReprintCount`, SUM(`ZXLessDiscVE`) as ZXLessDiscVE"

    Dim ZReadTableFields As String = "`ZXBegSiNo`, `ZXEndSINo`, `ZXBegTransNo`, `ZXEndTransNo`, `ZXBegBalance`, `ZXCashTotal`, `ZXGross`, `ZXLessVat`, `ZXLessVatDiplomat`, `ZXLessVatOthers`, `ZXAdditionalVat`, `ZXVatAmount`, `ZXLocalGovTax`, `ZXVatableSales`, `ZXZeroRatedSales`, `ZXDailySales`, `ZXNetSales`, `ZXCashlessTotal`, `ZXGcash`, `ZXPaymaya`, `ZXGrabFood`, `ZXFoodPanda`, `ZXShopeePay`, `ZXCashlessOthers`, `ZXRepExpense`, `ZXCreditCard`, `ZXDebitCard`, `ZXMiscCheques`, `ZXGiftCard`, `ZXGiftCardSum`, `ZXAR`, `ZXTotalExpenses`, `ZXCardOthers`, `ZXDeposits`, `ZXCashInDrawer`, `ZXTotalDiscounts`, `ZXSeniorCitizen`, `ZXPWD`, `ZXAthlete`, `ZXSingleParent`, `ZXItemVoidEC`, `ZXTransactionVoid`, `ZXTransactionCancel`, `ZXTakeOutCharge`, `ZXDeliveryCharge`, `ZXReturnsExchange`, `ZXReturnsRefund`, `ZXTotalQTYSold`, `ZXTotalTransactionCount`, `ZXTotalGuess`, `ZXCurrentTotalSales`, `ZXOldGrandTotalSales`, `ZXNewGrandtotalSales`, `ZXSimplyPerfect`, `ZXPerfectCombination`, `ZXSavoury`, `ZXCombo`, `ZXFamousBlends`, `ZXAddOns`, `ZXThousandQty`, `ZXFiveHundredQty`, `ZXTwoHundredQty`, `ZXOneHundredQty`, `ZXFiftyQty`, `ZXTwentyQty`, `ZXTenQty`, `ZXFiveQty`, `ZXOneQty`, `ZXPointTwentyFiveQty`, `ZXPointFiveQty`, `ZXThousandTotal`, `ZXFiveHundredTotal`, `ZXTwoHundredTotal`, `ZXOneHundredTotal`, `ZXFiftyTotal`, `ZXTwentyTotal`, `ZXTenTotal`, `ZXFiveTotal`, `ZXOneTotal`, `ZXPointTwentyFiveTotal`, `ZXPointFiveTotal`, `ZXdate`, `created_by`, `status`, `ZXVatExemptSales`, `ZXPremium` ,`ZXLessDiscVE`"
    Dim ZReadValues As String = "@1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11, @12, @13, @14, @15, @16, @17, @18, @19, @20, @21, @22, @23, @24, @25, @26, @27, @28, @29, @30, @31, @32, @33, @34, @35, @36, @37, @38, @39, @40, @41, @42, @43, @44, @45, @46, @47, @48, @49, @50, @51, @52, @53, @54, @55, @56, @57, @58, @59, @60, @61, @62, @63, @64, @65, @66, @67, @68, @69, @70, @71, @72, @73, @74, @75, @76, @77, @78, @79, @80, @81, @82, @83, @84, @85, @86, @87"

    Public Sub InsertZReadXRead()
        Try
            Dim ConnectionLocal As MySqlConnection = LocalhostConn()
            Dim Query As String = "INSERT INTO " & ZReadTable & " (" & ZReadTableFields & ") VALUES (" & ZReadValues & ")"
            Dim Command As MySqlCommand = New MySqlCommand(Query, ConnectionLocal)

            Command.Parameters.Add("@1", MySqlDbType.Text).Value = ZXBegSINo
            Command.Parameters.Add("@2", MySqlDbType.Text).Value = ZXEndSINo
            Command.Parameters.Add("@3", MySqlDbType.Text).Value = ZXBegTransNo
            Command.Parameters.Add("@4", MySqlDbType.Text).Value = ZXEndTransNo
            Command.Parameters.Add("@5", MySqlDbType.Text).Value = ZXBegBalance
            Command.Parameters.Add("@6", MySqlDbType.Double).Value = ZXCashTotal
            Command.Parameters.Add("@7", MySqlDbType.Double).Value = ZXGross
            Command.Parameters.Add("@8", MySqlDbType.Double).Value = ZXLessVat
            Command.Parameters.Add("@9", MySqlDbType.Double).Value = ZXLessVatDiplomat
            Command.Parameters.Add("@10", MySqlDbType.Double).Value = ZXLessVatOthers
            Command.Parameters.Add("@11", MySqlDbType.Double).Value = ZXAdditionalVat
            Command.Parameters.Add("@12", MySqlDbType.Double).Value = ZXVatAmount
            Command.Parameters.Add("@13", MySqlDbType.Double).Value = ZXLocalGovTax
            Command.Parameters.Add("@14", MySqlDbType.Double).Value = ZXVatableSales
            Command.Parameters.Add("@15", MySqlDbType.Double).Value = ZXZeroRatedSales
            Command.Parameters.Add("@16", MySqlDbType.Double).Value = ZXDailySales
            Command.Parameters.Add("@17", MySqlDbType.Double).Value = ZXNetSales
            Command.Parameters.Add("@18", MySqlDbType.Double).Value = ZXCashlessTotal
            Command.Parameters.Add("@19", MySqlDbType.Double).Value = ZXGcash
            Command.Parameters.Add("@20", MySqlDbType.Double).Value = ZXPaymaya
            Command.Parameters.Add("@21", MySqlDbType.Double).Value = ZXGrabFood
            Command.Parameters.Add("@22", MySqlDbType.Double).Value = ZXFoodPanda
            Command.Parameters.Add("@23", MySqlDbType.Double).Value = ZXShopeePay
            Command.Parameters.Add("@24", MySqlDbType.Double).Value = ZXCashlessOthers
            Command.Parameters.Add("@25", MySqlDbType.Double).Value = ZXRepExpense
            Command.Parameters.Add("@26", MySqlDbType.Double).Value = ZXCreditCard
            Command.Parameters.Add("@27", MySqlDbType.Double).Value = ZXDebitCard
            Command.Parameters.Add("@28", MySqlDbType.Double).Value = ZXMiscCheques
            Command.Parameters.Add("@29", MySqlDbType.Double).Value = ZXGiftCard
            Command.Parameters.Add("@30", MySqlDbType.Double).Value = ZXGiftCardSum
            Command.Parameters.Add("@31", MySqlDbType.Double).Value = ZXAR
            Command.Parameters.Add("@32", MySqlDbType.Double).Value = ZXTotalExpenses
            Command.Parameters.Add("@33", MySqlDbType.Double).Value = ZXCardOthers
            Command.Parameters.Add("@34", MySqlDbType.Double).Value = ZXDeposits
            Command.Parameters.Add("@35", MySqlDbType.Double).Value = ZXCashInDrawer
            Command.Parameters.Add("@36", MySqlDbType.Double).Value = ZXTotalDiscounts
            Command.Parameters.Add("@37", MySqlDbType.Double).Value = ZXSeniorCitizen
            Command.Parameters.Add("@38", MySqlDbType.Double).Value = ZXPWD
            Command.Parameters.Add("@39", MySqlDbType.Double).Value = ZXAthlete
            Command.Parameters.Add("@40", MySqlDbType.Double).Value = ZXSingleParent
            Command.Parameters.Add("@41", MySqlDbType.Double).Value = ZXItemVoidEC
            Command.Parameters.Add("@42", MySqlDbType.Double).Value = ZXTransactionVoid
            Command.Parameters.Add("@43", MySqlDbType.Double).Value = ZXTransactionCancel
            Command.Parameters.Add("@44", MySqlDbType.Double).Value = ZXTakeOutCharge
            Command.Parameters.Add("@45", MySqlDbType.Double).Value = ZXDeliveryCharge
            Command.Parameters.Add("@46", MySqlDbType.Double).Value = ZXReturnsExchange
            Command.Parameters.Add("@47", MySqlDbType.Double).Value = ZXReturnsRefund
            Command.Parameters.Add("@48", MySqlDbType.Double).Value = ZXTotalQTYSold
            Command.Parameters.Add("@49", MySqlDbType.Double).Value = ZXTotalTransactionCount
            Command.Parameters.Add("@50", MySqlDbType.Double).Value = ZXTotalGuess
            Command.Parameters.Add("@51", MySqlDbType.Double).Value = ZXCurrentTotalSales
            Command.Parameters.Add("@52", MySqlDbType.Double).Value = ZXOldGrandTotalSales
            Command.Parameters.Add("@53", MySqlDbType.Double).Value = ZXNewGrandtotalSales
            Command.Parameters.Add("@54", MySqlDbType.Double).Value = ZXSimplyPerfect
            Command.Parameters.Add("@55", MySqlDbType.Double).Value = ZXPerfectCombination
            Command.Parameters.Add("@56", MySqlDbType.Double).Value = ZXSavoury
            Command.Parameters.Add("@57", MySqlDbType.Double).Value = ZXCombo
            Command.Parameters.Add("@58", MySqlDbType.Double).Value = ZXFamousBlends
            Command.Parameters.Add("@59", MySqlDbType.Double).Value = ZXAddOns
            Command.Parameters.Add("@60", MySqlDbType.Double).Value = ZXThousandQty
            Command.Parameters.Add("@61", MySqlDbType.Double).Value = ZXFiveHundredQty
            Command.Parameters.Add("@62", MySqlDbType.Double).Value = ZXTwoHundredQty
            Command.Parameters.Add("@63", MySqlDbType.Double).Value = ZXOneHundredQty
            Command.Parameters.Add("@64", MySqlDbType.Double).Value = ZXFiftyQty
            Command.Parameters.Add("@65", MySqlDbType.Double).Value = ZXTwentyQty
            Command.Parameters.Add("@66", MySqlDbType.Double).Value = ZXTenQty
            Command.Parameters.Add("@67", MySqlDbType.Double).Value = ZXFiveQty
            Command.Parameters.Add("@68", MySqlDbType.Double).Value = ZXOneQty
            Command.Parameters.Add("@69", MySqlDbType.Double).Value = ZXPointTwentyFiveQty
            Command.Parameters.Add("@70", MySqlDbType.Double).Value = ZXPointFiveQty
            Command.Parameters.Add("@71", MySqlDbType.Double).Value = ZXThousandTotal
            Command.Parameters.Add("@72", MySqlDbType.Double).Value = ZXFiveHundredTotal
            Command.Parameters.Add("@73", MySqlDbType.Double).Value = ZXTwoHundredTotal
            Command.Parameters.Add("@74", MySqlDbType.Double).Value = ZXOneHundredTotal
            Command.Parameters.Add("@75", MySqlDbType.Double).Value = ZXFiftyTotal
            Command.Parameters.Add("@76", MySqlDbType.Double).Value = ZXTwentyTotal
            Command.Parameters.Add("@77", MySqlDbType.Double).Value = ZXTenTotal
            Command.Parameters.Add("@78", MySqlDbType.Double).Value = ZXFiveTotal
            Command.Parameters.Add("@79", MySqlDbType.Double).Value = ZXOneTotal
            Command.Parameters.Add("@80", MySqlDbType.Double).Value = ZXPointTwentyFiveTotal
            Command.Parameters.Add("@81", MySqlDbType.Double).Value = ZXPointFiveTotal
            Command.Parameters.Add("@82", MySqlDbType.Text).Value = ZXdate
            Command.Parameters.Add("@83", MySqlDbType.Text).Value = ClientCrewID
            Command.Parameters.Add("@84", MySqlDbType.Text).Value = 1
            Command.Parameters.Add("@85", MySqlDbType.Double).Value = ZXVatExemptSales
            Command.Parameters.Add("@86", MySqlDbType.Double).Value = ZXPremium
            Command.Parameters.Add("@87", MySqlDbType.Double).Value = ZXLessDiscVE
            Command.ExecuteNonQuery()

        Catch ex As Exception
            AuditTrail.LogToAuditTrail("System", "ModZXRead/InsertZReadXRead(): " & ex.ToString, "Critical")
        End Try
    End Sub
    Public Function ReturnCashBreakdown(DateFrom As String, DateTo As String) As DataTable
        Dim CashBreakDownDatatable As DataTable = New DataTable
        Try
            Dim ConnectionLocal As MySqlConnection = LocalhostConn()
            Dim Query As String = "select * from loc_cash_breakdown, (select max(created_at) DateDesc,SUM(`1000`) as Thousand, SUM(`500`) as FiveHundreds, SUM(`200`) as TwoHundred, SUM(`100`) as OneHundred, SUM(`50`) as Fifty, SUM(`20`) as Twenty, SUM(`10`) as Ten, SUM(`5`) as Five, SUM(`1`) as One, SUM(`.25`) as PointTwoFive, SUM(`.05`) as PointFive from loc_cash_breakdown group by zreading) max_breakdown where loc_cash_breakdown.created_at = max_breakdown.DateDesc AND loc_cash_breakdown.zreading >= '" & DateFrom & "' AND loc_cash_breakdown.zreading  <= '" & DateTo & "'"
            Dim Command As MySqlCommand = New MySqlCommand(Query, ConnectionLocal)
            Dim Da As MySqlDataAdapter = New MySqlDataAdapter(Command)
            Da.Fill(CashBreakDownDatatable)
        Catch ex As Exception
            AuditTrail.LogToAuditTrail("System", "ModZXRead/ReturnCashBreakdown(): " & ex.ToString, "Critical")
        End Try
        Return CashBreakDownDatatable
    End Function

    Public Sub ResetZReadingVariables()
        ZXBegSINo = ""
        ZXEndSINo = ""
        ZXBegTransNo = ""
        ZXEndTransNo = ""
        ZXBegBalance = 0
        ZXCashTotal = 0
        ZXGross = 0
        ZXLessVat = 0
        ZXLessVatDiplomat = 0
        ZXLessVatOthers = 0
        ZXAdditionalVat = 0
        ZXVatAmount = 0
        ZXLocalGovTax = 0
        ZXVatableSales = 0
        ZXZeroRatedSales = 0
        ZXDailySales = 0
        ZXNetSales = 0
        ZXCashlessTotal = 0
        ZXGcash = 0
        ZXPaymaya = 0
        ZXGrabFood = 0
        ZXFoodPanda = 0
        ZXShopeePay = 0
        ZXCashlessOthers = 0
        ZXRepExpense = 0
        ZXCreditCard = 0
        ZXDebitCard = 0
        ZXMiscCheques = 0
        ZXGiftCard = 0
        ZXGiftCardSum = 0
        ZXAR = 0
        ZXTotalExpenses = 0
        ZXCardOthers = 0
        ZXDeposits = 0
        ZXCashInDrawer = 0
        ZXTotalDiscounts = 0
        ZXSeniorCitizen = 0
        ZXPWD = 0
        ZXAthlete = 0
        ZXSingleParent = 0
        ZXItemVoidEC = 0
        ZXTransactionVoid = 0
        ZXTransactionCancel = 0
        ZXTakeOutCharge = 0
        ZXDeliveryCharge = 0
        ZXReturnsExchange = 0
        ZXReturnsRefund = 0
        ZXTotalQTYSold = 0
        ZXTotalTransactionCount = 0
        ZXTotalGuess = 0
        ZXCurrentTotalSales = 0
        ZXOldGrandTotalSales = 0
        ZXNewGrandtotalSales = 0
        ZXSimplyPerfect = 0
        ZXPerfectCombination = 0
        ZXSavoury = 0
        ZXCombo = 0
        ZXFamousBlends = 0
        ZXAddOns = 0
        ZXThousandQty = 0
        ZXFiveHundredQty = 0
        ZXTwoHundredQty = 0
        ZXOneHundredQty = 0
        ZXFiftyQty = 0
        ZXTwentyQty = 0
        ZXTenQty = 0
        ZXFiveQty = 0
        ZXOneQty = 0
        ZXPointTwentyFiveQty = 0
        ZXPointFiveQty = 0
        ZXThousandTotal = 0
        ZXFiveHundredTotal = 0
        ZXTwoHundredTotal = 0
        ZXOneHundredTotal = 0
        ZXFiftyTotal = 0
        ZXTwentyTotal = 0
        ZXTenTotal = 0
        ZXFiveTotal = 0
        ZXOneTotal = 0
        ZXPointTwentyFiveTotal = 0
        ZXPointFiveTotal = 0
        ZXVatExemptSales = 0
        ZXPremium = 0
    End Sub
End Module
