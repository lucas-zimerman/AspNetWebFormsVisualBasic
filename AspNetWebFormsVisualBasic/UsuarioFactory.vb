Imports SharpRaven.Data


'classe utilizada para colocar o usuário
Public Class SentryFactory
    Implements IJsonPacketFactory

    Private Function IJsonPacketFactory_Create(project As String, message As SentryMessage, Optional level As ErrorLevel = ErrorLevel.Info, Optional tags As IDictionary(Of String, String) = Nothing, Optional fingerprint() As String = Nothing, Optional extra As Object = Nothing) As JsonPacket Implements IJsonPacketFactory.Create
        Dim evento = New SentryEvent(message) With {
            .Level = level,
            .Extra = extra,
            .Tags = tags
        }
        Return IJsonPacketFactory_Create2(project, evento)
    End Function

    Private Function IJsonPacketFactory_Create1(project As String, exception As Exception, Optional message As SentryMessage = Nothing, Optional level As ErrorLevel = ErrorLevel.Error, Optional tags As IDictionary(Of String, String) = Nothing, Optional fingerprint() As String = Nothing, Optional extra As Object = Nothing) As JsonPacket Implements IJsonPacketFactory.Create
        Dim evento = New SentryEvent(exception) With {
            .Message = message,
            .Level = level,
            .Extra = extra,
            .Tags = tags
        }
        Return IJsonPacketFactory_Create2(project, evento)
    End Function

    Private Function IJsonPacketFactory_Create2(project As String, [event] As SentryEvent) As JsonPacket Implements IJsonPacketFactory.Create
        Dim json = New JsonPacket(project, [event]) With {
            .Breadcrumbs = [event].Breadcrumbs,
            .User = FilterUserInfoFromTags([event])
        }
        Return OnCreate(json)
    End Function

    Protected Overridable Function OnCreate(ByVal jsonPacket As JsonPacket) As JsonPacket
        Return jsonPacket
    End Function

    Private Function FilterUserInfoFromTags(ByVal evento As SentryEvent) As SentryUser
        Dim user = New SentryUser("")

        If evento.Tags.ContainsKey("user.id") Then
            user.Id = evento.Tags("user.id")
            evento.Tags.Remove("user.id")
        End If

        If evento.Tags.ContainsKey("user.username") Then
            user.Username = evento.Tags("user.username")
            evento.Tags.Remove("user.username")
        End If

        If evento.Tags.ContainsKey("user.email") Then
            user.Email = evento.Tags("user.email")
            evento.Tags.Remove("user.email")
        End If

        Return user
    End Function

End Class
