Imports MySql.Data.MySqlClient
Module serverlocalconn
    Public Function ServerCloudCon() As MySqlConnection
        Dim servercloudconn As MySqlConnection = New MySqlConnection
        Try
            Dim sql = "SELECT `C_Server`, `C_Username`, `C_Password`, `C_Database`, `C_Port` FROM loc_settings WHERE settings_id = 1"
            Dim cmd As MySqlCommand = New MySqlCommand(sql, LocalhostConn)
            Dim da As MySqlDataAdapter = New MySqlDataAdapter(cmd)
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            CloudConnectionString = "server=" & ConvertB64ToString(dt(0)(0)) &
            ";userid= " & ConvertB64ToString(dt(0)(1)) &
            ";password=" & ConvertB64ToString(dt(0)(2)) &
            ";port=" & ConvertB64ToString(dt(0)(4)) &
            ";database=" & ConvertB64ToString(dt(0)(3))
            servercloudconn.ConnectionString = CloudConnectionString
            servercloudconn.Open()
            If servercloudconn.State = ConnectionState.Open Then
                ValidCloudConnection = True
            End If
        Catch ex As Exception
            ValidCloudConnection = False
        End Try
        Return servercloudconn
    End Function
End Module
