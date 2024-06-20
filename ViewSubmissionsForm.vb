Imports System.Net.Http
Imports Newtonsoft.Json
Imports System.Text
Imports System.Threading.Tasks

Public Class ViewSubmissionsForm
    ' Constructor
    Public Sub New()
        InitializeComponent()
        ' Add KeyPreview to true to catch key events
        Me.KeyPreview = True
    End Sub

    ' Event handler for key down events
    Private Sub ViewSubmissionsForm_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.P Then
            btnPrevious.PerformClick()
        ElseIf e.Control AndAlso e.KeyCode = Keys.N Then
            btnNext.PerformClick()
        ElseIf e.Control AndAlso e.KeyCode = Keys.D Then
            btnDelete.PerformClick()
        ElseIf e.Control AndAlso e.KeyCode = Keys.E Then
            btnEdit.PerformClick()
        End If
    End Sub

    ' Event handler for form load
    Private Async Sub ViewSubmissionsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Await LoadSubmission(currentIndex)
    End Sub

    ' Method to load submission by index
    Private Async Function LoadSubmission(index As Integer) As Task
        Using client As New HttpClient()
            Dim response As HttpResponseMessage = Await client.GetAsync($"http://localhost:5000/api/read?index={index}")
            If response.IsSuccessStatusCode Then
                Dim json As String = Await response.Content.ReadAsStringAsync()
                Dim submission As Submission = JsonConvert.DeserializeObject(Of Submission)(json)
                DisplaySubmission(submission)
            Else
                MessageBox.Show("No more submissions")
            End If
        End Using
    End Function

    ' Method to display submission data
    Private Sub DisplaySubmission(submission As Submission)
        txtName.Text = submission.name
        txtEmail.Text = submission.email
        txtPhone.Text = submission.phone
        txtGithubLink.Text = submission.github_link
        txtStopwatchTime.Text = submission.stopwatch_time
        SetReadOnly(True)
    End Sub

    ' Method to set text boxes read-only property
    Private Sub SetReadOnly(isReadOnly As Boolean)
        txtName.ReadOnly = isReadOnly
        txtEmail.ReadOnly = isReadOnly
        txtPhone.ReadOnly = isReadOnly
        txtGithubLink.ReadOnly = isReadOnly
        txtStopwatchTime.ReadOnly = isReadOnly
    End Sub

    ' Event handler for previous button click
    Private Async Sub btnPrevious_Click(sender As Object, e As EventArgs) Handles btnPrevious.Click
        If currentIndex > 0 Then
            currentIndex -= 1
            Await LoadSubmission(currentIndex)
        End If
    End Sub

    ' Event handler for next button click
    Private Async Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        currentIndex += 1
        Await LoadSubmission(currentIndex)
    End Sub

    ' Event handler for delete button click
    Private Async Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to delete this submission?", "Confirm Delete", MessageBoxButtons.YesNo)
        If result = DialogResult.Yes Then
            Using client As New HttpClient()
                Dim response As HttpResponseMessage = Await client.DeleteAsync($"http://localhost:5000/api/delete?index={currentIndex}")
                If response.IsSuccessStatusCode Then
                    MessageBox.Show("Submission deleted")
                    ' Refresh the form to reflect the deletion
                    currentIndex = Math.Max(0, currentIndex - 1)
                    Await LoadSubmission(currentIndex)
                Else
                    MessageBox.Show("Deletion failed")
                End If
            End Using
        End If
    End Sub

    ' Event handler for edit button click
    Private Async Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        If isEditMode Then
            Dim submission As New Submission With {
                .name = txtName.Text,
                .email = txtEmail.Text,
                .phone = txtPhone.Text,
                .github_link = txtGithubLink.Text,
                .stopwatch_time = txtStopwatchTime.Text
            }

            Dim json As String = JsonConvert.SerializeObject(submission)
            Using client As New HttpClient()
                Dim content As New StringContent(json, Encoding.UTF8, "application/json")
                Dim response As HttpResponseMessage = Await client.PutAsync($"http://localhost:5000/api/edit?index={currentIndex}", content)
                If response.IsSuccessStatusCode Then
                    MessageBox.Show("Submission updated")
                    SetReadOnly(True)
                    btnEdit.Text = "EDIT (CTRL + E)"
                    isEditMode = False
                Else
                    MessageBox.Show("Update failed")
                End If
            End Using
        Else
            SetReadOnly(False)
            btnEdit.Text = "SAVE (CTRL + E)"
            isEditMode = True
        End If
    End Sub

    ' Submission class to store submission data
    Private Class Submission
        Public Property name As String
        Public Property email As String
        Public Property phone As String
        Public Property github_link As String
        Public Property stopwatch_time As String
    End Class

    ' Private variables
    Private currentIndex As Integer = 0
    Private submissions As List(Of Submission)
    Private isEditMode As Boolean = False
End Class
