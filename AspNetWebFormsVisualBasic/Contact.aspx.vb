Public Class Contact
    Inherits Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        GlobalVariables.ravenClient.Capture(New SharpRaven.Data.SentryEvent("Navegar Contact"))
        Try
            Throw New Exception("Sample Exception")
            Exit Try
        Catch ex As Exception
            Dim x
            GlobalVariables.ravenClient.Capture(New SharpRaven.Data.SentryEvent(ex))
        End Try


    End Sub
End Class