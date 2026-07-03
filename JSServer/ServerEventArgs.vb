Imports System

Public Class ServerEventArgs

    Private _client As TCPClient

    Public ReadOnly Property Client() As TCPClient
        Get
            Return _client
        End Get
    End Property

    Public Sub New(ByVal c As TCPClient)
        Me._client = c
    End Sub

End Class