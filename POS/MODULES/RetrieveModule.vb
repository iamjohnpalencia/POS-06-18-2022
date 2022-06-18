Imports MySql.Data.MySqlClient
Imports System.IO
Imports System.Text
Module RetrieveModule
    Dim user_id As String
    Dim acroo As String
    Dim fullname As String
    Dim Location_control As New Point(10, 10)
    Public Count_control As Integer = 0
    Dim result As Integer
    Dim dr
    Dim municipality
    Dim province
    Dim full_name
    Dim productcode As String
    Dim password As String = Login.txtpassword.Text
    Dim wrapper As New Simple3Des(password)
    Dim returnval
    Dim RowsReturned As Integer
    Dim product_line
    Dim available_stock
    Dim dailysales
    Dim critical_item
    Dim product
    Dim cipherText As String

    Public Sub retrieveLoginDetails()
        Try
            If Login.txtusername.Text = "" Then
                MessageBox.Show("Input username first", "Login Form", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Login.txtusername.Focus()
            ElseIf Login.txtpassword.Text = "" Then
                MessageBox.Show("Input password first", "Login Form", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Login.txtpassword.Focus()
            Else
                Try
                    cipherText = ConvertPassword(SourceString:=Login.txtpassword.Text)
                    sql = "SELECT * FROM loc_users WHERE username = @Username AND password = @Password AND active = 1;"
                    cmd = New MySqlCommand(sql, LocalhostConn())
                    With cmd
                        .Parameters.Clear()
                        .Parameters.AddWithValue("@Username", Login.txtusername.Text)
                        .Parameters.AddWithValue("@UserID", Login.txtusername.Text)
                        .Parameters.AddWithValue("@Password", cipherText)
                        .Parameters.AddWithValue("@StoreID", ClientStoreID)
                        Dim reader As MySqlDataReader
                        reader = .ExecuteReader()
                        While reader.Read()
                            user_id = reader("uniq_id")
                        End While
                        reader.Close()
                    End With
                    da = New MySqlDataAdapter
                    dt = New DataTable
                    da.SelectCommand = cmd
                    da.Fill(dt)
                Catch ex As MySqlException
                    MsgBox(ex.ToString)
                    SendErrorReport(ex.ToString)
                Finally
                    da.Dispose()
                    If dt.Rows.Count > 0 Then
                        Dim crew_id, username, password, fullname, userlevel, active, storeid, franguid, role As String
                        crew_id = dt.Rows(0).Item(0)
                        role = dt.Rows(0).Item(1)
                        username = dt.Rows(0).Item(3)
                        password = dt.Rows(0).Item(4)
                        userlevel = dt.Rows(0).Item(7)
                        fullname = dt.Rows(0).Item(2)
                        active = dt.Rows(0).Item(11)
                        franguid = dt.Rows(0).Item(12)
                        storeid = dt.Rows(0).Item(13)
                        ClientRole = role
                        If Login.txtusername.Text = username And cipherText = password And userlevel = "Crew" And ClientStoreID = storeid And active = 1 And franguid = ClientGuid Then
                            MessageBox.Show("Welcome " + fullname + "!", "Login Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Login.txtusername.Text = ""
                            Login.txtpassword.Text = ""
                            ClientCrewID = user_id
                            'messageboxappearance = True
                            SystemLogType = "LOGIN"
                            SystemLogDesc = "User Login: " & username & " : " & ClientRole
                            GLOBAL_SYSTEM_LOGS(SystemLogType, SystemLogDesc)
                            Shift = ""
                            If S_Layout = "POS" Then
                                Login.Close()
                                POS.Show()
                            ElseIf S_Layout = "GROCERY" Then
                                Login.Close()
                                Grocery.Show()
                            End If
                            AuditTrail.LogToAuditTral("User", "User Login: " & ClientCrewID, "Normal")
                        ElseIf Login.txtusername.Text = username And cipherText = password And userlevel = "Head Crew" And ClientStoreID = storeid And active = 1 And franguid = ClientGuid Then
                            MessageBox.Show("Welcome " + fullname + "!", "Login Successfully(" & ClientRole & ")", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Login.txtusername.Text = ""
                            Login.txtpassword.Text = ""
                            ClientCrewID = user_id
                            'messageboxappearance = True
                            SystemLogType = "LOGIN"
                            SystemLogDesc = "User Login: " & username & " : " & ClientRole
                            GLOBAL_SYSTEM_LOGS(SystemLogType, SystemLogDesc)
                            Shift = ""
                            If S_Layout = "POS" Then
                                Login.Close()
                                POS.Show()
                            ElseIf S_Layout = "GROCERY" Then
                                Login.Close()
                                Grocery.Show()
                            End If
                            AuditTrail.LogToAuditTral("User", "User Login: " & ClientCrewID, "Normal")
                        ElseIf Login.txtusername.Text = username And cipherText = password And userlevel = "Manager" And ClientStoreID = storeid And active = 1 And franguid = ClientGuid Then
                            MessageBox.Show("Welcome " + fullname + "!", "Login Successfully(" & ClientRole & ")", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Login.txtusername.Text = ""
                            Login.txtpassword.Text = ""
                            ClientCrewID = user_id
                            'messageboxappearance = True
                            SystemLogType = "LOGIN"
                            SystemLogDesc = "User Login: " & username & " : " & ClientRole
                            GLOBAL_SYSTEM_LOGS(SystemLogType, SystemLogDesc)
                            Shift = ""
                            If S_Layout = "POS" Then
                                Login.Close()
                                POS.Show()
                            ElseIf S_Layout = "GROCERY" Then
                                Login.Close()
                                Grocery.Show()
                            End If
                            AuditTrail.LogToAuditTral("User", "User Login: " & ClientCrewID, "Normal")
                        ElseIf Login.txtusername.Text = username And cipherText = password And userlevel = "Admin" Then
                            MessageBox.Show("Welcome " + fullname + "!", "Login Successfully(" & ClientRole & ")", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Login.txtusername.Text = ""
                            Login.txtpassword.Text = ""
                            ClientCrewID = user_id
                            'messageboxappearance = True
                            SystemLogType = "LOGIN"
                            SystemLogDesc = "User Login: " & username & " : " & ClientRole
                            GLOBAL_SYSTEM_LOGS(SystemLogType, SystemLogDesc)
                            Shift = ""
                            If S_Layout = "POS" Then
                                Login.Close()
                                POS.Show()
                            ElseIf S_Layout = "GROCERY" Then
                                Login.Close()
                                Grocery.Show()
                            End If
                            AuditTrail.LogToAuditTral("User", "User Login: " & ClientCrewID, "Normal")
                        Else
                            MessageBox.Show("Incorrect username or password!", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            SystemLogType = "ERROR"
                            SystemLogDesc = "FAILED TO LOGIN: Username: " & Login.txtusername.Text & " Password: " & Login.txtpassword.Text
                            GLOBAL_SYSTEM_LOGS(SystemLogType, SystemLogDesc)
                            AuditTrail.LogToAuditTral("User", "Invalid Credentials: " & Login.txtusername.Text, "Normal")
                        End If
                    Else
                        MessageBox.Show("Incorrect username or password!", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Login.txtusername.Focus()
                        'messageboxappearance = True
                        SystemLogType = "ERROR"
                        SystemLogDesc = "FAILED TO LOGIN: Username and password input " & Login.txtusername.Text & " " & Login.txtpassword.Text
                        GLOBAL_SYSTEM_LOGS(SystemLogType, SystemLogDesc)
                        AuditTrail.LogToAuditTral("User", "Invalid Credentials: " & Login.txtusername.Text, "Normal")
                    End If
                End Try
            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Retrieve Module: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Public Function CheckUserName(Username) As Boolean
        Dim ReturnUsername As Boolean = False
        Try
            Dim ConnectionLocal As MySqlConnection = LocalhostConn()
            Dim sql = "SELECT username FROM loc_users WHERE username = '" & Username & "'"
            Dim cmd As MySqlCommand = New MySqlCommand(sql, ConnectionLocal)
            Using reader As MySqlDataReader = cmd.ExecuteReader()
                If reader.HasRows Then
                    ReturnUsername = True
                Else
                    ReturnUsername = False
                End If
            End Using
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
        Return ReturnUsername
    End Function
    Public Function CheckEmail(Email) As Boolean
        Dim ReturnUsername As Boolean = False
        Try
            Dim ConnectionLocal As MySqlConnection = LocalhostConn()
            Dim sql = "SELECT email FROM loc_users WHERE email = '" & Email & "'"
            Dim cmd As MySqlCommand = New MySqlCommand(sql, ConnectionLocal)
            Using reader As MySqlDataReader = cmd.ExecuteReader()
                If reader.HasRows Then
                    ReturnUsername = True
                Else
                    ReturnUsername = False
                End If
            End Using
            ConnectionLocal.Close()
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Retrieve Module: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
        Return ReturnUsername
    End Function
    Public Function CheckContactNumber(ContactNumber) As Boolean
        Dim ConnectionLocal As MySqlConnection = LocalhostConn()
        Dim ReturnUsername As Boolean = False
        Try
            Dim sql = "SELECT contact_number FROM loc_users WHERE contact_number = '" & ContactNumber & "'"
            Dim cmd As MySqlCommand = New MySqlCommand(sql, ConnectionLocal)
            Using reader As MySqlDataReader = cmd.ExecuteReader()
                If reader.HasRows Then
                    ReturnUsername = True
                Else
                    ReturnUsername = False
                End If
            End Using
            ConnectionLocal.Close()
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Retrieve Module: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
        Return ReturnUsername
    End Function
    Dim r As Random = New Random(Guid.NewGuid().GetHashCode())
    Public Function CheckUserId() As String
        Dim Uniqid = ""
        Try
            Dim ReturnThis As Boolean = True
            Dim ConnectionLocal As MySqlConnection = LocalhostConn()
            Do
                Uniqid = ClientStorename & "-" & r.[Next](1000, 10000)
                Dim sql = "SELECT uniq_id FROM loc_users WHERE uniq_id = '" & Uniqid & "'"
                Dim cmd As MySqlCommand = New MySqlCommand(sql, ConnectionLocal)
                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    If reader.HasRows Then
                        ReturnThis = True
                    Else
                        ReturnThis = False
                    End If
                End Using
            Loop Until (ReturnThis = False)
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Retrieve Module: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
        Return Uniqid
    End Function
    'FUNCTION LOADING EXPENCES / POS ==================================================================================== 
    Public Sub listviewproductsshow(ByVal where As String, ByVal Partners As String)
        Try
            Dim Query As String = ""
            Dim Connectionlocal As MySqlConnection = LocalhostConn()
            If where = "Others" Then
                Query = "SELECT product_id, product_name, product_image, product_price, formula_id, product_sku FROM loc_admin_products WHERE product_category ='" & where & "' AND product_status = 1 AND store_id = " & ClientStoreID & " AND partners = '" & Partners & "'"
            Else
                Query = "SELECT product_id, product_name, product_image, product_price, formula_id, product_sku FROM loc_admin_products WHERE product_category ='" & where & "' AND product_status = 1 AND partners = '" & Partners & "'"
            End If
            With POS
                .PanelProducts.Controls.Clear()
                Dim cmd As MySqlCommand = New MySqlCommand(Query, Connectionlocal)
                Dim da As MySqlDataAdapter = New MySqlDataAdapter(cmd)
                Dim dt As DataTable = New DataTable()
                da.Fill(dt)
                For Each row As DataRow In dt.Rows
                    Count_control += 1
                    Dim new_Button_product As New Button
                    Dim buttonname As String = row("product_sku")
                    Dim newlabel As New Label
                    productprice = row("product_price")
                    productID = row("product_id")
                    With new_Button_product
                        .Name = buttonname
                        .Text = productprice
                        .TextImageRelation = TextImageRelation.ImageBeforeText
                        .TextAlign = ContentAlignment.TopLeft
                        If where = "Premium" Then
                            .ForeColor = Color.White
                        Else
                            .ForeColor = Color.Black
                        End If
                        .Font = New Font("Tahoma", 10)
                        .BackgroundImage = Base64ToImage(row("product_image"))
                        .FlatStyle = FlatStyle.Flat
                        .FlatAppearance.BorderSize = 0
                        .BackgroundImageLayout = ImageLayout.Stretch
                        .Location = New Point(Location_control.X, Location_control.Y)
                        .Width = 148
                        .Height = 120
                        .Cursor = Cursors.Hand
                        With newlabel
                            .Text = buttonname
                            .Font = New Font("Tahoma", 10)
                            .ForeColor = Color.White
                            .Width = 148
                            .Location = New Point(0, 100)
                            .BackColor = Color.SlateGray
                            .Parent = new_Button_product
                            .TextAlign = ContentAlignment.TopCenter
                        End With
                        .Controls.Add(newlabel)
                    End With
                    .PanelProducts.Controls.Add(new_Button_product)
                    AddHandler new_Button_product.Click, AddressOf new_product_button_click
                Next
                Connectionlocal.Close()
            End With
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Retrieve Module: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    'MANAGE PRODUCTS PANEL ========================================================
    Public Sub selectmax(ByVal whatform As Integer)
        Try
            If whatform = 1 Then
                Dim TransactionNum As Integer = GetTransactionNumber()

                If TransactionNum > 0 Then
                    TransactionNum += 1
                Else
                    TransactionNum = 1
                End If
                S_TRANSACTION_NUMBER = TransactionNum

                Dim SINum As Integer = GetSINumber()

                If SINum > 0 Then
                    SINum += 1
                Else
                    SINum = 1
                End If

                S_SI_NUMBER = SINum

            ElseIf whatform = 2 Then
                Addexpense.TextBoxMAXID.Text = Format(Now, "yydd-MMHH-mmssyy")
            ElseIf whatform = 3 Then
                Registration.TextBoxMAXID.Text = Format(Now, "yydd-MMHH-mmssyy")
            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Retrieve Module: " & ex.ToString, "Critical")

            MsgBox(ex.ToString)
        End Try
    End Sub
    Dim formulaid
    Public Function selectmaxformula(ByVal whatid As String, ByVal fromtable As String, ByVal flds As String)
        Try
            Dim ConnectionLocal As MySqlConnection = LocalhostConn()
            Dim sql = "Select " & flds & " FROM " & fromtable & " ORDER BY " & whatid & " DESC LIMIT 1"
            Dim cmd As MySqlCommand = New MySqlCommand(sql, ConnectionLocal)
            formulaid = cmd.ExecuteScalar()
            cmd.Dispose()
            ConnectionLocal.Close()
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Retrieve Module: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
        Return formulaid
    End Function
    Public Function returnfullname(ByVal where As String)
        Dim FullName As String = ""
        Try
            Dim ConnectionLocal As MySqlConnection = LocalhostConn()
            Dim cmd As MySqlCommand = New MySqlCommand("SELECT full_name FROM loc_users WHERE uniq_id = '" + where + "' ", ConnectionLocal)
            Dim da As MySqlDataAdapter = New MySqlDataAdapter(cmd)
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            FullName = dt(0)(0).ToString
            LocalhostConn.Close()
            da.Dispose()
            cmd.Dispose()
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Retrieve Module: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
        Return FullName
    End Function
    Dim valuetoreturn
    Dim MyLocalConnection As MySqlConnection
    Dim MyCloudConnection As MySqlConnection
    Dim MyCmd As MySqlCommand
    Public Function GLOBAL_RETURN_FUNCTION(tbl As String, flds As String, toreturn As String, thisislocalconn As Boolean)
        Try
            Dim sql = "SELECT " & flds & " FROM " & tbl
            If thisislocalconn = True Then
                MyLocalConnection = New MySqlConnection
                MyLocalConnection.ConnectionString = LocalConnectionString
                MyLocalConnection.Open()
                MyCmd = New MySqlCommand(sql, MyLocalConnection)
            Else
                MyCloudConnection = New MySqlConnection
                MyCloudConnection.ConnectionString = CloudConnectionString
                MyCloudConnection.Open()
                MyCmd = New MySqlCommand(sql, MyCloudConnection)
            End If
            Using reader As MySqlDataReader = MyCmd.ExecuteReader()
                If reader.HasRows Then
                    While reader.Read
                        valuetoreturn = reader(toreturn)
                    End While
                End If
            End Using
            If thisislocalconn = True Then
                MyLocalConnection.Close()
            Else
                MyCloudConnection.Close()
            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Retrieve Module: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
        Return valuetoreturn
    End Function
    Public Function AsDatatable(table, fields, datagridd) As DataTable
        datagridd.rows.clear
        Dim dttable As DataTable = New DataTable
        Dim ConnectionLocal As MySqlConnection = LocalhostConn()
        Try
            Dim sql = "SELECT " & fields & " FROM " & table
            'Console.WriteLine(sql)
            Dim cmd As MySqlCommand = New MySqlCommand(sql, ConnectionLocal)
            Dim da As MySqlDataAdapter = New MySqlDataAdapter(cmd)
            da.Fill(dttable)
            With datagridd
                .RowHeadersVisible = False
                .AllowUserToAddRows = False
                .AllowUserToDeleteRows = False
                .AllowUserToOrderColumns = False
                .AllowUserToResizeColumns = False
                .AllowUserToResizeRows = False
                .Font = New Font("tahoma", 10)
                .CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
                .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            End With
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Retrieve Module: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        Finally
            ConnectionLocal.Close()
            cmd.Dispose()
            da.Dispose()
        End Try
        Return dttable
    End Function
    Public Function AsDatatableFontIncrease(table, fields, datagridd) As DataTable
        datagridd.rows.clear
        Dim dttable As DataTable = New DataTable
        Dim ConnectionLocal As MySqlConnection = LocalhostConn()
        Try
            Dim sql = "SELECT " & fields & " FROM " & table
            Dim cmd As MySqlCommand = New MySqlCommand(sql, ConnectionLocal)
            Dim da As MySqlDataAdapter = New MySqlDataAdapter(cmd)
            da.Fill(dttable)
            With datagridd
                .RowHeadersVisible = False
                .AllowUserToAddRows = False
                .AllowUserToDeleteRows = False
                .AllowUserToOrderColumns = False
                .AllowUserToResizeColumns = False
                .AllowUserToResizeRows = False
                .Font = New Font("tahoma", 12)
                .CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
                .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                .rowtemplate.height = 30
            End With
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Retrieve Module: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        Finally
            ConnectionLocal.Close()
            cmd.Dispose()
            da.Dispose()
        End Try
        Return dttable
    End Function
    Public Sub GLOBAL_SELECT_ALL_FUNCTION(ByVal table As String, ByVal fields As String, ByRef datagrid As DataGridView)
        Try
            Dim ConnectionLocal As MySqlConnection = LocalhostConn()
            Dim sql As String = "SELECT " + fields + " FROM " + table
            Dim cmd As MySqlCommand = New MySqlCommand(sql, ConnectionLocal)
            Dim da As MySqlDataAdapter = New MySqlDataAdapter(cmd)
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            With datagrid
                .DataSource = Nothing
                .DataSource = dt
                .RowHeadersVisible = False
                .AllowUserToAddRows = False
                .AllowUserToDeleteRows = False
                .AllowUserToOrderColumns = False
                .AllowUserToResizeColumns = False
                .AllowUserToResizeRows = False
                .Font = New Font("tahoma", 10)
                .CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
                .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            End With
            ConnectionLocal.Close()
            cmd.Dispose()
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Retrieve Module: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Public Sub GLOBAL_SELECT_ALL_FUNCTION_WHERE(ByVal table As String, ByVal fields As String, ByVal where As String, ByRef datagrid As DataGridView)
        Try
            Dim ConnectionLocal As MySqlConnection = LocalhostConn()
            Dim sql As String = "SELECT " + fields + " FROM " + table + " WHERE " + where
            Dim cmd As MySqlCommand = New MySqlCommand(sql, ConnectionLocal)
            Dim da As MySqlDataAdapter = New MySqlDataAdapter(cmd)
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            With datagrid
                .DataSource = Nothing
                .DataSource = dt
                .RowHeadersVisible = False
                .AllowUserToAddRows = False
                .AllowUserToDeleteRows = False
                .AllowUserToOrderColumns = False
                .AllowUserToResizeColumns = False
                .AllowUserToResizeRows = False
                .Font = New Font("tahoma", 10)
                .CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
                .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            End With
            ConnectionLocal.Close()
            cmd.Dispose()
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Retrieve Module: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Public Sub GLOBAL_SELECT_ALL_FUNCTION_COMBOBOX(table As String, fields As String, combobox As ComboBox, Loccon As Boolean)
        Try
            Dim ConnectionLocal As MySqlConnection = LocalhostConn()
            Dim ConnectionCloud As MySqlConnection = ServerCloudCon()
            Dim sql = "SELECT " + fields + " FROM " + table

            Dim cmd As MySqlCommand
            If Loccon Then
                cmd = New MySqlCommand(sql, ConnectionLocal)
            Else
                cmd = New MySqlCommand(sql, ConnectionCloud)
            End If
            Dim da As MySqlDataAdapter = New MySqlDataAdapter(cmd)
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            With combobox
                For i As Integer = 0 To dt.Rows.Count - 1 Step +1
                    .Items.Add(dt(i)(0))
                Next
            End With
            ConnectionLocal.Close()
            ConnectionCloud.Close()
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Retrieve Module: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        Finally
            da.Dispose()
        End Try
    End Sub
    Public Function GLOBAL_SELECT_FUNCTION_RETURN(ByVal table As String, ByVal fields As String, ByVal values As String, ByVal returnvalrow As String)
        Try
            Dim ConnectionLocal As MySqlConnection = LocalhostConn()
            Dim sql = "SELECT " + fields + " FROM " + table + " WHERE " + values
            'Console.WriteLine(sql)
            Dim cmd As MySqlCommand = New MySqlCommand(sql, ConnectionLocal)
            Using readerObj As MySqlDataReader = cmd.ExecuteReader
                While readerObj.Read
                    returnval = readerObj(returnvalrow).ToString
                End While
            End Using
            cmd.Dispose()
            ConnectionLocal.Close()
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Retrieve Module: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
        Return returnval
    End Function
    '=================================================================MDI FORM
    Dim returncount As String
    Public Function count(ByVal tocount As String, ByVal table As String)
        Try
            Dim ConnectionLocal As MySqlConnection = LocalhostConn()
            Dim sql As String = "SELECT COUNT(" & tocount & ") FROM " & table
            Dim cmd As MySqlCommand = New MySqlCommand(sql, ConnectionLocal)
            Dim da As MySqlDataAdapter = New MySqlDataAdapter(cmd)
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            For Each row As DataRow In dt.Rows
                returncount = row("COUNT(" & tocount & ")")
            Next
            cmd.Dispose()
            ConnectionLocal.Close()
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Retrieve Module: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
        Return returncount
    End Function
    Dim returnsum
    Public Function roundsum(tototal As String, table As String, Columncall As String)
        Try
            Dim ConnectionLocal As MySqlConnection = LocalhostConn()
            Dim sql As String = "SELECT SUM(ROUND(" & tototal & ",0)) AS " & Columncall & " FROM " & table
            Dim cmd As MySqlCommand = New MySqlCommand(sql, ConnectionLocal)
            Dim da As MySqlDataAdapter = New MySqlDataAdapter(cmd)
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            If IsDBNull(dt.Rows(0)(0)) Then
                returnsum = 0
            Else
                For Each row As DataRow In dt.Rows
                    returnsum = row(Columncall)
                Next
            End If
            cmd.Dispose()
            ConnectionLocal.Close()
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Retrieve Module: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
        Return returnsum
    End Function
    Public Function sum(ByVal tototal As String, ByVal table As String)
        Try
            Dim ConnectionLocal As MySqlConnection = LocalhostConn()
            Dim sql As String = "SELECT SUM(" & tototal & ") FROM " & table
            Dim cmd As MySqlCommand = New MySqlCommand(sql, ConnectionLocal)
            Dim da As MySqlDataAdapter = New MySqlDataAdapter(cmd)
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            If IsDBNull(dt.Rows(0)(0)) Then
                returnsum = 0
            Else
                For Each row As DataRow In dt.Rows
                    returnsum = row("SUM(" & tototal & ")")
                Next
            End If
            cmd.Dispose()
            ConnectionLocal.Close()
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Retrieve Module: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try

        Return returnsum
    End Function

    Public Function returnselect(toreturn As String, table As String)
        Dim RetunSel As String = ""
        Try
            Dim ConnectionLocal As MySqlConnection = LocalhostConn()
            Dim sql As String = "SELECT `" & toreturn & "` as RETURNSEL FROM " & table
            Dim cmd As MySqlCommand = New MySqlCommand(sql, ConnectionLocal)
            Using reader As MySqlDataReader = cmd.ExecuteReader
                If reader.HasRows Then
                    While reader.Read
                        RetunSel = reader("RETURNSEL")
                    End While
                End If
            End Using
            cmd.Dispose()
            ConnectionLocal.Close()
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Retrieve Module: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
        Return RetunSel
    End Function

    Public Function ReturnRowDouble(toReturn As String, Table As String) As Double
        Dim RetunSel As Double = 0
        Try
            Dim ConnectionLocal As MySqlConnection = LocalhostConn()
            Dim sql As String = "SELECT `" & toReturn & "` FROM " & Table
            Dim cmd As MySqlCommand = New MySqlCommand(sql, ConnectionLocal)
            Using reader As MySqlDataReader = cmd.ExecuteReader
                If reader.HasRows Then
                    While reader.Read
                        RetunSel = reader(toReturn)
                    End While
                End If
            End Using
            cmd.Dispose()
            ConnectionLocal.Close()
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Retrieve Module: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
        Return RetunSel
    End Function

    Public Function ReturnMunicipalityName(id) As String
        Dim ServerConn As MySqlConnection = ServerCloudCon()
        Dim ReturnMun As String = ""
        Try
            Dim sql = "SELECT mn_name FROM admin_municipality WHERE mn_id = " & id & ""
            Dim cmd As MySqlCommand = New MySqlCommand(sql, ServerConn)
            Using reader As MySqlDataReader = cmd.ExecuteReader()
                If reader.HasRows Then
                    While reader.Read
                        ReturnMun = reader("mn_name")
                    End While
                End If
            End Using
            cmd.Dispose()
            ServerConn.Close()

        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Retrieve Module: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
        Return ReturnMun
    End Function
    Public Function ReturnProvinceName(id) As String
        Dim ServerConn As MySqlConnection = ServerCloudCon()
        Dim ReturnProv As String = ""
        Try
            Dim sql = "SELECT province FROM admin_province WHERE add_id = " & id & ""
            Dim cmd As MySqlCommand = New MySqlCommand(sql, ServerConn)
            Using reader As MySqlDataReader = cmd.ExecuteReader()
                If reader.HasRows Then
                    While reader.Read
                        ReturnProv = reader("province")
                    End While
                End If
            End Using
            ServerConn.Close()
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Retrieve Module: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
        Return ReturnProv
    End Function
    Public Sub GetOldGrandtotal()
        Try
            Dim ConnectionLocal As MySqlConnection = LocalhostConn()
            Dim Query = "SELECT S_Old_Grand_Total FROM loc_settings WHERE settings_id = 1"
            Dim Cmd As MySqlCommand = New MySqlCommand(Query, ConnectionLocal)
            Using reader As MySqlDataReader = Cmd.ExecuteReader
                If reader.HasRows Then
                    While reader.Read
                        'If reader("S_Old_Grand_Total") < 1 Then
                        '    S_OLDGRANDTOTAL = sum("amountdue", "loc_daily_transaction WHERE zreading <> '" & S_Zreading & "'")
                        'Else
                        Dim CurrentZreadTotal As Double = sum("amountdue", "loc_daily_transaction WHERE zreading = '" & S_Zreading & "'")
                        S_OLDGRANDTOTAL = reader("S_Old_Grand_Total") + CurrentZreadTotal
                        'End If
                    End While
                End If
            End Using
            Query = "UPDATE loc_settings SET S_Old_Grand_Total = " & S_OLDGRANDTOTAL & " WHERE settings_id = 1"
            Cmd = New MySqlCommand(Query, ConnectionLocal)
            Cmd.ExecuteReader()
            ConnectionLocal.Close()
            Cmd.Dispose()
            'Console.WriteLine(S_OLDGRANDTOTAL)
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Retrieve Module: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Public Sub LoadOldGrandtotal()
        Try
            Dim ConnectionLocal As MySqlConnection = LocalhostConn()
            Dim Query = "SELECT S_Old_Grand_Total FROM loc_settings WHERE settings_id = 1"
            Dim Cmd As MySqlCommand = New MySqlCommand(Query, ConnectionLocal)
            Using reader As MySqlDataReader = Cmd.ExecuteReader
                If reader.HasRows Then
                    While reader.Read
                        S_OLDGRANDTOTAL = reader("S_Old_Grand_Total")
                    End While
                End If
            End Using
            ConnectionLocal.Close()
            Cmd.Dispose()
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Retrieve Module: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub

    Public Function GetTransactionNumber() As Integer
        Dim RetTransactionNum As Integer = 0
        Try
            Dim ConnectionLocal As MySqlConnection = LocalhostConn()
            Dim Sql = "SELECT S_Trn_No FROM loc_settings LIMIT 1"
            Dim Cmd As MySqlCommand = New MySqlCommand(Sql, ConnectionLocal)
            Using reader As MySqlDataReader = Cmd.ExecuteReader
                If reader.HasRows Then
                    While reader.Read
                        RetTransactionNum = reader("S_Trn_No")
                    End While
                End If
            End Using
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Retrieve Module: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
        Return RetTransactionNum
    End Function

    Public Function GetSINumber() As Integer
        Dim RetSINo As Integer = 0
        Try
            Dim ConnectionLocal As MySqlConnection = LocalhostConn()
            Dim Sql = "SELECT S_SI_No FROM loc_settings LIMIT 1"
            Dim Cmd As MySqlCommand = New MySqlCommand(Sql, ConnectionLocal)
            Using reader As MySqlDataReader = Cmd.ExecuteReader
                If reader.HasRows Then
                    While reader.Read
                        RetSINo = reader("S_SI_No")
                    End While
                End If
            End Using
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Retrieve Module: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
        Return RetSINo
    End Function

    Public Function CheckColumnIfExist(toreturn, table) As Boolean
        Dim ReturnMe As Boolean = False
        Try
            Dim ConnectionLocal As MySqlConnection = LocalhostConn()
            Dim Sql = "SELECT " & toreturn & " FROM " & table & " "
            Console.WriteLine(Sql)
            Dim Cmd As MySqlCommand = New MySqlCommand(Sql, ConnectionLocal)
            Using reader As MySqlDataReader = Cmd.ExecuteReader
                If reader.HasRows Then
                    ReturnMe = True
                Else
                    ReturnMe = False
                End If
            End Using
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Retrieve Module: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
        Return ReturnMe
    End Function


#Region "UPDATES"
    Public Sub GetUpdatesRowCount(FromPosUpdate As Integer)
        Try
            Dim Products As Integer = count("product_id", "loc_admin_products")
            Dim Category As Integer = count("category_id", "loc_admin_category")
            Dim Inventory As Integer = count("inventory_id", "loc_pos_inventory")
            Dim Formula As Integer = count("formula_id", "loc_product_formula")
            Dim Coupons As Integer = count("ID", "tbcoupon")
            Dim Partners As Integer = count("id", "loc_partners_transaction")
            UPDATE_ROW_COUNT = Products + Category + Inventory + Formula + Coupons + Partners
            If FromPosUpdate = 1 Then
                CheckingForUpdates.Instance.Invoke(Sub()
                                                       CheckingForUpdates.ProgressBar1.Maximum = UPDATE_ROW_COUNT
                                                   End Sub)
            ElseIf FromPosUpdate = 2 Then
                SettingsForm.Instance.Invoke(Sub()
                                                 SettingsForm.ProgressBar1.Maximum = UPDATE_ROW_COUNT
                                             End Sub)
            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Retrieve Module: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Public Sub GetProductUpdates(FromPosUpdate As Integer)
        Try
            Dim ConnectionLocal As MySqlConnection = LocalhostConn()
            Dim ConnectionServer As MySqlConnection = ServerCloudCon()

            UPDATE_PRODUCTS_DATATABLE = New DataTable
            UPDATE_PRODUCTS_DATATABLE.Columns.Add("product_id")
            UPDATE_PRODUCTS_DATATABLE.Columns.Add("product_sku")
            UPDATE_PRODUCTS_DATATABLE.Columns.Add("product_name")
            UPDATE_PRODUCTS_DATATABLE.Columns.Add("formula_id")
            UPDATE_PRODUCTS_DATATABLE.Columns.Add("product_barcode")
            UPDATE_PRODUCTS_DATATABLE.Columns.Add("product_category")
            UPDATE_PRODUCTS_DATATABLE.Columns.Add("product_price")
            UPDATE_PRODUCTS_DATATABLE.Columns.Add("product_desc")
            UPDATE_PRODUCTS_DATATABLE.Columns.Add("product_image")
            UPDATE_PRODUCTS_DATATABLE.Columns.Add("product_status")
            UPDATE_PRODUCTS_DATATABLE.Columns.Add("origin")
            UPDATE_PRODUCTS_DATATABLE.Columns.Add("date_modified")
            UPDATE_PRODUCTS_DATATABLE.Columns.Add("inventory_id")
            UPDATE_PRODUCTS_DATATABLE.Columns.Add("addontype")
            UPDATE_PRODUCTS_DATATABLE.Columns.Add("half_batch")
            UPDATE_PRODUCTS_DATATABLE.Columns.Add("partners")
            UPDATE_PRODUCTS_DATATABLE.Columns.Add("arrangement")

            Dim Query = "SELECT * FROM loc_admin_products"
            Dim CmdCheck As MySqlCommand = New MySqlCommand(Query, ConnectionLocal)
            Dim DaCheck As MySqlDataAdapter = New MySqlDataAdapter(CmdCheck)
            Dim DtCheck As DataTable = New DataTable
            DaCheck.Fill(DtCheck)

            If DtCheck.Rows.Count < 1 Then
                GetAllProducts(FromPosUpdate)
            Else
                Dim DtCount As DataTable
                Dim DtCountProductIds As DataTable = New DataTable
                Dim SqlCount = "SELECT product_id FROM admin_products_org"
                Dim CmdCount As MySqlCommand = New MySqlCommand(SqlCount, ConnectionServer)
                Dim DataAdapter As MySqlDataAdapter = New MySqlDataAdapter(CmdCount)
                DataAdapter.Fill(DtCountProductIds)
                Dim DaCount As MySqlDataAdapter
                Dim FillDt As DataTable = New DataTable

                For i As Integer = 0 To DtCountProductIds.Rows.Count - 1 Step +1
                    If UPDATE_WORKER_CANCEL Then
                        Exit For
                    End If
                    Dim Query1 As String = "SELECT date_modified, price_change FROM loc_admin_products WHERE server_product_id = " & DtCountProductIds(i)(0)
                    Dim cmd As MySqlCommand = New MySqlCommand(Query1, ConnectionLocal)
                    DaCount = New MySqlDataAdapter(cmd)
                    FillDt = New DataTable
                    DaCount.Fill(FillDt)
                    Dim Prod As DataRow = UPDATE_PRODUCTS_DATATABLE.NewRow
                    If FillDt.Rows.Count > 0 Then
                        If UPDATE_WORKER_CANCEL = False Then
                            If FromPosUpdate = 1 Then
                                CheckingForUpdates.Instance.Invoke(Sub()
                                                                       If CheckingForUpdates.ProgressBar1.Maximum > 0 Then
                                                                           CheckingForUpdates.ProgressBar1.Value += 1
                                                                           CheckingForUpdates.Label1.Text = CheckingForUpdates.ProgressBar1.Value
                                                                       End If
                                                                   End Sub)
                            ElseIf FromPosUpdate = 2 Then

                            End If
                        End If

                        Dim PriceChange = FillDt(0)(1)
                        'Exist then check for update
                        Query1 = "SELECT * FROM admin_products_org WHERE product_id = " & DtCountProductIds(i)(0)
                        'Console.WriteLine(Query1)
                        cmd = New MySqlCommand(Query1, ConnectionServer)
                        DaCount = New MySqlDataAdapter(cmd)
                        DtCount = New DataTable
                        DaCount.Fill(DtCount)
                        If FillDt(0)(0).ToString <> DtCount(0)(11) Then
                            Prod("product_id") = DtCount(0)(0)
                            Prod("product_sku") = DtCount(0)(1)
                            Prod("product_name") = DtCount(0)(2)
                            Prod("formula_id") = DtCount(0)(3)
                            Prod("product_barcode") = DtCount(0)(4)
                            Prod("product_category") = DtCount(0)(5)
                            If FillDt(0)(1) = 1 Then
                                Dim sql2 = "SELECT product_price FROM loc_admin_products WHERE server_product_id = " & DtCountProductIds(i)(0)
                                Dim cmd2 As MySqlCommand = New MySqlCommand(sql2, LocalhostConn)
                                Dim da2 As MySqlDataAdapter = New MySqlDataAdapter(cmd2)
                                Dim dt2 As DataTable = New DataTable
                                da2.Fill(dt2)
                                Prod("product_price") = dt2(0)(0)
                            Else
                                Prod("product_price") = DtCount(0)(6)
                            End If
                            Prod("product_desc") = DtCount(0)(7)
                            Prod("product_image") = DtCount(0)(8)
                            Prod("product_status") = DtCount(0)(9)
                            Prod("origin") = DtCount(0)(10)
                            Prod("date_modified") = DtCount(0)(11)
                            Prod("inventory_id") = DtCount(0)(12)
                            Prod("addontype") = DtCount(0)(13)
                            Prod("half_batch") = DtCount(0)(14)
                            Prod("partners") = DtCount(0)(15)
                            Prod("arrangement") = DtCount(0)(16)
                            UPDATE_PRODUCTS_DATATABLE.Rows.Add(Prod)
                        End If
                    Else
                        'Insert new product
                        Query1 = "SELECT * FROM admin_products_org WHERE product_id = " & DtCountProductIds(i)(0)

                        cmd = New MySqlCommand(Query1, ConnectionServer)
                        DaCount = New MySqlDataAdapter(cmd)
                        DtCount = New DataTable
                        DaCount.Fill(DtCount)

                        Prod("product_id") = DtCount(0)(0)
                        Prod("product_sku") = DtCount(0)(1)
                        Prod("product_name") = DtCount(0)(2)
                        Prod("formula_id") = DtCount(0)(3)
                        Prod("product_barcode") = DtCount(0)(4)
                        Prod("product_category") = DtCount(0)(5)
                        Prod("product_price") = DtCount(0)(6)
                        Prod("product_desc") = DtCount(0)(7)
                        Prod("product_image") = DtCount(0)(8)
                        Prod("product_status") = DtCount(0)(9)
                        Prod("origin") = DtCount(0)(10)
                        Prod("date_modified") = DtCount(0)(11)
                        Prod("inventory_id") = DtCount(0)(12)
                        Prod("addontype") = DtCount(0)(13)
                        Prod("half_batch") = DtCount(0)(14)
                        Prod("partners") = DtCount(0)(15)
                        Prod("arrangement") = DtCount(0)(16)
                        UPDATE_PRODUCTS_DATATABLE.Rows.Add(Prod)

                    End If

                Next


                ConnectionLocal.Close()
                ConnectionServer.Close()
            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Retrieve Module: " & ex.ToString, "Critical")
            SendErrorReport(ex.ToString)
            Exit Sub

        End Try
    End Sub
    Private Sub GetAllProducts(FromPosUpdate As Integer)
        Try
            Dim Connection As MySqlConnection = ServerCloudCon()
            Dim SqlCount = "SELECT product_id FROM admin_products_org"
            Dim CmdCount As MySqlCommand = New MySqlCommand(SqlCount, Connection)
            Dim DataAdapter As MySqlDataAdapter = New MySqlDataAdapter(CmdCount)
            Dim DaGet As DataTable = New DataTable
            DataAdapter.Fill(DaGet)

            Dim Cmd As MySqlCommand

            Dim DaCount As MySqlDataAdapter
            Dim FillDt As DataTable = New DataTable

            If UPDATE_WORKER_CANCEL = False Then
                If FromPosUpdate = 1 Then
                    CheckingForUpdates.Instance.Invoke(Sub()
                                                           CheckingForUpdates.ProgressBar1.Maximum += DaGet.Rows.Count
                                                           Console.WriteLine("PRODUCTS ADDED LINE: " & DaGet.Rows.Count)
                                                           Console.WriteLine("PRODUCTS : " & CheckingForUpdates.ProgressBar1.Maximum)
                                                       End Sub)
                ElseIf FromPosUpdate = 2 Then

                End If
            End If

            For a As Integer = 0 To DaGet.Rows.Count - 1 Step +1
                If UPDATE_WORKER_CANCEL Then
                    Exit For
                End If
                If FromPosUpdate = 1 Then
                    CheckingForUpdates.Instance.Invoke(Sub()
                                                           If CheckingForUpdates.ProgressBar1.Maximum > 0 Then
                                                               CheckingForUpdates.ProgressBar1.Value += 1
                                                               CheckingForUpdates.Label1.Text = CheckingForUpdates.ProgressBar1.Value
                                                           End If
                                                       End Sub)
                ElseIf FromPosUpdate = 2 Then
                End If

                Dim Query As String = "SELECT * FROM admin_products_org WHERE product_id = " & DaGet(a)(0)
                Cmd = New MySqlCommand(Query, Connection)
                DaCount = New MySqlDataAdapter(Cmd)
                FillDt = New DataTable
                DaCount.Fill(FillDt)
                For i As Integer = 0 To FillDt.Rows.Count - 1 Step +1
                    Dim Prod As DataRow = UPDATE_PRODUCTS_DATATABLE.NewRow
                    Prod("product_id") = FillDt(i)(0)
                    Prod("product_sku") = FillDt(i)(1)
                    Prod("product_name") = FillDt(i)(2)
                    Prod("formula_id") = FillDt(i)(3)
                    Prod("product_barcode") = FillDt(i)(4)
                    Prod("product_category") = FillDt(i)(5)
                    Prod("product_price") = FillDt(i)(6)
                    Prod("product_desc") = FillDt(i)(7)
                    Prod("product_image") = FillDt(i)(8)
                    Prod("product_status") = FillDt(i)(9)
                    Prod("origin") = FillDt(i)(10)
                    Prod("date_modified") = FillDt(i)(11)
                    Prod("inventory_id") = FillDt(i)(12)
                    Prod("addontype") = FillDt(i)(13)
                    Prod("half_batch") = FillDt(i)(14)
                    Prod("partners") = FillDt(i)(15)
                    Prod("arrangement") = FillDt(i)(16)
                    UPDATE_PRODUCTS_DATATABLE.Rows.Add(Prod)

                Next
            Next
        Catch ex As Exception
            Exit Sub
            SendErrorReport(ex.ToString)
        End Try
    End Sub

    Private Function LoadCategoryLocal() As DataTable
        Dim cmdlocal As MySqlCommand
        Dim dalocal As MySqlDataAdapter
        Dim dtlocal As DataTable = New DataTable
        dtlocal.Columns.Add("updated_at")
        dtlocal.Columns.Add("category_id")
        Dim dtlocal1 As DataTable = New DataTable
        Try
            Dim sql = "SELECT updated_at, category_id FROM loc_admin_category"
            cmdlocal = New MySqlCommand(sql, LocalhostConn())
            dalocal = New MySqlDataAdapter(cmdlocal)
            dalocal.Fill(dtlocal1)
            For i As Integer = 0 To dtlocal1.Rows.Count - 1 Step +1
                Dim Cat As DataRow = dtlocal.NewRow
                Cat("updated_at") = dtlocal1(i)(0).ToString
                Cat("category_id") = dtlocal1(i)(1)
                dtlocal.Rows.Add(Cat)
            Next
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Retrieve Module: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
        Return dtlocal
    End Function

    Public Sub GetCategoryUpdate(FromPosUpdate As Integer)
        Try

            UPDATE_CATEGORY_DATATABLE = New DataTable
            UPDATE_CATEGORY_DATATABLE.Columns.Add("category_id")
            UPDATE_CATEGORY_DATATABLE.Columns.Add("category_name")
            UPDATE_CATEGORY_DATATABLE.Columns.Add("brand_name")
            UPDATE_CATEGORY_DATATABLE.Columns.Add("updated_at")
            UPDATE_CATEGORY_DATATABLE.Columns.Add("origin")
            UPDATE_CATEGORY_DATATABLE.Columns.Add("status")


            Dim Query = "SELECT * FROM loc_admin_category"
            Dim CmdCheck As MySqlCommand = New MySqlCommand(Query, LocalhostConn)
            Dim DaCheck As MySqlDataAdapter = New MySqlDataAdapter(CmdCheck)
            Dim DtCheck As DataTable = New DataTable
            DaCheck.Fill(DtCheck)
            Dim cmdserver As MySqlCommand
            Dim daserver As MySqlDataAdapter
            Dim dtserver As DataTable
            If DtCheck.Rows.Count < 1 Then
                Dim sql = "SELECT `category_id`, `category_name`, `brand_name`, `updated_at`, `origin`, `status` FROM admin_category"
                cmdserver = New MySqlCommand(sql, ServerCloudCon())
                daserver = New MySqlDataAdapter(cmdserver)
                dtserver = New DataTable
                daserver.Fill(dtserver)
                If UPDATE_WORKER_CANCEL = False Then
                    If FromPosUpdate = 1 Then
                        CheckingForUpdates.Instance.Invoke(Sub()
                                                               CheckingForUpdates.ProgressBar1.Maximum += dtserver.Rows.Count
                                                               Console.WriteLine("CATEGORY ADDED LINE: " & dtserver.Rows.Count)
                                                               Console.WriteLine("CATEGORY : " & CheckingForUpdates.ProgressBar1.Maximum)
                                                           End Sub)
                    ElseIf FromPosUpdate = 2 Then
                    End If
                End If

                For i As Integer = 0 To dtserver.Rows.Count - 1 Step +1
                    Dim Prod As DataRow = UPDATE_CATEGORY_DATATABLE.NewRow
                    Prod("category_id") = dtserver(i)(0)
                    Prod("category_name") = dtserver(i)(1)
                    Prod("brand_name") = dtserver(i)(2)
                    Prod("updated_at") = dtserver(i)(3)
                    Prod("origin") = dtserver(i)(4)
                    Prod("status") = dtserver(i)(5)
                    UPDATE_CATEGORY_DATATABLE.Rows.Add(Prod)
                    If UPDATE_WORKER_CANCEL = False Then
                        If FromPosUpdate = 1 Then
                            CheckingForUpdates.Instance.Invoke(Sub()
                                                                   If CheckingForUpdates.ProgressBar1.Maximum > 0 Then
                                                                       CheckingForUpdates.ProgressBar1.Value += 1
                                                                       CheckingForUpdates.Label1.Text = CheckingForUpdates.ProgressBar1.Value
                                                                   End If
                                                               End Sub)
                        ElseIf FromPosUpdate = 2 Then
                        End If
                    End If
                Next
            Else
                Dim Ids As String = ""
                If ValidCloudConnection = True Then
                    For i As Integer = 0 To LoadCategoryLocal.Rows.Count - 1 Step +1
                        If Ids = "" Then
                            Ids = "" & LoadCategoryLocal(i)(1) & ""
                        Else
                            Ids += "," & LoadCategoryLocal(i)(1) & ""
                        End If
                    Next
                    Dim sql = "SELECT `category_id`, `category_name`, `brand_name`, `updated_at`, `origin`, `status` FROM admin_category WHERE category_id IN (" & Ids & ")"
                    cmdserver = New MySqlCommand(sql, ServerCloudCon())
                    daserver = New MySqlDataAdapter(cmdserver)
                    dtserver = New DataTable
                    daserver.Fill(dtserver)
                    For i As Integer = 0 To dtserver.Rows.Count - 1 Step +1
                        If LoadCategoryLocal(i)(0).ToString <> dtserver(i)(3).ToString Then
                            Dim Prod As DataRow = UPDATE_CATEGORY_DATATABLE.NewRow
                            Prod("category_id") = dtserver(i)(0)
                            Prod("category_name") = dtserver(i)(1)
                            Prod("brand_name") = dtserver(i)(2)
                            Prod("updated_at") = dtserver(i)(3)
                            Prod("origin") = dtserver(i)(4)
                            Prod("status") = dtserver(i)(5)
                            UPDATE_CATEGORY_DATATABLE.Rows.Add(Prod)
                        End If
                        If UPDATE_WORKER_CANCEL = False Then
                            If FromPosUpdate = 1 Then
                                CheckingForUpdates.Instance.Invoke(Sub()
                                                                       If CheckingForUpdates.ProgressBar1.Maximum > 0 Then
                                                                           CheckingForUpdates.ProgressBar1.Value += 1
                                                                           CheckingForUpdates.Label1.Text = CheckingForUpdates.ProgressBar1.Value
                                                                       End If
                                                                   End Sub)
                            ElseIf FromPosUpdate = 2 Then
                            End If
                        End If

                    Next
                    Dim sql2 = "SELECT `category_id`, `category_name`, `brand_name`, `updated_at`, `origin`, `status` FROM admin_category WHERE category_id NOT IN (" & Ids & ")"
                    cmdserver = New MySqlCommand(sql2, ServerCloudCon())
                    daserver = New MySqlDataAdapter(cmdserver)
                    dtserver = New DataTable
                    daserver.Fill(dtserver)
                    For i As Integer = 0 To dtserver.Rows.Count - 1 Step +1
                        If LoadCategoryLocal(i)(0) <> dtserver(i)(3) Then
                            Dim Prod As DataRow = UPDATE_PRODUCTS_DATATABLE.NewRow
                            Prod("category_id") = dtserver(i)(0)
                            Prod("category_name") = dtserver(i)(1)
                            Prod("brand_name") = dtserver(i)(2)
                            Prod("updated_at") = dtserver(i)(3)
                            Prod("origin") = dtserver(i)(4)
                            Prod("status") = dtserver(i)(5)
                            UPDATE_CATEGORY_DATATABLE.Rows.Add(Prod)
                            If UPDATE_WORKER_CANCEL = False Then
                                If FromPosUpdate = 1 Then
                                    CheckingForUpdates.Instance.Invoke(Sub()
                                                                           If CheckingForUpdates.ProgressBar1.Maximum > 0 Then
                                                                               CheckingForUpdates.ProgressBar1.Maximum += 1
                                                                               CheckingForUpdates.ProgressBar1.Value += 1
                                                                               CheckingForUpdates.Label1.Text = CheckingForUpdates.ProgressBar1.Value
                                                                           End If
                                                                       End Sub)
                                ElseIf FromPosUpdate = 2 Then
                                End If
                            End If
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Retrieve Module: " & ex.ToString, "Critical")

            Exit Sub
            SendErrorReport(ex.ToString)
            'If table doesnt have data
        End Try
    End Sub
    Private Function LoadFormulaLocal() As DataTable
        Dim cmdlocal As MySqlCommand
        Dim dalocal As MySqlDataAdapter
        Dim dtlocal As DataTable = New DataTable
        dtlocal.Columns.Add("server_date_modified")
        dtlocal.Columns.Add("server_formula_id")
        Dim dtlocal1 As DataTable = New DataTable
        Try
            Dim sql = "SELECT server_date_modified, server_formula_id FROM loc_product_formula"
            cmdlocal = New MySqlCommand(sql, LocalhostConn)
            dalocal = New MySqlDataAdapter(cmdlocal)
            dalocal.Fill(dtlocal1)
            For i As Integer = 0 To dtlocal1.Rows.Count - 1 Step +1
                Dim Cat As DataRow = dtlocal.NewRow
                Cat("server_date_modified") = dtlocal1(i)(0).ToString
                Cat("server_formula_id") = dtlocal1(i)(1)
                dtlocal.Rows.Add(Cat)
            Next
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Retrieve Module: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
        Return dtlocal
    End Function
    Public Sub GetFormulaUpdate(FromPosUpdate As Integer)
        Try

            UPDATE_FORMULA_DATATABLE = New DataTable
            UPDATE_FORMULA_DATATABLE.Columns.Add("server_formula_id")
            UPDATE_FORMULA_DATATABLE.Columns.Add("product_ingredients")
            UPDATE_FORMULA_DATATABLE.Columns.Add("primary_unit")
            UPDATE_FORMULA_DATATABLE.Columns.Add("primary_value")
            UPDATE_FORMULA_DATATABLE.Columns.Add("secondary_unit")
            UPDATE_FORMULA_DATATABLE.Columns.Add("secondary_value")
            UPDATE_FORMULA_DATATABLE.Columns.Add("serving_unit")
            UPDATE_FORMULA_DATATABLE.Columns.Add("serving_value")
            UPDATE_FORMULA_DATATABLE.Columns.Add("no_servings")
            UPDATE_FORMULA_DATATABLE.Columns.Add("status")
            UPDATE_FORMULA_DATATABLE.Columns.Add("date_modified")
            UPDATE_FORMULA_DATATABLE.Columns.Add("unit_cost")
            UPDATE_FORMULA_DATATABLE.Columns.Add("origin")


            Dim ConnectionLocal As MySqlConnection = LocalhostConn()
            Dim ConnectionServer As MySqlConnection = ServerCloudCon()
            Dim FormulaLocal = LoadFormulaLocal()

            Dim Query = "SELECT * FROM loc_product_formula"
            Dim CmdCheck As MySqlCommand = New MySqlCommand(Query, ConnectionLocal)
            Dim DaCheck As MySqlDataAdapter = New MySqlDataAdapter(CmdCheck)
            Dim DtCheck As DataTable = New DataTable
            DaCheck.Fill(DtCheck)
            Dim cmdserver As MySqlCommand
            Dim daserver As MySqlDataAdapter
            Dim dtserver As DataTable
            If DtCheck.Rows.Count < 1 Then
                Dim sql = "SELECT `server_formula_id`, `product_ingredients`, `primary_unit`, `primary_value`, `secondary_unit`, `secondary_value`, `serving_unit`, `serving_value`, `no_servings`, `status`, `date_modified`, `unit_cost`, `origin` FROM admin_product_formula_org"
                cmdserver = New MySqlCommand(sql, ConnectionServer)
                daserver = New MySqlDataAdapter(cmdserver)
                dtserver = New DataTable
                daserver.Fill(dtserver)
                If UPDATE_WORKER_CANCEL = False Then

                    If FromPosUpdate = 1 Then
                        CheckingForUpdates.Instance.Invoke(Sub()
                                                               CheckingForUpdates.ProgressBar1.Maximum += dtserver.Rows.Count
                                                               Console.WriteLine("FORMULA ADDED LINE: " & dtserver.Rows.Count)
                                                               Console.WriteLine("FORMULA : " & CheckingForUpdates.ProgressBar1.Maximum)
                                                           End Sub)
                    ElseIf FromPosUpdate = 2 Then
                    End If
                End If

                For i As Integer = 0 To dtserver.Rows.Count - 1 Step +1
                    Dim Prod As DataRow = UPDATE_FORMULA_DATATABLE.NewRow
                    Prod("server_formula_id") = dtserver(i)(0)
                    Prod("product_ingredients") = dtserver(i)(1)
                    Prod("primary_unit") = dtserver(i)(2)
                    Prod("primary_value") = dtserver(i)(3)
                    Prod("secondary_unit") = dtserver(i)(4)
                    Prod("secondary_value") = dtserver(i)(5)
                    Prod("serving_unit") = dtserver(i)(6)
                    Prod("serving_value") = dtserver(i)(7)
                    Prod("no_servings") = dtserver(i)(8)
                    Prod("status") = dtserver(i)(9)
                    Prod("date_modified") = dtserver(i)(10)
                    Prod("unit_cost") = dtserver(i)(11)
                    Prod("origin") = dtserver(i)(12)
                    UPDATE_FORMULA_DATATABLE.Rows.Add(Prod)

                    If UPDATE_WORKER_CANCEL = False Then
                        If FromPosUpdate = 1 Then
                            CheckingForUpdates.Instance.Invoke(Sub()
                                                                   If CheckingForUpdates.ProgressBar1.Maximum > 0 Then
                                                                       CheckingForUpdates.ProgressBar1.Value += 1
                                                                       CheckingForUpdates.Label1.Text = CheckingForUpdates.ProgressBar1.Value
                                                                   End If
                                                               End Sub)
                        ElseIf FromPosUpdate = 2 Then
                        End If
                    End If
                Next
            Else
                Dim Ids As String = ""
                If ValidCloudConnection = True Then
                    For i As Integer = 0 To FormulaLocal.Rows.Count - 1 Step +1
                        If Ids = "" Then
                            Ids = "" & FormulaLocal(i)(1) & ""
                        Else
                            Ids += "," & FormulaLocal(i)(1) & ""
                        End If
                    Next
                    Dim sql = "SELECT `server_formula_id`, `product_ingredients`, `primary_unit`, `primary_value`, `secondary_unit`, `secondary_value`, `serving_unit`, `serving_value`, `no_servings`, `status`, `date_modified`, `unit_cost`, `origin` FROM admin_product_formula_org WHERE server_formula_id  IN (" & Ids & ") "
                    cmdserver = New MySqlCommand(sql, ConnectionServer)
                    daserver = New MySqlDataAdapter(cmdserver)
                    dtserver = New DataTable
                    daserver.Fill(dtserver)
                    For i As Integer = 0 To dtserver.Rows.Count - 1 Step +1
                        If FormulaLocal(i)(0).ToString <> dtserver(i)(10).ToString Then
                            Dim Prod As DataRow = UPDATE_FORMULA_DATATABLE.NewRow
                            Prod("server_formula_id") = dtserver(i)(0)
                            Prod("product_ingredients") = dtserver(i)(1)
                            Prod("primary_unit") = dtserver(i)(2)
                            Prod("primary_value") = dtserver(i)(3)
                            Prod("secondary_unit") = dtserver(i)(4)
                            Prod("secondary_value") = dtserver(i)(5)
                            Prod("serving_unit") = dtserver(i)(6)
                            Prod("serving_value") = dtserver(i)(7)
                            Prod("no_servings") = dtserver(i)(8)
                            Prod("status") = dtserver(i)(9)
                            Prod("date_modified") = dtserver(i)(10)
                            Prod("unit_cost") = dtserver(i)(11)
                            Prod("origin") = dtserver(i)(12)
                            UPDATE_FORMULA_DATATABLE.Rows.Add(Prod)
                        End If
                        If UPDATE_WORKER_CANCEL = False Then
                            If FromPosUpdate = 1 Then
                                CheckingForUpdates.Instance.Invoke(Sub()
                                                                       If CheckingForUpdates.ProgressBar1.Maximum > 0 Then
                                                                           CheckingForUpdates.ProgressBar1.Value += 1
                                                                           CheckingForUpdates.Label1.Text = CheckingForUpdates.ProgressBar1.Value
                                                                       End If
                                                                   End Sub)
                            ElseIf FromPosUpdate = 2 Then
                            End If
                        End If
                    Next
                    Dim sql2 = "SELECT `server_formula_id`, `product_ingredients`, `primary_unit`, `primary_value`, `secondary_unit`, `secondary_value`, `serving_unit`, `serving_value`, `no_servings`, `status`, `date_modified`, `unit_cost`, `origin` FROM admin_product_formula_org WHERE server_formula_id NOT IN (" & Ids & ") "
                    cmdserver = New MySqlCommand(sql2, ConnectionServer)
                    daserver = New MySqlDataAdapter(cmdserver)
                    dtserver = New DataTable
                    daserver.Fill(dtserver)
                    For i As Integer = 0 To dtserver.Rows.Count - 1 Step +1
                        If FormulaLocal(i)(0).ToString <> dtserver(i)(10) Then
                            Dim Prod As DataRow = UPDATE_FORMULA_DATATABLE.NewRow
                            Prod("server_formula_id") = dtserver(i)(0)
                            Prod("product_ingredients") = dtserver(i)(1)
                            Prod("primary_unit") = dtserver(i)(2)
                            Prod("primary_value") = dtserver(i)(3)
                            Prod("secondary_unit") = dtserver(i)(4)
                            Prod("secondary_value") = dtserver(i)(5)
                            Prod("serving_unit") = dtserver(i)(6)
                            Prod("serving_value") = dtserver(i)(7)
                            Prod("no_servings") = dtserver(i)(8)
                            Prod("status") = dtserver(i)(9)
                            Prod("date_modified") = dtserver(i)(10)
                            Prod("unit_cost") = dtserver(i)(11)
                            Prod("origin") = dtserver(i)(12)
                            UPDATE_FORMULA_DATATABLE.Rows.Add(Prod)
                            If UPDATE_WORKER_CANCEL = False Then
                                If FromPosUpdate = 1 Then
                                    CheckingForUpdates.Instance.Invoke(Sub()
                                                                           If CheckingForUpdates.ProgressBar1.Maximum > 0 Then
                                                                               CheckingForUpdates.ProgressBar1.Maximum += 1
                                                                               CheckingForUpdates.ProgressBar1.Value += 1
                                                                               CheckingForUpdates.Label1.Text = CheckingForUpdates.ProgressBar1.Value
                                                                           End If
                                                                       End Sub)
                                ElseIf FromPosUpdate = 2 Then
                                End If
                            End If
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Retrieve Module: " & ex.ToString, "Critical")

            Exit Sub
            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Function LoadInventoryLocal() As DataTable
        Dim cmdlocal As MySqlCommand
        Dim dalocal As MySqlDataAdapter
        Dim dtlocal As DataTable = New DataTable
        dtlocal.Columns.Add("server_date_modified")
        dtlocal.Columns.Add("server_inventory_id")
        Dim dtlocal1 As DataTable = New DataTable
        Try
            Dim sql = "SELECT server_date_modified , server_inventory_id FROM loc_pos_inventory"
            cmdlocal = New MySqlCommand(sql, LocalhostConn)
            dalocal = New MySqlDataAdapter(cmdlocal)
            dalocal.Fill(dtlocal)
            For i As Integer = 0 To dtlocal1.Rows.Count - 1 Step +1
                Dim Cat As DataRow = dtlocal.NewRow
                Cat("server_date_modified") = dtlocal1(i)(0).ToString
                Cat("server_inventory_id") = dtlocal1(i)(1)
                dtlocal.Rows.Add(Cat)
            Next
            LocalhostConn.Close()
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Retrieve Module: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
        Return dtlocal
    End Function
    Public Sub GetInventoryUpdate(FromPosUpdate As Integer)
        Try

            UPDATE_INVENTORY_DATATABLE = New DataTable
            UPDATE_INVENTORY_DATATABLE.Columns.Add("server_inventory_id")
            UPDATE_INVENTORY_DATATABLE.Columns.Add("formula_id")
            UPDATE_INVENTORY_DATATABLE.Columns.Add("product_ingredients")
            UPDATE_INVENTORY_DATATABLE.Columns.Add("sku")
            UPDATE_INVENTORY_DATATABLE.Columns.Add("stock_primary")
            UPDATE_INVENTORY_DATATABLE.Columns.Add("stock_secondary")
            UPDATE_INVENTORY_DATATABLE.Columns.Add("stock_no_of_servings")
            UPDATE_INVENTORY_DATATABLE.Columns.Add("stock_status")
            UPDATE_INVENTORY_DATATABLE.Columns.Add("critical_limit")
            UPDATE_INVENTORY_DATATABLE.Columns.Add("date_modified")
            UPDATE_INVENTORY_DATATABLE.Columns.Add("main_inventory_id")
            UPDATE_INVENTORY_DATATABLE.Columns.Add("origin")
            UPDATE_INVENTORY_DATATABLE.Columns.Add("show_stockin")

            Dim ConnectionLocal As MySqlConnection = LocalhostConn()
            Dim ConnectionServer As MySqlConnection = ServerCloudCon()
            Dim InventoryLocal = LoadInventoryLocal()

            Dim Query = "SELECT * FROM loc_pos_inventory"
            Dim CmdCheck As MySqlCommand = New MySqlCommand(Query, ConnectionLocal)
            Dim DaCheck As MySqlDataAdapter = New MySqlDataAdapter(CmdCheck)
            Dim DtCheck As DataTable = New DataTable
            DaCheck.Fill(DtCheck)
            Dim cmdserver As MySqlCommand
            Dim daserver As MySqlDataAdapter
            Dim dtserver As DataTable
            If DtCheck.Rows.Count < 1 Then
                Dim sql = "SELECT `server_inventory_id`, `product_ingredients`, `sku`, `stock_primary`, `stock_secondary`, `stock_no_of_servings`, `stock_status`, `critical_limit`, `date_modified`, `main_inventory_id`, `origin`, `show_stockin` FROM admin_pos_inventory_org"
                cmdserver = New MySqlCommand(sql, ConnectionServer)
                daserver = New MySqlDataAdapter(cmdserver)
                dtserver = New DataTable
                daserver.Fill(dtserver)
                If UPDATE_WORKER_CANCEL = False Then
                    If FromPosUpdate = 1 Then
                        CheckingForUpdates.Instance.Invoke(Sub()
                                                               CheckingForUpdates.ProgressBar1.Maximum += dtserver.Rows.Count
                                                               Console.WriteLine("INVENTORY ADDED LINE: " & dtserver.Rows.Count)
                                                               Console.WriteLine("INVENTORY : " & CheckingForUpdates.ProgressBar1.Maximum)
                                                           End Sub)
                    ElseIf FromPosUpdate = 2 Then
                    End If
                End If

                For i As Integer = 0 To dtserver.Rows.Count - 1 Step +1
                    Dim Prod As DataRow = UPDATE_INVENTORY_DATATABLE.NewRow
                    Prod("server_inventory_id") = dtserver(i)(0)
                    Prod("formula_id") = 0
                    Prod("product_ingredients") = dtserver(i)(1)
                    Prod("sku") = dtserver(i)(2)
                    Prod("stock_primary") = dtserver(i)(3)
                    Prod("stock_secondary") = dtserver(i)(4)
                    Prod("stock_no_of_servings") = dtserver(i)(5)
                    Prod("stock_status") = dtserver(i)(6)
                    Prod("critical_limit") = dtserver(i)(7)
                    Prod("date_modified") = dtserver(i)(8)
                    Prod("main_inventory_id") = dtserver(i)(9)
                    Prod("origin") = dtserver(i)(10)
                    Prod("show_stockin") = dtserver(i)(11)
                    UPDATE_INVENTORY_DATATABLE.Rows.Add(Prod)
                    If UPDATE_WORKER_CANCEL = False Then
                        If FromPosUpdate = 1 Then
                            CheckingForUpdates.Instance.Invoke(Sub()
                                                                   If CheckingForUpdates.ProgressBar1.Maximum > 0 Then
                                                                       CheckingForUpdates.ProgressBar1.Value += 1
                                                                       CheckingForUpdates.Label1.Text = CheckingForUpdates.ProgressBar1.Value
                                                                   End If
                                                               End Sub)
                        ElseIf FromPosUpdate = 2 Then
                        End If
                    End If
                Next
            Else
                Dim Ids As String = ""

                If ValidCloudConnection = True Then
                    For i As Integer = 0 To InventoryLocal.Rows.Count - 1 Step +1
                        If Ids = "" Then
                            Ids = "" & InventoryLocal(i)(1) & ""
                        Else
                            Ids += "," & InventoryLocal(i)(1) & ""
                        End If
                    Next
                    Dim sql = "SELECT `server_inventory_id`, `product_ingredients`, `sku`, `stock_primary`, `stock_secondary`, `stock_no_of_servings`, `stock_status`, `critical_limit`, `date_modified`,`main_inventory_id`, `origin`, `show_stockin` FROM admin_pos_inventory_org WHERE server_inventory_id IN (" & Ids & ")"
                    cmdserver = New MySqlCommand(sql, ConnectionServer)
                    daserver = New MySqlDataAdapter(cmdserver)
                    dtserver = New DataTable
                    daserver.Fill(dtserver)
                    For i As Integer = 0 To dtserver.Rows.Count - 1 Step +1
                        If InventoryLocal(i)(0).ToString <> dtserver(i)(8).ToString Then
                            Dim Prod As DataRow = UPDATE_INVENTORY_DATATABLE.NewRow
                            Prod("server_inventory_id") = dtserver(i)(0)
                            Prod("formula_id") = 0
                            Prod("product_ingredients") = dtserver(i)(1)
                            Prod("sku") = dtserver(i)(2)
                            Prod("stock_primary") = dtserver(i)(3)
                            Prod("stock_secondary") = dtserver(i)(4)
                            Prod("stock_no_of_servings") = dtserver(i)(5)
                            Prod("stock_status") = dtserver(i)(6)
                            Prod("critical_limit") = dtserver(i)(7)
                            Prod("date_modified") = dtserver(i)(8)
                            Prod("main_inventory_id") = dtserver(i)(9)
                            Prod("origin") = dtserver(i)(10)
                            Prod("show_stockin") = dtserver(i)(11)
                            UPDATE_INVENTORY_DATATABLE.Rows.Add(Prod)
                        End If
                        If UPDATE_WORKER_CANCEL = False Then
                            If FromPosUpdate = 1 Then
                                CheckingForUpdates.Instance.Invoke(Sub()
                                                                       If CheckingForUpdates.ProgressBar1.Maximum > 0 Then
                                                                           CheckingForUpdates.ProgressBar1.Value += 1
                                                                           CheckingForUpdates.Label1.Text = CheckingForUpdates.ProgressBar1.Value
                                                                       End If
                                                                   End Sub)
                            ElseIf FromPosUpdate = 2 Then
                            End If
                        End If
                    Next
                    Dim sql2 = "SELECT `server_inventory_id`, `product_ingredients`, `sku`, `stock_primary`, `stock_secondary`, `stock_no_of_servings`, `stock_status`, `critical_limit`, `date_modified`,`main_inventory_id`, `origin`, `show_stockin` FROM admin_pos_inventory_org WHERE server_inventory_id NOT IN (" & Ids & ")"
                    cmdserver = New MySqlCommand(sql2, ConnectionServer)
                    daserver = New MySqlDataAdapter(cmdserver)
                    dtserver = New DataTable
                    daserver.Fill(dtserver)
                    For i As Integer = 0 To dtserver.Rows.Count - 1 Step +1
                        If InventoryLocal(i)(0).ToString <> dtserver(i)(8) Then
                            Dim Prod As DataRow = UPDATE_INVENTORY_DATATABLE.NewRow
                            Prod("server_inventory_id") = dtserver(i)(0)
                            Prod("formula_id") = 0
                            Prod("product_ingredients") = dtserver(i)(1)
                            Prod("sku") = dtserver(i)(2)
                            Prod("stock_primary") = dtserver(i)(3)
                            Prod("stock_secondary") = dtserver(i)(4)
                            Prod("stock_no_of_servings") = dtserver(i)(5)
                            Prod("stock_status") = dtserver(i)(6)
                            Prod("critical_limit") = dtserver(i)(7)
                            Prod("date_modified") = dtserver(i)(8)
                            Prod("main_inventory_id") = dtserver(i)(9)
                            Prod("origin") = dtserver(i)(10)
                            Prod("show_stockin") = dtserver(i)(11)
                            UPDATE_INVENTORY_DATATABLE.Rows.Add(Prod)
                            If UPDATE_WORKER_CANCEL = False Then
                                If FromPosUpdate = 1 Then
                                    CheckingForUpdates.Instance.Invoke(Sub()
                                                                           If CheckingForUpdates.ProgressBar1.Maximum > 0 Then
                                                                               CheckingForUpdates.ProgressBar1.Maximum += 1
                                                                               CheckingForUpdates.ProgressBar1.Value += 1
                                                                               CheckingForUpdates.Label1.Text = CheckingForUpdates.ProgressBar1.Value
                                                                           End If
                                                                       End Sub)
                                ElseIf FromPosUpdate = 2 Then
                                End If
                            End If
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Retrieve Module: " & ex.ToString, "Critical")

            Exit Sub
            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Function LoadCouponsLocal() As DataTable
        Dim cmdlocal As MySqlCommand
        Dim dalocal As MySqlDataAdapter
        Dim dtlocal As DataTable = New DataTable
        dtlocal.Columns.Add("date_created")
        dtlocal.Columns.Add("ID")
        Dim dtlocal1 As DataTable = New DataTable
        Try
            Dim sql = "SELECT date_created, ID FROM tbcoupon"
            cmdlocal = New MySqlCommand(sql, LocalhostConn())
            dalocal = New MySqlDataAdapter(cmdlocal)
            dalocal.Fill(dtlocal1)
            For i As Integer = 0 To dtlocal1.Rows.Count - 1 Step +1
                Dim Coup As DataRow = dtlocal.NewRow
                Coup("date_created") = dtlocal1(i)(0).ToString
                Coup("ID") = dtlocal1(i)(1)
                dtlocal.Rows.Add(Coup)
            Next
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Retrieve Module: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
        Return dtlocal
    End Function
    Public Sub GetCouponsUpdate(FromPosUpdate As Integer)
        Try

            UPDATE_COUPONS_DATATABLE = New DataTable
            UPDATE_COUPONS_DATATABLE.Columns.Add("ID")
            UPDATE_COUPONS_DATATABLE.Columns.Add("Couponname_")
            UPDATE_COUPONS_DATATABLE.Columns.Add("Desc_")
            UPDATE_COUPONS_DATATABLE.Columns.Add("Discountvalue_")
            UPDATE_COUPONS_DATATABLE.Columns.Add("Referencevalue_")
            UPDATE_COUPONS_DATATABLE.Columns.Add("Type")
            UPDATE_COUPONS_DATATABLE.Columns.Add("Bundlebase_")
            UPDATE_COUPONS_DATATABLE.Columns.Add("BBValue_")
            UPDATE_COUPONS_DATATABLE.Columns.Add("Bundlepromo_")
            UPDATE_COUPONS_DATATABLE.Columns.Add("BPValue_")
            UPDATE_COUPONS_DATATABLE.Columns.Add("Effectivedate")
            UPDATE_COUPONS_DATATABLE.Columns.Add("Expirydate")
            UPDATE_COUPONS_DATATABLE.Columns.Add("date_created")
            UPDATE_COUPONS_DATATABLE.Columns.Add("active")

            Dim Query = "SELECT * FROM tbcoupon"
            Dim CmdCheck As MySqlCommand = New MySqlCommand(Query, LocalhostConn)
            Dim DaCheck As MySqlDataAdapter = New MySqlDataAdapter(CmdCheck)
            Dim DtCheck As DataTable = New DataTable
            DaCheck.Fill(DtCheck)
            Dim cmdserver As MySqlCommand
            Dim daserver As MySqlDataAdapter
            Dim dtserver As DataTable
            If DtCheck.Rows.Count < 1 Then
                Dim sql = "SELECT `ID`,`Couponname_`,`Desc_`,`Discountvalue_`,`Referencevalue_`,`Type`,`Bundlebase_`,`BBValue_`,`Bundlepromo_`,`BPValue_`,`Effectivedate`,`Expirydate`,`date_created`,`active` FROM admin_coupon"
                cmdserver = New MySqlCommand(sql, ServerCloudCon())
                daserver = New MySqlDataAdapter(cmdserver)
                dtserver = New DataTable
                daserver.Fill(dtserver)

                If UPDATE_WORKER_CANCEL = False Then
                    If FromPosUpdate = 1 Then
                        CheckingForUpdates.Instance.Invoke(Sub()
                                                               CheckingForUpdates.ProgressBar1.Maximum += dtserver.Rows.Count
                                                               Console.WriteLine("COUPONS ADDED LINE: " & dtserver.Rows.Count)
                                                               Console.WriteLine("COUPONS : " & CheckingForUpdates.ProgressBar1.Maximum)
                                                           End Sub)
                    ElseIf FromPosUpdate = 2 Then
                    End If
                End If

                For i As Integer = 0 To dtserver.Rows.Count - 1 Step +1
                    Dim Prod As DataRow = UPDATE_COUPONS_DATATABLE.NewRow
                    Prod("ID") = dtserver(i)(0)
                    Prod("Couponname_") = dtserver(i)(1)
                    Prod("Desc_") = dtserver(i)(2)
                    Prod("Discountvalue_") = dtserver(i)(3)
                    Prod("Referencevalue_") = dtserver(i)(4)
                    Prod("Type") = dtserver(i)(5)
                    Prod("Bundlebase_") = dtserver(i)(6)
                    Prod("BBValue_") = dtserver(i)(7)
                    Prod("Bundlepromo_") = dtserver(i)(8)
                    Prod("BPValue_") = dtserver(i)(9)
                    Prod("Effectivedate") = dtserver(i)(10)
                    Prod("Expirydate") = dtserver(i)(11)
                    Prod("date_created") = dtserver(i)(12)
                    Prod("active") = dtserver(i)(13)
                    UPDATE_COUPONS_DATATABLE.Rows.Add(Prod)
                    If UPDATE_WORKER_CANCEL = False Then
                        If FromPosUpdate = 1 Then
                            CheckingForUpdates.Instance.Invoke(Sub()
                                                                   If CheckingForUpdates.ProgressBar1.Maximum > 0 Then
                                                                       CheckingForUpdates.ProgressBar1.Value += 1
                                                                       CheckingForUpdates.Label1.Text = CheckingForUpdates.ProgressBar1.Value
                                                                   End If
                                                               End Sub)
                        ElseIf FromPosUpdate = 2 Then
                        End If
                    End If
                Next
            Else
                Dim Ids As String = ""
                If ValidCloudConnection = True Then
                    For i As Integer = 0 To LoadCouponsLocal.Rows.Count - 1 Step +1
                        If Ids = "" Then
                            Ids = "" & LoadCouponsLocal(i)(1) & ""
                        Else
                            Ids += "," & LoadCouponsLocal(i)(1) & ""
                        End If
                    Next
                    Dim sql = "SELECT `ID`,`Couponname_`,`Desc_`,`Discountvalue_`,`Referencevalue_`,`Type`,`Bundlebase_`,`BBValue_`,`Bundlepromo_`,`BPValue_`,`Effectivedate`,`Expirydate`,`date_created`,`active` FROM admin_coupon WHERE ID IN (" & Ids & ")"
                    cmdserver = New MySqlCommand(sql, ServerCloudCon())
                    daserver = New MySqlDataAdapter(cmdserver)
                    dtserver = New DataTable
                    daserver.Fill(dtserver)
                    For i As Integer = 0 To dtserver.Rows.Count - 1 Step +1
                        If LoadCouponsLocal(i)(0).ToString <> dtserver(i)(12).ToString Then
                            Dim Prod As DataRow = UPDATE_COUPONS_DATATABLE.NewRow
                            Prod("ID") = dtserver(i)(0)
                            Prod("Couponname_") = dtserver(i)(1)
                            Prod("Desc_") = dtserver(i)(2)
                            Prod("Discountvalue_") = dtserver(i)(3)
                            Prod("Referencevalue_") = dtserver(i)(4)
                            Prod("Type") = dtserver(i)(5)
                            Prod("Bundlebase_") = dtserver(i)(6)
                            Prod("BBValue_") = dtserver(i)(7)
                            Prod("Bundlepromo_") = dtserver(i)(8)
                            Prod("BPValue_") = dtserver(i)(9)
                            Prod("Effectivedate") = dtserver(i)(10)
                            Prod("Expirydate") = dtserver(i)(11)
                            Prod("date_created") = dtserver(i)(12)
                            Prod("active") = dtserver(i)(13)
                            UPDATE_COUPONS_DATATABLE.Rows.Add(Prod)
                        End If
                        If UPDATE_WORKER_CANCEL = False Then
                            If FromPosUpdate = 1 Then
                                CheckingForUpdates.Instance.Invoke(Sub()
                                                                       If CheckingForUpdates.ProgressBar1.Maximum > 0 Then
                                                                           CheckingForUpdates.ProgressBar1.Value += 1
                                                                           CheckingForUpdates.Label1.Text = CheckingForUpdates.ProgressBar1.Value
                                                                       End If
                                                                   End Sub)
                            ElseIf FromPosUpdate = 2 Then
                            End If
                        End If
                    Next
                    Dim sql2 = "SELECT `ID`,`Couponname_`,`Desc_`,`Discountvalue_`,`Referencevalue_`,`Type`,`Bundlebase_`,`BBValue_`,`Bundlepromo_`,`BPValue_`,`Effectivedate`,`Expirydate`,`date_created`,`active` FROM admin_coupon WHERE ID NOT IN (" & Ids & ")"
                    cmdserver = New MySqlCommand(sql2, ServerCloudCon())
                    daserver = New MySqlDataAdapter(cmdserver)
                    dtserver = New DataTable
                    daserver.Fill(dtserver)
                    For i As Integer = 0 To dtserver.Rows.Count - 1 Step +1
                        If LoadCouponsLocal(i)(0) <> dtserver(i)(12) Then
                            Dim Prod As DataRow = UPDATE_COUPONS_DATATABLE.NewRow
                            Prod("ID") = dtserver(i)(0)
                            Prod("Couponname_") = dtserver(i)(1)
                            Prod("Desc_") = dtserver(i)(2)
                            Prod("Discountvalue_") = dtserver(i)(3)
                            Prod("Referencevalue_") = dtserver(i)(4)
                            Prod("Type") = dtserver(i)(5)
                            Prod("Bundlebase_") = dtserver(i)(6)
                            Prod("BBValue_") = dtserver(i)(7)
                            Prod("Bundlepromo_") = dtserver(i)(8)
                            Prod("BPValue_") = dtserver(i)(9)
                            Prod("Effectivedate") = dtserver(i)(10)
                            Prod("Expirydate") = dtserver(i)(11)
                            Prod("date_created") = dtserver(i)(12)
                            Prod("active") = dtserver(i)(13)
                            UPDATE_COUPONS_DATATABLE.Rows.Add(Prod)
                            If UPDATE_WORKER_CANCEL = False Then
                                If FromPosUpdate = 1 Then
                                    CheckingForUpdates.Instance.Invoke(Sub()
                                                                           If CheckingForUpdates.ProgressBar1.Maximum > 0 Then
                                                                               CheckingForUpdates.ProgressBar1.Maximum += 1
                                                                               CheckingForUpdates.ProgressBar1.Value += 1
                                                                               CheckingForUpdates.Label1.Text = CheckingForUpdates.ProgressBar1.Value
                                                                           End If
                                                                       End Sub)
                                ElseIf FromPosUpdate = 2 Then
                                End If
                            End If
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Retrieve Module: " & ex.ToString, "Critical")

            Exit Sub
            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Function LoadPartnersCategory() As DataTable
        Dim cmdlocal As MySqlCommand
        Dim dalocal As MySqlDataAdapter
        Dim dtlocal As DataTable = New DataTable
        dtlocal.Columns.Add("date_modified")
        dtlocal.Columns.Add("id")
        Dim dtlocal1 As DataTable = New DataTable
        Try
            Dim sql = "SELECT date_modified, id FROM loc_partners_transaction"
            cmdlocal = New MySqlCommand(sql, LocalhostConn())
            dalocal = New MySqlDataAdapter(cmdlocal)
            dalocal.Fill(dtlocal1)
            For i As Integer = 0 To dtlocal1.Rows.Count - 1 Step +1
                Dim Cat As DataRow = dtlocal.NewRow
                Cat("date_modified") = dtlocal1(i)(0).ToString
                Cat("id") = dtlocal1(i)(1)
                dtlocal.Rows.Add(Cat)
            Next
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Retrieve Module: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
        Return dtlocal
    End Function
    Public Sub GetPartnersUpdate(FromPosUpdate As Integer)
        Try

            UPDATE_PARTNERS_DATATABLE = New DataTable
            UPDATE_PARTNERS_DATATABLE.Columns.Add("id")
            UPDATE_PARTNERS_DATATABLE.Columns.Add("arrid")
            UPDATE_PARTNERS_DATATABLE.Columns.Add("bankname")
            UPDATE_PARTNERS_DATATABLE.Columns.Add("date_modified")
            UPDATE_PARTNERS_DATATABLE.Columns.Add("active")

            Dim Query = "SELECT * FROM loc_partners_transaction"
            Dim CmdCheck As MySqlCommand = New MySqlCommand(Query, LocalhostConn)
            Dim DaCheck As MySqlDataAdapter = New MySqlDataAdapter(CmdCheck)
            Dim DtCheck As DataTable = New DataTable
            DaCheck.Fill(DtCheck)
            Dim cmdserver As MySqlCommand
            Dim daserver As MySqlDataAdapter
            Dim dtserver As DataTable
            If DtCheck.Rows.Count < 1 Then
                Dim sql = "SELECT `id`, `arrid`, `bankname`, `date_modified`, `active` FROM admin_partners_transaction_org"
                cmdserver = New MySqlCommand(sql, ServerCloudCon())
                daserver = New MySqlDataAdapter(cmdserver)
                dtserver = New DataTable
                daserver.Fill(dtserver)
                If UPDATE_WORKER_CANCEL = False Then
                    If FromPosUpdate = 1 Then
                        CheckingForUpdates.Instance.Invoke(Sub()
                                                               CheckingForUpdates.ProgressBar1.Maximum += dtserver.Rows.Count
                                                               Console.WriteLine("PARTNERS : " & CheckingForUpdates.ProgressBar1.Maximum)
                                                           End Sub)
                    ElseIf FromPosUpdate = 2 Then
                    End If
                End If

                For i As Integer = 0 To dtserver.Rows.Count - 1 Step +1
                    Dim Prod As DataRow = UPDATE_PARTNERS_DATATABLE.NewRow
                    Prod("id") = dtserver(i)(0)
                    Prod("arrid") = dtserver(i)(1)
                    Prod("bankname") = dtserver(i)(2)
                    Prod("date_modified") = dtserver(i)(3)
                    Prod("active") = dtserver(i)(4)
                    UPDATE_PARTNERS_DATATABLE.Rows.Add(Prod)

                    If UPDATE_WORKER_CANCEL = False Then
                        If FromPosUpdate = 1 Then
                            CheckingForUpdates.Instance.Invoke(Sub()
                                                                   If CheckingForUpdates.ProgressBar1.Maximum > 0 Then
                                                                       CheckingForUpdates.ProgressBar1.Value += 1
                                                                       CheckingForUpdates.Label1.Text = CheckingForUpdates.ProgressBar1.Value
                                                                   End If
                                                               End Sub)
                        ElseIf FromPosUpdate = 2 Then
                        End If
                    End If
                Next
            Else
                Dim Ids As String = ""
                If ValidCloudConnection = True Then
                    For i As Integer = 0 To LoadPartnersCategory.Rows.Count - 1 Step +1
                        If Ids = "" Then
                            Ids = "" & LoadPartnersCategory(i)(1) & ""
                        Else
                            Ids += "," & LoadPartnersCategory(i)(1) & ""
                        End If
                    Next
                    Dim sql = "SELECT `id`, `arrid`, `bankname`, `date_modified`, `active` FROM admin_partners_transaction_org WHERE id IN (" & Ids & ")"
                    cmdserver = New MySqlCommand(sql, ServerCloudCon())
                    daserver = New MySqlDataAdapter(cmdserver)
                    dtserver = New DataTable
                    daserver.Fill(dtserver)
                    For i As Integer = 0 To dtserver.Rows.Count - 1 Step +1
                        If LoadPartnersCategory(i)(0).ToString <> dtserver(i)(3).ToString Then
                            Dim Prod As DataRow = UPDATE_PARTNERS_DATATABLE.NewRow
                            Prod("id") = dtserver(i)(0)
                            Prod("arrid") = dtserver(i)(1)
                            Prod("bankname") = dtserver(i)(2)
                            Prod("date_modified") = dtserver(i)(3)
                            Prod("active") = dtserver(i)(4)
                            UPDATE_PARTNERS_DATATABLE.Rows.Add(Prod)
                        End If
                        If UPDATE_WORKER_CANCEL = False Then
                            If FromPosUpdate = 1 Then
                                CheckingForUpdates.Instance.Invoke(Sub()
                                                                       If CheckingForUpdates.ProgressBar1.Maximum > 0 Then
                                                                           CheckingForUpdates.ProgressBar1.Value += 1
                                                                           CheckingForUpdates.Label1.Text = CheckingForUpdates.ProgressBar1.Value
                                                                       End If
                                                                   End Sub)
                            ElseIf FromPosUpdate = 2 Then
                            End If
                        End If
                    Next
                    Dim sql2 = "SELECT `id`, `arrid`, `bankname`, `date_modified`, `active` FROM admin_partners_transaction_org WHERE id NOT IN (" & Ids & ")"
                    cmdserver = New MySqlCommand(sql2, ServerCloudCon())
                    daserver = New MySqlDataAdapter(cmdserver)
                    dtserver = New DataTable
                    daserver.Fill(dtserver)
                    For i As Integer = 0 To dtserver.Rows.Count - 1 Step +1
                        If LoadPartnersCategory(i)(0) <> dtserver(i)(3) Then
                            Dim Prod As DataRow = UPDATE_PARTNERS_DATATABLE.NewRow
                            Prod("id") = dtserver(i)(0)
                            Prod("arrid") = dtserver(i)(1)
                            Prod("bankname") = dtserver(i)(2)
                            Prod("date_modified") = dtserver(i)(3)
                            Prod("active") = dtserver(i)(4)
                            UPDATE_PARTNERS_DATATABLE.Rows.Add(Prod)
                            If UPDATE_WORKER_CANCEL = False Then
                                If FromPosUpdate = 1 Then
                                    CheckingForUpdates.Instance.Invoke(Sub()
                                                                           If CheckingForUpdates.ProgressBar1.Maximum > 0 Then
                                                                               CheckingForUpdates.ProgressBar1.Maximum += 1
                                                                               CheckingForUpdates.ProgressBar1.Value += 1
                                                                               CheckingForUpdates.Label1.Text = CheckingForUpdates.ProgressBar1.Value
                                                                           End If
                                                                       End Sub)
                                ElseIf FromPosUpdate = 2 Then
                                    SettingsForm.Instance.Invoke(Sub()
                                                                     If SettingsForm.ProgressBar1.Maximum > 0 Then
                                                                         SettingsForm.ProgressBar1.Maximum += 1
                                                                         SettingsForm.ProgressBar1.Value += 1
                                                                         SettingsForm.Label1.Text = SettingsForm.ProgressBar1.Value
                                                                     End If
                                                                 End Sub)
                                End If
                            End If
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Retrieve Module: " & ex.ToString, "Critical")

            Exit Sub
            SendErrorReport(ex.ToString)
        End Try
    End Sub

    Public Sub CheckPriceChanges()
        Try
            Dim ConnectionServer As MySqlConnection = ServerCloudCon()
            Dim Query = "SELECT * FROM admin_price_request WHERE store_id = '" & ClientStoreID & "' AND guid = '" & ClientGuid & "' AND synced = 'Unsynced' AND active = 2"
            Dim CmdCheck As MySqlCommand = New MySqlCommand(Query, ConnectionServer)
            Dim DaCheck As MySqlDataAdapter = New MySqlDataAdapter(CmdCheck)
            UPDATE_PRICE_CHANGE_DATATABLE = New DataTable
            DaCheck.Fill(UPDATE_PRICE_CHANGE_DATATABLE)
            If UPDATE_PRICE_CHANGE_DATATABLE.Rows.Count > 0 Then
                UPDATE_PRICE_CHANGE_BOOL = True
            Else
                UPDATE_PRICE_CHANGE_BOOL = False
            End If
        Catch ex As Exception
            Exit Sub
        End Try
    End Sub

    Public Sub CouponApproval()
        Try
            Dim ConnectionServer As MySqlConnection = ServerCloudCon()
            Dim Query = "SELECT ID FROM admin_custom_coupon WHERE store_id = '" & ClientStoreID & "' AND guid = '" & ClientGuid & "' AND active = 1 AND synced = 'Unsynced'"
            Dim CmdCheck As MySqlCommand = New MySqlCommand(Query, ConnectionServer)
            Dim DaCheck As MySqlDataAdapter = New MySqlDataAdapter(CmdCheck)
            UPDATE_COUPON_APPROVAL_DATATABLE = New DataTable
            DaCheck.Fill(UPDATE_COUPON_APPROVAL_DATATABLE)
            If UPDATE_COUPON_APPROVAL_DATATABLE.Rows.Count > 0 Then
                UPDATE_COUPON_APPROVAL_BOOL = True
            Else
                UPDATE_COUPON_APPROVAL_BOOL = False
            End If
        Catch ex As Exception
            Exit Sub
        End Try
    End Sub

    Public Sub CustomProductApproval()
        Try
            Dim ConnectionServer As MySqlConnection = ServerCloudCon()
            Dim Query = "SELECT loc_product_id, product_formula_id FROM loc_product_list WHERE store_id = '" & ClientStoreID & "' AND guid = '" & ClientGuid & "' AND product_status = 1 AND synced = 'Unsynced'"
            Dim CmdCheck As MySqlCommand = New MySqlCommand(Query, ConnectionServer)
            Dim DaCheck As MySqlDataAdapter = New MySqlDataAdapter(CmdCheck)
            UPDATE_CUSTOM_PROD_APP_DATATABLE = New DataTable
            DaCheck.Fill(UPDATE_CUSTOM_PROD_APP_DATATABLE)
            If UPDATE_CUSTOM_PROD_APP_DATATABLE.Rows.Count > 0 Then
                UPDATE_CUSTOM_PROD_APP_BOOL = True
            Else
                UPDATE_CUSTOM_PROD_APP_BOOL = False
            End If
        Catch ex As Exception
            Exit Sub
        End Try
    End Sub

    Public Sub PromptMessage()
        Dim LocMessageDatatable As DataTable

        PROMPT_MESSAGE_DATATABLE = New DataTable
        PROMPT_MESSAGE_DATATABLE.Columns.Add("message_id")
        PROMPT_MESSAGE_DATATABLE.Columns.Add("from")
        PROMPT_MESSAGE_DATATABLE.Columns.Add("subject")
        PROMPT_MESSAGE_DATATABLE.Columns.Add("content")
        PROMPT_MESSAGE_DATATABLE.Columns.Add("guid")
        PROMPT_MESSAGE_DATATABLE.Columns.Add("store_id")
        PROMPT_MESSAGE_DATATABLE.Columns.Add("active")
        PROMPT_MESSAGE_DATATABLE.Columns.Add("created_at")
        PROMPT_MESSAGE_DATATABLE.Columns.Add("origin")

        Try
            Dim ConnectionLocal As MySqlConnection = LocalhostConn()
            Dim ConnectionCloud As MySqlConnection = ServerCloudCon()
            Dim Query = "SELECT server_message_id FROM loc_message"
            Dim Command As MySqlCommand = New MySqlCommand(Query, ConnectionLocal)
            Dim da As MySqlDataAdapter = New MySqlDataAdapter(Command)
            LocMessageDatatable = New DataTable
            da.Fill(LocMessageDatatable)
            If LocMessageDatatable.Rows.Count > 0 Then
                Dim MessageIDS = ""
                For i As Integer = 0 To LocMessageDatatable.Rows.Count - 1 Step +1
                    MessageIDS += LocMessageDatatable(i)(0).ToString & ","
                Next
                MessageIDS = MessageIDS.TrimEnd(CChar(","))
                Query = "SELECT * FROM admin_message WHERE message_id NOT IN (" & MessageIDS & ") AND guid = '" & ClientGuid & "' "
                Command = New MySqlCommand(Query, ServerCloudCon)
                da = New MySqlDataAdapter(Command)
                Dim dt As DataTable = New DataTable
                da.Fill(dt)
                For i As Integer = 0 To dt.Rows.Count - 1 Step +1
                    Dim Mess As DataRow = PROMPT_MESSAGE_DATATABLE.NewRow
                    Mess("message_id") = dt(i)(0)
                    Mess("from") = dt(i)(1)
                    Mess("subject") = dt(i)(5)
                    Mess("content") = dt(i)(6)
                    Mess("guid") = dt(i)(7)
                    Mess("store_id") = ClientStoreID
                    Mess("active") = 1
                    Mess("created_at") = dt(i)(8)
                    Mess("origin") = "Server"
                    PROMPT_MESSAGE_DATATABLE.Rows.Add(Mess)
                Next
            Else
                Query = "SELECT * FROM admin_message WHERE guid = '" & ClientGuid & "' "
                Command = New MySqlCommand(Query, ServerCloudCon)
                da = New MySqlDataAdapter(Command)
                Dim dt As DataTable = New DataTable
                da.Fill(dt)
                For i As Integer = 0 To dt.Rows.Count - 1 Step +1
                    Dim Mess As DataRow = PROMPT_MESSAGE_DATATABLE.NewRow

                    Mess("message_id") = dt(i)(0)
                    Mess("from") = dt(i)(1)
                    Mess("subject") = dt(i)(5)
                    Mess("content") = dt(i)(6)
                    Mess("guid") = dt(i)(7)
                    Mess("store_id") = ClientStoreID
                    Mess("active") = 1
                    Mess("created_at") = dt(i)(8)
                    Mess("origin") = "Server"
                    PROMPT_MESSAGE_DATATABLE.Rows.Add(Mess)

                Next
            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Retrieve Module: " & ex.ToString, "Critical")

            Exit Sub
            SendErrorReport(ex.ToString)
        End Try
    End Sub

    Public Sub DisplayInbox()
        Try
            If PROMPT_MESSAGE_DATATABLE.Rows.Count > 0 Then
                POS.Enabled = False
                Dim ConnectionLocal As MySqlConnection = LocalhostConn()
                For i As Integer = 0 To PROMPT_MESSAGE_DATATABLE.Rows.Count - 1 Step +1
                    Dim sql = "INSERT INTO loc_message (`server_message_id`,`from`, `subject`, `content`, `guid`, `store_id`, `active`, `created_at`, `origin`, `seen`) VALUES (@1,@2,@3,@4,@5,@6,@7,@8,@9,@10)"
                    Dim cmd As MySqlCommand = New MySqlCommand(sql, ConnectionLocal)
                    cmd.Parameters.Add("@1", MySqlDbType.Int64).Value = PROMPT_MESSAGE_DATATABLE(i)(0).ToString
                    cmd.Parameters.Add("@2", MySqlDbType.Text).Value = PROMPT_MESSAGE_DATATABLE(i)(1).ToString
                    cmd.Parameters.Add("@3", MySqlDbType.Text).Value = PROMPT_MESSAGE_DATATABLE(i)(2).ToString
                    cmd.Parameters.Add("@4", MySqlDbType.Text).Value = PROMPT_MESSAGE_DATATABLE(i)(3).ToString
                    cmd.Parameters.Add("@5", MySqlDbType.Text).Value = PROMPT_MESSAGE_DATATABLE(i)(4).ToString
                    cmd.Parameters.Add("@6", MySqlDbType.Text).Value = PROMPT_MESSAGE_DATATABLE(i)(5).ToString
                    cmd.Parameters.Add("@7", MySqlDbType.Int64).Value = PROMPT_MESSAGE_DATATABLE(i)(6)
                    cmd.Parameters.Add("@8", MySqlDbType.Text).Value = PROMPT_MESSAGE_DATATABLE(i)(7).ToString
                    cmd.Parameters.Add("@9", MySqlDbType.Text).Value = PROMPT_MESSAGE_DATATABLE(i)(8).ToString
                    cmd.Parameters.Add("@10", MySqlDbType.Int64).Value = 0
                    cmd.ExecuteNonQuery()
                    cmd.Dispose()
                Next
                For i As Integer = 0 To PROMPT_MESSAGE_DATATABLE.Rows.Count - 1 Step +1
                    If PROMPT_MESSAGE_DATATABLE(i)(4).ToString = "Server" Then
                        Message.Show()
                    ElseIf PROMPT_MESSAGE_DATATABLE(i)(4).ToString = ClientGuid Then
                        If PROMPT_MESSAGE_DATATABLE(i)(5).ToString = ClientStoreID Then
                            Message.Show()
                        End If
                    End If
                Next
            Else
                POS.Enabled = True
                For Each btn As Button In POS.Panel3.Controls.OfType(Of Button)()
                    If btn.Text = "Simply Perfect" Then
                        btn.PerformClick()
                    End If
                Next
            End If

        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Retrieve Module: " & ex.ToString, "Critical")
            SendErrorReport(ex.ToString)
        End Try
    End Sub

#End Region

End Module