Imports System.Drawing.Imaging
Imports System.Threading
Imports MySql.Data.MySqlClient

Public Class AddEditProducts
    Dim thread1 As System.Threading.Thread

    Public productcode
    Public product
    Public productbarcode
    Public productprice
    Public productdesc
    Public productimage
    Public productid

    Public AddNewProduct As Boolean = True

    Dim encodeType As ImageFormat = ImageFormat.Jpeg
    Dim decodingstring As String = String.Empty
    Private ImagePath As String = ""
    Dim InsertFormulaID
    Dim SelectFormulaID

    Private Sub AddEditProducts_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If AddNewProduct = False Then

            SelectFormulaID = returnselect("formula_id", "loc_admin_products WHERE product_id = " & productid)
            Dim sql = "SELECT product_image FROM loc_admin_products WHERE product_id = " & productid
            Dim cmd As MySqlCommand = New MySqlCommand(sql, LocalhostConn)
            Dim result = cmd.ExecuteScalar
            TextBoxProductCode.Text = productcode
            TextBoxProductName.Text = product
            TextBoxBarcode.Text = productbarcode
            TextBoxPrice.Text = productprice
            TextBoxDescription.Text = productdesc
            TextBoxbase64.Text = result
            TextBoxCriticalLimit.Text = returnselect("critical_limit", "loc_pos_inventory WHERE formula_id = " & SelectFormulaID)
            PictureBoxProductImage.Image = Base64ToImage(result)
        End If
    End Sub

    Private Sub convertimage()
        Try
            Dim ImageToConvert As Bitmap = Bitmap.FromFile(ImagePath)
            ImageToConvert.MakeTransparent()
            Dim encoding As String = String.Empty
            If ImagePath.ToLower.EndsWith(".jpg") Then
                encodeType = ImageFormat.Jpeg
            ElseIf ImagePath.ToLower.EndsWith(".png") Then
                encodeType = ImageFormat.Png
            ElseIf ImagePath.ToLower.EndsWith(".gif") Then
                encodeType = ImageFormat.Gif
            ElseIf ImagePath.ToLower.EndsWith(".bmp") Then
                encodeType = ImageFormat.Bmp
            End If
            decodingstring = encoding
            TextBoxbase64.Text = ImageToBase64(ImageToConvert, encodeType)
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextboxIsEmpty(Me) = True Then
            SubmitbuttonClicked()
            POS.new_Button_click_category(sender, e)
            listviewproductsshow("Simply Perfect", POS.ComboBoxPartners.Text)
        Else
            MsgBox("All fields are required")
        End If
    End Sub

    Private Sub SubmitbuttonClicked()
        Try
            Dim cmd As MySqlCommand
            Dim ConnectionLocal As MySqlConnection = LocalhostConn()
            InsertFormulaID = returnselect("formula_id", "loc_product_formula ORDER by formula_id DESC LIMIT 1") + 1
            If AddNewProduct = True Then
                'Insert Statement
                Dim sql = "INSERT INTO loc_admin_products (`product_sku`, `product_name`, `formula_id`, `product_barcode`, `product_category`, `product_price`, `product_desc`, `product_image`, `product_status`, `origin`, `date_modified`, `guid`, `store_id`, `crew_id`, `synced`, `server_product_id`, `server_inventory_id`, `price_change`, `addontype`) VALUES "
                sql += " (@1,@2,@3,@4,@5,@6,@7,@8,@9,@10,@11,@12,@13,@14,@15,@16,@17,@18,@19)"

                cmd = New MySqlCommand(sql, ConnectionLocal)
                cmd.Parameters.Add("@1", MySqlDbType.VarChar).Value = Trim(TextBoxProductCode.Text)
                cmd.Parameters.Add("@2", MySqlDbType.VarChar).Value = Trim(TextBoxProductName.Text)
                cmd.Parameters.Add("@3", MySqlDbType.VarChar).Value = InsertFormulaID
                cmd.Parameters.Add("@4", MySqlDbType.VarChar).Value = Trim(TextBoxBarcode.Text)
                cmd.Parameters.Add("@5", MySqlDbType.VarChar).Value = "Others"
                cmd.Parameters.Add("@6", MySqlDbType.Int64).Value = Trim(TextBoxPrice.Text)
                cmd.Parameters.Add("@7", MySqlDbType.VarChar).Value = Trim(TextBoxDescription.Text)
                cmd.Parameters.Add("@8", MySqlDbType.LongText).Value = Trim(TextBoxbase64.Text)
                cmd.Parameters.Add("@9", MySqlDbType.VarChar).Value = 0
                cmd.Parameters.Add("@10", MySqlDbType.VarChar).Value = "Local"
                cmd.Parameters.Add("@11", MySqlDbType.Text).Value = FullDate24HR()
                cmd.Parameters.Add("@12", MySqlDbType.VarChar).Value = ClientGuid
                cmd.Parameters.Add("@13", MySqlDbType.Int64).Value = ClientStoreID
                cmd.Parameters.Add("@14", MySqlDbType.VarChar).Value = ClientCrewID
                cmd.Parameters.Add("@15", MySqlDbType.VarChar).Value = "Unsynced"
                cmd.Parameters.Add("@16", MySqlDbType.Int64).Value = 0
                cmd.Parameters.Add("@17", MySqlDbType.Int64).Value = 0
                cmd.Parameters.Add("@18", MySqlDbType.Int64).Value = 0
                cmd.Parameters.Add("@19", MySqlDbType.Text).Value = "N/A"
                cmd.ExecuteNonQuery()

                sql = "INSERT INTO loc_product_formula (`product_ingredients`, `primary_unit`, `primary_value`, `secondary_unit`, `secondary_value`, `serving_unit`, `serving_value`, `no_servings`, `status`, `date_modified`, `unit_cost`, `store_id`, `guid`, `crew_id`, `origin`, `server_formula_id`, `server_date_modified`) VALUES "
                sql += " (@1,@2,@3,@4,@5,@6,@7,@8,@9,@10,@11,@12,@13,@14,@15,@16,@17)"
                cmd = New MySqlCommand(sql, ConnectionLocal)
                cmd.Parameters.Add("@1", MySqlDbType.VarChar).Value = Trim(TextBoxProductName.Text)
                cmd.Parameters.Add("@2", MySqlDbType.VarChar).Value = "piece(s)"
                cmd.Parameters.Add("@3", MySqlDbType.VarChar).Value = "1"
                cmd.Parameters.Add("@4", MySqlDbType.VarChar).Value = "piece(s)"
                cmd.Parameters.Add("@5", MySqlDbType.VarChar).Value = "1"
                cmd.Parameters.Add("@6", MySqlDbType.VarChar).Value = "piece(s)"
                cmd.Parameters.Add("@7", MySqlDbType.VarChar).Value = "1"
                cmd.Parameters.Add("@8", MySqlDbType.VarChar).Value = "1"
                cmd.Parameters.Add("@9", MySqlDbType.Int64).Value = 0
                cmd.Parameters.Add("@10", MySqlDbType.Text).Value = FullDate24HR()
                cmd.Parameters.Add("@11", MySqlDbType.Decimal).Value = 0
                cmd.Parameters.Add("@12", MySqlDbType.VarChar).Value = ClientStoreID
                cmd.Parameters.Add("@13", MySqlDbType.VarChar).Value = ClientGuid
                cmd.Parameters.Add("@14", MySqlDbType.VarChar).Value = ClientCrewID
                cmd.Parameters.Add("@15", MySqlDbType.VarChar).Value = "Local"
                cmd.Parameters.Add("@16", MySqlDbType.Int64).Value = 0
                cmd.Parameters.Add("@17", MySqlDbType.Text).Value = "N/A"
                cmd.ExecuteNonQuery()

                sql = "INSERT INTO loc_pos_inventory (`store_id`, `formula_id`, `product_ingredients`, `sku`, `stock_primary`, `stock_secondary`, `stock_no_of_servings`, `stock_status`, `critical_limit`, `guid`, `date_modified`, `crew_id`, `synced`, `server_date_modified`, `server_inventory_id`, `main_inventory_id`, `origin`, `zreading`) VALUES "
                sql += "(@1,@2,@3,@4,@5,@6,@7,@8,@9,@10,@11,@12,@13,@14,@15,@16,@17,@18)"
                cmd = New MySqlCommand(sql, ConnectionLocal)
                cmd.Parameters.Add("@1", MySqlDbType.VarChar).Value = ClientStoreID
                cmd.Parameters.Add("@2", MySqlDbType.Int64).Value = InsertFormulaID
                cmd.Parameters.Add("@3", MySqlDbType.VarChar).Value = Trim(TextBoxProductName.Text)
                cmd.Parameters.Add("@4", MySqlDbType.VarChar).Value = Trim(TextBoxProductCode.Text)
                cmd.Parameters.Add("@5", MySqlDbType.Double).Value = 0
                cmd.Parameters.Add("@6", MySqlDbType.Double).Value = 0
                cmd.Parameters.Add("@7", MySqlDbType.Double).Value = 0
                cmd.Parameters.Add("@8", MySqlDbType.Int64).Value = 0
                cmd.Parameters.Add("@9", MySqlDbType.Int64).Value = Trim(TextBoxCriticalLimit.Text)
                cmd.Parameters.Add("@10", MySqlDbType.VarChar).Value = ClientGuid
                cmd.Parameters.Add("@11", MySqlDbType.Text).Value = FullDate24HR()
                cmd.Parameters.Add("@12", MySqlDbType.VarChar).Value = ClientCrewID
                cmd.Parameters.Add("@13", MySqlDbType.VarChar).Value = "Unsynced"
                cmd.Parameters.Add("@14", MySqlDbType.Text).Value = "N/A"
                cmd.Parameters.Add("@15", MySqlDbType.Int64).Value = 0
                cmd.Parameters.Add("@16", MySqlDbType.Int64).Value = 0
                cmd.Parameters.Add("@17", MySqlDbType.Text).Value = "Local"
                cmd.Parameters.Add("@18", MySqlDbType.Text).Value = S_Zreading
                cmd.ExecuteNonQuery()

                MDIFORM.newMDIchildManageproduct.LoadOthersPending()
                MDIFORM.newMDIchildManageproduct.LoadOthersApprove()

                MsgBox("Submitted")
                Close()
            Else

                'Update Statement
                Dim sql = "UPDATE loc_admin_products SET `product_sku` = @1, `product_name` = @2, `product_barcode` = @4, `product_category` =@5, `product_price` = @6, `product_desc` = @7, `product_image` = @8, `product_status` = @9, `origin` = @10, `date_modified` = @11, `guid` = @12, `store_id` = @13, `crew_id` = @14, `synced` = @15, `server_product_id` = @16, `server_inventory_id` = @17, `price_change` = @18, `addontype` = @19 WHERE product_id = " & productid
                cmd = New MySqlCommand(sql, ConnectionLocal)
                cmd.Parameters.Add("@1", MySqlDbType.VarChar).Value = Trim(TextBoxProductCode.Text)
                cmd.Parameters.Add("@2", MySqlDbType.VarChar).Value = Trim(TextBoxProductName.Text)
                'cmd.Parameters.Add("@3", MySqlDbType.VarChar).Value = returnselect("product_id", "loc_admin_products") + 1
                cmd.Parameters.Add("@4", MySqlDbType.VarChar).Value = Trim(TextBoxBarcode.Text)
                cmd.Parameters.Add("@5", MySqlDbType.VarChar).Value = "Others"
                cmd.Parameters.Add("@6", MySqlDbType.Int64).Value = Trim(TextBoxPrice.Text)
                cmd.Parameters.Add("@7", MySqlDbType.VarChar).Value = TextBoxDescription.Text
                cmd.Parameters.Add("@8", MySqlDbType.LongText).Value = Trim(TextBoxbase64.Text)
                cmd.Parameters.Add("@9", MySqlDbType.VarChar).Value = 0
                cmd.Parameters.Add("@10", MySqlDbType.VarChar).Value = "Local"
                cmd.Parameters.Add("@11", MySqlDbType.Text).Value = FullDate24HR()
                cmd.Parameters.Add("@12", MySqlDbType.VarChar).Value = ClientGuid
                cmd.Parameters.Add("@13", MySqlDbType.Int64).Value = ClientStoreID
                cmd.Parameters.Add("@14", MySqlDbType.VarChar).Value = ClientCrewID
                cmd.Parameters.Add("@15", MySqlDbType.VarChar).Value = "Unsynced"
                cmd.Parameters.Add("@16", MySqlDbType.Int64).Value = 0
                cmd.Parameters.Add("@17", MySqlDbType.Int64).Value = 0
                cmd.Parameters.Add("@18", MySqlDbType.Int64).Value = 0
                cmd.Parameters.Add("@19", MySqlDbType.Text).Value = "N/A"
                cmd.ExecuteNonQuery()

                sql = "UPDATE loc_product_formula SET `product_ingredients` = @1, `date_modified` = @10 WHERE formula_id = " & SelectFormulaID
                cmd = New MySqlCommand(sql, ConnectionLocal)
                cmd.Parameters.Add("@1", MySqlDbType.VarChar).Value = Trim(TextBoxProductName.Text)
                cmd.Parameters.Add("@10", MySqlDbType.Text).Value = FullDate24HR()
                cmd.ExecuteNonQuery()

                sql = "UPDATE loc_pos_inventory SET `product_ingredients` = @1, `sku` = @2,`critical_limit` = @3, `date_modified` = @4 WHERE formula_id = " & SelectFormulaID
                cmd = New MySqlCommand(sql, ConnectionLocal)
                cmd.Parameters.Add("@1", MySqlDbType.VarChar).Value = Trim(TextBoxProductName.Text)
                cmd.Parameters.Add("@2", MySqlDbType.VarChar).Value = Trim(TextBoxProductCode.Text)
                cmd.Parameters.Add("@3", MySqlDbType.Int64).Value = Trim(TextBoxCriticalLimit.Text)
                cmd.Parameters.Add("@4", MySqlDbType.Text).Value = FullDate24HR()
                cmd.ExecuteNonQuery()

                MDIFORM.newMDIchildManageproduct.LoadOthersPending()
                MDIFORM.newMDIchildManageproduct.LoadOthersApprove()
                MsgBox("Updated")
                Close()
            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Add Edit Products: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub



    Private Sub AddEditProducts_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            MDIFORM.newMDIchildManageproduct.Enabled = True
            MDIFORM.newMDIchildManageproduct.LoadOthersPending()
            AddNewProduct = True
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        'convertimage()
        TextBoxbase64.Clear()
        Try
            With OpenFileDialog1
                .Filter = ("Images | *.png; *.bmp; *.jpg; *.jpeg; *.gif; *.ico;")
                .FilterIndex = 4
            End With
            OpenFileDialog1.FileName = ""
            If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
                If My.Computer.FileSystem.FileExists(ImagePath) Then
                    Button1.Enabled = False
                    ButtonKeyboard.Enabled = False
                    BackgroundWorker1.WorkerSupportsCancellation = True
                    BackgroundWorker1.WorkerReportsProgress = True
                    BackgroundWorker1.RunWorkerAsync()
                End If
                PictureBoxProductImage.Image = Image.FromFile(OpenFileDialog1.FileName)
                PictureBoxProductImage.SizeMode = PictureBoxSizeMode.Zoom
            End If
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Dim threadList As List(Of Thread) = New List(Of Thread)
    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Try
            For i = 0 To 100
                ToolStripStatusLabel1.Text = "Uploading image please wait " & i & " %"
                BackgroundWorker1.ReportProgress(i)
                If i = 10 Then
                    thread1 = New System.Threading.Thread(AddressOf convertimage)
                    thread1.Start()
                    threadList.Add(thread1)
                ElseIf i = 100 Then

                End If
                Threading.Thread.Sleep(10)
            Next
            For Each t In threadList
                t.Join()
            Next
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Add Edit Products: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub BackgroundWorker1_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorker1.ProgressChanged
        ToolStripProgressBar1.Value = e.ProgressPercentage
    End Sub
    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        Try
            If e.Error IsNot Nothing Then
                MessageBox.Show(e.Error.Message)
                Label1.Text = "Error occurred!"
            ElseIf e.Cancelled Then
                MessageBox.Show("Task cancelled!")
                Label1.Text = "Task Cancelled!"
            Else
                Button1.Enabled = True
                ButtonKeyboard.Enabled = True
                ToolStripStatusLabel1.Text = "Successfully Uploaded!"
            End If
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub OpenFileDialog1_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog1.FileOk
        ImagePath = OpenFileDialog1.FileName
    End Sub

    Private Sub TextBoxProductCode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBoxProductName.KeyPress, TextBoxProductCode.KeyPress, TextBoxDescription.KeyPress, TextBoxBarcode.KeyPress
        Try
            If InStr(DisallowedCharacters, e.KeyChar) > 0 Then
                e.Handled = True
            End If
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub

    Private Sub TextBoxPrice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBoxPrice.KeyPress, TextBoxCriticalLimit.KeyPress
        Try
            Numeric(sender, e)
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub
End Class