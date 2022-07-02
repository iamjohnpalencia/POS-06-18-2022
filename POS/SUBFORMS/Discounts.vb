Public Class Discounts

    Public COUPONNAME As String
    Public COUPONVALUE As Double
    Private Sub Discounts_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        With POS

            Dim ColumnID As Integer = 1
            For i As Integer = 0 To .DiscountsDatatable.Rows.Count - 1 Step +1
                DataGridView1.Rows.Add(.DiscountsDatatable(i)(0), .DiscountsDatatable(i)(1), .DiscountsDatatable(i)(2), .DiscountsDatatable(i)(3), ColumnID)
                ColumnID += 1
            Next
        End With
    End Sub
    Public DiscountsDatatable As DataTable
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try

            With DataGridView1
                Dim DataExist As Boolean = False
                If .SelectedRows.Count = 1 Then

                    For Each row In DataGridView2.Rows
                        If .SelectedRows(0).Cells(4).Value = row.Cells("Column10Two").value Then
                            DataExist = True
                            Exit For
                        End If
                    Next

                    If DataExist = False Then
                        Dim Name = .SelectedRows(0).Cells(0).Value.ToString
                        Dim Qty = .SelectedRows(0).Cells(1).Value.ToString
                        Dim Price = .SelectedRows(0).Cells(2).Value.ToString
                        Dim TotalPrice = .SelectedRows(0).Cells(3).Value.ToString
                        Dim ColumnID = .SelectedRows(0).Cells(4).Value.ToString
                        DataGridView2.Rows.Add(Name, Qty, Price, TotalPrice, ColumnID)
                    End If

                    Dim dr As DataGridViewRow
                    For Each dr In .SelectedRows
                        .Rows.Remove(dr)
                    Next
                End If
            End With
        Catch ex As Exception
            AuditTrail.LogToAuditTrail("System", "Discounts/Button2: " & ex.ToString, "Critical")
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            With DataGridView2
                Dim DataExist As Boolean = False
                If .SelectedRows.Count = 1 Then

                    For Each row In DataGridView1.Rows
                        If .SelectedRows(0).Cells(4).Value = row.Cells("Column10").value Then
                            DataExist = True
                            Exit For
                        End If
                    Next

                    If DataExist = False Then
                        Dim Name = .SelectedRows(0).Cells(0).Value.ToString
                        Dim Qty = .SelectedRows(0).Cells(1).Value.ToString
                        Dim Price = .SelectedRows(0).Cells(2).Value.ToString
                        Dim TotalPrice = .SelectedRows(0).Cells(3).Value.ToString
                        Dim ColumnID = .SelectedRows(0).Cells(4).Value.ToString
                        DataGridView1.Rows.Add(Name, Qty, Price, TotalPrice, ColumnID)
                    End If

                    Dim dr As DataGridViewRow
                    For Each dr In .SelectedRows
                        .Rows.Remove(dr)
                    Next
                End If
            End With
        Catch ex As Exception
            AuditTrail.LogToAuditTrail("System", "Discounts/Button1: " & ex.ToString, "Critical")
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            DiscountsDatatable = New DataTable
            DiscountsDatatable.Columns.Add("name")
            DiscountsDatatable.Columns.Add("price")
            DiscountsDatatable.Columns.Add("total")
            DiscountsDatatable.Columns.Add("product_id")
            With DataGridView2
                For i As Integer = 0 To .Rows.Count - 1 Step +1
                    Dim Prod As DataRow = DiscountsDatatable.NewRow
                    Prod("name") = .Rows(i).Cells(0).Value
                    Prod("price") = .Rows(i).Cells(1).Value
                    Prod("total") = .Rows(i).Cells(2).Value
                    Prod("product_id") = .Rows(i).Cells(3).Value
                    DiscountsDatatable.Rows.Add(Prod)
                Next
            End With
        Catch ex As Exception
            AuditTrail.LogToAuditTrail("System", "Discounts: " & ex.ToString, "Critical")


        End Try
        If DataGridView2.Rows.Count > 0 Then
            'MsgBox(GETNOTDISCOUNTEDAMOUNT)

            SeniorDetails.COUPONNAME = COUPONNAME
            SeniorDetails.COUPONVALUE = COUPONVALUE
            SeniorDetails.NOTDISCOUNTEDAMOUNT = SumOfColumnsToDecimal(DataGridView1, 2) - GETNOTDISCOUNTEDAMOUNT
            GETNOTDISCOUNTEDAMOUNT += SumOfColumnsToDecimal(DataGridView2, 2)


            SeniorDetails.Show()
            Enabled = False
        Else
            MessageBox.Show("Select item first", "NOTICE", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

    End Sub

    Private Sub Discounts_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        CouponCode.Enabled = True
    End Sub
End Class