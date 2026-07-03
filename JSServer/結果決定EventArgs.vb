Public Class 結果決定EventArgs

    Inherits EventArgs

    Private _区分番号 As String
    Private _ラウンド番号 As String

    Public Sub New(区分番号 As String, ラウンド番号 As String)

        _区分番号 = 区分番号
        _ラウンド番号 = ラウンド番号

    End Sub

    Public ReadOnly Property 区分番号() As String
        Get
            Return Me._区分番号
        End Get

    End Property


    Public ReadOnly Property ラウンド番号() As String
        Get
            Return Me._ラウンド番号
        End Get

    End Property

End Class
