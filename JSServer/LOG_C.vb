Imports System
Imports System.IO
Imports System.Text

Public Class LOG_C

    'LOG作成ツール

    Private ON_OFF_Flag As String

    Private LOG_Path As String
    Private LOG_Filename As String

    Public LOG_Level As Integer
    ''''
    '  １：ERR エラー
    '  ２：WARNING 警告
    '  ３：INFO Information
    '  ４：DEBUG  Debug用
    '  ５：DEB_Detail

    Public ERR = 1
    Public WARNING = 2
    Public INFO = 3
    Public DEBUG = 4
    Public DEB_Detail = 5


    '''
    '''  コンストラクタ
    ''' 
    Public Sub New()
        LOG_Level = 1　'Defaltでは、1以下のものだけをLOGする。

    End Sub

    '''
    '''  ログレベルをセット
    ''' 
    Public Sub SetLogLevel(Level As Integer)

        LOG_Level = Level

    End Sub





    '''
    '''  ファイル作成
    ''' 
    Public Function CreateFile()
        'Logファイルを作成し、ファイル名を返す

        ON_OFF_Flag = "ON"

        Dim LOG_Path As String = System.IO.Directory.GetCurrentDirectory()

        LOG_Filename = "LOG" & Format(Now, "yyyyMMddHHmmss") & ".log"


        CreateFile = LOG_Filename

    End Function

    '''
    '''  ログを追加する
    '''  
    Public Sub LogAdd(ByVal cmt As String, ByVal Level As Integer)
        'cmt の内容をLOGとして書き出し

        If ON_OFF_Flag = "ON" Then

            If Level <= LOG_Level Then

                Using writer = New StreamWriter(LOG_Filename, True)

                    writer.WriteLine(Format(Now, "yyyy/MM/dd") & " " & Format(Now, "HH:mm:ss") & " " & Level & " " & cmt)

                End Using

            End If
        End If
    End Sub

    '''
    '''  ログ開始FLAGをON
    '''  
    Public Sub Set_ON()
        'ON_OFF_FlagをOnにセット

        ON_OFF_Flag = "ON"

    End Sub

    '''
    '''  ログ開始FLAGをOFF
    '''  
    Public Sub Set_OFF()
        'ON_OFF_FlagをOFFにセット

        ON_OFF_Flag = "OFF"

    End Sub

End Class
