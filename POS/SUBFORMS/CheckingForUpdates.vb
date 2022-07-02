Imports System.Threading

Public Class CheckingForUpdates
    Private Shared _instance As CheckingForUpdates
    Public CheckingUpdatesUPDATED As Boolean = False

    Public ReadOnly Property Instance As CheckingForUpdates
        Get
            Return _instance
        End Get
    End Property
    Private Sub CheckingForUpdates_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            _instance = Me
            Timer1.Start()
            CheckForIllegalCrossThreadCalls = False
            BackgroundWorker1.WorkerReportsProgress = True
            BackgroundWorker1.WorkerSupportsCancellation = True
            BackgroundWorker1.RunWorkerAsync()
            ChangeProgBarColor(ProgressBar1, ProgressBarColor.Yellow)
        Catch ex As Exception
            AuditTrail.LogToAuditTrail("System", "CheckUpdates/Load: " & ex.ToString, "Critical")
        End Try
    End Sub

    Private Sub CheckingForUpdates_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            If POS.BackgroundWorkerUpdates.IsBusy Then
                e.Cancel = True
            Else
                If POS.BackgroundWorkerInstallUpdates.IsBusy Then
                    e.Cancel = True
                Else
                    POS.Enabled = True
                End If
            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTrail("System", "CheckUpdates/FormClosing: " & ex.ToString, "Critical")
        End Try
    End Sub


    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Try
            If HASUPDATE Then
                If LabelCheckingUpdates.Text = "Installing updates." Then
                    LabelCheckingUpdates.Text = "Installing updates.."
                ElseIf LabelCheckingUpdates.Text = "Installing updates.." Then
                    LabelCheckingUpdates.Text = "Installing updates..."
                ElseIf LabelCheckingUpdates.Text = "Installing updates..." Then
                    LabelCheckingUpdates.Text = "Installing updates."
                End If
            Else
                If LabelCheckingUpdates.Text = "Checking for updates." Then
                    LabelCheckingUpdates.Text = "Checking for updates.."
                ElseIf LabelCheckingUpdates.Text = "Checking for updates.." Then
                    LabelCheckingUpdates.Text = "Checking for updates..."
                ElseIf LabelCheckingUpdates.Text = "Checking for updates..." Then
                    LabelCheckingUpdates.Text = "Checking for updates."
                End If
            End If

            If ClickCancel Then
                If LabelCheckingUpdates.Text = "Processing, Please wait." Then
                    LabelCheckingUpdates.Text = "Processing, Please wait.."
                ElseIf LabelCheckingUpdates.Text = "Processing, Please wait.." Then
                    LabelCheckingUpdates.Text = "Processing, Please wait..."
                Else
                    LabelCheckingUpdates.Text = "Processing, Please wait."
                End If
                Close()
            End If
            If Not OnlineOffline Then
                ClickCancel = True
            Else
                ClickCancel = False
            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTrail("System", "CheckUpdates/Timer1: " & ex.ToString, "Critical")
        End Try
    End Sub
    Dim ClickCancel As Boolean = False
    Dim Unresponsive As Integer = 0
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            With POS
                UPDATE_WORKER_CANCEL = True
                If .BackgroundWorkerUpdates.IsBusy Then
                    LabelCheckingUpdates.Text = "Processing, Please wait."
                    .BackgroundWorkerUpdates.CancelAsync()
                    ClickCancel = True
                    Close()
                    Button1.Enabled = False
                Else
                    Close()
                End If
            End With
        Catch ex As Exception
            AuditTrail.LogToAuditTrail("System", "CheckUpdates/Button1: " & ex.ToString, "Critical")
        End Try
    End Sub
End Class