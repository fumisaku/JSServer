Public Class RPT_Parm_H1

    Inherits RPT_Parm_共通

    Public ヒート番号(5) As String

    Public ヒート(5) As PRT_Parm_H1_ヒート情報

    Public Sub New()

        For i = 1 To 5
            ヒート(i) = New PRT_Parm_H1_ヒート情報
        Next i
    End Sub

End Class

Public Class PRT_Parm_H1_ヒート情報

    Public 背番号(20) As String
    Public 選手名(20) As String

End Class

