Imports SharpRaven.Data


'classe utilizada para colocar o usuário
Public Class UsuarioFactory : Implements ISentryUserFactory

    Public Function Create() As SentryUser Implements ISentryUserFactory.Create
        Dim teste As SentryUser = New SentryUser("LoginUsuario")
        teste.Email = "teste@email.com"
        teste.Id = "12345"
        Return teste

    End Function
End Class
