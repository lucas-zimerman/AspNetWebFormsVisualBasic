Imports System.Net
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
                                                         requester.Packet.Tags.Add("Empresa.Test", "1234")
                                                     End If
                                                     Return requester
                                                 End Function

        GlobalVariables.ravenClient.Capture(New Data.SentryEvent("hello world"))

    End Sub
End Class