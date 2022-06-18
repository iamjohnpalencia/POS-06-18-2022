Imports System.IO
Imports System.Text
Imports MySql.Data.MySqlClient

Module EJournal
    Property PosWriter As Boolean = False
    Public Sub FillEJournalContent(EjornalString As String, LoopText As String(), Position As String, TextFont As Boolean, CopyRow As Boolean)
        EJOURLAN_Content &= EjornalString & vbCrLf
        EJOURNAL_TotalLines += 1

        If PosWriter Then
            Select Case POS.Reprint
                Case 1
                    Select Case CopyRow
                        Case True
                            Dim StrChange As String() = {EjornalString}
                            CreateXMLFile(Position, StrChange, publicfunctions.XML_Writer, TextFont)
                        Case False
                            CreateXMLFile(Position, LoopText, publicfunctions.XML_Writer, TextFont)
                    End Select
            End Select
        Else
            Select Case CopyRow
                Case True
                    Dim StrChange As String() = {EjornalString}
                    CreateXMLFile(Position, StrChange, publicfunctions.XML_Writer, TextFont)
                Case False
                    CreateXMLFile(Position, LoopText, publicfunctions.XML_Writer, TextFont)
            End Select
        End If
    End Sub

    Public Sub InsertIntoEJournal()
        Try
            Dim ConnectionLocal As MySqlConnection = LocalhostConn()
            Dim Query As String = "INSERT INTO loc_e_journal (`totallines`, `content`, `crew_id`, `store_id`, `created_at`, `active`, `zreading`, `synced`) VALUES (@1,@2,@3,@4,@5,@6,@7,@8)"
            Dim Command As MySqlCommand = New MySqlCommand(Query, ConnectionLocal)
            Command.Parameters.Add("@1", MySqlDbType.Int64).Value = EJOURNAL_TotalLines
            Command.Parameters.Add("@2", MySqlDbType.Text).Value = EJOURLAN_Content
            Command.Parameters.Add("@3", MySqlDbType.Text).Value = ClientCrewID
            Command.Parameters.Add("@4", MySqlDbType.Text).Value = ClientStoreID
            Command.Parameters.Add("@5", MySqlDbType.Text).Value = FullDate24HR()
            Command.Parameters.Add("@6", MySqlDbType.Text).Value = "1"
            Command.Parameters.Add("@7", MySqlDbType.Text).Value = S_Zreading
            Command.Parameters.Add("@8", MySqlDbType.Text).Value = "Unsynced"
            Command.ExecuteNonQuery()
            EJOURLAN_Content = ""
            EJOURNAL_TotalLines = 0
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "E-Journal Module: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub

    Public Function GetHeader(VoidReturn As Boolean) As DataTable
        Dim HeaderStrings As DataTable = New DataTable
        Dim Denom As String = ""
        Try
            Dim ConnectionLocal As MySqlConnection = LocalhostConn()
            Dim Command As MySqlCommand
            Dim DataAdapter As MySqlDataAdapter
            Dim Query As String = ""
            If VoidReturn Then
                Denom = "-"
                Query = "SELECT description FROM loc_receipt WHERE type IN ('Header','REFUND-HEADER','OFFICIAL-REFUND') AND status = 1 ORDER BY id ASC"
            Else
                Query = "SELECT description FROM loc_receipt WHERE type IN ('Header','SALES-INVOICE','OFFICIAL-INVOICE') AND status = 1 ORDER BY id ASC"
            End If
            Command = New MySqlCommand(Query, ConnectionLocal)
            DataAdapter = New MySqlDataAdapter(Command)
            DataAdapter.Fill(HeaderStrings)
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "E-Journal Module: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
        Return HeaderStrings
    End Function

    Public Function GetFooter() As DataTable
        Dim FooterString As DataTable = New DataTable
        Try
            Dim ConnectionLocal As MySqlConnection = LocalhostConn()
            Dim Command As MySqlCommand
            Dim DataAdapter As MySqlDataAdapter
            Dim Query As String = ""
            Query = "SELECT description FROM loc_receipt WHERE type = 'Footer' AND status = 1 ORDER BY id ASC"
            Command = New MySqlCommand(Query, ConnectionLocal)
            DataAdapter = New MySqlDataAdapter(Command)
            DataAdapter.Fill(FooterString)
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "E-Journal Module: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
        Return FooterString
    End Function

    Public Function GetValidity() As DataTable
        Dim ValidityString As DataTable = New DataTable
        Try
            Dim ConnectionLocal As MySqlConnection = LocalhostConn()
            Dim Command As MySqlCommand
            Dim DataAdapter As MySqlDataAdapter
            Dim Query As String = ""
            Query = "SELECT description FROM loc_receipt WHERE type = 'VALIDITY' AND status = 1"
            Command = New MySqlCommand(Query, ConnectionLocal)
            DataAdapter = New MySqlDataAdapter(Command)
            DataAdapter.Fill(ValidityString)
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "E-Journal Module: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
        Return ValidityString
    End Function

    Public Function GetRefundFooter(VoidReturn As Boolean)
        Dim RefundFooterString As DataTable = New DataTable
        Try
            If VoidReturn Then
                Dim ConnectionLocal As MySqlConnection = LocalhostConn()
                Dim Command As MySqlCommand
                Dim DataAdapter As MySqlDataAdapter
                Dim Query As String = ""
                Query = "SELECT * FROM loc_receipt WHERE type = 'REFUND-FOOTER' AND status = 1"
                Command = New MySqlCommand(Query, ConnectionLocal)
                DataAdapter = New MySqlDataAdapter(Command)
                DataAdapter.Fill(RefundFooterString)
            End If

        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "E-Journal Module: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
        Return RefundFooterString
    End Function

End Module
