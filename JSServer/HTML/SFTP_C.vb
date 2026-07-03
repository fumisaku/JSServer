Public Class SFTP_C

    'コンストラクタ
    Sub New()


    End Sub

    Public Sub Sync()

        Dim マスタデータ As マスタデータ
        マスタデータ = New マスタデータ



        'Dim LocalPath As String = "C:\DATA\NewServer\19TT01\result\"
        Dim LocalPath As String = マスタデータ.Z_システム設定.HTML_filepath

        'Dim RemotePath As String = "/web/adm.jdsf.jp/www/result/"
        Dim RemotePath As String = マスタデータ.Z_システム設定.FTPRemotePath & マスタデータ.A_競技会マスタ.公認競技会NO & "/"

        'Dim Server名 As String = "59.106.222.159"  '本番サーバー
        Dim Server名 As String = マスタデータ.Z_システム設定.FTPServer
        Dim Port番号 As Integer = CInt(マスタデータ.Z_システム設定.SFTPPort)
        Dim User As String = マスタデータ.Z_システム設定.FTPUser
        Dim pass As String = マスタデータ.Z_システム設定.FTPPass


        Dim _sftpService As SftpService
        _sftpService = New SftpService(Server名, Port番号, User, pass)

        _sftpService.mkdir(RemotePath)

        _sftpService.Synchronize(LocalPath, RemotePath)




        'index.html（競技会一覧をUP)
        UploadFile(マスタデータ.Z_システム設定.Comp_filepath, "index.html")

        マスタデータ = Nothing




    End Sub




    Public Sub UploadFile(パス名 As String, file名 As String)

        Dim マスタデータ As マスタデータ
        マスタデータ = New マスタデータ

        Dim Server名 As String = マスタデータ.Z_システム設定.FTPServer
        Dim Port番号 As Integer = CInt(マスタデータ.Z_システム設定.SFTPPort)
        Dim User As String = マスタデータ.Z_システム設定.FTPUser
        Dim pass As String = マスタデータ.Z_システム設定.FTPPass

        Dim RemotePath As String = マスタデータ.Z_システム設定.FTPRemotePath


        Dim _sftpService As SftpService
        _sftpService = New SftpService(Server名, Port番号, User, pass)

        Dim result = _sftpService.UploadFile(パス名 & "\" & file名, RemotePath)

        If result = True Then
            MsgBox("File successfully uploaded")
        Else
            MsgBox("Error: " & file名 & " upload")
        End If


        _sftpService = Nothing

        マスタデータ = Nothing

    End Sub



End Class
