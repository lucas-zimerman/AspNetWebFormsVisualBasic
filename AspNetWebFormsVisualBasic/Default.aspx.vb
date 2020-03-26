Public Class _Default
    Inherits Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        GlobalVariables.ravenClient.Capture(New SharpRaven.Data.SentryEvent("Navegar Default"))

    End Sub
End Class