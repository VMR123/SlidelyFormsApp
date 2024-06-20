Imports System.Net.Http
Imports System.Text
Imports Newtonsoft.Json
Imports System.Text.RegularExpressions
Imports System.Diagnostics

Public Class CreateSubmissionForm
    Private stopwatch As New Stopwatch()

    ' Constructor
    Public Sub New()
        InitializeComponent()
        ' Add KeyPreview to true to catch key events
        Me.KeyPreview = True
    End Sub

    ' Event handler for key down events
    Private Sub CreateSubmissionForm_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.T Then
            btnToggleStopwatch.PerformClick()
        ElseIf e.Control AndAlso e.KeyCode = Keys.S Then
            btnSubmit.PerformClick()
        End If
    End Sub

    ' Event handler for submit button click
    Private Async Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        If ValidateInputs() Then
            Dim name As String = txtName.Text
            Dim email As String = txtEmail.Text
            Dim phone As String = txtPhone.Text
            Dim githubLink As String = txtGithubLink.Text
            Dim elapsedTime As String = Label1.Text

            Dim submission = New With {
                .name = name,
                .email = email,
                .phone = phone,
                .github_link = githubLink,
                .stopwatch_time = elapsedTime
            }

            Dim json As String = JsonConvert.SerializeObject(submission)
            Using client As New HttpClient()
                Dim content As New StringContent(json, Encoding.UTF8, "application/json")
                Dim response As HttpResponseMessage = Await client.PostAsync("http://localhost:5000/api/submit", content)
                If response.IsSuccessStatusCode Then
                    MessageBox.Show("Submission successful")
                Else
                    MessageBox.Show("Submission failed")
                End If
            End Using
        End If
    End Sub

    ' Event handler for toggle stopwatch button click
    Private Sub btnToggleStopwatch_Click(sender As Object, e As EventArgs) Handles btnToggleStopwatch.Click
        If stopwatch.IsRunning Then
            stopwatch.Stop()
        Else
            stopwatch.Start()
        End If
        Timer1.Start()
    End Sub

    ' Event handler for timer tick
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label1.Text = stopwatch.Elapsed.ToString("hh\:mm\:ss")
    End Sub

    ' Method to validate inputs
    Private Function ValidateInputs() As Boolean
        ' Validate name
        If String.IsNullOrWhiteSpace(txtName.Text) Then
            MessageBox.Show("Name is required")
            Return False
        End If

        ' Validate email
        If Not Regex.IsMatch(txtEmail.Text, "^[^\s@]+@[^\s@]+\.[^\s@]+$") Then
            MessageBox.Show("Invalid email address")
            Return False
        End If

        ' Validate phone number
        If Not Regex.IsMatch(txtPhone.Text, "^\d{10}$") Then
            MessageBox.Show("Phone number must be 10 digits")
            Return False
        End If

        ' Validate GitHub link
        If Not Uri.IsWellFormedUriString(txtGithubLink.Text, UriKind.Absolute) Then
            MessageBox.Show("Invalid GitHub link")
            Return False
        End If

        Return True
    End Function
End Class
