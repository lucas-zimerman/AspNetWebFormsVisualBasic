Imports SharpRaven.Data

Public Class About
    Inherits Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        'cria um evento do tipo info
        Dim evento = New SharpRaven.Data.SentryEvent("Navegar About")
        'adiciona dados adicionais'
        evento.Extra = New Dictionary(Of String, String)() From {
    {"a", "b"}
}
        GlobalVariables.ravenClient.Capture(evento)
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Try
            Throw New Exception("Button Exception")
            Exit Try
        Catch ex As Exception
            Dim evento = New SharpRaven.Data.SentryEvent(ex)
            'tag que vai acompanhar somente este evento'
            evento.Tags.Add("sampleTag", "casa")
            evento.Tags.Add("user.id", "12")
            evento.Tags.Add("user.username", "12")
            evento.Tags.Add("user.email", "12@12.com")


            'envia o evento
            Dim eventId = GlobalVariables.ravenClient.Capture(evento)


            Dim evento2 = New SharpRaven.Data.SentryEvent("Novo evento")
            GlobalVariables.ravenClient.Capture(evento2)

            'Exemplo de enviar feedback'
            Dim feedback = New SharpRaven.Data.SentryUserFeedback()
            feedback.Comments = "Comentario"
            feedback.Email = "blabla@email.com"
            feedback.Name = "nome meu"
            feedback.EventID = eventId
            GlobalVariables.ravenClient.SendUserFeedback(feedback)
        End Try
    End Sub

    Protected Sub addbreadcrumb_click_Click(sender As Object, e As EventArgs) Handles addbreadcrumb_click.Click
        'cria um evento do tipo info
        Dim evento = New SentryEvent("Ação finalizada")
        evento.Breadcrumbs = New List(Of Breadcrumb)
        'normalmente uso ui.click para click do usuário ou somente console'
        evento.Breadcrumbs.Add(New Breadcrumb("ui.click") With {
        .Message = "nome da ação",
        .Level = BreadcrumbLevel.Info
    })
        Dim i As Integer
        i = 5
        evento.Breadcrumbs.Add(New Breadcrumb("console") With {
        .Message = "nome da ação terminada",
        .Level = BreadcrumbLevel.Debug
    })
        GlobalVariables.ravenClient.Capture(evento)
    End Sub
End Class