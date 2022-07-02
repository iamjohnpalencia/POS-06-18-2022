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
                btncolor(changecolor:=ButtonLeaderBoards)
                btndefaut(defaultcolor:=ButtonLeaderBoards)
                formclose(closeform:=Leaderboards)
                newMDIchild.MdiParent = Me
                newMDIchild.ShowIcon = False
                newMDIchild.Show()
            End If

            LoadMDIFORM()

            If ClientRole = "Crew" Then
                ButtonUserSettings.Visible = False
                ButtonProducts.Visible = False
                ButtonLeaderBoards.Location = New Point(20, 276)
                ButtonReports.Location = New Point(20, 320)
                ButtonSync.Location = New Point(20, 364)
                ButtonPointofSales.Location = New Point(20, 408)
                ButtonAbout.Location = New Point(20, 452)
            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTrail("System", "MDI/Load: " & ex.ToString, "Critical")
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
            AuditTrail.LogToAuditTrail("System", "MDI/LoadMDIFORM(): " & ex.ToString, "Critical")
        End Try
    End Sub
    Public newMDIchildManageproduct As ManageProducts
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles ButtonProducts.Click
        If Application.OpenForms().OfType(Of SynctoCloud).Any Then
            SynctoCloud.TopMost = False
        End If
        Try
            If DisableFormClose = False Then
                If Application.OpenForms().OfType(Of ManageProducts).Any Then
                Else
                    newMDIchildManageproduct = New ManageProducts
                    btndefaut(defaultcolor:=ButtonProducts)
                    btncolor(changecolor:=ButtonProducts)
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
            AuditTrail.LogToAuditTrail("System", "MDI/ButtonProducts: " & ex.ToString, "Critical")
        End Try
    End Sub
    Public newMDIchildInventory As Inventory
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles ButtonInventory.Click
        If Application.OpenForms().OfType(Of SynctoCloud).Any Then
            SynctoCloud.TopMost = False
        End If
        Try
            If DisableFormClose = False Then
                newMDIchildInventory = New Inventory
                If Application.OpenForms().OfType(Of Inventory).Any Then
                Else

                    btncolor(changecolor:=ButtonInventory)
                    btndefaut(defaultcolor:=ButtonInventory)
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
            AuditTrail.LogToAuditTrail("System", "MDI/ButtonInventory: " & ex.ToString, "Critical")
        End Try
    End Sub
    Public newMDIchildReports As Reports
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles ButtonReports.Click
        If Application.OpenForms().OfType(Of SynctoCloud).Any Then
            SynctoCloud.TopMost = False
        End If
        Try
            If DisableFormClose = False Then
                newMDIchildReports = New Reports
                If Application.OpenForms().OfType(Of Reports).Any Then
                Else
                    btncolor(changecolor:=ButtonReports)
                    btndefaut(defaultcolor:=ButtonReports)
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
            AuditTrail.LogToAuditTrail("System", "MDI/ButtonReports: " & ex.ToString, "Critical")
        End Try
    End Sub
    Public newMDIchildUser As UserSettings
    Private Sub Button8_Click_1(sender As Object, e As EventArgs) Handles ButtonUserSettings.Click
        If Application.OpenForms().OfType(Of SynctoCloud).Any Then
            SynctoCloud.TopMost = False
        End If
        Try
            If DisableFormClose = False Then
                If Application.OpenForms().OfType(Of UserSettings).Any Then
                Else
                    newMDIchildUser = New UserSettings
                    btncolor(changecolor:=ButtonUserSettings)
                    btndefaut(defaultcolor:=ButtonUserSettings)
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
            AuditTrail.LogToAuditTrail("System", "MDI/ButtonUserSettings: " & ex.ToString, "Critical")
        End Try
    End Sub
    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles ButtonSync.Click
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
        btncolor(changecolor:=ButtonSync)
        btndefaut(defaultcolor:=ButtonSync)
        formclose(closeform:=SynctoCloud)
        SynctoCloud.Show()
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles ButtonLeaderBoards.Click
        If Application.OpenForms().OfType(Of SynctoCloud).Any Then
            SynctoCloud.TopMost = False
        End If
        Try
            If DisableFormClose = False Then
                Dim newMDIchild As New Leaderboards()
                If Application.OpenForms().OfType(Of Leaderboards).Any Then
                    Leaderboards.TopMost = True
                Else
                    btncolor(changecolor:=ButtonLeaderBoards)
                    btndefaut(defaultcolor:=ButtonLeaderBoards)
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
            AuditTrail.LogToAuditTrail("System", "MDI/ButtonLeaderBoards: " & ex.ToString, "Critical")
        End Try
    End Sub
    Private Sub Button4_Click_1(sender As Object, e As EventArgs) Handles ButtonAbout.Click
        If Application.OpenForms().OfType(Of SynctoCloud).Any Then
            SynctoCloud.TopMost = False
        End If
        Try
            If DisableFormClose = False Then
                Dim newMDIchild As New About()
                If Application.OpenForms().OfType(Of About).Any Then
                Else
                    btncolor(changecolor:=ButtonAbout)
                    btndefaut(defaultcolor:=ButtonAbout)
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
            AuditTrail.LogToAuditTrail("System", "MDI/ButtonAbout: " & ex.ToString, "Critical")
        End Try
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles ButtonDeposit.Click
        If Application.OpenForms().OfType(Of SynctoCloud).Any Then
            SynctoCloud.TopMost = False
        End If
        Try
            If DisableFormClose = False Then
                Dim newMDIchild As New DepositSlip()
                If Application.OpenForms().OfType(Of DepositSlip).Any Then
                Else
                    btncolor(changecolor:=ButtonDeposit)
                    btndefaut(defaultcolor:=ButtonDeposit)
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
            AuditTrail.LogToAuditTrail("System", "MDI/ButtonDeposit: " & ex.ToString, "Critical")
        End Try
    End Sub
    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles ButtonInbox.Click
        Messageboolean = True
        If Application.OpenForms().OfType(Of SynctoCloud).Any Then
            SynctoCloud.TopMost = False
        End If
        Try
            If DisableFormClose = False Then
                Dim newMDIchild As New Message()
                If Application.OpenForms().OfType(Of Message).Any Then
                Else
                    btncolor(changecolor:=ButtonInbox)
                    btndefaut(defaultcolor:=ButtonInbox)
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
            AuditTrail.LogToAuditTrail("System", "MDI/ButtonInbox: " & ex.ToString, "Critical")
        End Try
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles ButtonPointofSales.Click
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
            AuditTrail.LogToAuditTrail("System", "MDI/ButtonPointofSales: " & ex.ToString, "Critical")
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
            AuditTrail.LogToAuditTrail("System", "MDI/btndefaut(): " & ex.ToString, "Critical")
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
            AuditTrail.LogToAuditTrail("System", "MDI/formclose(): " & ex.ToString, "Critical")
        End Try
    End Sub
    Dim iflogout As Boolean
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles ButtonLogout.Click
        If SyncIsOnProcess = True Then
            MessageBox.Show("Sync is on process please wait.", "Syncing", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            If MessageBox.Show("Are you sure you really want to Logout ?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbYes Then
                AuditTrail.LogToAuditTrail("User", "User Logout: " & ClientCrewID, "Normal")
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