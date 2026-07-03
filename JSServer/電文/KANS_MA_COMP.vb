Public Class KANS_MA_COMP

    Public 公認競技会NO
    Public 競技会名
    Public 開催日
    Public 主催団体
    Public 開催場所



    Sub New()

    End Sub


    Function Create電文(端末名 As String, マスタデータ As マスタデータ) As String

        Dim Denbun As String



        Denbun = "JK,KANS_MA_COMP,"
        Denbun = Denbun & 端末名 & ","
        Denbun = Denbun & "1" & ","      '全レコード数
        Denbun = Denbun & "1" & ","　　　'当レコード数
        Denbun = Denbun & マスタデータ.A_競技会マスタ.公認競技会NO & ","
        Denbun = Denbun & マスタデータ.A_競技会マスタ.競技会名 & ","
        Denbun = Denbun & マスタデータ.A_競技会マスタ.開催日 & ","
        Denbun = Denbun & マスタデータ.A_競技会マスタ.主催団体 & ","
        Denbun = Denbun & マスタデータ.A_競技会マスタ.開催場所 & ","


        Return Denbun




    End Function

End Class
