Public Class ReceivedDataEventArgs

    Inherits EventArgs

    Private _receivedString As String
    Public ReadOnly Property ReceivedString() As String
        Get
            Return Me._receivedString
        End Get
    End Property

    Private _client As TCPClient
    Public ReadOnly Property Client() As TCPClient
        Get
            Return _client
        End Get
    End Property

    Public Sub New(ByVal c As TCPClient, ByVal str As String)
        Me._client = c
        Me._receivedString = str
    End Sub

End Class
