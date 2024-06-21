Imports System.Net.Http
Imports System.Text
Imports Newtonsoft.Json
Imports System.Threading.Tasks
Imports System.Diagnostics

Public Class CreateForm
    Private stopwatch As New Stopwatch()

    ' Define the async function to submit the form
    Private Async Function SubmitForm(name As String, email As String, phone As String, githubLink As String, stopwatchTime As String) As Task
        Using client As New HttpClient()
            Dim submission = New With {
                .name = name,
                .email = email,
                .phone = phone,
                .github_link = githubLink,
                .stopwatch_time = stopwatchTime
            }

            Dim json = JsonConvert.SerializeObject(submission)
            Dim content = New StringContent(json, Encoding.UTF8, "application/json")
            Dim response = Await client.PostAsync("http://localhost:3000/submit", content)

            If response.IsSuccessStatusCode Then
                MessageBox.Show("Submission saved successfully!")
            Else
                MessageBox.Show("Error saving submission!")
            End If
        End Using
    End Function

    ' Handle the submit button click event
    Private Async Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Await SubmitForm(txtName.Text, txtEmail.Text, txtPhoneNumber.Text, txtGitHubLink.Text, txtStopwatchTime.Text)
    End Sub

    ' Handle the start/stop button click event
    Private Sub btnStartStop_Click(sender As Object, e As EventArgs) Handles btnStartStop.Click
        If stopwatch.IsRunning Then
            stopwatch.Stop()
        Else
            stopwatch.Start()
        End If
        txtStopwatchTime.Text = stopwatch.Elapsed.ToString("hh\:mm\:ss")
    End Sub
End Class

