Imports System
Imports System.Net
Imports System.Net.Sockets

Public Delegate Sub ServerEventHandler(ByVal sender As Object, ByVal e As ServerEventArgs)


Public Class TCPServer

    Private LOG As LOG_C

    Private _server As Socket
    Public WithEvents client As TCPClient

    Public Sub New()
        Me._acceptedClients = System.Collections.ArrayList.Synchronized(New System.Collections.ArrayList)


    End Sub

    Public Sub Set_LOG(_LOG As LOG_C)

        LOG = _LOG

    End Sub


    'クライアントの接続待ちスタート
    Private Sub StartAccept(ByVal server As System.Net.Sockets.Socket)


        '接続要求待機を開始する
        server.BeginAccept(New System.AsyncCallback(AddressOf AcceptCallback), server)


    End Sub

    'BeginAcceptのコールバック
    Private Sub AcceptCallback(ByVal ar As System.IAsyncResult)
        'サーバーSocketの取得
        Dim server As System.Net.Sockets.Socket = CType(ar.AsyncState, System.Net.Sockets.Socket)

        '接続要求を受け入れる
        Dim socket As System.Net.Sockets.Socket = Nothing
        Try
            'クライアントSocketの取得
            socket = server.EndAccept(ar)
        Catch
            LOG.LogAdd("TCPServer.AcceptCallback 接続要求受入失敗", 1)
            'System.Console.WriteLine("閉じました。")
            Return
        End Try



        'クライアントが接続した時の処理をここに書く
        Dim TCPClient = New TCPClient
        Dim client As TCPClient = CreateClient(socket)

        'コレクションに追加
        Me._acceptedClients.Add(client)

        client.Set_LOG(LOG)


        'イベントハンドラの追加
        AddHandler client.Disconnected, AddressOf client_Disconnected
        AddHandler client.ReceivedData, AddressOf client_ReceivedData


        'イベントハンドラの追加
        AddHandler client.SendTempイベント, AddressOf ReceiveSendTemp

        'イベントを発生
        Me.OnAcceptedClient(New ServerEventArgs(client))
        'データ受信開始
        If Not client.IsClosed Then
            client.StartReceive()
        End If


        '接続要求待機を再開する
        server.BeginAccept(New System.AsyncCallback(AddressOf AcceptCallback), server)
    End Sub

    'クライアントからデータを受信した時
    Private Sub client_ReceivedData(ByVal sender As Object, ByVal e As ReceivedDataEventArgs)
        'イベントを発生
        Me.OnReceivedData(New ReceivedDataEventArgs(CType(sender, TCPClient), e.ReceivedString))
    End Sub

    'クライアントが切断した時
    Private Sub client_Disconnected(ByVal sender As Object, ByVal e As EventArgs) '


        'リストから削除する
        Me._acceptedClients.Remove(CType(sender, TCPClient))
        'イベントを発生
        Me.OnDisconnectedClient(New ServerEventArgs(CType(sender, TCPClient)))

        'LOGに書き出し
        LOG.LogAdd(CType(sender, TCPClient).端末名 & " が切断しました。", 3)


    End Sub


    Public Event SendTempイベント(ByVal sender As Object, ByVal ジャッジ結果_J As S_採点結果_V2_J)

    Private Sub ReceiveSendTemp(ByVal sender As Object, ByVal ジャッジ結果_J As S_採点結果_V2_J)

        RaiseEvent SendTempイベント(sender, ジャッジ結果_J)

    End Sub



    Protected Function CreateClient(ByVal socket As Socket) As TCPClient
        Return New TCPClient(socket)
    End Function


    Private _socketEP As IPEndPoint

    Public Sub Listen(ByVal host As String, ByVal portNum As Integer）

        Me._server = New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)


        Me._socketEP = New IPEndPoint(IPAddress.Any, portNum)
        Me._server.Bind(Me._socketEP)

        'Listenを開始する
        Dim backlog = 100
        Me._server.Listen(backlog)
        'Me._serverState = ServerState.Listening

        '接続要求施行を開始する
        _server.BeginAccept(New System.AsyncCallback(AddressOf AcceptCallback), _server)
    End Sub

    ''' <summary>
    ''' クライアントを受け入れた
    ''' </summary>
    Public Event AcceptedClient As ServerEventHandler
    Protected Overridable Sub OnAcceptedClient(ByVal e As ServerEventArgs)
        RaiseEvent AcceptedClient(Me, e)
    End Sub

    ''' <summary>
    ''' クライアントがデータを受信した
    ''' </summary>
    Public Event ReceivedData As ReceivedDataEventHandler
    Protected Sub OnReceivedData(ByVal e As ReceivedDataEventArgs)
        RaiseEvent ReceivedData(Me, e)
    End Sub

    ''' <summary>
    ''' クライアントが切断した
    ''' </summary>
    Public Event DisconeetctedClent As ServerEventHandler
    Protected Overridable Sub OnDisconnectedClient(ByVal e As ServerEventArgs)
        RaiseEvent DisconeetctedClent(Me, e)
    End Sub




    Protected _acceptedClients As System.Collections.ArrayList
    ''' <summary>
    ''' 接続中のクライアント
    ''' </summary>
    Public Overridable ReadOnly Property AcceptedClients() As TCPClient()
        Get
            Return CType(Me._acceptedClients.ToArray(GetType(TCPClient)), TCPClient())
        End Get
    End Property

    ''' <summary>
    ''' 接続中のすべてのクライアントに文字列を送信する
    ''' </summary>
    ''' <param name="str">送信する文字列</param>
    Public Sub SendToAllClients(ByVal str As String)
        'CRLFを削除
        str = str.Replace(vbCrLf, "")

        SyncLock Me._acceptedClients.SyncRoot
            Dim i As Integer
            For i = 0 To (Me._acceptedClients.Count) - 1
                CType(Me._acceptedClients(i), TCPClient).Send(str)
            Next i
        End SyncLock
    End Sub


    '=====Clientのイベントハンドル
    'Public Event E01_Connect_SVR As ReceivedDataEventHandler
    'Public Event E02_DisConnect_SVR As ReceivedDataEventHandler

    'Public Event E11_LOGIN_SVR As ServerEventHandler
    'Public Event E12_RCVTemp_SVR As ReceivedDataEventHandler
    'Public Event E13_RCV_SVR As ReceivedDataEventHandler
    'Public Event E19_LOGOUT_SVR As ReceivedDataEventHandler
    'Public Event E51_ReqHelp_SVR As ReceivedDataEventHandler

    'Private Sub E11_LOGIN(ByVal sender As Object, ByVal e As ServerEventArgs) Handles client.E11_LOGIN

    'RaiseEvent E11_LOGIN_SVR(sender, e)

    'End Sub

    Public Sub Server_Close()

        Me._server.Close()

    End Sub


End Class
