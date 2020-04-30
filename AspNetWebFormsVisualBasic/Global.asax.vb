Imports System.Net
Imports System.Runtime.ExceptionServices
Imports System.Web.Optimization
Imports SharpRaven
Imports SharpRaven.Data

Public Class Global_asax
    Inherits HttpApplication

    Sub Application_Start(sender As Object, e As EventArgs)
        ' É acionado quando o aplicativo é iniciado
        RouteConfig.RegisterRoutes(RouteTable.Routes)
        BundleConfig.RegisterBundles(BundleTable.Bundles)

        If ServicePointManager.SecurityProtocol.HasFlag(SecurityProtocolType.Tls12) = False Then
            ServicePointManager.SecurityProtocol = ServicePointManager.SecurityProtocol Or SecurityProtocolType.Tls12 Or SecurityProtocolType.Tls11
        End If

        GlobalVariables.ravenClient = New RavenClient("DSN")

        'Para pegar manualmente os dados do usuario
        '        GlobalVariables.ravenClient = New RavenClient("DSN",
        '                                              Nothing,
        '                                              Nothing,
        '                                              New UsuarioFactory())
        GlobalVariables.ravenClient.Environment = "Dev"
        GlobalVariables.ravenClient.Release = "0"

        'Um bom local para adicionar dados globais do usuário como empresa, unidade,...
        GlobalVariables.ravenClient.BeforeSend = Function(requester)

                                                     If requester.Packet IsNot Nothing Then
                                                         If requester.Packet.Tags.ContainsKey("user.id") Then
                                                             requester.Packet.User.Id = requester.Packet.Tags("user.id")
                                                             requester.Packet.Tags.Remove("user.id")
                                                         End If

                                                         If requester.Packet.Tags.ContainsKey("user.username") Then
                                                             requester.Packet.User.Username = requester.Packet.Tags("user.username")
                                                             requester.Packet.Tags.Remove("user.username")
                                                         End If

                                                         If requester.Packet.Tags.ContainsKey("user.email") Then
                                                             requester.Packet.User.Email = requester.Packet.Tags("user.email")
                                                             requester.Packet.Tags.Remove("user.email")
                                                         End If

                                                     End If
                                                     Return requester
                                                 End Function

        GlobalVariables.ravenClient.Capture(New Data.SentryEvent("hello world"))

        AddHandler AppDomain.CurrentDomain.FirstChanceException, AddressOf FirstChanceExceptionEventHandler


    End Sub

    Public Sub FirstChanceExceptionEventHandler(ByVal source As Object, ByVal e As FirstChanceExceptionEventArgs)
        On Error Resume Next
        Dim evento = New SentryEvent(e.Exception)
        GlobalVariables.ravenClient.Capture(evento)
    End Sub

End Class