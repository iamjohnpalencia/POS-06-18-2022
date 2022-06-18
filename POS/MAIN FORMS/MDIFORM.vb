Public Class MDIFORM
    Private m_ChildFormNumber As Integer
    Private Sub MDIFORM_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            PictureBox1.Image = Base64ToImage(S_Logo)
            PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage

            Dim newMDIchild As New Leaderboards()
            If Application.OpenForms().OfType(Of Leaderboards).Any Then
                Leaderboards.TopMost = True
            Else
                btncolor(changecolor:=Button2)
                btndefaut(defaultcolor:=Button2)
                formclose(closeform:=Leaderboards)
                newMDIchild.MdiParent = Me
                newMDIchild.ShowIcon = False
                newMDIchild.Show()
            End If

            LoadMDIFORM()

            If ClientRole = "Crew" Then
                Button8.Visible = False
                Button5.Visible = False
                Button2.Location = New Point(20, 276)
                Button3.Location = New Point(20, 320)
                Button10.Location = New Point(20, 364)
                Button1.Location = New Point(20, 408)
                Button4.Location = New Point(20, 452)
            End If
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Public Sub LoadMDIFORM()
        Try
            LabelFOOTER.Text = My.Settings.Footer
            LabelTotalProdLine.Text = count(table:="loc_admin_products WHERE store_id = " & ClientStoreID & " AND guid = '" & ClientGuid & "'", tocount:="product_id")
            LabelTotalProdLine.Text = Val(LabelTotalProdLine.Text) + count(table:="loc_admin_products WHERE product_category <> 'Others'", tocount:="product_id")
            LabelTotalAvailStock.Text = roundsum("stock_primary", "loc_pos_inventory WHERE store_id = " & ClientStoreID & " AND guid = '" & ClientGuid & "'", "P")
            LabelTotalSales.Text = sum(table:="loc_daily_transaction_details WHERE zreading = '" & Format(Now(), "yyyy-MM-dd") & "' AND active = 1 AND store_id = '" & ClientStoreID & "' AND guid = '" & ClientGuid & "' ", tototal:="total")
            LabelTotalCrititems.Text = count(table:="loc_pos_inventory WHERE stock_status = 1 AND critical_limit >= stock_primary AND store_id ='" & ClientStoreID & "' AND guid = '" & ClientGuid & "'", tocount:="inventory_id")
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "MDI: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Public newMDIchildManageproduct As ManageProducts
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If Application.OpenForms().OfType(Of SynctoCloud).Any Then
            SynctoCloud.TopMost = False
        End If
        Try
            If DisableFormClose = False Then
                If Application.OpenForms().OfType(Of ManageProducts).Any Then
                Else
                    newMDIchildManageproduct = New ManageProducts
                    btndefaut(defaultcolor:=Button5)
                    btncolor(changecolor:=Button5)
                    formclose(closeform:=ManageProducts)
                    newMDIchildManageproduct.MdiParent = Me
                    newMDIchildManageproduct.ShowIcon = False
                    newMDIchildManageproduct.Show()
                End If
                If SyncIsOnProcess = False Then
                    SynctoCloud.Close()
                End If
            End If

        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "MDI: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Public newMDIchildInventory As Inventory
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        If Application.OpenForms().OfType(Of SynctoCloud).Any Then
            SynctoCloud.TopMost = False
        End If
        Try
            If DisableFormClose = False Then
                newMDIchildInventory = New Inventory
                If Application.OpenForms().OfType(Of Inventory).Any Then
                Else

                    btncolor(changecolor:=Button6)
                    btndefaut(defaultcolor:=Button6)
                    formclose(closeform:=Inventory)
                    newMDIchildInventory.MdiParent = Me
                    newMDIchildInventory.ShowIcon = False
                    newMDIchildInventory.Show()
                End If
                If SyncIsOnProcess = False Then
                    SynctoCloud.Close()
                End If
            End If

        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "MDI: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Public newMDIchildReports As Reports
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If Application.OpenForms().OfType(Of SynctoCloud).Any Then
            SynctoCloud.TopMost = False
        End If
        Try
            If DisableFormClose = False Then
                newMDIchildReports = New Reports
                If Application.OpenForms().OfType(Of Reports).Any Then
                Else
                    btncolor(changecolor:=Button3)
                    btndefaut(defaultcolor:=Button3)
                    formclose(closeform:=Reports)
                    newMDIchildReports.MdiParent = Me
                    newMDIchildReports.ShowIcon = False
                    newMDIchildReports.Show()
                End If
                If SyncIsOnProcess = False Then
                    SynctoCloud.Close()
                End If
            End If

        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "MDI: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Public newMDIchildUser As UserSettings
    Private Sub Button8_Click_1(sender As Object, e As EventArgs) Handles Button8.Click
        If Application.OpenForms().OfType(Of SynctoCloud).Any Then
            SynctoCloud.TopMost = False
        End If
        Try
            If DisableFormClose = False Then
                If Application.OpenForms().OfType(Of UserSettings).Any Then
                Else
                    newMDIchildUser = New UserSettings
                    btncolor(changecolor:=Button8)
                    btndefaut(defaultcolor:=Button8)
                    formclose(closeform:=UserSettings)
                    newMDIchildUser.MdiParent = Me
                    newMDIchildUser.ShowIcon = False
                    newMDIchildUser.Show()
                End If
                If SyncIsOnProcess = False Then
                    SynctoCloud.Close()
                End If
            End If

        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "MDI: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        If Application.OpenForms().OfType(Of SynctoCloud).Any Then
            SynctoCloud.TopMost = True
        End If
        'Dim newMDIchild As New SynctoCloud()
        'If Application.OpenForms().OfType(Of SynctoCloud).Any Then

        'Else
        '    If SyncIsOnProcess = True Then
        '        SynctoCloud.WindowState = FormWindowState.Normal
        '    Else
        '        btncolor(changecolor:=Button10)
        '        btndefaut(defaultcolor:=Button10)
        '        formclose(closeform:=SynctoCloud)
        '        newMDIchild.MdiParent = Me
        '        newMDIchild.ShowIcon = False
        '        newMDIchild.Show()
        '    End If
        'End If
        btncolor(changecolor:=Button10)
        btndefaut(defaultcolor:=Button10)
        formclose(closeform:=SynctoCloud)
        SynctoCloud.Show()
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If Application.OpenForms().OfType(Of SynctoCloud).Any Then
            SynctoCloud.TopMost = False
        End If
        Try
            If DisableFormClose = False Then
                Dim newMDIchild As New Leaderboards()
                If Application.OpenForms().OfType(Of Leaderboards).Any Then
                    Leaderboards.TopMost = True
                Else
                    btncolor(changecolor:=Button2)
                    btndefaut(defaultcolor:=Button2)
                    formclose(closeform:=Leaderboards)
                    newMDIchild.MdiParent = Me
                    newMDIchild.ShowIcon = False
                    newMDIchild.Show()
                End If
                If SyncIsOnProcess = False Then
                    SynctoCloud.Close()
                End If
            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "MDI: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub Button4_Click_1(sender As Object, e As EventArgs) Handles Button4.Click
        If Application.OpenForms().OfType(Of SynctoCloud).Any Then
            SynctoCloud.TopMost = False
        End If
        Try
            If DisableFormClose = False Then
                Dim newMDIchild As New About()
                If Application.OpenForms().OfType(Of About).Any Then
                Else
                    btncolor(changecolor:=Button4)
                    btndefaut(defaultcolor:=Button4)
                    formclose(closeform:=About)
                    newMDIchild.MdiParent = Me
                    newMDIchild.ShowIcon = False
                    newMDIchild.Show()
                End If
                If SyncIsOnProcess = False Then
                    SynctoCloud.Close()
                End If
            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "MDI: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        If Application.OpenForms().OfType(Of SynctoCloud).Any Then
            SynctoCloud.TopMost = False
        End If
        Try
            If DisableFormClose = False Then
                Dim newMDIchild As New DepositSlip()
                If Application.OpenForms().OfType(Of DepositSlip).Any Then
                Else
                    btncolor(changecolor:=Button12)
                    btndefaut(defaultcolor:=Button12)
                    formclose(closeform:=DepositSlip)
                    newMDIchild.MdiParent = Me
                    newMDIchild.ShowIcon = False
                    newMDIchild.Show()
                End If
                If SyncIsOnProcess = False Then
                    SynctoCloud.Close()
                End If
            End If

        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "MDI: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Messageboolean = True
        If Application.OpenForms().OfType(Of SynctoCloud).Any Then
            SynctoCloud.TopMost = False
        End If
        Try
            If DisableFormClose = False Then
                Dim newMDIchild As New Message()
                If Application.OpenForms().OfType(Of Message).Any Then
                Else
                    btncolor(changecolor:=Button9)
                    btndefaut(defaultcolor:=Button9)
                    formclose(closeform:=Message)
                    newMDIchild.MdiParent = Me
                    newMDIchild.ShowIcon = False
                    newMDIchild.Show()

                End If
                If SyncIsOnProcess = False Then
                    SynctoCloud.Close()
                End If
            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "MDI: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            If DisableFormClose = False Then
                If Application.OpenForms().OfType(Of SynctoCloud).Any Then
                    SynctoCloud.TopMost = False
                End If
                iflogout = False
                Me.Close()
                SynctoCloud.Close()
            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "MDI: " & ex.ToString, "Critical")

            Dispose()
            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Public Sub btncolor(ByVal changecolor As Button)
        changecolor.ForeColor = Color.White
        changecolor.BackColor = Color.FromArgb(23, 162, 184)
    End Sub
    Public Sub btndefaut(ByVal defaultcolor As Button)
        Try
            For Each P As Control In Controls
                If TypeOf P Is Panel Then
                    For Each ctrl As Control In P.Controls
                        If TypeOf ctrl Is Button Then
                            If ctrl.Name <> defaultcolor.Name Then
                                CType(ctrl, Button).ForeColor = Color.Black
                                CType(ctrl, Button).BackColor = Color.White
                            End If
                        End If
                    Next
                End If
            Next
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Public Sub formclose(ByVal closeform As Form)
        Try
            For Each P As Control In Controls
                For Each ctrl As Control In P.Controls
                    If TypeOf ctrl Is Form Then
                        If ctrl.Name <> closeform.Name Then
                            CType(ctrl, Form).FindForm.Close()
                        End If
                    End If
                Next
            Next
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Dim iflogout As Boolean
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        If SyncIsOnProcess = True Then
            MessageBox.Show("Sync is on process please wait.", "Syncing", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            If MessageBox.Show("Are you sure you really want to Logout ?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbYes Then
                AuditTrail.LogToAuditTral("User", "User Logout: " & ClientCrewID, "Normal")
                iflogout = True
                LOGOUTFROMPOS = False
                CashBreakdown.Show()
                Enabled = False
            End If
        End If
    End Sub
    Private Sub MDIFORM_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If iflogout = False Then
            POS.Enabled = True
            POS.BringToFront()
        End If
    End Sub
End Class