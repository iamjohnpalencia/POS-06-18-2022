Imports System.IO
Imports System.Security.Cryptography
Imports System.Text
Imports System.Net
Imports MySql.Data.MySqlClient
Imports System.Drawing.Printing
Imports System.Text.RegularExpressions
Imports System.Globalization
Imports System.Xml

Module publicfunctions
    Public drasd
    Dim dr2
    Dim hashable As String
    Dim dateformat
    Dim timeformat
    Declare Function Wow64DisableWow64FsRedirection Lib "kernel32" (ByRef oldvalue As Long) As Boolean
    Private osk As String = "C:\Windows\System32\osk.exe"

    Dim sp As New System.IO.Ports.SerialPort("COM2", 2400, IO.Ports.Parity.None And IO.Ports.StopBits.One)
    Public Declare Function com_init Lib "api_com.dll" (ByVal com As Integer, ByVal baud As Integer) As Boolean
    Public Declare Function com_send Lib "api_com.dll" (ByVal buf As String, ByVal lens As Long) As Boolean
    Public Declare Function com_rest Lib "api_com.dll" () As Boolean

    Public Sub ShowKeyboard()
        Try
            Wow64DisableWow64FsRedirection(0)
            Process.Start(osk)
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Public Sub ButtonEnableability(ByVal root As Control, ENB As Boolean)
        For Each ctrl As Control In root.Controls
            ButtonEnableability(ctrl, ENB)
            If TypeOf ctrl Is Button Then
                CType(ctrl, Button).Enabled = ENB
            End If
        Next ctrl
    End Sub
    Public Sub CheckBoxEnabled(ByVal root As Control, ENB As Boolean)
        For Each ctrl As Control In root.Controls
            CheckBoxEnabled(ctrl, ENB)
            If TypeOf ctrl Is CheckBox Then
                CType(ctrl, CheckBox).Checked = ENB
            End If
        Next ctrl
    End Sub
    Public Function TextboxIsEmpty(ByVal root As Control)
        Dim ReturnThisThing As Boolean
        For Each tb As TextBox In root.Controls.OfType(Of TextBox)()
            If tb.Text = String.Empty Then

                ReturnThisThing = False
                Exit For
            Else
                ReturnThisThing = True
            End If
        Next
        Return ReturnThisThing
    End Function
    Public Sub TextboxEnableability(ByVal root As Control, ENB As Boolean)
        Try
            For Each ctrl As Control In root.Controls
                TextboxEnableability(ctrl, ENB)
                If TypeOf ctrl Is TextBox Then
                    CType(ctrl, TextBox).Enabled = ENB
                End If
            Next ctrl
        Catch ex As Exception
            MsgBox(ex.ToString)
            SendErrorReport(ex.ToString)
        End Try
    End Sub

    Public Sub ReturnZero(ByVal root As Control)

        '        If String.IsNullOrEmpty(textBox2.Text)) Then


        '    textBox2.Text = "0";                
        '}

        Try
            For Each ctrl As Control In root.Controls
                ReturnZero(ctrl)
                If TypeOf ctrl Is TextBox Then
                    If String.IsNullOrEmpty(CType(ctrl, TextBox).Text) Then
                        CType(ctrl, TextBox).Text = "0"
                        CType(ctrl, TextBox).SelectAll()
                    End If
                End If
            Next ctrl

        Catch ex As Exception
            MsgBox(ex.ToString)
            SendErrorReport(ex.ToString)
        End Try

    End Sub
    'Public Sub TextboxReadOnly(ByVal root As Control, ENB As Boolean)
    '    Try
    '        For Each ctrl As Control In root.Controls
    '            TextboxReadOnly(ctrl, ENB)
    '            If TypeOf ctrl Is TextBox Then
    '                CType(ctrl, TextBox).ReadOnly = ENB
    '            End If
    '        Next ctrl
    '    Catch ex As Exception
    '        MsgBox(ex.ToString)
    '        SendErrorReport(ex.ToString)
    '    End Try
    'End Sub
    Public Sub ClearTextBox(ByVal root As Control)
        Try
            For Each ctrl As Control In root.Controls
                ClearTextBox(ctrl)
                If TypeOf ctrl Is TextBox Then
                    CType(ctrl, TextBox).Text = String.Empty
                End If
            Next ctrl
        Catch ex As Exception
            MsgBox(ex.ToString)
            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Public Sub ClearDataGridViewRows(ByVal root As Control)
        Try
            For Each ctrl As Control In root.Controls
                ClearDataGridViewRows(ctrl)
                If TypeOf ctrl Is DataGridView Then
                    CType(ctrl, DataGridView).DataSource = Nothing
                    CType(ctrl, DataGridView).Rows.Clear()
                End If
            Next ctrl
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Public Sub SpecialCharRestriction(ByVal root As Control, ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Try
            For Each ctrl As Control In root.Controls
                SpecialCharRestriction(ctrl, sender, e)
                If TypeOf ctrl Is TextBox Then
                    Dim allowedChars As String = "[`~!@#\$%\^&\*\(\)_\-\+=\{\}\[\]\\\|:;""'<>,\.\?/"
                    If Not allowedChars.IndexOf(e.KeyChar) = -1 Then
                        e.Handled = True
                    End If
                End If
            Next ctrl
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Public Sub buttonpressedenter(ByVal btntext As String)
        If Val(POS.TextBoxQTY.Text) <> 0 Then
            POS.TextBoxQTY.Text += btntext
        Else
            POS.TextBoxQTY.Text = btntext
        End If
    End Sub
    Public Sub buttonpressedenterpayment(ByVal btntext As String)
        If Val(PaymentForm.TextBoxMONEY.Text) <> 0 Then
            PaymentForm.TextBoxMONEY.Text += btntext
        Else
            PaymentForm.TextBoxMONEY.Text = btntext
        End If
    End Sub
    Public Sub btnformcolor(ByVal changecolor As Button)
        changecolor.BackColor = Color.FromArgb(23, 162, 184)
    End Sub
    Public Sub btndefaut(ByVal defaultcolor As Button, ByVal form As Form)
        For Each P As Control In form.Controls
            If TypeOf P Is Panel Then
                For Each ctrl As Control In P.Controls
                    If TypeOf ctrl Is Button Then
                        If ctrl.Name <> defaultcolor.Name Then
                            CType(ctrl, Button).BackColor = Color.FromArgb(41, 39, 40)
                        End If
                    End If
                Next
            End If
        Next
    End Sub
    Public Function ConvertToBase64(str As String)
        Dim byt As Byte() = System.Text.Encoding.UTF8.GetBytes(str)
        Dim byt2 = Convert.ToBase64String(byt)
        Return byt2
    End Function
    Public Function ConvertB64ToString(str As String)
        Dim b As Byte() = Convert.FromBase64String(str)
        Dim byt2 = System.Text.Encoding.UTF8.GetString(b)
        Return byt2
    End Function
    'Dim MyPublicIpAddress As String
    'Public Function PublicIpAddress(myform As Form)
    '    Dim client As New WebClient
    '    '// Add a user agent header in case the requested URI contains a query.
    '    client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR1.0.3705;)")
    '    Dim baseurl As String = "http://localhost/ipadd/"
    '    ' with proxy server only:
    '    Dim proxy As IWebProxy = WebRequest.GetSystemWebProxy()
    '    proxy.Credentials = CredentialCache.DefaultNetworkCredentials
    '    client.Proxy = proxy
    '    Dim data As Stream
    '    Try
    '        data = client.OpenRead(baseurl)
    '    Catch ex As Exception
    '        MsgBox("open url " & ex.Message)
    '        Exit Function
    '    End Try
    '    Dim reader As StreamReader = New StreamReader(data)
    '    MyPublicIpAddress = reader.ReadToEnd()
    '    data.Close()
    '    reader.Close()
    '    MyPublicIpAddress = MyPublicIpAddress.Replace("<html><head><title>Current IP Check</title></head><body>", "").Replace("</body></html>", "").ToString()
    '    myform.Text = MyPublicIpAddress
    '    Return MyPublicIpAddress
    'End Function

    Public Function RemoveCharacter(ByVal stringToCleanUp, ByVal characterToRemove)
        ' replace the target with nothing
        ' Replace() returns a new String and does not modify the current one
        Return stringToCleanUp.Replace(characterToRemove, "")
    End Function
    '_________________________________________________________________________________________________________________
    'IMAGE TO TEXT
    Public Function ImageToBase64(ByVal image As Image, ByVal format As System.Drawing.Imaging.ImageFormat) As String
        Dim base64String As String = ""
        Try
            Using ms As New MemoryStream()
                ' Convert Image to byte[]
                image.Save(ms, format)
                Dim imageBytes As Byte() = ms.ToArray()
                ' Convert byte[] to Base64 String
                base64String = Convert.ToBase64String(imageBytes)
            End Using
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
        Return base64String
    End Function
    'TEXT TO IMAGE
    Public Function Base64ToImage(ByVal base64String As String) As Image
        ' Convert Base64 String to byte[]
        Dim imageBytes As Byte() = Convert.FromBase64String(base64String)
        Dim ms As New MemoryStream(imageBytes, 0, imageBytes.Length)
        ' Convert byte[] to Image
        ms.Write(imageBytes, 0, imageBytes.Length)
        Dim ConvertedBase64Image As Image = Image.FromStream(ms, True)
        Return ConvertedBase64Image
    End Function
    Private ImagePath As String = ""
    '_________________________________________________________________________________________________________________
    'POS FUNCTIONS
    Public Function GetHash(theInput As String) As String
        Using hasher As MD5 = MD5.Create()    ' create hash object
            ' Convert to byte array and get hash
            Dim dbytes As Byte() =
             hasher.ComputeHash(Encoding.UTF8.GetBytes(theInput))
            ' sb to create string from bytes
            Dim sBuilder As New StringBuilder()
            ' convert byte data to hex string
            For n As Integer = 0 To dbytes.Length - 1
                sBuilder.Append(dbytes(n).ToString("x2"))
            Next n
            Return sBuilder.ToString()
        End Using
    End Function
    Public Function ConvertPassword(ByVal SourceString As String)
        Dim ConvertedString As String
        Dim byt As Byte() = System.Text.Encoding.UTF8.GetBytes(SourceString)
        ConvertedString = Convert.ToBase64String(byt)
        Using md5Hash As MD5 = MD5.Create()
            hashable = GetHash(ConvertedString)
        End Using
        Return hashable
    End Function
    Public Function CheckForInternetConnection() As Boolean
        Try
            Using client = New WebClient()
                Using stream = client.OpenRead("https://www.google.com/")
                    Return True
                End Using
            End Using
        Catch ex As Exception
            'MsgBox(ex.ToString)
            Return False
        End Try
    End Function
    Public Function GetMonthName(dat As Date) As String
        Dim iMonth As Integer = Month(dat)
        GetMonthName = MonthName(iMonth)
    End Function
    Public resetinventory As Boolean
    Public Function FirstDayOfMonth(ByVal sourceDate As DateTime)
        Dim displaythis = ""
        Try
            Dim FirstDay As DateTime = New DateTime(sourceDate.Year, sourceDate.Month, 1)
            Dim FormatDay As String = "yyyy-MM-dd"
            displaythis = FirstDay.ToString(FormatDay)
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
        Return displaythis
    End Function
    Dim dtRESET As DataTable
    Public Function CheckIfNeedToReset() As Boolean
        Try
            Dim cmd As MySqlCommand
            Dim da As MySqlDataAdapter
            Dim firstday = FirstDayOfMonth(Date.Now)
            Try
                Dim sql = "SELECT * FROM loc_inv_temp_data WHERE created_at = '" & firstday & "'"
                cmd = New MySqlCommand(sql, LocalhostConn)
                da = New MySqlDataAdapter(cmd)
                dtRESET = New DataTable
                da.Fill(dtRESET)
            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try

        Catch ex As Exception
            MsgBox(ex.ToString)
            SendErrorReport(ex.ToString)
        End Try
        If dtRESET.Rows.Count = 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Dim DateNow
    Public Function FullDate24HR()
        Try
            DateNow = Format(Now(), "yyyy-MM-dd HH:mm:ss")
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
        Return DateNow
    End Function
    Dim DateSave
    Public Function FullDateFormatForSaving()
        Try
            DateSave = Format(Now(), "yyyy-MM-dd HH-mm-ss")
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
        Return DateSave
    End Function
    Public Sub EndBalance()
        Try
            If Shift = "First Shift" Then
                SystemLogType = "END-1"
                Dim DailySales = sum(table:="loc_daily_transaction_details WHERE created_at = '" & Format(Now(), "yyyy-MM-dd") & "' AND active = 1 AND store_id = '" & ClientStoreID & "' AND guid = '" & ClientGuid & "' ", tototal:="total")
                EndingBalance = BeginningBalance + Val(DailySales)
            ElseIf Shift = "Second Shift" Then
                SystemLogType = "END-2"
                Dim DailySales = sum(table:="loc_daily_transaction_details WHERE created_at = '" & Format(Now(), "yyyy-MM-dd") & "' AND active = 1 AND store_id = '" & ClientStoreID & "' AND guid = '" & ClientGuid & "' ", tototal:="total")
                EndingBalance = BeginningBalance + Val(DailySales)
            ElseIf Shift = "Third Shift" Then
                SystemLogType = "END-3"
                Dim DailySales = sum(table:="loc_daily_transaction_details WHERE created_at = '" & Format(Now(), "yyyy-MM-dd") & "' AND active = 1 AND store_id = '" & ClientStoreID & "' AND guid = '" & ClientGuid & "' ", tototal:="total")
                EndingBalance = BeginningBalance + Val(DailySales)
            Else
                SystemLogType = "END-4"
                Dim DailySales = sum(table:="loc_daily_transaction_details WHERE created_at = '" & Format(Now(), "yyyy-MM-dd") & "' AND active = 1 AND store_id = '" & ClientStoreID & "' AND guid = '" & ClientGuid & "' ", tototal:="total")
                EndingBalance = BeginningBalance + Val(DailySales)
            End If
            SystemLogDesc = EndingBalance
            GLOBAL_SYSTEM_LOGS(SystemLogType, SystemLogDesc)
            Shift = ""
            BeginningBalance = 0
            EndingBalance = 0
            BegBalanceBool = False
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Public Module: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Dim total
    Public Function SumOfColumnsToDecimal(ByVal datagrid As DataGridView, ByVal celltocompute As Integer)
        Dim SumTotal As Decimal
        Try
            With datagrid
                For i As Integer = 0 To .Rows.Count() - 1 Step +1
                    SumTotal = SumTotal + .Rows(i).Cells(celltocompute).Value
                Next
            End With
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
        Return NUMBERFORMAT(SumTotal)
    End Function
    Public Function SumOfColumnsToInt(ByVal datagrid As DataGridView, ByVal celltocompute As Integer)
        Dim SumTotal As Integer
        Try
            With datagrid
                For i As Integer = 0 To .Rows.Count() - 1 Step +1
                    SumTotal += .Rows(i).Cells(celltocompute).Value
                Next
            End With
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
        Return SumTotal
    End Function
    Public Sub Numeric(ByVal sender As Object, ByVal e As KeyPressEventArgs)
        If e.KeyChar <> ControlChars.Back Then
            e.Handled = Not (Char.IsDigit(e.KeyChar) Or e.KeyChar = ".")
        End If
    End Sub
    Public Function NUMBERFORMAT(FormatThis)
        Return Format(FormatThis, "###,###,##0.00")
    End Function

    Dim ReturnRowIndex
    Public Function getCurrentCellButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        With POS
            If .DataGridViewOrders.Rows.Count > 0 Then
                ReturnRowIndex = .DataGridViewOrders.CurrentCell.RowIndex
            End If
        End With
        Return ReturnRowIndex
    End Function
    Public Sub RightToLeftDisplay(sender As Object, e As PrintPageEventArgs, position As Integer, lefttext As String, righttext As String, myfont As Font, wth As Single, frompoint As Single)
        Dim format As StringFormat = New StringFormat(StringFormatFlags.DirectionRightToLeft)
        Dim rect3 As RectangleF
        If My.Settings.PrintSize = "57mm" Then
            rect3 = New RectangleF(10.0F + frompoint, position, 173.0F + wth, 100.0F)
        Else
            rect3 = New RectangleF(10.0F + frompoint, position, 203.0F + wth, 100.0F)
        End If
        e.Graphics.DrawString(lefttext, myfont, Brushes.Black, rect3)
        e.Graphics.DrawString(righttext, myfont, Brushes.Black, rect3, format)
    End Sub
    Public Sub PrintStars(sender As Object, e As PrintPageEventArgs, FontDefault As Font, RowA As Integer)
        If My.Settings.PrintSize = "57mm" Then
            SimpleTextDisplay(sender, e, "*************************************", FontDefault, 0, RowA)
        Else
            SimpleTextDisplay(sender, e, "*********************************************", FontDefault, 0, RowA)
        End If
    End Sub
    Public Sub PrintCenterStars(sender As Object, e As PrintPageEventArgs, FontDefault As Font, RowA As Integer)
        If My.Settings.PrintSize = "57mm" Then
            FontDefault = New Font("Tahoma", 6)
        Else
            FontDefault = New Font("Tahoma", 7)
        End If
        CenterTextDisplay(sender, e, "*************************************", FontDefault, RowA)
        'FillEJournalContent("*************************************")
    End Sub
    Public Sub PrintSmallLine(sender As Object, e As PrintPageEventArgs, FontDefault As Font, RowA As Integer)
        SimpleTextDisplay(sender, e, "------------------------------------------------------------", FontDefault, 0, RowA)
        'FillEJournalContent("------------------------------------------------------------")
    End Sub

    Public Sub RightDisplay1(sender As Object, e As PrintPageEventArgs, position As Integer, lefttext As String, righttext As String, myfont As Font, wth As Single, frompoint As Single)
        Dim format As StringFormat = New StringFormat(StringFormatFlags.DirectionRightToLeft)
        Dim rect3 As RectangleF = New RectangleF(10.0F + frompoint, position, 0 + wth, 0)
        e.Graphics.DrawString(lefttext, myfont, Brushes.Black, rect3)
        e.Graphics.DrawString(righttext, myfont, Brushes.Black, rect3, format)
    End Sub
    Public Sub RightDisplay(sender As Object, e As PrintPageEventArgs, position As Integer, righttext As String, myfont As Font, wth As Single, frompoint As Single)
        Dim format As StringFormat = New StringFormat(StringFormatFlags.DirectionRightToLeft)
        Dim rect3 As RectangleF = New RectangleF(10.0F + frompoint, position, 120.0F + wth, 100.0F)
        e.Graphics.DrawString(righttext, myfont, Brushes.Black, rect3, format)
    End Sub
    Public Sub CenterTextDisplay(sender As Object, e As PrintPageEventArgs, myText As String, myFont As Font, myPosition As Integer)
        Dim sngCenterPagebrand As Single
        sngCenterPagebrand = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString(myText, myFont).Width / 2)
        e.Graphics.DrawString(myText, myFont, Brushes.Black, sngCenterPagebrand, myPosition)
    End Sub
    Public Sub SimpleTextDisplay(sender As Object, e As PrintPageEventArgs, myText As String, myFont As Font, ShopX As Integer, ShopY As Integer)
        Dim shopnameX As Integer = 10, shopnameY As Integer = 20
        e.Graphics.DrawString(myText, myFont, Brushes.Black, New PointF(shopnameX + ShopX, shopnameY + ShopY))
    End Sub
    Public Sub FormIsOpen()
        If Application.OpenForms().OfType(Of SynctoCloud).Any Then
            SynctoCloud.Close()
        End If
    End Sub
    Declare Auto Function SendMessage Lib "user32.dll" (ByVal hWnd As IntPtr, ByVal msg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
    Enum ProgressBarColor
        Green = &H1
        Red = &H2
        Yellow = &H3
    End Enum
    Public Sub ChangeProgBarColor(ByVal ProgressBar_Name As System.Windows.Forms.ProgressBar, ByVal ProgressBar_Color As ProgressBarColor)
        SendMessage(ProgressBar_Name.Handle, &H410, ProgressBar_Color, 0)
    End Sub
    Dim SalesInvoiceHeader As String = ""
    Dim OfficialInvoiceRefundBody As String = ""
    Dim ReceiptValidity As String = ""

    Public XML_Writer As XmlTextWriter


    Public Sub ReceiptHeaderOne(sender As Object, e As PrintPageEventArgs, VoidReturn As Boolean, TransactionNumber As String, ReprintSales As Boolean, SalesHeader As Boolean)
        Try
            RECEIPTLINECOUNT = 20
            Dim BrandFont As Font
            Dim FontDefault As Font
            Dim FontDefaultBold As Font
            Dim FontDefaultLine As Font
            If My.Settings.PrintSize = "57mm" Then
                BrandFont = New Font("Tahoma", 7, FontStyle.Bold)
                FontDefault = New Font("Tahoma", 5)
                FontDefaultBold = New Font("Tahoma", 6, FontStyle.Bold)
                FontDefaultLine = New Font("Tahoma", 6)
            Else
                BrandFont = New Font("Tahoma", 8, FontStyle.Bold)
                FontDefault = New Font("Tahoma", 6)
                FontDefaultBold = New Font("Tahoma", 7, FontStyle.Bold)
                FontDefaultLine = New Font("Tahoma", 7)
            End If
            CenterTextDisplay(sender, e, ClientBrand.ToUpper, BrandFont, 10)
            FillEJournalContent(ClientBrand.ToUpper, {}, "C", True, True)

            If SalesHeader Then

                Dim ConnectionLocal As MySqlConnection = LocalhostConn()
                Dim Sql As String

                If VoidReturn Then
                    Sql = "SELECT description, type FROM loc_receipt WHERE type IN ('Header','REFUND-HEADER','SALES-INVOICE','OFFICIAL-REFUND','VALIDITY') AND status = 1 ORDER BY id ASC"
                Else
                    Sql = "SELECT description, type FROM loc_receipt WHERE type IN ('Header','SALES-INVOICE','OFFICIAL-INVOICE','VALIDITY') AND status = 1 ORDER BY id ASC"
                End If

                Dim Cmd As MySqlCommand = New MySqlCommand(Sql, ConnectionLocal)
                Dim Da As MySqlDataAdapter = New MySqlDataAdapter(Cmd)
                Dim Dt As DataTable = New DataTable
                Da.Fill(Dt)

                For i As Integer = 0 To Dt.Rows.Count - 1 Step +1
                    If Dt(i)(1).ToString = "SALES-INVOICE" Then
                        SalesInvoiceHeader = Dt(i)(0).ToString
                    ElseIf Dt(i)(1).ToString = "REFUND-HEADER" Then
                        SalesInvoiceHeader = Dt(i)(0).ToString
                    ElseIf Dt(i)(1).ToString = "OFFICIAL-INVOICE" Then
                        OfficialInvoiceRefundBody = Dt(i)(0).ToString
                    ElseIf Dt(i)(1).ToString = "OFFICIAL-REFUND" Then
                        OfficialInvoiceRefundBody = Dt(i)(0).ToString
                    ElseIf Dt(i)(1).ToString = "VALIDITY" Then
                        ReceiptValidity = Dt(i)(0).ToString
                    Else
                        CenterTextDisplay(sender, e, Dt(i)(0), FontDefault, RECEIPTLINECOUNT)
                        FillEJournalContent(Dt(i)(0), {}, "C", False, True)
                        RECEIPTLINECOUNT += 10
                    End If
                Next

                RECEIPTLINECOUNT -= 20

                With POS

                    If ReprintSales Then
                        Sql = "SELECT * FROM loc_daily_transaction WHERE transaction_number = '" & TransactionNumber & "'"
                        Cmd = New MySqlCommand(Sql, ConnectionLocal)
                        Using reader As MySqlDataReader = Cmd.ExecuteReader
                            If reader.HasRows Then
                                While reader.Read
                                    PrintSmallLine(sender, e, FontDefaultLine, RECEIPTLINECOUNT)
                                    RECEIPTLINECOUNT += 10
                                    Dim SINumber = reader("si_number")
                                    If ReprintSales Then
                                        SimpleTextDisplay(sender, e, "SI No.: " & Format(SINumber, S_SIFormat) & " | REPRINT COPY", FontDefault, 0, RECEIPTLINECOUNT)
                                        FillEJournalContent("SI No.: " & Format(SINumber, S_SIFormat) & " | REPRINT COPY", {}, "S", False, True)
                                    Else
                                        If .Reprint = 1 Then
                                            SimpleTextDisplay(sender, e, "SI No.: " & Format(SINumber, S_SIFormat) & " | CUSTOMERS COPY", FontDefault, 0, RECEIPTLINECOUNT)
                                            FillEJournalContent("SI No.: " & Format(SINumber, S_SIFormat) & " | CUSTOMERS COPY", {}, "S", False, True)
                                        Else
                                            SimpleTextDisplay(sender, e, "SI No.: " & Format(SINumber, S_SIFormat) & " | REPRINT COPY", FontDefault, 0, RECEIPTLINECOUNT)
                                            FillEJournalContent("SI No.: " & Format(SINumber, S_SIFormat) & " | REPRINT COPY", {}, "S", False, True)
                                        End If
                                    End If

                                    RECEIPTLINECOUNT += 10
                                    SimpleTextDisplay(sender, e, "Cashier: " & reader("crew_id") & " " & returnfullname(reader("crew_id")), FontDefault, 0, RECEIPTLINECOUNT)
                                    FillEJournalContent("Cashier: " & reader("crew_id") & " " & returnfullname(reader("crew_id")), {}, "S", False, True)
                                    RECEIPTLINECOUNT += 10
                                    SimpleTextDisplay(sender, e, "Date: " & reader("created_at"), FontDefault, 0, RECEIPTLINECOUNT)
                                    FillEJournalContent("Date: " & reader("created_at"), {}, "S", False, True)
                                    RECEIPTLINECOUNT += 10
                                    PrintCenterStars(sender, e, FontDefault, RECEIPTLINECOUNT + 25)
                                    FillEJournalContent("*************************************", {}, "C", True, True)
                                    RECEIPTLINECOUNT += 10
                                    CenterTextDisplay(sender, e, SalesInvoiceHeader, FontDefaultBold, RECEIPTLINECOUNT + 23)
                                    FillEJournalContent(SalesInvoiceHeader, {}, "C", True, True)
                                    RECEIPTLINECOUNT += 10
                                    PrintCenterStars(sender, e, FontDefault, RECEIPTLINECOUNT + 25)
                                    FillEJournalContent("*************************************", {}, "C", True, True)
                                    RECEIPTLINECOUNT += 10
                                    PrintSmallLine(sender, e, FontDefaultLine, RECEIPTLINECOUNT)
                                    FillEJournalContent("------------------------------------------------------------", {}, "C", True, True)
                                    RECEIPTLINECOUNT += 10
                                End While
                            End If
                        End Using
                    Else
                        PrintSmallLine(sender, e, FontDefaultLine, RECEIPTLINECOUNT)
                        FillEJournalContent("------------------------------------------------------------", {}, "C", True, True)
                        RECEIPTLINECOUNT += 10

                        If VoidReturn Then
                            SimpleTextDisplay(sender, e, "SI No.: " & SiNumberToString & " | CUSTOMERS COPY", FontDefault, 0, RECEIPTLINECOUNT)
                            FillEJournalContent("SI No.: " & SiNumberToString & " | CUSTOMERS COPY", {}, "S", False, True)
                        Else
                            If .Reprint = 1 Then
                                SimpleTextDisplay(sender, e, "SI No.: " & SiNumberToString & " | CUSTOMERS COPY", FontDefault, 0, RECEIPTLINECOUNT)
                                FillEJournalContent("SI No.: " & SiNumberToString & " | CUSTOMERS COPY", {}, "S", False, True)
                            Else
                                SimpleTextDisplay(sender, e, "SI No.: " & SiNumberToString & " | REPRINT COPY", FontDefault, 0, RECEIPTLINECOUNT)
                                FillEJournalContent("SI No.: " & SiNumberToString & " | REPRINT COPY", {}, "S", False, True)
                            End If
                        End If
                        RECEIPTLINECOUNT += 10
                        Dim Cashier = "Cashier: " & ClientCrewID & " " & returnfullname(where:=ClientCrewID)
                        SimpleTextDisplay(sender, e, Cashier, FontDefault, 0, RECEIPTLINECOUNT)
                        FillEJournalContent(Cashier, {}, "S", False, True)
                        RECEIPTLINECOUNT += 10
                        SimpleTextDisplay(sender, e, "Date: " & .INSERTTHISDATE, FontDefault, 0, RECEIPTLINECOUNT)
                        FillEJournalContent("Date: " & .INSERTTHISDATE, {}, "S", False, True)
                        RECEIPTLINECOUNT += 10
                        PrintCenterStars(sender, e, FontDefault, RECEIPTLINECOUNT + 25)
                        FillEJournalContent("*************************************", {}, "C", True, True)
                        RECEIPTLINECOUNT += 10
                        CenterTextDisplay(sender, e, SalesInvoiceHeader, FontDefaultBold, RECEIPTLINECOUNT + 23)
                        FillEJournalContent(SalesInvoiceHeader, {}, "C", False, True)
                        RECEIPTLINECOUNT += 10
                        PrintCenterStars(sender, e, FontDefault, RECEIPTLINECOUNT + 25)
                        FillEJournalContent("*************************************", {}, "C", True, True)
                        RECEIPTLINECOUNT += 10
                        PrintSmallLine(sender, e, FontDefaultLine, RECEIPTLINECOUNT)
                        FillEJournalContent("------------------------------------------------------------", {}, "C", True, True)
                        RECEIPTLINECOUNT += 10
                    End If
                End With
            Else

                Dim ConnectionLocal As MySqlConnection = LocalhostConn()
                Dim Sql As String = "SELECT description, type FROM loc_receipt WHERE type IN ('Header') AND status = 1 ORDER BY id ASC"
                Dim Cmd As MySqlCommand = New MySqlCommand(Sql, ConnectionLocal)
                Dim Da As MySqlDataAdapter = New MySqlDataAdapter(Cmd)
                Dim Dt As DataTable = New DataTable
                Da.Fill(Dt)

                For i As Integer = 0 To Dt.Rows.Count - 1 Step +1
                    If Dt(i)(1).ToString = "Header" Then
                        CenterTextDisplay(sender, e, Dt(i)(0), FontDefault, RECEIPTLINECOUNT)
                        FillEJournalContent(Dt(i)(0), {}, "C", True, True)
                        RECEIPTLINECOUNT += 10
                    End If
                Next
                RECEIPTLINECOUNT -= 20
            End If

        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Public Module: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub


    Public Sub ZXBody(sender As Object, e As PrintPageEventArgs)
        Try
            Dim BrandFont As Font
            Dim FontDefault As Font
            Dim FontDefaultBold As Font
            Dim BodySpacing As Integer = 0
            Dim FontDefaultLine As Font
            If My.Settings.PrintSize = "57mm" Then
                FontDefaultLine = New Font("Tahoma", 6)
                BrandFont = New Font("Tahoma", 7, FontStyle.Bold)
                FontDefault = New Font("Tahoma", 5)
                FontDefaultBold = New Font("Tahoma", 5, FontStyle.Bold)
            Else
                BrandFont = New Font("Tahoma", 8, FontStyle.Bold)
                FontDefault = New Font("Tahoma", 6)
                FontDefaultBold = New Font("Tahoma", 6, FontStyle.Bold)
                FontDefaultLine = New Font("Tahoma", 7)
                BodySpacing = 20
            End If

            SimpleTextDisplay(sender, e, XREADORZREAD, FontDefaultBold, 0, RECEIPTLINECOUNT)
            FillEJournalContent(XREADORZREAD, {}, "S", True, True)
            RECEIPTLINECOUNT += 10
            PrintSmallLine(sender, e, FontDefaultLine, RECEIPTLINECOUNT)
            FillEJournalContent("------------------------------------------------------------", {}, "C", True, True)
            RECEIPTLINECOUNT += 10
            SimpleTextDisplay(sender, e, "DESCRIPTION", FontDefaultBold, 0, RECEIPTLINECOUNT)
            SimpleTextDisplay(sender, e, "QTY/AMOUNT", FontDefaultBold, 130 + BodySpacing, RECEIPTLINECOUNT)
            FillEJournalContent("DESCRIPTION                    QTY/AMOUNT", {"DESCRIPTION", " QTY/AMOUNT"}, "LR", False, False)
            RECEIPTLINECOUNT += 10
            PrintSmallLine(sender, e, FontDefaultLine, RECEIPTLINECOUNT)
            FillEJournalContent("------------------------------------------------------------", {}, "C", True, True)
            RECEIPTLINECOUNT += 30
            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "TERMINAL NO.", S_Terminal_No, FontDefault, 5, 0)
            FillEJournalContent("TERMINAL NO.          " & S_Terminal_No, {"TERMINAL NO.", S_Terminal_No}, "LR", True, False)
            RECEIPTLINECOUNT += 10
            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "GROSS", NUMBERFORMAT(ZXGross), FontDefault, 5, 0)
            FillEJournalContent("GROSS         " & NUMBERFORMAT(ZXGross), {"GROSS", NUMBERFORMAT(ZXGross)}, "LR", False, False)
            RECEIPTLINECOUNT += 10
            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "LESS VAT (VE)", NUMBERFORMAT(ZXLessVat), FontDefault, 5, 0)
            FillEJournalContent("LESS VAT (VE)         " & ZXLessVat, {"LESS VAT (VE)", NUMBERFORMAT(ZXLessVat)}, "LR", False, False)
            RECEIPTLINECOUNT += 10
            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "LESS VAT DIPLOMAT", "0.00", FontDefault, 5, 0)
            FillEJournalContent("LESS VAT DIPLOMAT         0.00", {"LESS VAT DIPLOMAT", "0.00"}, "LR", False, False)
            RECEIPTLINECOUNT += 10
            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "LESS VAT (OTHER)", NUMBERFORMAT(ZXLessVatOthers), FontDefault, 5, 0)
            FillEJournalContent("LESS VAT (OTHER)          0.00", {"LESS VAT (OTHER)", "0.00"}, "LR", False, False)
            RECEIPTLINECOUNT += 10
            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "ADD VAT", NUMBERFORMAT(ZXVatAmount), FontDefault, 5, 0)
            FillEJournalContent("ADD VAT          0.00", {"ADD VAT", NUMBERFORMAT(ZXVatAmount)}, "LR", False, False)
            RECEIPTLINECOUNT += 10
            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "DAILY SALES", NUMBERFORMAT(ZXDailySales), FontDefault, 5, 0)
            FillEJournalContent("DAILY SALES          " & NUMBERFORMAT(ZXDailySales), {"DAILY SALES", NUMBERFORMAT(ZXDailySales)}, "LR", False, False)
            RECEIPTLINECOUNT += 20
            FillEJournalContent("", {}, "C", True, True)
            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "VAT AMOUNT", NUMBERFORMAT(ZXVatAmount), FontDefault, 5, 0)
            FillEJournalContent("VAT AMOUNT          " & NUMBERFORMAT(ZXVatAmount), {"VAT AMOUNT", NUMBERFORMAT(ZXVatAmount)}, "LR", False, False)
            RECEIPTLINECOUNT += 10
            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "LOCAL GOV'T TAX", "0.00", FontDefault, 5, 0)
            FillEJournalContent("LOCAL GOV'T TAX          0.00", {"LOCAL GOV'T TAX", "0.00"}, "LR", False, False)
            RECEIPTLINECOUNT += 10
            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "VATABLE SALES", NUMBERFORMAT(ZXVatableSales), FontDefault, 5, 0)
            FillEJournalContent("VATABLE SALES          " & NUMBERFORMAT(ZXVatableSales), {"VATABLE SALES", NUMBERFORMAT(ZXVatableSales)}, "LR", False, False)
            RECEIPTLINECOUNT += 10
            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "ZERO RATED SALES", NUMBERFORMAT(ZXZeroRatedSales), FontDefault, 5, 0)
            FillEJournalContent("ZERO RATED SALES          " & NUMBERFORMAT(ZXZeroRatedSales), {"ZERO RATED SALES", NUMBERFORMAT(ZXZeroRatedSales)}, "LR", False, False)
            RECEIPTLINECOUNT += 10
            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "VAT EXEMPT SALES", NUMBERFORMAT(ZXVatExemptSales), FontDefault, 5, 0)
            FillEJournalContent("VAT EXEMPT SALES          " & NUMBERFORMAT(ZXVatExemptSales), {"VAT EXEMPT SALES", NUMBERFORMAT(ZXVatExemptSales)}, "LR", False, False)
            RECEIPTLINECOUNT += 10
            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "LESS DISC (VE)", NUMBERFORMAT(ZXLessDiscVE), FontDefault, 5, 0)
            FillEJournalContent("LESS DISC (VE)          " & NUMBERFORMAT(ZXLessDiscVE), {"LESS DISC (VE)", NUMBERFORMAT(ZXLessDiscVE)}, "LR", False, False)
            RECEIPTLINECOUNT += 10
            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "NET SALES", NUMBERFORMAT(ZXNetSales), FontDefault, 5, 0)
            FillEJournalContent("NET SALES          " & NUMBERFORMAT(ZXNetSales), {"NET SALES", NUMBERFORMAT(ZXNetSales)}, "LR", False, False)
            RECEIPTLINECOUNT += 20

            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "CASH TOTAL", NUMBERFORMAT(ZXDailySales), FontDefault, 5, 0)
            FillEJournalContent("CASH TOTAL          " & NUMBERFORMAT(ZXDailySales), {"CASH TOTAL", NUMBERFORMAT(ZXDailySales)}, "LR", False, False)
            RECEIPTLINECOUNT += 10
            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "CREDIT CARD", "N/A", FontDefault, 5, 0)
            FillEJournalContent("CREDIT CARD          N/A", {"CREDIT CARD", "N/A"}, "LR", False, False)
            RECEIPTLINECOUNT += 10
            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "DEBIT CARD", "N/A", FontDefault, 5, 0)
            FillEJournalContent("DEBIT CARD          N/A", {"DEBIT CARD", "N/A"}, "LR", False, False)
            RECEIPTLINECOUNT += 10
            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "MISC/CHEQUES", "N/A", FontDefault, 5, 0)
            FillEJournalContent("MISC/CHEQUES          N/A", {"MISC/CHEQUES", "N/A"}, "LR", False, False)
            RECEIPTLINECOUNT += 10
            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "GIFT CARD(GC)", NUMBERFORMAT(ZXGiftCard), FontDefault, 5, 0)
            FillEJournalContent("GIFT CARD(GC)          " & NUMBERFORMAT(ZXGiftCard), {"GIFT CARD(GC)", NUMBERFORMAT(ZXGiftCard)}, "LR", False, False)
            RECEIPTLINECOUNT += 10
            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "GIFT CARD SUM", NUMBERFORMAT(ZXGiftCardSum), FontDefault, 5, 0)
            FillEJournalContent("GIFT CARD SUM          " & NUMBERFORMAT(ZXGiftCardSum), {"GIFT CARD SUM", NUMBERFORMAT(ZXGiftCardSum)}, "LR", False, False)
            RECEIPTLINECOUNT += 10
            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "A/R", "N/A", FontDefault, 5, 0)
            FillEJournalContent("A/R          N/A", {"A/R", "N/A"}, "LR", False, False)
            RECEIPTLINECOUNT += 10
            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "TOTAL EXPENSES", NUMBERFORMAT(ZXTotalExpenses), FontDefault, 5, 0)
            FillEJournalContent("TOTAL EXPENSES         " & NUMBERFORMAT(ZXTotalExpenses), {"TOTAL EXPENSES", NUMBERFORMAT(ZXTotalExpenses)}, "LR", False, False)
            RECEIPTLINECOUNT += 10
            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "OTHERS", "N/A", FontDefault, 5, 0)
            FillEJournalContent("OTHERS         N/A", {"OTHERS", "N/A"}, "LR", False, False)
            RECEIPTLINECOUNT += 10

            If ZXBegBalance = 0 Then
                RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "BEG.BALANCE", "0.00", FontDefault, 5, 0)
                FillEJournalContent("BEG.BALANCE         0.00", {"BEG.BALANCE", "0.00"}, "LR", False, False)
                RECEIPTLINECOUNT += 10
            Else
                RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "BEG.BALANCE", NUMBERFORMAT(Double.Parse(ZXBegBalance)), FontDefault, 5, 0)
                FillEJournalContent("BEG.BALANCE          " & NUMBERFORMAT(Double.Parse(ZXBegBalance)), {"BEG.BALANCE", NUMBERFORMAT(Double.Parse(ZXBegBalance))}, "LR", False, False)
                RECEIPTLINECOUNT += 10
            End If

            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "DEPOSIT", NUMBERFORMAT(ZXDeposits), FontDefault, 5, 0)
            FillEJournalContent("DEPOSIT          " & NUMBERFORMAT(ZXDeposits), {"DEPOSIT", NUMBERFORMAT(ZXDeposits)}, "LR", False, False)
            RECEIPTLINECOUNT += 10
            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "CASH IN DRAWER", NUMBERFORMAT(ZXCashInDrawer), FontDefault, 5, 0)
            FillEJournalContent("CASH IN DRAWER          " & NUMBERFORMAT(ZXCashInDrawer), {"CASH IN DRAWER", NUMBERFORMAT(ZXCashInDrawer)}, "LR", False, False)
            RECEIPTLINECOUNT += 20
            FillEJournalContent("", {}, "C", True, True)
            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "CASHLESS", NUMBERFORMAT(ZXCashlessTotal), FontDefault, 5, 0)
            FillEJournalContent("CASHLESS          " & NUMBERFORMAT(ZXCashlessTotal), {"CASHLESS", NUMBERFORMAT(ZXCashlessTotal)}, "LR", False, False)
            RECEIPTLINECOUNT += 10
            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "GCASH", NUMBERFORMAT(ZXGcash), FontDefault, 5, 0)
            FillEJournalContent("GCASH          " & NUMBERFORMAT(ZXGcash), {"GCASH", NUMBERFORMAT(ZXGcash)}, "LR", False, False)
            RECEIPTLINECOUNT += 10
            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "PAYMAYA", NUMBERFORMAT(ZXPaymaya), FontDefault, 5, 0)
            FillEJournalContent("PAYMAYA          " & NUMBERFORMAT(ZXPaymaya), {"PAYMAYA", NUMBERFORMAT(ZXPaymaya)}, "LR", False, False)
            RECEIPTLINECOUNT += 10
            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "SHOPEE", NUMBERFORMAT(ZXShopeePay), FontDefault, 5, 0)
            FillEJournalContent("SHOPEE          " & NUMBERFORMAT(ZXShopeePay), {"SHOPEE", NUMBERFORMAT(ZXShopeePay)}, "LR", False, False)
            RECEIPTLINECOUNT += 10
            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "FOODPANDA", NUMBERFORMAT(ZXFoodPanda), FontDefault, 5, 0)
            FillEJournalContent("FOODPANDA          " & NUMBERFORMAT(ZXFoodPanda), {"FOODPANDA", NUMBERFORMAT(ZXFoodPanda)}, "LR", False, False)
            RECEIPTLINECOUNT += 10
            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "GRAB", NUMBERFORMAT(ZXGrabFood), FontDefault, 5, 0)
            FillEJournalContent("GRAB          " & NUMBERFORMAT(ZXGrabFood), {"GRAB", NUMBERFORMAT(ZXGrabFood)}, "LR", False, False)
            RECEIPTLINECOUNT += 10
            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "COMPLIMENTARY EXPENSES", NUMBERFORMAT(ZXRepExpense), FontDefault, 5, 0)
            FillEJournalContent("COMPLIMENTARY EXPENSES          " & NUMBERFORMAT(ZXRepExpense), {"COMPLIMENTARY EXPENSES", NUMBERFORMAT(ZXRepExpense)}, "LR", False, False)
            RECEIPTLINECOUNT += 10
            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "OTHERS", NUMBERFORMAT(ZXCashlessOthers), FontDefault, 5, 0)
            FillEJournalContent("OTHERS          " & NUMBERFORMAT(ZXCashlessOthers), {"OTHERS", NUMBERFORMAT(ZXCashlessOthers)}, "LR", False, False)
            RECEIPTLINECOUNT += 10
            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "ITEM VOID E/C", NUMBERFORMAT(ZXItemVoidEC), FontDefault, 5, 0)
            FillEJournalContent("ITEM VOID E/C          " & NUMBERFORMAT(ZXItemVoidEC), {"ITEM VOID E/C", NUMBERFORMAT(ZXItemVoidEC)}, "LR", False, False)
            RECEIPTLINECOUNT += 10
            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "TRANSACTION VOID", NUMBERFORMAT(ZXReturnsExchange), FontDefault, 5, 0)
            FillEJournalContent("TRANSACTION VOID         " & NUMBERFORMAT(ZXReturnsExchange), {"TRANSACTION VOID", NUMBERFORMAT(ZXReturnsExchange)}, "LR", False, False)
            RECEIPTLINECOUNT += 10
            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "TRANSACTION CANCEL", NUMBERFORMAT(ZXTransactionCancel), FontDefault, 5, 0)
            FillEJournalContent("TRANSACTION CANCEL         " & NUMBERFORMAT(ZXTransactionCancel), {"TRANSACTION CANCEL", NUMBERFORMAT(ZXTransactionCancel)}, "LR", False, False)
            RECEIPTLINECOUNT += 10
            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "DIMPLOMAT", "N/A", FontDefault, 5, 0)
            FillEJournalContent("DIMPLOMAT         N/A", {"DIMPLOMAT", "N/A"}, "LR", False, False)
            RECEIPTLINECOUNT += 10
            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "TOTAL DISCOUNTS", NUMBERFORMAT(ZXTotalDiscounts), FontDefault, 5, 0)
            FillEJournalContent("TOTAL DISCOUNTS         " & NUMBERFORMAT(ZXTotalDiscounts), {"TOTAL DISCOUNTS", NUMBERFORMAT(ZXTotalDiscounts)}, "LR", False, False)
            RECEIPTLINECOUNT += 10
            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, " - SENIOR CITIZEN", NUMBERFORMAT(ZXSeniorCitizen), FontDefault, 5, 0)
            FillEJournalContent(" - SENIOR CITIZEN         " & NUMBERFORMAT(ZXSeniorCitizen), {" - SENIOR CITIZEN", NUMBERFORMAT(ZXSeniorCitizen)}, "LR", False, False)
            RECEIPTLINECOUNT += 10
            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, " - PWD", NUMBERFORMAT(ZXPWD), FontDefault, 5, 0)
            FillEJournalContent(" - PWD         " & NUMBERFORMAT(ZXPWD), {" -PWD", NUMBERFORMAT(ZXPWD)}, "LR", False, False)
            RECEIPTLINECOUNT += 10
            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, " - ATHLETE", NUMBERFORMAT(ZXAthlete), FontDefault, 5, 0)
            FillEJournalContent(" - ATHLETE         " & NUMBERFORMAT(ZXAthlete), {" - ATHLETE", NUMBERFORMAT(ZXAthlete)}, "LR", False, False)
            RECEIPTLINECOUNT += 10
            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, " - OTHERS", "0.00", FontDefault, 5, 0)
            FillEJournalContent(" - OTHERS         0.00", {" - OTHERS", "0.00"}, "LR", False, False)
            RECEIPTLINECOUNT += 10
            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "TAKE OUT CHARGE", "N/A", FontDefault, 5, 0)
            FillEJournalContent("TAKE OUT CHARGE         N/A", {"TAKE OUT CHARGE", "N/A"}, "LR", False, False)
            RECEIPTLINECOUNT += 10
            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "DELIVERY CHARGE", "N/A", FontDefault, 5, 0)
            FillEJournalContent("DELIVERY CHARGE         N/A", {"DELIVERY CHARGE", "N/A"}, "LR", False, False)
            RECEIPTLINECOUNT += 10
            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "RETURNS EXCHANGE", NUMBERFORMAT(ZXReturnsExchange), FontDefault, 5, 0)
            FillEJournalContent("RETURNS EXCHANGE         " & NUMBERFORMAT(ZXReturnsExchange), {"RETURNS EXCHANGE", NUMBERFORMAT(ZXReturnsExchange)}, "LR", False, False)
            RECEIPTLINECOUNT += 10
            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "RETURNS REFUND", NUMBERFORMAT(ZXReturnsRefund), FontDefault, 5, 0)
            FillEJournalContent("RETURNS REFUND         " & NUMBERFORMAT(ZXReturnsRefund), {"RETURNS REFUND", NUMBERFORMAT(ZXReturnsRefund)}, "LR", False, False)
            RECEIPTLINECOUNT += 10
            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "TOTAL QTY SOLD", ZXTotalQTYSold, FontDefault, 5, 0)
            FillEJournalContent("TOTAL QTY SOLD         " & NUMBERFORMAT(ZXTotalQTYSold), {"TOTAL QTY SOLD", NUMBERFORMAT(ZXTotalQTYSold)}, "LR", False, False)
            RECEIPTLINECOUNT += 10
            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "TOTAL TRANS. COUNT", ZXTotalTransactionCount, FontDefault, 5, 0)
            FillEJournalContent("TOTAL TRANS. COUNT         " & NUMBERFORMAT(ZXTotalTransactionCount), {"TOTAL TRANS. COUNT", NUMBERFORMAT(ZXTotalTransactionCount)}, "LR", False, False)
            RECEIPTLINECOUNT += 10
            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "TOTAL GUEST", ZXTotalGuess, FontDefault, 5, 0)
            FillEJournalContent("TOTAL GUEST         " & ZXTotalGuess, {"TOTAL GUEST", ZXTotalGuess}, "LR", False, False)
            RECEIPTLINECOUNT += 10
            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "BEGINNING S.I NO.", ZXBegSINo, FontDefault, 5, 0)
            FillEJournalContent("BEGINNING S.I NO.         " & ZXBegSINo, {"BEGINNING S.I NO.", ZXBegSINo}, "LR", False, False)
            RECEIPTLINECOUNT += 10
            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "END S.I NO.", ZXEndSINo, FontDefault, 5, 0)
            FillEJournalContent("END S.I NO.         " & ZXEndSINo, {"END S.I NO.", ZXEndSINo}, "LR", False, False)
            RECEIPTLINECOUNT += 10
            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "BEGINNING TRANS. NO.", ZXBegTransNo, FontDefault, 5, 0)
            FillEJournalContent("BEGINNING TRANS. NO.         " & ZXBegTransNo, {"BEGINNING TRANS. NO.", ZXBegTransNo}, "LR", False, False)
            RECEIPTLINECOUNT += 10
            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "END TRANS. NO.", ZXEndTransNo, FontDefault, 5, 0)
            FillEJournalContent("END TRANS. NO.         " & ZXEndTransNo, {"END TRANS. NO.", ZXEndTransNo}, "LR", False, False)
            RECEIPTLINECOUNT += 10
            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "CURRENT TOTAL SALES", NUMBERFORMAT(ZXNetSales), FontDefault, 5, 0)
            FillEJournalContent("CURRENT TOTAL SALES         " & NUMBERFORMAT(ZXNetSales), {"CURRENT TOTAL SALES", NUMBERFORMAT(ZXNetSales)}, "LR", False, False)
            RECEIPTLINECOUNT += 10
            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "OLD GRAND TOTAL", NUMBERFORMAT(S_OLDGRANDTOTAL), FontDefault, 5, 0)
            FillEJournalContent("OLD GRAND TOTAL         " & NUMBERFORMAT(S_OLDGRANDTOTAL), {"OLD GRAND TOTAL", NUMBERFORMAT(S_OLDGRANDTOTAL)}, "LR", False, False)
            RECEIPTLINECOUNT += 10
            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "NEW GRAND TOTAL", NUMBERFORMAT(ZXNewGrandtotalSales), FontDefault, 5, 0)
            FillEJournalContent("NEW GRAND TOTAL         " & NUMBERFORMAT(ZXNewGrandtotalSales), {"NEW GRAND TOTAL", NUMBERFORMAT(ZXNewGrandtotalSales)}, "LR", False, False)
            RECEIPTLINECOUNT += 10

            If XREADORZREAD = "Z-READ" Then
                RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "RESET COUNTER", ZXResetCounter, FontDefault, 5, 0)
                FillEJournalContent("RESET COUNTER         " & NUMBERFORMAT(ZXResetCounter), {"RESET COUNTER", NUMBERFORMAT(ZXResetCounter)}, "LR", False, False)
                RECEIPTLINECOUNT += 10
                RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "Z-COUNTER", ZXZreadCounter, FontDefault, 5, 0)
                FillEJournalContent("Z-COUNTER         " & NUMBERFORMAT(ZXZreadCounter), {"Z-COUNTER", NUMBERFORMAT(ZXZreadCounter)}, "LR", False, False)
                RECEIPTLINECOUNT += 10
                RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "RE-PRINT COUNT: ", ZXReprintCount, FontDefault, 5, 0)
                FillEJournalContent("RE-PRINT COUNT:          " & ZXReprintCount, {"RE-PRINT COUNT: ", ZXReprintCount}, "LR", False, False)
            Else
                RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "CASHIER", ZXCashier, FontDefault, 5, 0)
                FillEJournalContent("CASHIER          " & ZXCashier, {"CASHIER", ZXCashier}, "LR", False, False)
            End If

            PrintSmallLine(sender, e, FontDefaultLine, RECEIPTLINECOUNT)
            FillEJournalContent("------------------------------------------------------------", {}, "C", True, True)
            RECEIPTLINECOUNT += 10
            SimpleTextDisplay(sender, e, "SALES BY CLASS", FontDefault, 0, RECEIPTLINECOUNT)
            FillEJournalContent("SALES BY CLASS", {}, "S", False, True)
            RECEIPTLINECOUNT += 30
            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "ADD ONS", ZXAddOns, FontDefault, 5, 0)
            FillEJournalContent("ADD ONS          " & ZXAddOns, {"ADD ONS", ZXAddOns}, "LR", False, False)
            RECEIPTLINECOUNT += 10
            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "FAMOUS BLENDS", ZXFamousBlends, FontDefault, 5, 0)
            FillEJournalContent("FAMOUS BLENDS          " & ZXFamousBlends, {"FAMOUS BLENDS", ZXFamousBlends}, "LR", False, False)
            RECEIPTLINECOUNT += 10
            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "COMBO", ZXCombo, FontDefault, 5, 0)
            FillEJournalContent("COMBO          " & ZXCombo, {"COMBO", ZXCombo}, "LR", False, False)
            RECEIPTLINECOUNT += 10
            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "PERFECT COMBINATION", ZXPerfectCombination, FontDefault, 5, 0)
            FillEJournalContent("PERFECT COMBINATION          " & ZXPerfectCombination, {"PERFECT COMBINATION", ZXPerfectCombination}, "LR", False, False)
            RECEIPTLINECOUNT += 10
            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "PREMIUM LINE", ZXPremium, FontDefault, 5, 0)
            FillEJournalContent("PREMIUM LINE          " & ZXPremium, {"PREMIUM LINE", ZXPremium}, "LR", False, False)
            RECEIPTLINECOUNT += 10
            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "SAVORY", ZXSavoury, FontDefault, 5, 0)
            FillEJournalContent("SAVORY          " & ZXSavoury, {"SAVORY", ZXSavoury}, "LR", False, False)
            RECEIPTLINECOUNT += 10
            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "SIMPY PERFECT", ZXSimplyPerfect, FontDefault, 5, 0)
            FillEJournalContent("SIMPY PERFECT          " & ZXSimplyPerfect, {"SIMPY PERFECT", ZXSimplyPerfect}, "LR", False, False)
            RECEIPTLINECOUNT -= 10

            PrintSmallLine(sender, e, FontDefaultLine, RECEIPTLINECOUNT)
            FillEJournalContent("------------------------------------------------------------", {}, "C", True, True)
            RECEIPTLINECOUNT += 10
            SimpleTextDisplay(sender, e, "CASH BREAK DOWN", FontDefaultBold, 0, RECEIPTLINECOUNT)
            FillEJournalContent("CASH BREAK DOWN", {}, "S", False, True)
            RECEIPTLINECOUNT += 10
            SimpleTextDisplay(sender, e, "Bill type", FontDefault, 0, RECEIPTLINECOUNT)
            SimpleTextDisplay(sender, e, "Quantity", FontDefault, 80, RECEIPTLINECOUNT)
            SimpleTextDisplay(sender, e, "Total", FontDefault, 160, RECEIPTLINECOUNT)
            FillEJournalContent("Bill type           Quantity           Total", {"Bill type", "Quantity", "Total"}, "S3", False, False)
            RECEIPTLINECOUNT += 30
            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "₱ 1000", NUMBERFORMAT(ZXThousandTotal), FontDefault, 5, 0)
            RightDisplay1(sender, e, RECEIPTLINECOUNT, "", ZXThousandQty, FontDefault, 0, 110)
            FillEJournalContent("₱ 1000           " & ZXThousandQty & "           " & NUMBERFORMAT(ZXThousandTotal), {"₱ 1000", ZXThousandQty, NUMBERFORMAT(ZXThousandTotal)}, "S3", False, False)
            RECEIPTLINECOUNT += 10
            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "₱ 500", NUMBERFORMAT(ZXFiveHundredTotal), FontDefault, 5, 0)
            RightDisplay1(sender, e, RECEIPTLINECOUNT, "", ZXFiveHundredQty, FontDefault, 0, 110)
            FillEJournalContent("₱ 500           " & ZXFiveHundredQty & "           " & NUMBERFORMAT(ZXFiveHundredTotal), {"₱ 500", ZXFiveHundredQty, NUMBERFORMAT(ZXFiveHundredTotal)}, "S3", False, False)
            RECEIPTLINECOUNT += 10
            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "₱ 200", NUMBERFORMAT(ZXTwoHundredTotal), FontDefault, 5, 0)
            RightDisplay1(sender, e, RECEIPTLINECOUNT, "", ZXTwoHundredQty, FontDefault, 0, 110)
            FillEJournalContent("₱ 200           " & ZXTwoHundredQty & "           " & NUMBERFORMAT(ZXTwoHundredTotal), {"₱ 200", ZXTwoHundredQty, NUMBERFORMAT(ZXTwoHundredTotal)}, "S3", False, False)
            RECEIPTLINECOUNT += 10
            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "₱ 100", NUMBERFORMAT(ZXOneHundredTotal), FontDefault, 5, 0)
            RightDisplay1(sender, e, RECEIPTLINECOUNT, "", ZXOneHundredQty, FontDefault, 0, 110)
            FillEJournalContent("₱ 100           " & ZXOneHundredQty & "           " & NUMBERFORMAT(ZXOneHundredTotal), {"₱ 100", ZXOneHundredQty, NUMBERFORMAT(ZXOneHundredTotal)}, "S3", False, False)
            RECEIPTLINECOUNT += 10
            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "₱ 50", NUMBERFORMAT(ZXFiftyTotal), FontDefault, 5, 0)
            RightDisplay1(sender, e, RECEIPTLINECOUNT, "", ZXFiftyQty, FontDefault, 0, 110)
            FillEJournalContent("₱ 50           " & ZXFiftyQty & "           " & NUMBERFORMAT(ZXFiftyTotal), {"₱ 50", ZXFiftyQty, NUMBERFORMAT(ZXFiftyTotal)}, "S3", False, False)
            RECEIPTLINECOUNT += 10
            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "₱ 20", NUMBERFORMAT(ZXTwentyTotal), FontDefault, 5, 0)
            RightDisplay1(sender, e, RECEIPTLINECOUNT, "", ZXTwentyQty, FontDefault, 0, 110)
            FillEJournalContent("₱ 20           " & ZXTwentyQty & "           " & NUMBERFORMAT(ZXTwentyTotal), {"₱ 20", ZXTwentyQty, NUMBERFORMAT(ZXTwentyTotal)}, "S3", False, False)
            RECEIPTLINECOUNT += 10
            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "₱ 10", NUMBERFORMAT(ZXTenTotal), FontDefault, 5, 0)
            RightDisplay1(sender, e, RECEIPTLINECOUNT, "", ZXTenQty, FontDefault, 0, 110)
            FillEJournalContent("₱ 10           " & ZXTenQty & "           " & NUMBERFORMAT(ZXTenTotal), {"₱ 10", ZXTenQty, NUMBERFORMAT(ZXTenTotal)}, "S3", False, False)
            RECEIPTLINECOUNT += 10
            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "₱ 5", NUMBERFORMAT(ZXFiveTotal), FontDefault, 5, 0)
            RightDisplay1(sender, e, RECEIPTLINECOUNT, "", ZXFiveQty, FontDefault, 0, 110)
            FillEJournalContent("₱ 5           " & ZXFiveQty & "           " & NUMBERFORMAT(ZXFiveTotal), {"₱ 5", ZXFiveQty, NUMBERFORMAT(ZXFiveTotal)}, "S3", False, False)
            RECEIPTLINECOUNT += 10
            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "₱ 1", NUMBERFORMAT(ZXOneTotal), FontDefault, 5, 0)
            RightDisplay1(sender, e, RECEIPTLINECOUNT, "", ZXOneQty, FontDefault, 0, 110)
            FillEJournalContent("₱ 1           " & ZXOneQty & "           " & NUMBERFORMAT(ZXOneTotal), {"₱ 1", ZXOneQty, NUMBERFORMAT(ZXOneTotal)}, "S3", False, False)
            RECEIPTLINECOUNT += 10
            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "₱ .25", NUMBERFORMAT(ZXPointTwentyFiveTotal), FontDefault, 5, 0)
            RightDisplay1(sender, e, RECEIPTLINECOUNT, "", ZXPointTwentyFiveQty, FontDefault, 0, 110)
            FillEJournalContent("₱ .25           " & ZXPointTwentyFiveQty & "           " & NUMBERFORMAT(ZXPointTwentyFiveTotal), {"₱ .25", ZXPointTwentyFiveQty, NUMBERFORMAT(ZXPointTwentyFiveTotal)}, "S3", False, False)
            RECEIPTLINECOUNT += 10
            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "₱ .05", NUMBERFORMAT(ZXPointFiveTotal), FontDefault, 5, 0)
            RightDisplay1(sender, e, RECEIPTLINECOUNT, "", ZXPointFiveQty, FontDefault, 0, 110)
            FillEJournalContent("₱ .05           " & ZXPointFiveQty & "           " & NUMBERFORMAT(ZXPointFiveTotal), {"₱ .05", ZXPointFiveQty, NUMBERFORMAT(ZXPointFiveTotal)}, "S3", False, False)
            RECEIPTLINECOUNT -= 10
            PrintSmallLine(sender, e, FontDefaultLine, RECEIPTLINECOUNT)
            FillEJournalContent("------------------------------------------------------------", {}, "C", True, True)
            RECEIPTLINECOUNT += 30

        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Public Module: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub

    Public Sub ZFooter(sender As Object, e As PrintPageEventArgs, Reptrint As Boolean, Fromdate As String, ToDate As String)
        Try
            Dim FontDefault As Font
            Dim FontDefaultBold As Font
            Dim BodySpacing As Integer = 0
            If My.Settings.PrintSize = "57mm" Then
                FontDefault = New Font("Tahoma", 5)
                FontDefaultBold = New Font("Tahoma", 5, FontStyle.Bold)
            Else
                FontDefault = New Font("Tahoma", 6)
                FontDefaultBold = New Font("Tahoma", 6, FontStyle.Bold)
                BodySpacing = 20
            End If

            Dim TotalCBQuantity As Integer = ZXThousandQty + ZXFiveHundredQty + ZXTwoHundredQty + ZXOneHundredQty + ZXFiftyQty + ZXTwentyQty + ZXTenQty + ZXFiveQty + ZXOneQty + ZXPointTwentyFiveQty + ZXPointFiveQty
            Dim GrandTotalCB As Double = ZXThousandTotal + ZXFiveHundredTotal + ZXTwoHundredTotal + ZXOneHundredTotal + ZXFiftyTotal + ZXTwentyTotal + ZXTenTotal + ZXFiveTotal + ZXOneTotal + ZXPointTwentyFiveTotal + ZXPointFiveTotal

            If XREADORZREAD = "Z-READ" Then
                RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "GRAND TOTAL", NUMBERFORMAT(GrandTotalCB), FontDefaultBold, 5, 0)
                RightDisplay1(sender, e, RECEIPTLINECOUNT, "", TotalCBQuantity, FontDefaultBold, 0, 110)
                FillEJournalContent("GRAND TOTAL          " & TotalCBQuantity, {"GRAND TOTAL", NUMBERFORMAT(GrandTotalCB)}, "LR", False, False)
                RECEIPTLINECOUNT += 10
                If Reptrint Then
                    With Reports
                        CenterTextDisplay(sender, e, Fromdate & " - " & ToDate, FontDefault, RECEIPTLINECOUNT)
                        FillEJournalContent(Fromdate & " - " & ToDate, {}, "C", True, True)
                        SimpleTextDisplay(sender, e, "DATE GENERATED: " & FullDate24HR(), FontDefaultBold, 0, RECEIPTLINECOUNT)
                        FillEJournalContent("DATE GENERATED: " & FullDate24HR(), {}, "C", True, True)
                    End With
                Else
                    CenterTextDisplay(sender, e, S_Zreading & " " & Format(Now(), "HH:mm:ss"), FontDefault, RECEIPTLINECOUNT)
                    FillEJournalContent(S_Zreading & " " & Format(Now(), "HH:mm:ss"), {}, "C", False, True)
                End If
            Else
                RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "GRAND TOTAL", NUMBERFORMAT(GrandTotalCB), FontDefaultBold, 5, 0)
                RightDisplay1(sender, e, RECEIPTLINECOUNT, "", TotalCBQuantity, FontDefaultBold, 0, 110)
                FillEJournalContent("GRAND TOTAL          " & NUMBERFORMAT(GrandTotalCB), {"GRAND TOTAL", NUMBERFORMAT(GrandTotalCB)}, "LR", False, False)
                RECEIPTLINECOUNT += 10
                CenterTextDisplay(sender, e, S_Zreading & " " & Format(Now(), "HH:mm:ss"), FontDefault, RECEIPTLINECOUNT)
                FillEJournalContent(S_Zreading & " " & Format(Now(), "HH:mm:ss"), {}, "C", False, False)
            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Public Module: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Public Sub ReceiptBody(sender As Object, e As PrintPageEventArgs, VoidReturn As Boolean, TransactionNumber As String, ReprintSales As Boolean)
        Try
            Dim FontAddOn As New Font("Tahoma", 5)
            Dim FontDefault As New Font("Tahoma", 5)
            Dim FontDefaultItalic As New Font("Tahoma", 5, FontStyle.Italic)
            Dim FontDefaultBold As New Font("Tahoma", 6, FontStyle.Bold)
            Dim AddLine As Integer = 20
            Dim CategorySpacing As Integer = 20
            Dim BodySpacing As Integer = 0
            Dim FontDefaultLine As Font
            If My.Settings.PrintSize = "57mm" Then
                FontDefaultLine = New Font("Tahoma", 6)
                FontDefault = New Font("Tahoma", 5)
                FontDefaultBold = New Font("Tahoma", 5, FontStyle.Bold)
                FontDefaultItalic = New Font("Tahoma", 5, FontStyle.Italic)
            Else
                FontDefaultLine = New Font("Tahoma", 7)
                FontDefault = New Font("Tahoma", 6)
                FontDefaultBold = New Font("Tahoma", 6, FontStyle.Bold)
                FontDefaultItalic = New Font("Tahoma", 6, FontStyle.Italic)
                CategorySpacing = 50
                BodySpacing = 32
            End If

            SimpleTextDisplay(sender, e, "Qty", FontDefault, 0, RECEIPTLINECOUNT)
            SimpleTextDisplay(sender, e, "Item", FontDefault, 50, RECEIPTLINECOUNT)
            SimpleTextDisplay(sender, e, "Price", FontDefault, 80 + CategorySpacing, RECEIPTLINECOUNT)
            SimpleTextDisplay(sender, e, "Total", FontDefault, 140 + CategorySpacing, RECEIPTLINECOUNT)
            FillEJournalContent("Qty        Item        Price        Total", {"Qty", "Item", "Price", "Total"}, "S4", False, False)
            RECEIPTLINECOUNT += 35

            If VoidReturn Then
                Dim ConnectionLocal As MySqlConnection = LocalhostConn()
                Dim Query As String = "SELECT * FROM loc_daily_transaction_details WHERE transaction_number = '" & TransactionNumber & "'"
                Dim Command As MySqlCommand = New MySqlCommand(Query, ConnectionLocal)
                Dim Da As MySqlDataAdapter = New MySqlDataAdapter(Command)
                Dim Dt As DataTable = New DataTable
                Da.Fill(Dt)

                For i As Integer = 0 To Dt.Rows.Count - 1 Step +1
                    Dim price = Dt(i)(6)
                    If Dt(i)(14).ToString = "Add-Ons" Then
                        If Dt(i)(18).ToString = "Classic" Then
                            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "     @" & Dt(i)(3), price, FontAddOn, 0, 40)
                            FillEJournalContent("     @" & Dt(i)(3) & "     " & price, {"     @" & Dt(i)(3), price}, "LR", False, False)
                        Else
                            SimpleTextDisplay(sender, e, Dt(i)(4), FontDefault, 0, RECEIPTLINECOUNT - 20)
                            SimpleTextDisplay(sender, e, Dt(i)(2), FontDefault, 50, RECEIPTLINECOUNT - 20)
                            RightDisplay1(sender, e, RECEIPTLINECOUNT, "", Dt(i)(5), FontDefault, 0, 122 + BodySpacing)
                            RightDisplay1(sender, e, RECEIPTLINECOUNT, "", price & "-", FontDefault, 0, 182 + BodySpacing)
                            FillEJournalContent("        " & Dt(i)(4) & "           " & Dt(i)(2) & "     " & Dt(i)(5) & "       " & "-" & price, {Dt(i)(4), Dt(i)(2), Dt(i)(5), "-" & price}, "S4", False, False)
                        End If
                    Else
                        SimpleTextDisplay(sender, e, Dt(i)(4), FontDefault, 0, RECEIPTLINECOUNT - 20)
                        SimpleTextDisplay(sender, e, Dt(i)(2), FontDefault, 50, RECEIPTLINECOUNT - 20)
                        RightDisplay1(sender, e, RECEIPTLINECOUNT, "", Dt(i)(5), FontDefault, 0, 122 + BodySpacing)
                        RightDisplay1(sender, e, RECEIPTLINECOUNT, "", price & "-", FontDefault, 0, 182 + BodySpacing)
                        FillEJournalContent(Dt(i)(4) & "           " & Dt(i)(2) & "           " & Dt(i)(5) & "       " & "-" & price, {Dt(i)(4), Dt(i)(2), Dt(i)(5), "-" & price}, "S4", False, False)
                        If Dt(i)(17) > 0 Then
                            RECEIPTLINECOUNT += 10
                            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "     + UPGRADE BRWN " & Dt(i)(17), "", FontAddOn, 0, 40)
                            FillEJournalContent("     + UPGRADE BRWN " & Dt(i)(17), {}, "S", False, True)
                        End If
                        If Dt(i)(20) > 0 Then
                            RECEIPTLINECOUNT += 10
                            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "    + " & Dt(i)(20) & " SENIOR DISCOUNT", "", FontAddOn, 0, 40)
                            FillEJournalContent("    + " & Dt(i)(20) & " SENIOR DISCOUNT", {}, "S", False, True)
                        End If
                        If Dt(i)(22) > 0 Then
                            RECEIPTLINECOUNT += 10
                            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "    + " & Dt(i)(22) & " PWD DISCOUNT", "", FontAddOn, 0, 40)
                            FillEJournalContent("    + " & Dt(i)(22) & " PWD DISCOUNT", {}, "S", False, True)
                        End If
                        If Dt(i)(24) > 0 Then
                            RECEIPTLINECOUNT += 10
                            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "    + " & Dt(i)(24) & " ATHLETE DISCOUNT", "", FontAddOn, 0, 40)
                            FillEJournalContent("    + " & Dt(i)(24) & " ATHLETE DISCOUNT", {}, "S", False, True)
                        End If
                        If Dt(i)(26) > 0 Then
                            RECEIPTLINECOUNT += 10
                            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "    + " & Dt(i)(26) & " S.P DISCOUNT", "", FontAddOn, 0, 40)
                            FillEJournalContent("    + " & Dt(i)(26) & " S.P DISCOUNT", {}, "S", False, True)
                        End If
                    End If
                    RECEIPTLINECOUNT += 10
                Next

                Dim GetStoreID As String = ""
                Dim GetTransactionType As String = ""
                Query = "SELECT * FROM loc_daily_transaction WHERE transaction_number = '" & TransactionNumber & "'"
                Command = New MySqlCommand(Query, ConnectionLocal)
                Using reader As MySqlDataReader = Command.ExecuteReader
                    If reader.HasRows Then
                        While reader.Read
                            GetStoreID = reader("store_id")
                            GetTransactionType = reader("transaction_type")
                            Dim NETSALES = reader("grosssales")
                            RECEIPTLINECOUNT += 20
                            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "Total Sales: ", NETSALES & "-", FontDefault, 11, 0)
                            FillEJournalContent("Total Sales:      " & "-" & NETSALES, {"Total Sales: ", "-" & NETSALES}, "LR", False, False)
                            RECEIPTLINECOUNT += 10
                            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "Amount Net of VAT: ", reader("vatablesales") & "-", FontDefault, 11, 0)
                            FillEJournalContent("Amount Net of VAT:     -" & reader("vatablesales"), {"Amount Net of VAT: ", "-" & reader("vatablesales")}, "LR", False, False)
                            RECEIPTLINECOUNT += 10

                            If reader("discount_type") <> "N/A" Then
                                RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "Less Discount: " & reader("discount_type"), reader("totaldiscount") & "-", FontDefault, 11, 0)
                                FillEJournalContent("Less Discount: " & reader("discount_type") & "     -" & reader("totaldiscount"), {"Less Discount: " & reader("discount_type"), "-" & reader("totaldiscount")}, "LR", False, False)
                            Else
                                RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "Discount: ", reader("totaldiscount") & "-", FontDefault, 11, 0)
                                FillEJournalContent("Less Discount:      -" & reader("totaldiscount"), {"Discount: ", "-" & reader("totaldiscount")}, "LR", False, False)
                            End If
                            RECEIPTLINECOUNT += 10

                            If reader("zeroratedsales") > 0 Then
                                RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "Less Vat: ", "0.00" & "-", FontDefault, 11, 0)
                                FillEJournalContent("Less VAT:     -0.00", {"Less Vat: ", "-0.00"}, "LR", False, False)
                            Else
                                RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "Less Vat: ", reader("lessvat") & "-", FontDefault, 11, 0)
                                FillEJournalContent("Less VAT:     -" & reader("lessvat"), {"Less Vat: ", "-" & reader("lessvat")}, "LR", False, False)
                            End If

                            RECEIPTLINECOUNT += 10
                            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "Amount Due: ", reader("amountdue") & "-", FontDefaultBold, 11, 0)
                            FillEJournalContent("Amount Due:      -" & reader("amountdue"), {"Amount Due: ", "-" & reader("amountdue")}, "LR", False, False)
                            RECEIPTLINECOUNT += 10
                            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "Add VAT: ", reader("vatpercentage") & "-", FontDefaultBold, 11, 0)
                            FillEJournalContent("Add VAT:      -0.00", {"Add VAT: ", "-" & reader("vatpercentage")}, "LR", False, False)
                            RECEIPTLINECOUNT += 10
                            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "Total Amount Due: ", reader("amountdue") & "-", FontDefaultBold, 11, 0)
                            FillEJournalContent("Total Amount Due:      -" & reader("amountdue"), {"Total Amount Due: ", "-" & reader("amountdue")}, "LR", False, False)
                            RECEIPTLINECOUNT += 10
                            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "Credit Sale: ", "0.00-", FontDefault, 11, 0)
                            FillEJournalContent("Credit Sale:      -0.00", {"Credit Sale: ", "-0.00"}, "LR", False, False)
                            RECEIPTLINECOUNT += 10
                            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "Change: ", "0.00-", FontDefault, 11, 0)
                            FillEJournalContent("Change:      -0.00", {"Change: ", "-0.00"}, "LR", False, False)
                            PrintCenterStars(sender, e, FontDefault, RECEIPTLINECOUNT + 15)
                            FillEJournalContent("*************************************", {}, "C", True, True)
                            PrintSmallLine(sender, e, FontDefaultLine, RECEIPTLINECOUNT)
                            FillEJournalContent("------------------------------------------------------------", {}, "C", True, True)
                            RECEIPTLINECOUNT += 30
                            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "Vatable Sales: ", reader("vatablesales") & "-", FontDefault, 11, 0)
                            FillEJournalContent("Vatable Sales:     -" & reader("vatablesales"), {"Vatable Sales: ", "-" & reader("vatablesales")}, "LR", False, False)
                            RECEIPTLINECOUNT += 10
                            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "Vat Amount: ", reader("vatpercentage") & "-", FontDefault, 11, 0)
                            FillEJournalContent("Vat Amount:     -" & reader("vatpercentage"), {"Vat Amount: ", "-" & reader("vatpercentage")}, "LR", False, False)
                            RECEIPTLINECOUNT += 10
                            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "Vat Exempt Sales: ", reader("vatexemptsales") & "-", FontDefault, 11, 0)
                            FillEJournalContent("Vat Exempt Sales:     -" & reader("vatexemptsales"), {"Vat Exempt Sales: ", "-" & reader("vatexemptsales")}, "LR", False, False)
                            RECEIPTLINECOUNT += 10

                            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "Less VAT: ", reader("lessvat") & "-", FontDefault, 11, 0)
                            FillEJournalContent("Less VAT:     -" & reader("lessvat"), {"Less VAT: ", "-" & reader("lessvat")}, "LR", False, False)

                            RECEIPTLINECOUNT += 10
                            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "Zero Rated Sales: ", reader("zeroratedsales") & "-", FontDefault, 11, 0)
                            FillEJournalContent("Zero Rated Sales:      -" & reader("zeroratedsales"), {"Zero Rated Sales: ", "-" & reader("zeroratedsales")}, "LR", False, False)
                            PrintCenterStars(sender, e, FontDefault, RECEIPTLINECOUNT + 15)
                            FillEJournalContent("*************************************", {}, "C", True, True)
                            PrintSmallLine(sender, e, FontDefaultLine, RECEIPTLINECOUNT)
                            FillEJournalContent("------------------------------------------------------------", {}, "C", True, True)
                        End While
                    End If
                End Using

                Dim Qty = sum("quantity", "loc_daily_transaction_details WHERE transaction_number = '" & TransactionNumber & "'")
                RECEIPTLINECOUNT += 10
                SimpleTextDisplay(sender, e, "Total Item/s: " & Qty, FontDefault, 0, RECEIPTLINECOUNT)
                FillEJournalContent("Total Item/s: " & Qty, {}, "S", False, True)
                RECEIPTLINECOUNT += 10
                SimpleTextDisplay(sender, e, "Store ID: " & GetStoreID, FontDefault, 0, RECEIPTLINECOUNT)
                FillEJournalContent("Store ID: " & GetStoreID, {}, "S", False, True)
                SimpleTextDisplay(sender, e, "Terminal No.: " & S_Terminal_No, FontDefault, 100, RECEIPTLINECOUNT)
                FillEJournalContent("Terminal No.: " & S_Terminal_No, {}, "S", False, True)
                RECEIPTLINECOUNT += 10
                SimpleTextDisplay(sender, e, "Transaction Type: " & GetTransactionType, FontDefault, 0, RECEIPTLINECOUNT)
                FillEJournalContent("Transaction Type: " & GetTransactionType, {}, "S", False, True)
                RECEIPTLINECOUNT += 10



                RECEIPTLINECOUNT += 15

                CenterTextDisplay(sender, e, OfficialInvoiceRefundBody, FontDefaultItalic, RECEIPTLINECOUNT + 20)
                FillEJournalContent(OfficialInvoiceRefundBody, {}, "C", True, True)
                RECEIPTLINECOUNT += 20
                PrintCenterStars(sender, e, FontDefault, RECEIPTLINECOUNT + 15)
                FillEJournalContent("*************************************", {}, "C", True, True)
                PrintSmallLine(sender, e, FontDefaultLine, RECEIPTLINECOUNT)
                FillEJournalContent("------------------------------------------------------------", {}, "C", True, True)
            Else
                If ReprintSales Then
                    Dim ConnectionLocal As MySqlConnection = LocalhostConn()
                    Dim Query As String = "SELECT * FROM loc_daily_transaction_details WHERE transaction_number = '" & TransactionNumber & "'"
                    Dim Command As MySqlCommand = New MySqlCommand(Query, ConnectionLocal)
                    Dim Da As MySqlDataAdapter = New MySqlDataAdapter(Command)
                    Dim Dt As DataTable = New DataTable
                    Da.Fill(Dt)

                    For i As Integer = 0 To Dt.Rows.Count - 1 Step +1
                        Dim price = Dt(i)(6)
                        If Dt(i)(14).ToString = "Add-Ons" Then
                            If Dt(i)(18).ToString = "Classic" Then
                                RightDisplay1(sender, e, RECEIPTLINECOUNT, "", Dt(i)(3) & "@", FontDefault, 0, 82 + BodySpacing)
                                RightDisplay1(sender, e, RECEIPTLINECOUNT, "", price, FontDefault, 0, 182 + BodySpacing)
                                FillEJournalContent("     @" & Dt(i)(3) & "     " & price, {}, "S", False, True)
                            Else
                                SimpleTextDisplay(sender, e, Dt(i)(4), FontDefault, 0, RECEIPTLINECOUNT - 20)
                                SimpleTextDisplay(sender, e, Dt(i)(2), FontDefault, 50, RECEIPTLINECOUNT - 20)
                                RightDisplay1(sender, e, RECEIPTLINECOUNT, "", Dt(i)(5), FontDefault, 0, 122 + BodySpacing)
                                RightDisplay1(sender, e, RECEIPTLINECOUNT, "", price, FontDefault, 0, 182 + BodySpacing)
                                FillEJournalContent("        " & Dt(i)(4) & "           " & Dt(i)(2) & "           " & Dt(i)(5) & "       " & price, {Dt(i)(4), Dt(i)(2), Dt(i)(5), price}, "S4", False, False)
                            End If
                        Else
                            SimpleTextDisplay(sender, e, Dt(i)(4), FontDefault, 0, RECEIPTLINECOUNT - 20)
                            SimpleTextDisplay(sender, e, Dt(i)(2), FontDefault, 50, RECEIPTLINECOUNT - 20)
                            RightDisplay1(sender, e, RECEIPTLINECOUNT, "", Dt(i)(5), FontDefault, 0, 122 + BodySpacing)
                            RightDisplay1(sender, e, RECEIPTLINECOUNT, "", price, FontDefault, 0, 182 + BodySpacing)
                            FillEJournalContent("        " & Dt(i)(4) & "           " & Dt(i)(2) & "           " & Dt(i)(5) & "       " & price, {Dt(i)(4), Dt(i)(2), Dt(i)(5), price}, "S4", False, False)
                            If Dt(i)(17) > 0 Then
                                RECEIPTLINECOUNT += 10
                                RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "     + UPGRADE BRWN " & Dt(i)(17), "", FontAddOn, 0, 40)
                                FillEJournalContent("     + UPGRADE BRWN " & Dt(i)(17), {}, "S", False, True)
                            End If
                            If Dt(i)(20) > 0 Then
                                RECEIPTLINECOUNT += 10
                                RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "    + " & Dt(i)(20) & " SENIOR DISCOUNT", "", FontAddOn, 0, 40)
                                FillEJournalContent("    + " & Dt(i)(20) & " SENIOR DISCOUNT", {}, "S", False, True)
                            End If
                            If Dt(i)(22) > 0 Then
                                RECEIPTLINECOUNT += 10
                                RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "    + " & Dt(i)(22) & " PWD DISCOUNT", "", FontAddOn, 0, 40)
                                FillEJournalContent("    + " & Dt(i)(22) & " PWD DISCOUNT", {}, "S", False, True)
                            End If
                            If Dt(i)(24) > 0 Then
                                RECEIPTLINECOUNT += 10
                                RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "    + " & Dt(i)(24) & " ATHLETE DISCOUNT", "", FontAddOn, 0, 40)
                                FillEJournalContent("    + " & Dt(i)(24) & " ATHLETE DISCOUNT", {}, "S", False, True)
                            End If
                            If Dt(i)(26) > 0 Then
                                RECEIPTLINECOUNT += 10
                                RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "    + " & Dt(i)(26) & " S.P DISCOUNT", "", FontAddOn, 0, 40)
                                FillEJournalContent("    + " & Dt(i)(26) & " S.P DISCOUNT", {}, "S", False, True)
                            End If
                        End If
                        RECEIPTLINECOUNT += 10
                    Next

                    Dim GetStoreID As String = ""
                    Dim GetTransactionType As String = ""
                    Query = "SELECT * FROM loc_daily_transaction WHERE transaction_number = '" & TransactionNumber & "'"
                    Command = New MySqlCommand(Query, ConnectionLocal)
                    Using reader As MySqlDataReader = Command.ExecuteReader
                        If reader.HasRows Then
                            While reader.Read
                                GetStoreID = reader("store_id")
                                GetTransactionType = reader("transaction_type")
                                Dim NETSALES = reader("grosssales")
                                RECEIPTLINECOUNT += 20
                                RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "Total Sales: ", NETSALES, FontDefault, 11, 0)
                                FillEJournalContent("Total Sales:      " & NETSALES, {"Total Sales: ", NETSALES}, "LR", False, False)
                                RECEIPTLINECOUNT += 10
                                RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "Amount Net of VAT: ", reader("vatablesales"), FontDefault, 11, 0)
                                FillEJournalContent("Amount Net of VAT:      " & reader("vatablesales"), {"Amount Net of VAT: ", reader("vatablesales")}, "LR", False, False)
                                RECEIPTLINECOUNT += 10

                                RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "Less Discount: ", reader("totaldiscount"), FontDefault, 11, 0)
                                FillEJournalContent("Less Discount: " & reader("totaldiscount"), {"Less Discount: ", reader("totaldiscount")}, "LR", False, False)
                                RECEIPTLINECOUNT += 10

                                RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "Less VAT: ", reader("lessvat"), FontDefault, 11, 0)
                                FillEJournalContent("Less VAT: :      " & reader("lessvat"), {"Less VAT: ", reader("lessvat")}, "LR", False, False)

                                RECEIPTLINECOUNT += 10
                                RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "Amount Due: ", reader("amountdue"), FontDefaultBold, 11, 0)
                                FillEJournalContent("Amount Due:      " & reader("amountdue"), {"Amount Due: ", reader("amountdue")}, "LR", False, False)
                                RECEIPTLINECOUNT += 10
                                RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "Add VAT: ", reader("vatpercentage"), FontDefaultBold, 11, 0)
                                FillEJournalContent("Amount Due:      0.00", {"Add VAT: ", reader("vatpercentage")}, "LR", False, False)
                                RECEIPTLINECOUNT += 10
                                RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "Total Amount Due: ", reader("amountdue"), FontDefaultBold, 11, 0)
                                FillEJournalContent("Total Amount Due:      " & reader("amountdue"), {"Total Amount Due: ", reader("amountdue")}, "LR", False, False)
                                RECEIPTLINECOUNT += 10
                                RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "Credit Sale: ", reader("amounttendered"), FontDefault, 11, 0)
                                FillEJournalContent("Credit Sale:      " & reader("amounttendered"), {"Credit Sale: ", reader("amounttendered")}, "LR", False, False)
                                RECEIPTLINECOUNT += 10
                                RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "Change: ", reader("change"), FontDefault, 11, 0)
                                FillEJournalContent("Change:      " & reader("change"), {"Change: ", reader("change")}, "LR", False, False)
                                PrintCenterStars(sender, e, FontDefault, RECEIPTLINECOUNT + 15)
                                FillEJournalContent("*************************************", {}, "C", True, True)
                                PrintSmallLine(sender, e, FontDefaultLine, RECEIPTLINECOUNT)
                                FillEJournalContent("------------------------------------------------------------", {}, "C", True, True)
                                RECEIPTLINECOUNT += 30
                                RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "Vatable Sales: ", reader("vatablesales"), FontDefault, 11, 0)
                                FillEJournalContent("Vatable Sales:      " & reader("vatablesales"), {"Vatable Sales: ", reader("vatablesales")}, "LR", False, False)
                                RECEIPTLINECOUNT += 10
                                RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "Vat Amount: ", reader("vatpercentage"), FontDefault, 11, 0)
                                FillEJournalContent("Vat Amount:      " & reader("vatpercentage"), {"Vat Amount: ", reader("vatpercentage")}, "LR", False, False)
                                RECEIPTLINECOUNT += 10
                                RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "Vat Exempt Sales: ", reader("vatexemptsales"), FontDefault, 11, 0)
                                FillEJournalContent("Vat Exempt Sales:      " & reader("vatexemptsales"), {"Vat Exempt Sales: ", reader("vatexemptsales")}, "LR", False, False)
                                RECEIPTLINECOUNT += 10

                                RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "Less Vat: ", reader("lessvat"), FontDefault, 11, 0)
                                FillEJournalContent("Less Vat: :      " & reader("lessvat"), {"Less Vat: ", reader("lessvat")}, "LR", False, False)

                                RECEIPTLINECOUNT += 10
                                RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "Zero Rated Sales: ", reader("zeroratedsales"), FontDefault, 11, 0)
                                FillEJournalContent("Zero Rated Sales:     " & reader("zeroratedsales"), {"Zero Rated Sales: ", reader("zeroratedsales")}, "LR", False, False)
                                PrintCenterStars(sender, e, FontDefault, RECEIPTLINECOUNT + 15)
                                FillEJournalContent("*************************************", {}, "C", True, True)
                                PrintSmallLine(sender, e, FontDefaultLine, RECEIPTLINECOUNT)
                                FillEJournalContent("------------------------------------------------------------", {}, "C", True, True)
                            End While
                        End If
                    End Using

                    Dim Qty = sum("quantity", "loc_daily_transaction_details WHERE transaction_number = '" & TransactionNumber & "'")
                    RECEIPTLINECOUNT += 10
                    SimpleTextDisplay(sender, e, "Total Item/s: " & Qty, FontDefault, 0, RECEIPTLINECOUNT)
                    FillEJournalContent("Total Item/s:      " & Qty, {}, "S", False, True)
                    RECEIPTLINECOUNT += 10
                    SimpleTextDisplay(sender, e, "Store ID: " & GetStoreID, FontDefault, 0, RECEIPTLINECOUNT)
                    FillEJournalContent("Store ID:      " & GetStoreID, {}, "S", False, True)
                    SimpleTextDisplay(sender, e, "Terminal No.: " & S_Terminal_No, FontDefault, 100, RECEIPTLINECOUNT)
                    FillEJournalContent("Terminal No.:      " & S_Terminal_No, {}, "S", False, True)
                    RECEIPTLINECOUNT += 10
                    SimpleTextDisplay(sender, e, "Transaction Type: " & GetTransactionType, FontDefault, 0, RECEIPTLINECOUNT)
                    FillEJournalContent("Transaction Type:      " & GetTransactionType, {}, "S", False, True)
                    RECEIPTLINECOUNT += 10

                    RECEIPTLINECOUNT += 15

                    CenterTextDisplay(sender, e, OfficialInvoiceRefundBody, FontDefaultItalic, RECEIPTLINECOUNT + 20)
                    FillEJournalContent(OfficialInvoiceRefundBody, {}, "C", True, True)

                    RECEIPTLINECOUNT += 20
                    PrintCenterStars(sender, e, FontDefault, RECEIPTLINECOUNT + 15)
                    FillEJournalContent("*************************************", {}, "C", True, True)
                    PrintSmallLine(sender, e, FontDefaultLine, RECEIPTLINECOUNT)
                    FillEJournalContent("------------------------------------------------------------", {}, "C", True, True)
                Else
                    With POS
                        For i As Integer = 0 To .DataGridViewOrders.Rows.Count - 1 Step +1
                            Dim price = .DataGridViewOrders.Rows(i).Cells(3).Value
                            If .DataGridViewOrders.Rows(i).Cells(7).Value.ToString = "Add-Ons" Then
                                If .DataGridViewOrders.Rows(i).Cells(13).Value.ToString = "Classic" Then
                                    RightDisplay1(sender, e, RECEIPTLINECOUNT, "", .DataGridViewOrders.Rows(i).Cells(0).Value & "@", FontDefault, 0, 82 + BodySpacing)
                                    RightDisplay1(sender, e, RECEIPTLINECOUNT, "", price, FontDefault, 0, 182 + BodySpacing)
                                    FillEJournalContent("     @" & .DataGridViewOrders.Rows(i).Cells(0).Value & "     " & price, {}, "S", False, True)
                                Else
                                    SimpleTextDisplay(sender, e, .DataGridViewOrders.Rows(i).Cells(1).Value, FontDefault, 0, RECEIPTLINECOUNT - 20)
                                    SimpleTextDisplay(sender, e, .DataGridViewOrders.Rows(i).Cells(6).Value, FontDefault, 50, RECEIPTLINECOUNT - 20)
                                    RightDisplay1(sender, e, RECEIPTLINECOUNT, "", .DataGridViewOrders.Rows(i).Cells(2).Value, FontDefault, 0, 122 + BodySpacing)
                                    RightDisplay1(sender, e, RECEIPTLINECOUNT, "", price, FontDefault, 0, 182 + BodySpacing)
                                    FillEJournalContent("        " & .DataGridViewOrders.Rows(i).Cells(1).Value & "           " & .DataGridViewOrders.Rows(i).Cells(6).Value & "           " & .DataGridViewOrders.Rows(i).Cells(2).Value & "       " & price, { .DataGridViewOrders.Rows(i).Cells(1).Value, .DataGridViewOrders.Rows(i).Cells(6).Value, .DataGridViewOrders.Rows(i).Cells(2).Value, price}, "S4", False, False)
                                End If
                            Else
                                SimpleTextDisplay(sender, e, .DataGridViewOrders.Rows(i).Cells(1).Value, FontDefault, 0, RECEIPTLINECOUNT - 20)
                                SimpleTextDisplay(sender, e, .DataGridViewOrders.Rows(i).Cells(6).Value, FontDefault, 50, RECEIPTLINECOUNT - 20)
                                RightDisplay1(sender, e, RECEIPTLINECOUNT, "", .DataGridViewOrders.Rows(i).Cells(2).Value, FontDefault, 0, 122 + BodySpacing)
                                RightDisplay1(sender, e, RECEIPTLINECOUNT, "", price, FontDefault, 0, 182 + BodySpacing)

                                FillEJournalContent("        " & .DataGridViewOrders.Rows(i).Cells(1).Value & "           " & .DataGridViewOrders.Rows(i).Cells(6).Value & "           " & .DataGridViewOrders.Rows(i).Cells(2).Value & "       " & price, { .DataGridViewOrders.Rows(i).Cells(1).Value, .DataGridViewOrders.Rows(i).Cells(6).Value, .DataGridViewOrders.Rows(i).Cells(2).Value, price}, "S4", False, False)
                                If .DataGridViewOrders.Rows(i).Cells(11).Value > 0 Then
                                    RECEIPTLINECOUNT += 10
                                    RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "     + UPGRADE BRWN " & .DataGridViewOrders.Rows(i).Cells(11).Value, "", FontAddOn, 0, 40)
                                    FillEJournalContent("     + UPGRADE BRWN " & .DataGridViewOrders.Rows(i).Cells(11).Value, {}, "S", False, True)
                                End If
                                If .DataGridViewOrders.Rows(i).Cells(15).Value > 0 Then
                                    RECEIPTLINECOUNT += 10
                                    RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "    + " & .DataGridViewOrders.Rows(i).Cells(16).Value & " SENIOR DISCOUNT", "", FontAddOn, 0, 40)
                                    FillEJournalContent("    + " & .DataGridViewOrders.Rows(i).Cells(16).Value & " SENIOR DISCOUNT", {}, "S", False, True)
                                End If
                                If .DataGridViewOrders.Rows(i).Cells(17).Value > 0 Then
                                    RECEIPTLINECOUNT += 10
                                    RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "    + " & .DataGridViewOrders.Rows(i).Cells(18).Value & " PWD DISCOUNT", "", FontAddOn, 0, 40)
                                    FillEJournalContent("    + " & .DataGridViewOrders.Rows(i).Cells(18).Value & " PWD DISCOUNT", {}, "S", False, True)
                                End If
                                If .DataGridViewOrders.Rows(i).Cells(19).Value > 0 Then
                                    RECEIPTLINECOUNT += 10
                                    RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "    + " & .DataGridViewOrders.Rows(i).Cells(20).Value & " ATHLETE DISCOUNT", "", FontAddOn, 0, 40)
                                    FillEJournalContent("    + " & .DataGridViewOrders.Rows(i).Cells(20).Value & " ATHLETE DISCOUNT", {}, "S", False, True)
                                End If
                                If .DataGridViewOrders.Rows(i).Cells(21).Value > 0 Then
                                    RECEIPTLINECOUNT += 10
                                    RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "    + " & .DataGridViewOrders.Rows(i).Cells(22).Value & " S.P DISCOUNT", "", FontAddOn, 0, 40)
                                    FillEJournalContent("    + " & .DataGridViewOrders.Rows(i).Cells(22).Value & " S.P DISCOUNT", {}, "S", False, True)
                                End If
                            End If
                            RECEIPTLINECOUNT += 10
                        Next
                        Dim NETSALES As Double = 0
                        If S_ZeroRated = "0" Then
                            NETSALES = Convert.ToDecimal(Double.Parse(.TextBoxGRANDTOTAL.Text))
                        Else
                            NETSALES = ZERORATEDNETSALES
                        End If
                        Dim Qty = SumOfColumnsToInt(.DataGridViewOrders, 1)
                        RECEIPTLINECOUNT += 20
                        RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "Total Sales: ", .TextBoxSUBTOTAL.Text, FontDefault, 11, 0)
                        FillEJournalContent("Total Sales:      " & .TextBoxSUBTOTAL.Text, {"Total Sales: ", .TextBoxSUBTOTAL.Text}, "LR", False, False)
                        RECEIPTLINECOUNT += 10
                        RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "Amount Net of VAT: ", NUMBERFORMAT(VATABLESALES), FontDefault, 11, 0)
                        FillEJournalContent("Amount Net of VAT:      " & NUMBERFORMAT(VATABLESALES), {"Amount Net of VAT: ", NUMBERFORMAT(VATABLESALES)}, "LR", False, False)
                        RECEIPTLINECOUNT += 10

                        If SeniorGCDiscount Then
                            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "Less Discount: " & PromoName, .TextBoxDISCOUNT.Text, FontDefault, 11, 0)
                            FillEJournalContent("Less Discount:      " & PromoName & "     " & .TextBoxDISCOUNT.Text, {"Less Discount: " & PromoName, .TextBoxDISCOUNT.Text}, "LR", False, False)
                        End If
                        If DiscAppleid Then
                            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "Less Discount: ", .TextBoxDISCOUNT.Text, FontDefault, 11, 0)
                            FillEJournalContent("Less Discount:      " & .TextBoxDISCOUNT.Text, {"Less Discount: ", .TextBoxDISCOUNT.Text}, "LR", False, False)
                        Else
                            If PromoApplied Then
                                RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "Less Discount: " & PromoName, .TextBoxDISCOUNT.Text, FontDefault, 11, 0)
                                FillEJournalContent("Less Discount:      " & PromoName & "     " & .TextBoxDISCOUNT.Text, {"Less Discount: " & PromoName, .TextBoxDISCOUNT.Text}, "LR", False, False)
                            Else
                                RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "Less Discount: ", .TextBoxDISCOUNT.Text, FontDefault, 11, 0)
                                FillEJournalContent("Less Discount:      " & .TextBoxDISCOUNT.Text, {"Less Discount: ", .TextBoxDISCOUNT.Text}, "LR", False, False)
                            End If
                        End If
                        RECEIPTLINECOUNT += 10
                        If S_ZeroRated = "0" Then
                            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "Less VAT: ", NUMBERFORMAT(LESSVAT), FontDefault, 11, 0)
                            FillEJournalContent("Less VAT:      " & NUMBERFORMAT(LESSVAT), {"Less VAT: ", NUMBERFORMAT(LESSVAT)}, "LR", False, False)
                        Else
                            RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "Less VAT: ", "0.00", FontDefault, 11, 0)
                            FillEJournalContent("Less VAT:      0.00", {"Less VAT: ", "0.00"}, "LR", False, False)
                        End If

                        RECEIPTLINECOUNT += 10
                        RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "Amount Due: ", NUMBERFORMAT(NETSALES), FontDefault, 11, 0)
                        FillEJournalContent("Amount Due:      " & NUMBERFORMAT(NETSALES), {"Amount Due: ", NUMBERFORMAT(NETSALES)}, "LR", False, False)
                        RECEIPTLINECOUNT += 10
                        RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "Add VAT: ", NUMBERFORMAT(VATABLESALES), FontDefault, 11, 0)
                        FillEJournalContent("Add VAT:      0.00", {"Add VAT: ", NUMBERFORMAT(VATABLESALES)}, "LR", False, False)
                        RECEIPTLINECOUNT += 10
                        RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "Total Amount Due: ", NUMBERFORMAT(NETSALES), FontDefaultBold, 11, 0)
                        FillEJournalContent("Total Amount Due:      " & NUMBERFORMAT(NETSALES), {"Total Amount Due: ", NUMBERFORMAT(NETSALES)}, "LR", False, False)
                        RECEIPTLINECOUNT += 10

                        RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "Credit Sale: ", NUMBERFORMAT(Double.Parse(TEXTBOXMONEYVALUE)), FontDefault, 11, 0)
                        FillEJournalContent("Credit Sale:      " & NUMBERFORMAT(Double.Parse(TEXTBOXMONEYVALUE)), {"Credit Sale: ", NUMBERFORMAT(Double.Parse(TEXTBOXMONEYVALUE))}, "LR", False, False)
                        RECEIPTLINECOUNT += 10
                        RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "Change: ", NUMBERFORMAT(Double.Parse(TEXTBOXCHANGEVALUE)), FontDefault, 11, 0)
                        FillEJournalContent("Change:      " & NUMBERFORMAT(Double.Parse(TEXTBOXCHANGEVALUE)), {"Change:", NUMBERFORMAT(Double.Parse(TEXTBOXCHANGEVALUE))}, "LR", False, False)
                        PrintCenterStars(sender, e, FontDefault, RECEIPTLINECOUNT + 15)
                        FillEJournalContent("*************************************", {}, "C", True, True)
                        PrintSmallLine(sender, e, FontDefaultLine, RECEIPTLINECOUNT)
                        FillEJournalContent("------------------------------------------------------------", {}, "C", True, True)
                        RECEIPTLINECOUNT += 30
                        RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "Vatable Sales: ", NUMBERFORMAT(VATABLESALES), FontDefault, 11, 0)
                        FillEJournalContent("Vatable Sales:      " & NUMBERFORMAT(VATABLESALES), {"Vatable Sales:", NUMBERFORMAT(VATABLESALES)}, "LR", False, False)
                        RECEIPTLINECOUNT += 10
                        RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "Vat Amount: ", NUMBERFORMAT(VAT12PERCENT), FontDefault, 11, 0)
                        FillEJournalContent("Vat Amount:      " & NUMBERFORMAT(VAT12PERCENT), {"Vat Amount:", NUMBERFORMAT(VAT12PERCENT)}, "LR", False, False)
                        RECEIPTLINECOUNT += 10
                        RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "Vat Exempt Sales: ", NUMBERFORMAT(VATEXEMPTSALES), FontDefault, 11, 0)
                        FillEJournalContent("Vat Exempt Sales:      " & NUMBERFORMAT(VATEXEMPTSALES), {"Vat Exempt Sales:", NUMBERFORMAT(VATEXEMPTSALES)}, "LR", False, False)
                        RECEIPTLINECOUNT += 10

                        RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "Less Vat: ", NUMBERFORMAT(LESSVAT), FontDefault, 11, 0)
                        FillEJournalContent("Less Vat:      " & NUMBERFORMAT(LESSVAT), {"Less Vat:      ", NUMBERFORMAT(LESSVAT)}, "LR", False, False)

                        RECEIPTLINECOUNT += 10
                        RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, "Zero Rated Sales: ", NUMBERFORMAT(ZERORATEDSALES), FontDefault, 11, 0)
                        FillEJournalContent("Zero Rated Sales:      " & NUMBERFORMAT(ZERORATEDSALES), {"Zero Rated Sales:", NUMBERFORMAT(ZERORATEDSALES)}, "LR", False, False)
                        PrintCenterStars(sender, e, FontDefault, RECEIPTLINECOUNT + 15)
                        FillEJournalContent("*************************************", {}, "C", True, True)
                        PrintSmallLine(sender, e, FontDefaultLine, RECEIPTLINECOUNT)
                        FillEJournalContent("------------------------------------------------------------", {}, "C", True, True)
                        RECEIPTLINECOUNT += 10
                        SimpleTextDisplay(sender, e, "Total Item/s: " & Qty, FontDefault, 0, RECEIPTLINECOUNT)
                        FillEJournalContent("Total Item/s: " & Qty, {}, "S", False, True)
                        RECEIPTLINECOUNT += 10
                        SimpleTextDisplay(sender, e, "Store ID: " & ClientStoreID, FontDefault, 0, RECEIPTLINECOUNT)
                        FillEJournalContent("Store ID: " & ClientStoreID, {}, "S", False, True)
                        SimpleTextDisplay(sender, e, "Terminal No.: " & S_Terminal_No, FontDefault, 100, RECEIPTLINECOUNT)
                        FillEJournalContent("Terminal No.: " & S_Terminal_No, {}, "S", False, True)
                        RECEIPTLINECOUNT += 10
                        SimpleTextDisplay(sender, e, "Transaction Type: " & Trim(TRANSACTIONMODE), FontDefault, 0, RECEIPTLINECOUNT)
                        FillEJournalContent("Transaction Type.: " & Trim(TRANSACTIONMODE), {}, "S", False, True)
                        RECEIPTLINECOUNT += 10

                        RECEIPTLINECOUNT += 15
                        CenterTextDisplay(sender, e, OfficialInvoiceRefundBody, FontDefaultItalic, RECEIPTLINECOUNT + 20)
                        FillEJournalContent(OfficialInvoiceRefundBody, {}, "C", True, True)
                        RECEIPTLINECOUNT += 20
                        PrintCenterStars(sender, e, FontDefault, RECEIPTLINECOUNT + 15)
                        FillEJournalContent("*************************************", {}, "C", True, True)
                        PrintSmallLine(sender, e, FontDefaultLine, RECEIPTLINECOUNT)
                        FillEJournalContent("------------------------------------------------------------", {}, "C", True, True)
                    End With
                End If
            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Public Module: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
        ', DISCAPPLIED As Boolean
    End Sub
    Public Sub ReceiptBodyFooter(sender As Object, e As PrintPageEventArgs, VoidReturn As Boolean, TRANSACTIONNUMBER As String, ReprintSales As Boolean, DiscApplied As Boolean)
        Try
            Dim FontDefault As New Font("Tahoma", 5)
            Dim BodySpacing As Integer = 0
            Dim LineToTextSpacing As Integer = 0
            Dim FontDefaultLine As Font
            If My.Settings.PrintSize = "57mm" Then
                FontDefaultLine = New Font("Tahoma", 6)
                FontDefault = New Font("Tahoma", 5)
            Else
                FontDefaultLine = New Font("Tahoma", 7)
                FontDefault = New Font("Tahoma", 6)
                BodySpacing += 30
                LineToTextSpacing += 5
            End If
            RECEIPTLINECOUNT += 30
            CenterTextDisplay(sender, e, "CUSTOMER INFORMATION", FontDefault, RECEIPTLINECOUNT)
            FillEJournalContent("CUSTOMER INFORMATION", {}, "C", False, True)
            RECEIPTLINECOUNT += 20
            e.Graphics.DrawLine(Pens.Black, 40 + LineToTextSpacing, RECEIPTLINECOUNT, 190 + BodySpacing, RECEIPTLINECOUNT)
            RECEIPTLINECOUNT += 10
            e.Graphics.DrawLine(Pens.Black, 28 + LineToTextSpacing, RECEIPTLINECOUNT, 190 + BodySpacing, RECEIPTLINECOUNT)
            RECEIPTLINECOUNT += 10
            e.Graphics.DrawLine(Pens.Black, 49 + LineToTextSpacing, RECEIPTLINECOUNT, 190 + BodySpacing, RECEIPTLINECOUNT)
            RECEIPTLINECOUNT += 10
            e.Graphics.DrawLine(Pens.Black, 70 + LineToTextSpacing, RECEIPTLINECOUNT, 190 + BodySpacing, RECEIPTLINECOUNT)
            RECEIPTLINECOUNT += 10
            e.Graphics.DrawLine(Pens.Black, 49 + LineToTextSpacing, RECEIPTLINECOUNT, 190 + BodySpacing, RECEIPTLINECOUNT)
            RECEIPTLINECOUNT += 10

            If DiscApplied Then
                CenterTextDisplay(sender, e, "DISCOUNT INFORMATION", FontDefault, RECEIPTLINECOUNT)

            End If

            If ReprintSales Then
                Dim CustName As String = ""
                Dim CustTin As String = ""
                Dim CustAddress As String = ""
                Dim CustBusiness As String = ""

                Dim ConnectionLocal As MySqlConnection = LocalhostConn()
                Dim Query As String = "SELECT * FROM loc_customer_info WHERE transaction_number = '" & TRANSACTIONNUMBER & "'"
                Dim Command As MySqlCommand = New MySqlCommand(Query, ConnectionLocal)
                Using reader As MySqlDataReader = Command.ExecuteReader
                    If reader.HasRows Then
                        While reader.Read
                            CustName = reader("cust_name")
                            CustTin = reader("cust_tin")
                            CustAddress = reader("cust_address")
                            CustBusiness = reader("cust_business")
                        End While
                    End If
                End Using

                SimpleTextDisplay(sender, e, "Name: " & CustName, FontDefault, 0, RECEIPTLINECOUNT - 78)
                FillEJournalContent("Name: " & CustName, {}, "S", False, True)
                RECEIPTLINECOUNT += 10
                SimpleTextDisplay(sender, e, "Tin: " & CustTin, FontDefault, 0, RECEIPTLINECOUNT - 78)
                FillEJournalContent("Tin: " & CustTin, {}, "S", False, True)
                RECEIPTLINECOUNT += 10
                SimpleTextDisplay(sender, e, "Address: " & CustAddress, FontDefault, 0, RECEIPTLINECOUNT - 78)
                FillEJournalContent("Address: " & CustAddress, {}, "S", False, True)
                RECEIPTLINECOUNT += 10
                SimpleTextDisplay(sender, e, "Business Style: " & CustBusiness, FontDefault, 0, RECEIPTLINECOUNT - 78)
                FillEJournalContent("Business Style: " & CustBusiness, {}, "S", False, True)
                RECEIPTLINECOUNT += 10
                SimpleTextDisplay(sender, e, "Signature:", FontDefault, 0, RECEIPTLINECOUNT - 78)
                FillEJournalContent("Signature:", {}, "S", False, True)
                RECEIPTLINECOUNT -= 20
            Else
                SimpleTextDisplay(sender, e, "Name: " & CUST_INFO_NAME, FontDefault, 0, RECEIPTLINECOUNT - 78)
                FillEJournalContent("Name: " & CUST_INFO_NAME, {}, "S", False, True)
                RECEIPTLINECOUNT += 10
                SimpleTextDisplay(sender, e, "Tin: " & CUST_INFO_TIN, FontDefault, 0, RECEIPTLINECOUNT - 78)
                FillEJournalContent("Tin: " & CUST_INFO_TIN, {}, "S", False, True)
                RECEIPTLINECOUNT += 10
                SimpleTextDisplay(sender, e, "Address: " & CUST_INFO_ADDRESS, FontDefault, 0, RECEIPTLINECOUNT - 78)
                FillEJournalContent("Address: " & CUST_INFO_ADDRESS, {}, "S", False, True)
                RECEIPTLINECOUNT += 10
                SimpleTextDisplay(sender, e, "Business Style: " & CUST_INFO_BUSINESS, FontDefault, 0, RECEIPTLINECOUNT - 78)
                FillEJournalContent("Business Style: " & CUST_INFO_BUSINESS, {}, "S", False, True)
                RECEIPTLINECOUNT += 10
                SimpleTextDisplay(sender, e, "Signature:", FontDefault, 0, RECEIPTLINECOUNT - 78)
                FillEJournalContent("Signature: ", {}, "S", False, True)
                RECEIPTLINECOUNT -= 20
            End If




            If DiscApplied Then
                e.Graphics.DrawLine(Pens.Black, 65 + LineToTextSpacing, RECEIPTLINECOUNT, 190 + BodySpacing, RECEIPTLINECOUNT)
                RECEIPTLINECOUNT += 10
                e.Graphics.DrawLine(Pens.Black, 75 + LineToTextSpacing, RECEIPTLINECOUNT, 190 + BodySpacing, RECEIPTLINECOUNT)
                RECEIPTLINECOUNT += 10
                e.Graphics.DrawLine(Pens.Black, 40 + LineToTextSpacing, RECEIPTLINECOUNT, 190 + BodySpacing, RECEIPTLINECOUNT)
                RECEIPTLINECOUNT += 10
                e.Graphics.DrawLine(Pens.Black, 70 + LineToTextSpacing, RECEIPTLINECOUNT, 190 + BodySpacing, RECEIPTLINECOUNT)
                RECEIPTLINECOUNT += 10
            End If

            If DiscApplied Then
                FillEJournalContent("", {}, "C", False, True)
                FillEJournalContent("DISCOUNT INFORMATION", {}, "C", False, True)
            End If

            If DiscApplied Then
                If ReprintSales Then
                    Dim ConnectionLocal As MySqlConnection = LocalhostConn()
                    Dim Query As String = "SELECT * FROM loc_senior_details WHERE transaction_number = '" & TRANSACTIONNUMBER & "'"
                    Dim Command As MySqlCommand = New MySqlCommand(Query, ConnectionLocal)
                    Using reader As MySqlDataReader = Command.ExecuteReader
                        If reader.HasRows Then
                            While reader.Read
                                'DISCAPPLIED = True
                                SimpleTextDisplay(sender, e, "Customer ID:", FontDefault, 0, RECEIPTLINECOUNT - 68)
                                SimpleTextDisplay(sender, e, reader("senior_id"), FontDefault, 60, RECEIPTLINECOUNT - 68)
                                FillEJournalContent("Customer ID: " & reader("senior_id"), {}, "S", False, True)
                                RECEIPTLINECOUNT += 10
                                SimpleTextDisplay(sender, e, "Customer Name:", FontDefault, 0, RECEIPTLINECOUNT - 68)
                                SimpleTextDisplay(sender, e, reader("senior_name"), FontDefault, 70, RECEIPTLINECOUNT - 68)
                                FillEJournalContent("Customer Name: " & reader("senior_name"), {}, "S", False, True)
                                RECEIPTLINECOUNT += 10
                                SimpleTextDisplay(sender, e, "Phone:", FontDefault, 0, RECEIPTLINECOUNT - 68)
                                SimpleTextDisplay(sender, e, reader("phone_number"), FontDefault, 60, RECEIPTLINECOUNT - 68)
                                FillEJournalContent("Phone: " & reader("phone_number"), {}, "S", False, True)
                                RECEIPTLINECOUNT += 10
                            End While
                        Else
                            SimpleTextDisplay(sender, e, "Customer ID:", FontDefault, 0, RECEIPTLINECOUNT - 68)
                            FillEJournalContent("Customer ID:", {}, "S", False, True)
                            RECEIPTLINECOUNT += 10
                            SimpleTextDisplay(sender, e, "Customer Name:", FontDefault, 0, RECEIPTLINECOUNT - 68)
                            FillEJournalContent("Customer Name:", {}, "S", False, True)
                            RECEIPTLINECOUNT += 10
                            SimpleTextDisplay(sender, e, "Phone:", FontDefault, 0, RECEIPTLINECOUNT - 68)
                            FillEJournalContent("Phone:", {}, "S", False, True)
                            RECEIPTLINECOUNT += 10
                        End If
                    End Using
                Else
                    SimpleTextDisplay(sender, e, "Customer ID:", FontDefault, 0, RECEIPTLINECOUNT - 68)
                    SimpleTextDisplay(sender, e, SeniorDetailsID, FontDefault, 60, RECEIPTLINECOUNT - 68)
                    FillEJournalContent("Customer ID: " & SeniorDetailsID, {}, "S", False, True)
                    RECEIPTLINECOUNT += 10
                    SimpleTextDisplay(sender, e, "Customer Name:", FontDefault, 0, RECEIPTLINECOUNT - 68)
                    SimpleTextDisplay(sender, e, SeniorDetailsName, FontDefault, 70, RECEIPTLINECOUNT - 68)
                    FillEJournalContent("Customer Name: " & SeniorDetailsName, {}, "S", False, True)
                    RECEIPTLINECOUNT += 10
                    SimpleTextDisplay(sender, e, "Phone:", FontDefault, 0, RECEIPTLINECOUNT - 68)
                    SimpleTextDisplay(sender, e, SeniorPhoneNumber, FontDefault, 60, RECEIPTLINECOUNT - 68)
                    FillEJournalContent("Phone: " & SeniorPhoneNumber, {}, "S", False, True)
                    RECEIPTLINECOUNT += 10

                End If


                SimpleTextDisplay(sender, e, "Customer Sign:", FontDefault, 0, RECEIPTLINECOUNT - 68)
                FillEJournalContent("Customer Sign:", {}, "S", False, True)
                RECEIPTLINECOUNT -= 50
            Else
                RECEIPTLINECOUNT -= 40
            End If

            PrintCenterStars(sender, e, FontDefault, RECEIPTLINECOUNT + 15)
            FillEJournalContent("*************************************", {}, "C", True, True)
            PrintSmallLine(sender, e, FontDefaultLine, RECEIPTLINECOUNT)
            FillEJournalContent("------------------------------------------------------------", {}, "C", True, True)
            RECEIPTLINECOUNT += 30

        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Public Module: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Public Sub ReceiptFooterOne(sender As Object, e As PrintPageEventArgs, VoidReturn As Boolean, ReceiptSummary As Boolean)
        Try

            Dim BodySpacing As Integer = 0
            Dim BrandFont As Font
            Dim FontDefault As New Font("Tahoma", 5)
            Dim FontDefaultBold As New Font("Tahoma", 5, FontStyle.Bold)
            Dim FontDefaultLine As Font
            If My.Settings.PrintSize = "57mm" Then
                FontDefaultLine = New Font("Tahoma", 6)
                BrandFont = New Font("Tahoma", 7, FontStyle.Bold)
                FontDefault = New Font("Tahoma", 5)
                FontDefaultBold = New Font("Tahoma", 5, FontStyle.Bold)
            Else
                FontDefaultLine = New Font("Tahoma", 7)
                BrandFont = New Font("Tahoma", 8, FontStyle.Bold)
                FontDefault = New Font("Tahoma", 6)
                FontDefaultBold = New Font("Tahoma", 6, FontStyle.Bold)
                BodySpacing = 32
            End If

            Dim sql As String = "SELECT * FROM loc_receipt WHERE type = 'Footer' AND status = 1"
            Dim cmd As MySqlCommand = New MySqlCommand(sql, LocalhostConn())
            Dim da As MySqlDataAdapter = New MySqlDataAdapter(cmd)
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            CenterTextDisplay(sender, e, S_Dev_Comp_Name.ToUpper, BrandFont, RECEIPTLINECOUNT)
            FillEJournalContent(S_Dev_Comp_Name.ToUpper, {}, "C", True, True)
            RECEIPTLINECOUNT += 10
            For i As Integer = 0 To dt.Rows.Count - 1 Step +1
                CenterTextDisplay(sender, e, dt(i)(2).ToUpper, FontDefault, RECEIPTLINECOUNT)
                FillEJournalContent(dt(i)(2).ToUpper, {}, "C", False, True)
                RECEIPTLINECOUNT += 10
            Next

            sql = "SELECT * FROM loc_receipt WHERE type = 'VALIDITY' AND status = 1"
            cmd = New MySqlCommand(sql, LocalhostConn())
            da = New MySqlDataAdapter(cmd)
            dt = New DataTable
            da.Fill(dt)

            For i As Integer = 0 To dt.Rows.Count - 1 Step +1
                CenterTextDisplay(sender, e, dt(i)(2), FontDefaultBold, RECEIPTLINECOUNT)
                FillEJournalContent(dt(i)(2), {}, "C", True, True)
                RECEIPTLINECOUNT += 10
            Next
            If VoidReturn Then
                sql = "SELECT * FROM loc_receipt WHERE type = 'REFUND-FOOTER' AND status = 1"
                cmd = New MySqlCommand(sql, LocalhostConn())
                da = New MySqlDataAdapter(cmd)
                dt = New DataTable
                da.Fill(dt)

                For i As Integer = 0 To dt.Rows.Count - 1 Step +1
                    CenterTextDisplay(sender, e, dt(i)(2), FontDefaultBold, RECEIPTLINECOUNT)
                    FillEJournalContent(dt(i)(2), {}, "C", True, True)
                    RECEIPTLINECOUNT += 10
                Next
            End If


            If ReceiptSummary Then
                PrintCenterStars(sender, e, FontDefault, RECEIPTLINECOUNT)
                FillEJournalContent("*************************************", {}, "C", True, True)
                PrintSmallLine(sender, e, FontDefaultLine, RECEIPTLINECOUNT - 15)
                FillEJournalContent("------------------------------------------------------------", {}, "C", True, True)
                RECEIPTLINECOUNT += 20
                CenterTextDisplay(sender, e, "RECEIPT SUMMARY", FontDefaultBold, RECEIPTLINECOUNT)
                FillEJournalContent("RECEIPT SUMMARY", {}, "C", True, True)
                RECEIPTLINECOUNT += 10
                With POS
                    For i As Integer = 0 To .DataGridViewOrders.Rows.Count - 1 Step +1
                        RightToLeftDisplay(sender, e, RECEIPTLINECOUNT, .DataGridViewOrders.Rows(i).Cells(6).Value, .DataGridViewOrders.Rows(i).Cells(1).Value, FontDefault, 0, 0)
                        FillEJournalContent(.DataGridViewOrders.Rows(i).Cells(6).Value & "          ", { .DataGridViewOrders.Rows(i).Cells(6).Value, .DataGridViewOrders.Rows(i).Cells(1).Value}, "LR", False, False)
                        RECEIPTLINECOUNT += 10
                    Next
                End With
            End If

            FillEJournalContent("   ---***---***---***---***---  ", {}, "C", True, True)
            FillEJournalContent("", {}, "C", True, True)
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Public Module: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Public Sub ReceiptHeader(sender As Object, e As PrintPageEventArgs, VoidReturn As Boolean)
        Try
            Dim brandfont As New Font("Tahoma", 7, FontStyle.Bold)
            Dim font As New Font("Tahoma", 6)
            Dim brand = ClientBrand.ToUpper
            Dim AddLine As Integer = 0
            CenterTextDisplay(sender, e, brand, brandfont, 10)

            If VoidReturn Then
                CenterTextDisplay(sender, e, "VOID/RETURN", font, 21)
                AddLine += 10
            End If

            CenterTextDisplay(sender, e, "VAT REG TIN " & ClientTin, font, 21 + AddLine)
            CenterTextDisplay(sender, e, "MSN : " & ClientMSN, font, 31 + AddLine)
            '============================================================================================================================
            CenterTextDisplay(sender, e, "MIN : " & ClientMIN, font, 41 + AddLine)
            '============================================================================================================================
            CenterTextDisplay(sender, e, "PTUN : " & ClientPTUN, font, 51 + AddLine)
            CenterTextDisplay(sender, e, ClientAddress, font, 61 + AddLine)
            CenterTextDisplay(sender, e, ClientBrgy & ", ", font, 71 + AddLine)
            CenterTextDisplay(sender, e, getmunicipality & ", " & getprovince, font, 81 + AddLine)
            CenterTextDisplay(sender, e, "TEL. NO.: " & ClientTel, font, 91 + AddLine)


            If DiscAppleid Then
                SimpleTextDisplay(sender, e, SeniorDetailsName & " - " & SeniorDetailsID, font, 30, 82 + AddLine)
            End If


            If My.Settings.PrintSize = "57mm" Then
                e.Graphics.DrawLine(Pens.Black, 40, 112 + AddLine, 180, 112 + AddLine)
                e.Graphics.DrawLine(Pens.Black, 28, 122 + AddLine, 180, 122 + AddLine)
                e.Graphics.DrawLine(Pens.Black, 49, 132 + AddLine, 180, 132 + AddLine)
                e.Graphics.DrawLine(Pens.Black, 75, 142 + AddLine, 180, 142 + AddLine)
            Else
                e.Graphics.DrawLine(Pens.Black, 40, 112 + AddLine, 210, 112 + AddLine)
                e.Graphics.DrawLine(Pens.Black, 28, 122 + AddLine, 210, 122 + AddLine)
                e.Graphics.DrawLine(Pens.Black, 49, 132 + AddLine, 210, 132 + AddLine)
                e.Graphics.DrawLine(Pens.Black, 75, 142 + AddLine, 210, 142 + AddLine)
            End If

            SimpleTextDisplay(sender, e, "Name:", font, 0, 85 + AddLine)
            SimpleTextDisplay(sender, e, "Tin:", font, 0, 95 + AddLine)
            SimpleTextDisplay(sender, e, "Address:", font, 0, 105 + AddLine)
            SimpleTextDisplay(sender, e, "Business Style:", font, 0, 115 + AddLine)

        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Public Sub ReceiptFooter(sender As Object, e As PrintPageEventArgs, a As Integer, ItemReturn As Boolean)
        Try
            Dim sql As String = "SELECT `Dev_Company_Name`, `Dev_Address`, `Dev_Tin`, `Dev_Accr_No`, `Dev_Accr_Date_Issued`, `Dev_Accr_Valid_Until`, `Dev_PTU_No`, `Dev_PTU_Date_Issued`, `Dev_PTU_Valid_Until` FROM loc_settings WHERE settings_id = 1"
            Dim cmd As MySqlCommand = New MySqlCommand(sql, LocalhostConn())
            Dim da As MySqlDataAdapter = New MySqlDataAdapter(cmd)
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            Dim brandfont As New Font("Tahoma", 7, FontStyle.Bold)
            Dim font As New Font("Tahoma", 6)
            Dim font1 As New Font("Tahoma", 5, FontStyle.Bold)
            CenterTextDisplay(sender, e, dt(0)(0).ToUpper, brandfont, a + 200)
            CenterTextDisplay(sender, e, "VAT REG TIN : " & dt(0)(2).ToString, font, a + 210)
            CenterTextDisplay(sender, e, dt(0)(1), font, a + 220)
            CenterTextDisplay(sender, e, "ACCR # : " & dt(0)(3), font, a + 230)
            CenterTextDisplay(sender, e, "DATE ISSUED : " & dt(0)(4), font, a + 240)
            CenterTextDisplay(sender, e, "VALID UNTIL : " & dt(0)(5), font, a + 250)
            CenterTextDisplay(sender, e, "PERMIT TO OPERATE : " & dt(0)(6), font, a + 260)
            CenterTextDisplay(sender, e, "DATE ISSUED : " & dt(0)(7), font, a + 270)
            CenterTextDisplay(sender, e, "VALID UNTIL : " & dt(0)(8), font, a + 280)
            CenterTextDisplay(sender, e, "THIS INVOICE/RECEIPT SHALL BE ", font1, a + 290)
            CenterTextDisplay(sender, e, "VALID FOR FIVE(5) YEARS FROM THE DATE ", font1, a + 300)
            CenterTextDisplay(sender, e, "OF THE PERMIT TO USE", font1, a + 310)

            If ItemReturn Then
                CenterTextDisplay(sender, e, "THIS DOCUMENT SHALL BE ", font1, a + 330)
                CenterTextDisplay(sender, e, "VALID FOR FIVE(5) YEARS FROM THE DATE ", font1, a + 340)
                CenterTextDisplay(sender, e, "OF THE PERMIT TO USE", font1, a + 350)
                CenterTextDisplay(sender, e, "THIS DOCUMENT IS NOT ", font1, a + 360)
                CenterTextDisplay(sender, e, "VALID FOR CLAIM OF INPUT TAX", font1, a + 370)
            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Public Module: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub

    Public Function TwoDecimalPlaces(ToRound)
        Try
            ToRound = Math.Round(ToRound, 2, MidpointRounding.AwayFromZero)
        Catch ex As Exception
            MsgBox(ex.ToString)
            SendErrorReport(ex.ToString)
        End Try
        Return ToRound
    End Function
    Public Sub Compute(Optional NotResetDiscount As Boolean = False, Optional TotalDiscount As Double = 0, Optional NotTriggerResetDisc As Boolean = False, Optional AutoCompute As Boolean = True)
        Try
            'MsgBox(TRANSACTIONMODE)
            With POS

                Select Case TotalDiscount
                    Case > 0
                        If Not NotTriggerResetDisc Then
                            .DiscountDefault()
                        End If
                End Select

                If PromoApplied Then
                    If Not NotResetDiscount Or Not NotTriggerResetDisc Then
                        If AutoCompute Then
                            .TextBoxDISCOUNT.Text = "0.00"
                            .PromoDefault()
                        End If
                    End If
                End If
                If AutoCompute Then
                    .Label76.Text = SumOfColumnsToDecimal(.DataGridViewOrders, 3)
                    .TextBoxSUBTOTAL.Text = .Label76.Text
                    Dim GRANDTOTAL As Double = Double.Parse(.Label76.Text) - Double.Parse(.TextBoxDISCOUNT.Text)
                    .TextBoxGRANDTOTAL.Text = NUMBERFORMAT(GRANDTOTAL)
                    Select Case NotResetDiscount And NotTriggerResetDisc
                        Case False
                            .TextBoxDISCOUNT.Text = NUMBERFORMAT(0)
                    End Select
                End If

            End With
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Public Sub LedDisplay(TextToDisplay As String, TotalOrChange As Boolean)
        Try
            Dim ComPort As String = My.Settings.SpPort
            Dim BaudRate As Integer = My.Settings.SpBaudrate

            If ComPort <> "" And BaudRate <> 0 Then
                Dim sp As New System.IO.Ports.SerialPort(ComPort, BaudRate, IO.Ports.Parity.None And IO.Ports.StopBits.One)
                sp.Open()
                sp.Write(Convert.ToString(ChrW(12)))
                If TotalOrChange Then
                    'Displays Price Amount AND word TOTAL in the pole display
                    sp.WriteLine(Chr(27) + Chr(81) + Chr(65) + TextToDisplay + Chr(13) + Chr(27) + Chr(115) + ”2”)
                Else
                    'Displays Price Amount AND word CHANGE in the pole display
                    sp.WriteLine(Chr(27) + Chr(81) + Chr(65) + TextToDisplay + Chr(13) + Chr(27) + Chr(115) + ”4”)
                End If
                ' sp.WriteLine(Chr(27) + Chr(115) + ”2”)
                sp.Close()
                sp.Dispose()
                sp = Nothing
            End If
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub

    Public Sub LedConfig(TextToDisplay As String, ComPort As String, BaudRate As Integer)
        Try
            'Displays Price Amount in the pole display
            Dim sp As New System.IO.Ports.SerialPort(ComPort, BaudRate, IO.Ports.Parity.None And IO.Ports.StopBits.One)
            sp.Open()
            sp.Write(Convert.ToString(ChrW(12)))
            sp.WriteLine(Chr(27) + Chr(81) + Chr(65) + TextToDisplay + Chr(13))
            sp.Close()
            sp.Dispose()
            sp = Nothing
            Dim msg = MessageBox.Show("Does sample text displays on LED panel?", "LED DISPLAY CONFIGURATION", MessageBoxButtons.YesNo)
            If msg = DialogResult.Yes Then
                My.Settings.LedDisplayTrue = True
                My.Settings.Save()
            Else
                My.Settings.LedDisplayTrue = False
                My.Settings.Save()
            End If
        Catch ex As Exception
            My.Settings.LedDisplayTrue = False
            My.Settings.Save()
            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Public Sub GetPorts(ToFill)
        Try
            ToFill.items.clear
            For Each sp As String In My.Computer.Ports.SerialPortNames
                ToFill.Items.Add(sp)
            Next
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub

    Public Function ReturnPrintSize() As Integer
        Dim PrintSize = 0
        If My.Settings.PrintSize = "57mm" Then
            PrintSize = 205
        Else
            PrintSize = 240
        End If
        Return PrintSize
    End Function

    Public Function ReturnStringToDate(StringToDate) As DateTime
        Dim iDate As String = StringToDate
        Dim oDate As DateTime = Convert.ToDateTime(iDate)
        Return oDate.Year & "-" & oDate.Month & "-" & oDate.Day
    End Function

    Public Sub PingInternetConnecion()
        Try
            If My.Computer.Network.Ping("www.google.com", 1000) Then
                OnlineOffline = True
            Else
                OnlineOffline = False
            End If
        Catch
            OnlineOffline = False
        End Try
    End Sub

    Public Sub LoadContent()
        Try
            Dim ConnectionLocal As MySqlConnection = LocalhostConn()
            Dim Query As String = "SELECT DISTINCT partners FROM loc_admin_products"
            Dim Command As MySqlCommand = New MySqlCommand(Query, ConnectionLocal)
            Dim Da As MySqlDataAdapter = New MySqlDataAdapter(Command)
            SELECT_DISCTINCT_PARTNERS_DT = New DataTable
            Da.Fill(SELECT_DISCTINCT_PARTNERS_DT)
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Public Module: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub

    Public Function StringToDate(StringToConvert) As Date
        Dim iDate As String = StringToConvert
        Dim oDate As DateTime = Convert.ToDateTime(iDate)
        Dim FullDate As Date = oDate.Day & " " & oDate.Month & "  " & oDate.Year
        Return FullDate
    End Function
End Module
