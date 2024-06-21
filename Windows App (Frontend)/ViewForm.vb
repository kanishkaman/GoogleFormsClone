Imports System.Net.Http
Imports Newtonsoft.Json
Imports System.Threading.Tasks

Public Class ViewForm
    Private currentIndex As Integer = 0

    Private Async Function ReadForm(index As Integer) As Task
        Using client As New HttpClient()
            Dim response = Await client.GetAsync($"http://localhost:3000/read?index={index}")

            If response.IsSuccessStatusCode Then
                Dim json = Await response.Content.ReadAsStringAsync()
                Dim submission = JsonConvert.DeserializeObject(Of Submission)(json)
                txtName.Text = submission.name
                txtEmail.Text = submission.email
                txtPhoneNumber.Text = submission.phone
                txtGitHubLink.Text = submission.github_link
                txtStopwatchTime.Text = submission.stopwatch_time
            Else
                MessageBox.Show("Error reading submission!")
            End If
        End Using
    End Function

    Private Async Sub btnPrevious_Click(sender As Object, e As EventArgs) Handles btnPrevious.Click
        If currentIndex > 0 Then
            currentIndex -= 1
            Await ReadForm(currentIndex)
        End If
    End Sub

    Private Async Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        currentIndex += 1
        Await ReadForm(currentIndex)
    End Sub

    Private Async Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Using client As New HttpClient()
            Dim response = Await client.DeleteAsync($"http://localhost:3000/delete?index={currentIndex}")
            If response.IsSuccessStatusCode Then
                MessageBox.Show("Submission deleted successfully!")
                ' Optionally, update the UI to reflect the deletion
            Else
                MessageBox.Show("Error deleting submission!")
            End If
        End Using
    End Sub

    Public Class Submission
        Public Property name As String
        Public Property email As String
        Public Property phone As String
        Public Property github_link As String
        Public Property stopwatch_time As String
    End Class
End Class

