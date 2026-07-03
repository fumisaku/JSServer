Imports System

Public Class ServerEventArgs2

    Private _client As TCPClient2

    Public ReadOnly Property Client() As TCPClient2
        Get
            Return _client
        End Get
    End Property

    Public Sub New(ByVal c As TCPClient2)
        Me._client = c
    End Sub

End Class